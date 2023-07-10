using CoffeeMachine.Types;

namespace CoffeeMachine.Entities;

public record Coffee(CoffeeName CoffeeName, int Water, int Milk, int Beans, int Price);