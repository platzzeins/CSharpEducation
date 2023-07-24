namespace MarkDown
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var ui = new Ui.Ui();
            while (!ui.IsUserExited)
            {
                ui.StartMenu();
            }
        }
    }
}
