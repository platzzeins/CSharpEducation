using CoffeeMachine.Entities;
using CoffeeMachine.Exceptions;
using CoffeeMachine.Types;
namespace CoffeeMachine.Core;

public class MachineCore
{
    public delegate void StateHandler(State state);
    public event StateHandler? Notify;
    public int Water { get; private set; }
    public int Milk { get; private set; }
    public int Beans { get; private set; }
    public int Cups { get; private set; }
    public int Money { get; private set; }
    public List<Coffee> Coffees = new List<Coffee>();


    public MachineCore()
    {
        Water = 400;
        Milk = 540;
        Beans = 120;
        Cups = 9;
        Money = 50;
        Coffees.Add(new Coffee(CoffeeName.Latte, 350, 75, 20, 7));
        Coffees.Add(new Coffee(CoffeeName.Espresso, 250, 0, 16, 4));
        Coffees.Add(new Coffee(CoffeeName.Cappuccino, 200, 100, 12, 6));
    }

    public void BuyDrink(CoffeeName coffeeName)
    {
        var drink = new Coffee(CoffeeName.Unknown, 0 ,0 ,0 ,0);
        foreach (var coffee in Coffees)
        {
            if (coffee.CoffeeName != coffeeName) continue;
            drink = coffee;
            break;
        }

        if (drink.CoffeeName == CoffeeName.Unknown)
        {
            throw new UnknownCoffeeException();
            return;
        }
        
        if (!IsAvailableForMaking(drink))
        {
            if (Water < drink.Water)
            {
                throw new IngredientException(Ingredient.Water);
            }
            if (Milk < drink.Milk)
            {
                throw new IngredientException(Ingredient.Milk);
            }
            if (Beans < drink.Beans)
            {
                throw new IngredientException(Ingredient.Beans);
            }
            if (Cups < 1)
            {
                throw new IngredientException(Ingredient.Cup);
            }
            if (Money < drink.Price)
            {
                throw new PriceException();
            }
            return;
        }

        const int delay = 1000;
        
        Money -= drink.Price;
        Notify?.Invoke(State.Start);
        Thread.Sleep(delay);
        Notify?.Invoke(State.Grind);
        Thread.Sleep(delay);
        Notify?.Invoke(State.Boil);
        Thread.Sleep(delay);
        Notify?.Invoke(State.PourCoffee);
        Thread.Sleep(delay);
        if (drink.Milk > 0)
        {
            Notify?.Invoke(State.PourMilk);
            Thread.Sleep(delay);
        }
        Notify?.Invoke(State.Ready);
    }

    private bool IsAvailableForMaking(Coffee drink)
    {
        return Water >= drink.Water && Milk >= drink.Milk && Beans >= drink.Beans && Money >= drink.Price && Cups >= 1;
    }
    
    public void FillMachine(int water, int milk, int beans, int cups)
    {
        Notify?.Invoke(State.AddedComponents);
        Water += water;
        Milk += milk;
        Beans += beans;
        Cups += cups;
    }

    public void CashOut() => Money = 0;
}