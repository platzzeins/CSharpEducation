namespace TicTacToe
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var ticTacToe = new TicTacToe();
            var computer = new Computer(ticTacToe);
            var gameUi = new GameUI();
            
            while (true)
            {
                gameUi.Board = ticTacToe.Board;
                gameUi.PrintBoard();

                if (IsEndSituation(ticTacToe, gameUi))
                {
                    break;
                }
                
                switch (ticTacToe.Status)
                {
                    case "User":
                        var userCoordinations = gameUi.InputCoordinations();
                        ticTacToe.MakeAMove(userCoordinations[0], userCoordinations[1], Player.User);
                        ticTacToe.Status = "Computer";
                        break;
                    case "Computer":
                        var coordinations = computer.GetCoordinations();
                        ticTacToe.MakeAMove(coordinations[0], coordinations[1], Player.Computer);
                        ticTacToe.Status = "User";
                        break;
                }
            }
        }

        private static bool IsEndSituation(TicTacToe ticTacToe, GameUI gameUi)
        {
            if (ticTacToe.IsDraw())
            {
                gameUi.PrintDrawEnd();
                return true;
            }
                
            var lineWinner = ticTacToe.IterateThroughLines();
            var columnWinner = ticTacToe.IterateThroughColumns();
            var diagonalWinner = ticTacToe.IterateThroughDiagonals();

            if (lineWinner != "None")
            {
                gameUi.PrintWinEnd(lineWinner);
                return true;
            }

            if (columnWinner != "None")
            {
                gameUi.PrintWinEnd(columnWinner);
                return true;
            }

            if (diagonalWinner != "None")
            {
                gameUi.PrintWinEnd(diagonalWinner);
                return true;
            }

            return false;
        }
    }
}