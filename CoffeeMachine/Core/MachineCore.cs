using CoffeeMachine.Entities;
using CoffeeMachine.Exceptions;
using CoffeeMachine.Types;
namespace CoffeeMachine.Core;

public class MachineCore
{
    public delegate void StateHandler(State state);
    public event StateHandler? Notify;
    private State _processState = State.Start;

    public State MachineState
    {
        get => _processState;
        set
        {
            _processState = value;
            Notify?.Invoke(MachineState);
        }
    }

    private readonly MachineStorage _machineStorage;
    public readonly List<Coffee> Coffees = new List<Coffee>();


    public MachineCore(MachineStorage machineStorage)
    {
        _machineStorage = machineStorage;
        _machineStorage.Water = 400;
        _machineStorage.Milk = 540;
        _machineStorage.Beans = 120;
        _machineStorage.Cups = 9;
        _machineStorage.Money = 50;
        Coffees.Add(new Coffee("Latte", 350, 75, 20, 7));
        Coffees.Add(new Coffee("Espresso", 250, 0, 16, 4));
        Coffees.Add(new Coffee("Cappuccino", 200, 100, 12, 6));
        FileHandler.WriteDataToHistory("Started new CoffeeMachine");
    }

    public void BuyDrink(int coffeeIndex)
    {
        MachineState = State.Buying;
        var drink = Coffees[coffeeIndex];
        
        if (!IsAvailableForMaking(drink))
        {
            try
            {
                if (_machineStorage.Water < drink.Water)
                {
                    throw new IngredientException(Ingredient.Water);
                }

                if (_machineStorage.Milk < drink.Milk)
                {
                    throw new IngredientException(Ingredient.Milk);
                }

                if (_machineStorage.Beans < drink.Beans)
                {
                    throw new IngredientException(Ingredient.Beans);
                }

                if (_machineStorage.Cups < 1)
                {
                    throw new IngredientException(Ingredient.Cup);
                }

                if (_machineStorage.Money < drink.Price)
                {
                    throw new PriceException();
                }
            }
            catch (IngredientException ie)
            {
                Console.WriteLine(ie.Message);
            }
            catch (PriceException pe)
            {
                Console.WriteLine(pe.Message);
            }
            return;
        }
        
        const int delay = 1000;
        
        _machineStorage.Money -= drink.Price;
        MachineState = State.Start;
        Thread.Sleep(delay);
        
        _machineStorage.Beans -= drink.Beans;
        MachineState = State.Grind;
        Thread.Sleep(delay);

        _machineStorage.Water -= drink.Water;
        MachineState = State.Boil;
        Thread.Sleep(delay);

        MachineState = State.PourCoffee;
        Thread.Sleep(delay);
        
        if (drink.Milk > 0)
        {
            _machineStorage.Milk -= drink.Milk;
            MachineState = State.PourMilk;
            Thread.Sleep(delay);
        }
        
        FileHandler.WriteDataToHistory($"Ordered {drink.CoffeeName}");
        
        MachineState = State.Ready;
    }

    private bool IsAvailableForMaking(Coffee drink)
    {
        return _machineStorage.Water >= drink.Water 
               && _machineStorage.Milk >= drink.Milk 
               && _machineStorage.Beans >= drink.Beans 
               && _machineStorage.Money >= drink.Price 
               && _machineStorage.Cups >= 1;
    }
    
    public void FillMachine(int water, int milk, int beans, int cups)
    {
        MachineState = State.Filling;
        Notify?.Invoke(MachineState);
        _machineStorage.Water += water;
        _machineStorage.Milk += milk;
        _machineStorage.Beans += beans;
        _machineStorage.Cups += cups;
    }

    public void CashOut()
    {
        MachineState = State.CashOut;
        _machineStorage.Money = 0;
    } 
    
}