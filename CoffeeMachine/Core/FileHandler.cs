namespace CoffeeMachine.Core;

public static class FileHandler
{
    public static void WriteDataToHistory(string info)
    {
        var date = DateTime.Now;
        var filePath = $"history_{date:yyyy-MM-dd}.txt";
        var docPath = Environment.CurrentDirectory;

        try
        {
            using (var writer = new StreamWriter(Path.Combine(docPath, filePath), true))
            {
                writer.WriteLine($"[{date:HH:mm:ss}] : {info}");
            }
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine("Something is wrong. Path is incorrect.");
        }
        
    }
}