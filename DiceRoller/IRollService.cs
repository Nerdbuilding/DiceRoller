namespace Nerdbuilding.DiceRoller;

using System.Diagnostics.CodeAnalysis;
using Models;
using ValueTypes;

public interface IRollService
{
  RollResult Roll(RollSettings settings);
  RollResult Roll(NaturalNumber numOfDice, DieSize size, int modifier = 0);
  bool TryRoll(string input, [NotNullWhen(true)] out RollResult? result);
}
