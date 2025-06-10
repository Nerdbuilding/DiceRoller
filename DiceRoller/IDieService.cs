namespace Nerdbuilding.DiceRoller;

using ValueTypes;

public interface IDieService
{
  NaturalNumber RollDie(DieSize size);
}
