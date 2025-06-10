namespace Nerdbuilding.DiceRoller.Models;

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using ValueTypes;

public class RollSettings
{
  public int Modifier { get; init; }
  public required NaturalNumber NumOfDice { get; init; }
  public required DieSize Size { get; init; }

  public RollSettings()
  {
  }

  [SetsRequiredMembers]
  public RollSettings(NaturalNumber numOfDice, DieSize size, int modifier = 0)
  {
    NumOfDice = numOfDice;
    Size = size;
    Modifier = modifier;
  }

  public override string ToString()
  {
    var modString = string.Empty;
    if (Modifier is not 0)
    {
      var sign = Modifier < 0
        ? " -"
        : " +";
      modString = $"{sign} {Math.Abs(Modifier)}";
    }

    return $"{NumOfDice}{Size}{modString}";
  }

  private static Regex RollSettingsRegex = new(@"^(?<dice>[0-9]+)d(?<size>[0-9]+)(?<mod>-|\+[0-9]+)?$", RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));
  public static RollSettings Parse(string input)
  {
    var m = RollSettingsRegex.Match(input.Replace(" ", string.Empty));
    if (!m.Success)
    {
      throw new ArgumentException("The dice roll was not in an expected format.", nameof(input));
    }

    var dice = int.Parse(m.Groups["dice"].ToString());
    if (dice < 1)
    {
      throw new Exception($"Number of dice must be greater than 0, but got {dice}.");
    }
    var size = int.Parse(m.Groups["size"].ToString());
    if (size < 2)
    {
      throw new Exception($"Die size must be 2 or greater, but got {size}.");
    }

    var mod = 0;
    if (m.Groups.TryGetValue("mod", out var modGroup))
    {
      var sign = modGroup.ToString().First();
      var modAbs = int.Parse(modGroup.ToString().Substring(1));
      mod = sign.Equals("-")
        ? modAbs * -1
        : modAbs;
    }

    return new(dice, size, mod);
  }

  public static bool TryParse(string input, [NotNullWhen(true)] out RollSettings? settings)
  {
    try
    {
      settings = Parse(input);
    }
    catch (Exception)
    {
      settings = null;
    }

    return settings is not null;
  }
}
