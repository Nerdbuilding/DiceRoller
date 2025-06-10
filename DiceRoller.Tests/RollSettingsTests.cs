namespace Nerdbuilding.DiceRoller.Tests;

using Models;
using ValueTypes;

public sealed class RollSettingsTests
{
  [TestCase(3, 4, 0, "3d4")]
  [TestCase(4, 6, 2, "4d6 + 2")]
  [TestCase(2, 8, -4, "2d8 - 4")]
  public void RollSettings_ToString_OutputsExpectedFormat(int dice, int size, int mod, string expectedResult)
  {
    var settings = new RollSettings(dice, size, mod);
    var result = settings.ToString();
    Assert.That(result, Is.EqualTo(expectedResult));
  }
}
