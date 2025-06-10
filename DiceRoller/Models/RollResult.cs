namespace Nerdbuilding.DiceRoller.Models;

using System.Diagnostics.CodeAnalysis;
using ValueTypes;

public class RollResult
{
  public required NaturalNumber[] Results { get; init; }
  public required RollSettings Settings { get; init; }
  public required int Total { get; init; }

  public RollResult()
  {
  }

  [SetsRequiredMembers]
  public RollResult(int total, RollSettings roll, NaturalNumber[] results)
  {
    Total = total;
    Settings = roll;
    Results = results;
  }

  public override string ToString() =>
    Total.ToString();
}
