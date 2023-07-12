namespace CoffeeMachine.Core;

[Serializable]
public class MachineStorage
{
    public int Water { get; set; }
    public int Milk{ get; set; }
    public int Beans{ get; set; }
    public int Cups{ get; set; }
    public int Money{ get; set; } 
    private const string FilePath = "Machine.storage";
    private static string _path = Path.Combine(Environment.CurrentDirectory, FilePath);

    public MachineStorage(int water = 540, int milk = 400, int beans = 120, int cups = 9, int money = 50)
    {
        Water = water;
        Milk = milk;
        Beans = beans;
        Cups = cups;
        Money = money;
    }
    
    public override string ToString()
    {
        return
            $"Storage contains: {Water} ml of water, {Milk} ml of milk, {Beans} gr of beans, {Cups} cups, {Money} money";
    }

    public void Save()
    {
        using var file = new FileStream(_path, FileMode.OpenOrCreate);
        using var binaryWriter = new BinaryWriter(file);
        
        binaryWriter.Write(Water);
        binaryWriter.Write(Milk);
        binaryWriter.Write(Beans);
        binaryWriter.Write(Cups);
        binaryWriter.Write(Money);
    }

    public static MachineStorage Load()
    {
        try
        {
            using var file = new FileStream(_path, FileMode.Open);
            using var binaryReader = new BinaryReader(file);
            var water = binaryReader.ReadInt32();
            var milk = binaryReader.ReadInt32();
            var beans = binaryReader.ReadInt32();
            var cups = binaryReader.ReadInt32();
            var money = binaryReader.ReadInt32();
            
            var tempStorage = new MachineStorage(water, milk, beans, cups, money);
            Console.WriteLine(tempStorage.ToString());
            return tempStorage;
        }
        catch (FileNotFoundException)
        {
            return new MachineStorage();
        }
    }
}