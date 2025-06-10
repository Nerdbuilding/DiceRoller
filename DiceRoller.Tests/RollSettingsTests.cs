namespace Nerdbuilding.DiceRoller.Tests;

using Models;
using ValueTypes;

public sealed class RollSettingsTests
{
  [TestCase("3d4", 3, 4, 0)]
  [TestCase("4D8 + 2", 4, 8, 2)]
  [TestCase("10 d 12-4", 10, 12, -4)]
  public void RollSettings_Parse_ParsesRollSettingsInCorrectSyntax(string input, int expectedNumOfDice, int expectedDieSize, int expectedMod)
  {
    var result = RollSettings.Parse(input);

    Assert.Multiple(() =>
    {
      Assert.That((int)result.NumOfDice, Is.EqualTo(expectedNumOfDice));
      Assert.That((int)result.Size, Is.EqualTo(expectedDieSize));
      Assert.That(result.Modifier, Is.EqualTo(expectedMod));
    });
  }

  [TestCase("4d4*2")]
  [TestCase("0d8+4")]
  [TestCase("5d1-3")]
  public void RollSettings_Parse_ThrowsExceptionsForInvalidInputs(string input)
  {
    Assert.Throws<ArgumentException>(() => RollSettings.Parse(input));
  }

  [TestCase(3, 4, 0, "3d4")]
  [TestCase(4, 6, 2, "4d6 + 2")]
  [TestCase(2, 8, -4, "2d8 - 4")]
  public void RollSettings_ToString_OutputsExpectedFormat(int dice, int size, int mod, string expectedResult)
  {
    var settings = new RollSettings(dice, size, mod);
    var result = settings.ToString();
    Assert.That(result, Is.EqualTo(expectedResult));
  }

  [Test]
  public void RollSettings_TryParse_ReturnsSuccessfullyForValidInput()
  {
    var success = RollSettings.TryParse("2d10-5", out var settings);

    Assert.That(success, Is.True);
    Assert.That(settings, Is.Not.Null);
  }

  [Test]
  public void RollSettings_TryParse_FailsForInvalidInputButDoesNotThrow()
  {
    Assert.DoesNotThrow(() => RollSettings.TryParse("invalid", out _));

    var success = RollSettings.TryParse("invalid", out var settings);
    Assert.That(success, Is.False);
    Assert.That(settings, Is.Null);
  }
}
