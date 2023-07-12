namespace CoffeeMachine.Exceptions;

public class PriceException : Exception
{
    public PriceException()
        : base("Not enough money in machine")
    {
    }
}