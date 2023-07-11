using System.Text.Json;

namespace CoffeeMachine.Core;

[Serializable]
public class MachineStorage
{
    public int Water { get; set; }
    public int Milk{ get; set; }
    public int Beans{ get; set; }
    public int Cups{ get; set; }
    public int Money{ get; set; } 
    private static string _path = Path.Combine(Environment.CurrentDirectory, "MachineStorageData.json");

    public MachineStorage()
    {
        Water = 400;
        Milk = 540;
        Beans = 120;
        Cups = 9;
        Money = 50;
    }
    
    public string GetInfo()
    {
        return
            $"Storage contains: {Water} ml of water, {Milk} ml of milk, {Beans} gr of beans, {Cups} cups, {Money} money";
    }

    public static void Serialize(MachineStorage storage)
    {
        var serializeText = JsonSerializer.Serialize(storage);
        Console.WriteLine(serializeText);
        
        File.WriteAllText(_path, serializeText);
    }

    public static MachineStorage Deserialize()
    {
        MachineStorage? storage = null;
        try
        {
            var serializedText = File.ReadAllText(_path);
            storage = JsonSerializer.Deserialize<MachineStorage>(serializedText);
        }
        catch (JsonException)
        {
            Console.WriteLine("Got exception");
        }

        if (storage is null)
        {
            return new MachineStorage();
        }

        return storage;
    }
}