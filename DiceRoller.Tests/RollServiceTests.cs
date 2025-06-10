namespace Nerdbuilding.DiceRoller.Tests;

using FakeItEasy;
using Models;
using Services;
using ValueTypes;

internal sealed class RollServiceTests
{
  private RollService _rollService = null!;

  [TestCase(4, 4, 0, 16)]
  [TestCase(2, 10, -3, 17)]
  [TestCase(1, 20, 4, 24)]
  public void RollService_Roll_ReturnsExpectedTotal(int dice, int size, int mod, int expectedTotal)
  {
    var result = _rollService.Roll(dice, size, mod);

    Assert.Multiple(() =>
    {
      Assert.That(result.Total, Is.EqualTo(expectedTotal));
      Assert.That(result.Results.Length, Is.EqualTo(dice));
    });
  }

  [Test]
  public void RollService_TryRoll_ReturnsResultsForValidInput()
  {
    var success = _rollService.TryRoll("1d4-5", out var result);

    Assert.That(success, Is.True);
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Total, Is.EqualTo(-1));
  }

  [Test]
  public void RollService_TryRoll_FailsForInvalidInputButDoesNotThrow()
  {
    Assert.DoesNotThrow(() => _rollService.TryRoll("invalid", out _));

    var success = _rollService.TryRoll("invalid", out var result);
    Assert.That(success, Is.False);
    Assert.That(result, Is.Null);
  }

  [OneTimeSetUp]
  public void InitialSetup()
  {
    var dieService = A.Fake<IDieService>();
    A.CallTo(() => dieService.RollDie(A<DieSize>._)).ReturnsLazily((DieSize size) => (int)size);

    _rollService = new RollService(dieService);
  }
}
