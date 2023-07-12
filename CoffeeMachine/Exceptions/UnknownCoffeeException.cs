namespace CoffeeMachine.Exceptions;

public class UnknownCoffeeException : Exception
{
    public UnknownCoffeeException()
        : base("Unknown type of coffee passed")
    {
    }
}