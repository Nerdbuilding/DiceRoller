namespace Nerdbuilding.DiceRoller.ValueTypes;

public readonly record struct NaturalNumber
{
  public int Value { get; }

  public NaturalNumber(int value)
  {
    if (value < 1)
    {
      throw new ArgumentOutOfRangeException(nameof(value), value, "A natural number must be greater than 0.");
    }

    Value = value;
  }

  public override string ToString() =>
    Value.ToString();

  public static implicit operator int(NaturalNumber num) =>
    num.Value;

  public static implicit operator NaturalNumber(int value) =>
    new(value);
}
