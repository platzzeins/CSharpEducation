namespace TicTacToe
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var ticTacToe = new TicTacToe();
            while (true)
            {
                ticTacToe.PrintBoard();
                ticTacToe.Game();
            }
        }
    }
}