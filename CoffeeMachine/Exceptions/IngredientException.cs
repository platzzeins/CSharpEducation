using CoffeeMachine.Types;

namespace CoffeeMachine.Exceptions;

public class IngredientException : Exception
{
    public IngredientException(Ingredient ingredient)
        : base($"{ingredient} out of stock") { }
}