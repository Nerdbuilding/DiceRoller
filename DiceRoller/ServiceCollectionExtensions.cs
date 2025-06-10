namespace Nerdbuilding.DiceRoller;

using Microsoft.Extensions.DependencyInjection;
using Services;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddDiceRoller(this IServiceCollection services) =>
    services.AddSingleton<IDieService, DieService>()
      .AddSingleton<IRollService, RollService>();
}
