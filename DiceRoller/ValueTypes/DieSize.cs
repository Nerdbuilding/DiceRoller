namespace Nerdbuilding.DiceRoller.ValueTypes;

public readonly record struct DieSize
{
  public static DieSize D4 = new(4);
  public static DieSize D6 = new(6);
  public static DieSize D8 = new(8);
  public static DieSize D10 = new(10);
  public static DieSize D12 = new(12);
  public static DieSize D20 = new(20);
  public static DieSize D100 = new(100);

  public int Value { get; }

  public DieSize(int value)
  {
    if (value < 2)
    {
      throw new ArgumentOutOfRangeException(nameof(value), value, "Die size cannot be less than 2.");
    }
    Value = value;
  }

  public override string ToString() =>
    $"d{Value}";

  public static implicit operator int(DieSize size) =>
    size.Value;

  public static implicit operator DieSize(int value) =>
    new(value);
}
