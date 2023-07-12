using CoffeeMachine.Core;
using CoffeeMachine.Types;

namespace CoffeeMachine.UI;

public class MachineUi
{
    private readonly MachineCore _machineCore;
    private readonly MachineStorage _machineStorage;

    public MachineUi(MachineCore machineCore, MachineStorage machineStorage)
    {
        _machineCore = machineCore;
        _machineStorage = machineStorage;
        _machineCore.Notify += PrintState;
    } 
    
    public void GeneralMenu()
    {
        Console.WriteLine("Write action (buy, fill, take, remaining, history, exit): ");
        Console.Write("> ");
        var response = Console.ReadLine()?.Trim().ToLower();
        switch (response){
            case "buy":
                PrintSelectionScreen();
                break;
            case "fill": 
                PrintFillActionScreen();
                break;
            case "take":
                PrintGaveMoneyScreen();
                break;
            case "remaining":
                PrintRemainingScreen();
                break;
            case "exit":
                _machineCore.MachineState = State.Exiting;
                break;
            default:
                Console.WriteLine("Incorrect value passed");
                break;
        }
    }

    private void PrintSelectionScreen()
    {
        while (true)
        {
            Console.WriteLine("Write, what coffee you want:");
            var coffees = _machineCore.Coffees;

            for (var i = 0; i < coffees.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {coffees[i].CoffeeName}");
            }
            
            Console.WriteLine();
            Console.Write(">");
            var userInput = RequestNumber() - 1;

            if (userInput > coffees.Count || userInput < 0)
            {
                Console.WriteLine("Index of coffee is pout of range");
            }
            else
            {
                _machineCore.BuyDrink(userInput);
            }
            return;
        }
    }

    private void PrintFillActionScreen()
    {
        Console.WriteLine("Write how many ml of water do you want to add: ");
        Console.Write(">");
        var water = RequestNumber();
        
        Console.WriteLine("Write how many ml of milk do you want to add: ");
        Console.Write(">");
        var milk = RequestNumber();
        
        Console.WriteLine("Write how many grams of coffee beans do you want to add:");
        Console.Write(">");
        var beans = RequestNumber();
        
        Console.WriteLine("Write how many disposable cups of coffee do you want to add:");
        Console.Write(">");
        var cups = RequestNumber();
        
        _machineCore.FillMachine(water, milk, beans, cups);
        PrintRemainingScreen();
    }

    private void PrintRemainingScreen()
    {
        _machineCore.MachineState = State.Remaining;
        Console.WriteLine("The coffee machine has: ");
        Console.WriteLine($"{_machineStorage.Water} ml of water");
        Console.WriteLine($"{_machineStorage.Milk} ml of milk");
        Console.WriteLine($"{_machineStorage.Beans} grams of beans");
        Console.WriteLine($"{_machineStorage.Cups} cups");
        Console.WriteLine($"{_machineStorage.Money} money");
    }

    private void PrintGaveMoneyScreen()
    {
        var money = _machineStorage.Money;
        Console.WriteLine($"I gave you {money}");
        _machineCore.CashOut();
    }

    private int RequestNumber()
    {
        while (true)
        {
            var notParsedNumber = RequestUserInput();
            if (int.TryParse(notParsedNumber, out var parsedNumber))
            {
                return parsedNumber;
            }
            Console.WriteLine("Invalid value entered");
        }
    }
    
    private string RequestUserInput()
    {
        Console.Write(">");
        var userInput = Console.ReadLine()?.Trim().ToLower() ?? "None";
        return userInput;
    }

    public void PrintState(State state)
    {
        switch (state)
        {
            case State.Start:
                Console.WriteLine("We are starting making your drink!");
                break;
            case State.Grind:
                Console.WriteLine("Grinding...");
                break;
            case State.Boil:
                Console.WriteLine("Boiling...");
                break;
            case State.Mix:
                Console.WriteLine("Mixing components...");
                break;
            case State.PourCoffee:
                Console.WriteLine("Pouring coffee...");
                break;
            case State.PourMilk:
                Console.WriteLine("Pouring milk...");
                break;
            case State.Ready:
                Console.WriteLine("Here is your order!...");
                break;
            case State.Filling:
                Console.WriteLine("You added components to CoffeeMachine...");
                break;
            case State.Buying:
                Console.WriteLine("You buying a coffee, we are processing your purchase");
                break;
            case State.Exiting:
                _machineCore.OnExit();
                Console.WriteLine("Goodbye;");
                break;
            case State.CashOut:
                Console.WriteLine("You cashed out the CoffeeMachine");
                break;
            case State.Remaining:
                Console.WriteLine("Showing you a remains of the CoffeeMachine");
                break;
            default:
                Console.WriteLine("Unknown process");
                break;
        }
    }
}