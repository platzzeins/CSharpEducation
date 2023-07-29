using MarkDown.Ui;

namespace MarkDown
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var ui = new UserInterface();
            while (!ui.IsUserExited)
            {
                ui.StartMenu();
            }
        }
    }
}
