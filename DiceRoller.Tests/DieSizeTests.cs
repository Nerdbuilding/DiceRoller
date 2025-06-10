namespace Nerdbuilding.DiceRoller.Tests;

using ValueTypes;

public sealed class DieSizeTests
{
  [Test]
  public void DieSize_DoesNotThrow_WhenValueIsTwoOrGreater([Values(2, 5, 654321)] int value)
  {
    Assert.DoesNotThrow(() => new DieSize(value));
  }

  [Test]
  public void DieSize_Throws_WhenValueIsLessThanTwo([Values(-1, 0, -1)] int value)
  {
    Assert.Throws<ArgumentOutOfRangeException>(() => new DieSize(value));
  }

  [Test]
  public void DieSize_BehavesAsInt()
  {
    DieSize num = 4;
    Assert.That(num + 2, Is.EqualTo(6));
    Assert.That(num * 4, Is.EqualTo(16));
  }

  [Test]
  public void DieSize_PrintsDieDenominationInExpectedFormat()
  {
    var size = new DieSize(20);
    Assert.That($"{size}", Is.EqualTo("d20"));
  }
}
