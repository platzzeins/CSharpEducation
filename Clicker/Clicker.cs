namespace Clicker;

public class Clicker
{
    private int _clicks;

    public void Start()
    {
        var thread1 = new Thread(StartAutoClicker);
        var thread2 = new Thread(StartEnterKeyListener);
        thread1.Start();
        thread2.Start();
    }

    private void StartEnterKeyListener()
    {
        while (Console.ReadKey().Key == ConsoleKey.Enter)
        {
            _clicks++;
            PrintClicks();
        }
    }

    private void StartAutoClicker()
    {
        while (true)
        {
            Thread.Sleep(1000);
            _clicks++;
            PrintClicks();
        }
    }

    private void PrintClicks()
    {
        Console.Clear();
        Console.WriteLine($"Scholarship for Nikita: {_clicks}");
    }
}