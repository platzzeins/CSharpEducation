namespace Clicker
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var clicker = new Clicker();
            clicker.Start();
            clicker.WaitForExit();
        }
    }
}