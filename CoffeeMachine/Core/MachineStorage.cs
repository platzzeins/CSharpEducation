namespace CoffeeMachine.Core;

public record MachineStorage()
{
    public int Water;
    public int Milk;
    public int Beans;
    public int Cups;
    public int Money;

    public string GetInfo()
    {
        return
            $"Storage contains: {Water} ml of water, {Milk} ml of milk, {Beans} gr of beans, {Cups} cups, {Money} money";
    }
}