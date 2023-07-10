namespace CoffeeMachine.Types;

public enum State
{
    Start,
    Grind,
    Boil,
    Mix,
    PourCoffee,
    PourMilk,
    Ready,
    Filling,
    Buying,
    Exiting,
    CashOut,
    Remaining
}