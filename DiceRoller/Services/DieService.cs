namespace Nerdbuilding.DiceRoller.Services;

using ValueTypes;

public sealed class DieService : IDieService
{
  private readonly Random _gen = new();

  public NaturalNumber RollDie(DieSize size) =>
    _gen.Next(1, size + 1);
}
