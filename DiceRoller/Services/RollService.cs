namespace Nerdbuilding.DiceRoller.Services;

using System.Diagnostics.CodeAnalysis;
using Models;
using ValueTypes;

public sealed class RollService : IRollService
{
  private readonly IDieService _dieService;

  public RollService(IDieService dieService)
  {
    _dieService = dieService;
  }

  public RollResult Roll(RollSettings settings)
  {
    var total = settings.Modifier;
    var results = new NaturalNumber[settings.NumOfDice];
    for (var i = 0; i < settings.NumOfDice; i++)
    {
      results[i] = _dieService.RollDie(settings.Size);
      total += results[i];
    }

    return new(total, settings, results);
  }

  public RollResult Roll(NaturalNumber numOfDice, DieSize size, int modifier = 0) =>
    Roll(new(numOfDice, size, modifier));

  public bool TryRoll(string input, [NotNullWhen(true)] out RollResult? result)
  {
    result = RollSettings.TryParse(input, out var settings)
      ? Roll(settings)
      : null;
    return result is not null;
  }
}
