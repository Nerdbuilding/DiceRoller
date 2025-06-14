# DiceRoller

A package with convenience models and methods for simulating dice rolls in an application.

## Installation

```
dotnet add package Nerdbuilding.DiceRoller
```

## Usage

First, you'll need to register the DiceRoller services for dependency injection:

```
using Nerdbuilding.DiceRoller;

// ...

builder.Services.AddDiceRoller();
```

You will then have access to the services this package exposes.

### DieService

If you just want to roll a single die, use `IDieService.RollDie(DieSize)` where `DieSize` is an integer greater than 1.

```
...
private readonly IDieService _dieService;
...
var result = _dieService.RollDie(6); // result will be a number from 1 to 6 inclusive
```

### RollService

To simulate more complex dice rolls, inject and use `IRollService`. The methods here return a `RollResult` object, which is detailed in the **Models** section below.

```
namespace Example;

using Nerdbuilding.DiceRoller;

public class RollServiceExample
{
  private readonly IRollService _rollService;

  public RollServiceExample(IRollService rollService)
  {
    _rollService = rollService;
  }

  public void RollSomeDice()
  {
    var numberOfDice = 4;
    var dieSize = 6;
    var modifier = -2;

    var result = _rollService.Roll(numberOfDice, dieSize, modifier);
    Console.WriteLine($"Result of rolling {result.Settings.ToString()}: {result.Total}.");
    Console.WriteLine($"Individual rolls are: {string.Join(", ", result.Results)}.");
    // Prints something like:
    // Result of rolling 4d6 - 2: 15
    // Individual results are: 6, 4, 4, 3

    var rerollResult = _rollService.Roll(result.Settings);
    Console.WriteLine($"Result of re-rolling {result.Settings.ToString()}: {rerollResult.Total}.");
    // Prints something like:
    // Result of re-rolling 4d6 - 2: 12

    var input = "2d10+3";
    if (_rollService.TryRoll(input, out var parsedRollResult))
    {
      Console.WriteLine($"Input `{input}` was successfully parsed and produced the total {parsedRollResult.Total}.");
      // Prints Something like:
      // Input '2d10+4' was successfully parsed and produced the total 13
    }
  }
}
```

### Value Types

This packages uses value types to constrain certain integers, but they are configured with implicit conversions to and from `int`.

#### NaturalNumber

Represents an integer greater than 0 and is used for the number of dice to roll as well as the result of rolling a single die with `IDieService.RollDie()`

#### DieSize

Represents an integer greater than 1 to ensure the die being "rolled" is valid.

DieSize also contains some static values for the typical polyhedral die sizes.

* DieSize.D4
* DieSize.D6
* DieSize.D8
* DieSize.D10
* DieSize.D12
* DieSize.D20
* DieSize.D100

### Models

#### RollResult

Returned when performing complex dice rolls in `IRollService`:

* Total (int) - The total, adding each individual die result and the modifier - if any.
* Results (int[]) - Contains each individual die result. Its `Length` will equal the number of dice rolled.
* Settings (RollSettings) - the settings object used to produce this result.

#### RollSettings

Used to store the information needed to produce a `RollResult`. You typically won'tneed to interact with it directly outside of getting a string representation of the roll or re-rolling with the same settings, but you can build it manually if desired.

* NumOfDice (NaturalNumber) - The number of dice to roll. Must be an integer 1 or greater.
* Size (DieSize) - The size (denomination) of the dice to roll. Must be an integer 2 or greater.
* Modifier (int (default 0)) - The flat modifier to apply to the result. Can be any integer, positive or negative.
* ToString() - Overrides `object.ToString()` and returns the settings represented as you would typically see in tabletop gaming: 2d10 + 3, 4d8, 1d20 - 4.
* Parse(string) - A static method which parses a string representation of a roll into a `RollSettings` object and throws various exceptions if it can't.
* TryParse(string, out RollSettings?) - A static method that attempts to parse the input into a `RollSettings` object. It returns `true` on success and populates the out param, and returns `false` with a `null` out param otherwise.

The expected format for the two parse methods is '`<NUMBER OF DICE>d<DIE SIZE><+ or -><MODIFIER>`' where the sign and modifier can be omitted. Parsing is case-insensitive and ignores whitespace.

Examples:

* 4d10
* 8D2
* 3 d 4 - 5
* 12 d 10 + 20
* 1d20+2
