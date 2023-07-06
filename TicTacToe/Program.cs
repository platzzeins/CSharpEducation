namespace TicTacToe
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var ticTacToe = new TicTacToe();
            var computer = new Computer(ticTacToe);
            var gameUi = new GameUI(ticTacToe);
            
            while (true)
            {
                if (IsEndSituation(ticTacToe, gameUi))
                {
                    break;
                }
               
                gameUi.PrintBoard();
                
                switch (ticTacToe.Status)
                {
                    case Player.User:
                        var userCoordinations = gameUi.InputCoordinations();
                        ticTacToe.MakeAMove(userCoordinations[0], userCoordinations[1], Player.User);
                        ticTacToe.Status = Player.Computer;
                        break;
                    case Player.Computer:
                        var coordinations = computer.GetCoordinations();
                        ticTacToe.MakeAMove(coordinations[0], coordinations[1], Player.Computer);
                        ticTacToe.Status = Player.User;
                        break;
                }
            }
        }

        private static bool IsEndSituation(TicTacToe ticTacToe, GameUI gameUi)
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