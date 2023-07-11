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

    public MachineStorage Storage;
    public readonly List<Coffee> Coffees = new List<Coffee>();


    public MachineCore()
    {
        Storage = MachineStorage.Deserialize();
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
                if (Storage.Water < drink.Water)
                {
                    throw new IngredientException(Ingredient.Water);
                }

                if (Storage.Milk < drink.Milk)
                {
                    throw new IngredientException(Ingredient.Milk);
                }

                if (Storage.Beans < drink.Beans)
                {
                    throw new IngredientException(Ingredient.Beans);
                }

                if (Storage.Cups < 1)
                {
                    throw new IngredientException(Ingredient.Cup);
                }

                if (Storage.Money < drink.Price)
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
        
        Storage.Money -= drink.Price;
        MachineState = State.Start;
        Thread.Sleep(delay);
        
        Storage.Beans -= drink.Beans;
        MachineState = State.Grind;
        Thread.Sleep(delay);

        Storage.Water -= drink.Water;
        MachineState = State.Boil;
        Thread.Sleep(delay);

        MachineState = State.PourCoffee;
        Thread.Sleep(delay);
        
        if (drink.Milk > 0)
        {
            Storage.Milk -= drink.Milk;
            MachineState = State.PourMilk;
            Thread.Sleep(delay);
        }
        
        FileHandler.WriteDataToHistory($"Ordered {drink.CoffeeName}");
        
        MachineState = State.Ready;
    }

    private bool IsAvailableForMaking(Coffee drink)
    {
        return Storage.Water >= drink.Water 
               && Storage.Milk >= drink.Milk 
               && Storage.Beans >= drink.Beans 
               && Storage.Money >= drink.Price 
               && Storage.Cups >= 1;
    }
    
    public void FillMachine(int water, int milk, int beans, int cups)
    {
        MachineState = State.Filling;
        Storage.Water += water;
        Storage.Milk += milk;
        Storage.Beans += beans;
        Storage.Cups += cups;
        FileHandler.WriteDataToHistory("Machine filled");
    }

    public void CashOut()
    {
        FileHandler.WriteDataToHistory($"Machine gave {Storage.Money} money");
        MachineState = State.CashOut;
        Storage.Money = 0;
        
    }

    public void OnExit()
    {
        MachineStorage.Serialize(Storage);
        FileHandler.WriteDataToHistory("Shut down CoffeeMachine");
    }
    
}