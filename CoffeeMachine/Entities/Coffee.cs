using CoffeeMachine.Types;

namespace CoffeeMachine.Entities;

public record Coffee(string CoffeeName, int Water, int Milk, int Beans, int Price);