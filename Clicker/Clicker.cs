namespace Clicker;

public class Clicker
{
    private int Cliks { get; set; }
    private Thread AutoClickerThread { get; set; }
    private Thread KeyListenerThread { get; set; }
    private bool IsClickerRunning { get; set; }

    public Clicker()
    {
        Cliks = 0;
        AutoClickerThread = new Thread(AutoClicker);
        KeyListenerThread = new Thread(EnterKeyListener);
    }


    public void Start()
    {
        if (IsClickerRunning)
        {
            Console.WriteLine("AutoClicker already started!");
            return;
        }

        IsClickerRunning = true;
        AutoClickerThread.Start();
        KeyListenerThread.Start();
    }

    public void WaitForExit()
    {
        AutoClickerThread.Join();
        KeyListenerThread.Join();
    }

    private void EnterKeyListener()
    {
        while (IsClickerRunning)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Q)
            {
                Console.WriteLine("You lost!");
                IsClickerRunning = false;
                break;
            }

            if (key.Key == ConsoleKey.Enter)
            {
                Cliks++;
            }

            PrintClicks();
        }
    }

    private void AutoClicker()
    {
        while (IsClickerRunning)
        {
            Cliks++;
            PrintClicks();
            Thread.Sleep(1000);
        }
    }

    private void PrintClicks()
    {
        Console.Clear();
        Console.WriteLine($"Scholarship for Nikita: {Cliks}");
    }
}