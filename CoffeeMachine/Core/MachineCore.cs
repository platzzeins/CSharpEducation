using CoffeeMachine.Entities;
using CoffeeMachine.Exceptions;
using CoffeeMachine.Types;
namespace CoffeeMachine.Core;

public class MachineCore
{
    public delegate void StateHandler(State state);
    public event StateHandler? Notify;
    public State MachineState = State.Start;
    public int Water { get; private set; }
    public int Milk { get; private set; }
    public int Beans { get; private set; }
    public int Cups { get; private set; }
    public int Money { get; private set; }
    private readonly List<Coffee> _coffees = new List<Coffee>();


    public MachineCore()
    {
        Water = 400;
        Milk = 540;
        Beans = 120;
        Cups = 9;
        Money = 50;
        _coffees.Add(new Coffee(CoffeeName.Latte, 350, 75, 20, 7));
        _coffees.Add(new Coffee(CoffeeName.Espresso, 250, 0, 16, 4));
        _coffees.Add(new Coffee(CoffeeName.Cappuccino, 200, 100, 12, 6));
    }

    public void BuyDrink(int coffeeIndex)
    {
        MachineState = State.Buying;
        var drink = _coffees[coffeeIndex];
        
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
        MachineState = State.Start;
        Notify?.Invoke(State.Start);
        Thread.Sleep(delay);
        
        Beans -= drink.Beans;
        MachineState = State.Grind;
        Notify?.Invoke(MachineState);
        Thread.Sleep(delay);

        Water -= drink.Water;
        MachineState = State.Boil;
        Notify?.Invoke(MachineState);
        Thread.Sleep(delay);

        MachineState = State.PourCoffee;
        Notify?.Invoke(MachineState);
        Thread.Sleep(delay);
        
        if (drink.Milk > 0)
        {
            Milk -= drink.Milk;
            MachineState = State.PourMilk;
            Notify?.Invoke(MachineState);
            Thread.Sleep(delay);
        }

        MachineState = State.Ready;
        Notify?.Invoke(MachineState);
    }

    private bool IsAvailableForMaking(Coffee drink)
    {
        return Water >= drink.Water && Milk >= drink.Milk && Beans >= drink.Beans && Money >= drink.Price && Cups >= 1;
    }
    
    public void FillMachine(int water, int milk, int beans, int cups)
    {
        MachineState = State.Filling;
        Notify?.Invoke(MachineState);
        Water += water;
        Milk += milk;
        Beans += beans;
        Cups += cups;
    }

    public void CashOut()
    {
        MachineState = State.CashOut;
        Money = 0;
    } 
    
}