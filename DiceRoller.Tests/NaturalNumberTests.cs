namespace Nerdbuilding.DiceRoller.Tests;

using ValueTypes;

public sealed class NaturalNumberTests
{
  [Test]
  public void NaturalNumber_DoesNotThrow_WhenValueIsOneOrGreater([Values(1, 5, 654321)] int value)
  {
    Assert.DoesNotThrow(() => new NaturalNumber(value));
  }

  [Test]
  public void NaturalNumber_Throws_WhenValueIsLessThanOne([Values(-0, -1)] int value)
  {
    Assert.Throws<ArgumentOutOfRangeException>(() => new NaturalNumber(value));
  }

  [Test]
  public void NaturalNumber_BehavesAsInt()
  {
    NaturalNumber num = 4;
    Assert.That(num + 2, Is.EqualTo(6));
    Assert.That(num * 4, Is.EqualTo(16));
    Assert.That($"{num}", Is.EqualTo("4"));
  }
}
