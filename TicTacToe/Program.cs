using TicTacToe.Entities;
using TicTacToe.Types;
using TicTacToe.UI;
namespace TicTacToe
{
    internal static class Program
    {
        public static void Main()
        {
            var ticTacToe = new Core.TicTacToe();
            var computer = new Computer(ticTacToe);
            var gameUi = new GameUi(ticTacToe);
            
            while (true)
            {
                if (IsEndSituation(ticTacToe, gameUi))
                {
                    break;
                }
               
                gameUi.PrintBoard();
                
                switch (ticTacToe.CurrentPlayer)
                {
                    case Player.User:
                    {
                        var userCoordinations = gameUi.InputCoordinate();
                        var (x, y) = userCoordinations;
                        ticTacToe.MakeAMove(x, y, Player.User);
                        ticTacToe.CurrentPlayer = Player.Computer;
                        break;
                    }
                    case Player.Computer:
                    {
                        var coordinations = computer.GetCoordinates();
                        var (x, y) = coordinations;
                        ticTacToe.MakeAMove(x, y, Player.Computer);
                        ticTacToe.CurrentPlayer = Player.User;
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static bool IsEndSituation(Core.TicTacToe ticTacToe, GameUi gameUi)
        {
            var lineWinner = ticTacToe.IterateThroughLines();
            var columnWinner = ticTacToe.IterateThroughColumns();
            var diagonalWinner = ticTacToe.IterateThroughDiagonals();

            if (lineWinner != "None")
            {
                gameUi.PrintBoard();
                gameUi.PrintWinEnd(lineWinner);
                return true;
            }

            if (columnWinner != "None")
            {
                gameUi.PrintBoard();
                gameUi.PrintWinEnd(columnWinner);
                return true;
            }

            if (diagonalWinner != "None")
            {
                gameUi.PrintBoard();
                gameUi.PrintWinEnd(diagonalWinner);
                return true;
            }
            
            if (ticTacToe.IsDraw())
            {
                gameUi.PrintDrawEnd();
                return true;
            }

            return false;
        }
    }
}