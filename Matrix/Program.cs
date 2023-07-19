using Matrix.Ui;

namespace Matrix
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var programUi = new ProgramUi();

            while (!programUi.IsUserQuited)
            {
                programUi.PrintSelectionScreen();
            }
        }
    }
}