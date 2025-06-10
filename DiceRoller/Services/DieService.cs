namespace Nerdbuilding.DiceRoller.Services;

using System.Diagnostics.CodeAnalysis;
using ValueTypes;

[ExcludeFromCodeCoverage(Justification = "Functionality is based directly on System.Random.")]
public sealed class DieService : IDieService
{
  private readonly Random _gen = new();

  public NaturalNumber RollDie(DieSize size) =>
    _gen.Next(1, size + 1);
}
