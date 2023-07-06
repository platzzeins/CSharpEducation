namespace TicTacToe;

public class GameUI
{
    private TicTacToe _ticTacToe;

    public GameUI(TicTacToe ticTacToe)
    {
        _ticTacToe = ticTacToe;
    }

    public void PrintBoard()
    {
        Console.Clear();
        PrintBar();
        for (var j = 0; j < _ticTacToe.Board.Length; j++)
        {
            var line = _ticTacToe.Board[j];
            for (var i = 0; i < line.Length; i++)
            {
                var userSign = _ticTacToe.UserSign;
                var computerSign = _ticTacToe.ComputerSign;

                if (_ticTacToe.IsWinCell(j, i))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    if (line[i] == userSign)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (line[i] == computerSign)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                }

                switch (i)
                {
                    case 0:
                        Console.Write($"| {line[i]} ");
                        break;
                    case 2:
                        Console.Write($" {line[i]} |");
                        break;
                    default:
                        Console.Write(line[i]);
                        break;
                }

                Console.ResetColor();
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        PrintBar();
    }

    private void PrintBar()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("---------");
        Console.ResetColor();
    }
    
    
    /// <summary>
    /// Request coordination's for cell from user 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="FieldException"></exception>
    public int[] InputCoordinations()
    {
        while (true)
        {
            Console.Write(">");
            var userInput = Console.ReadLine().Trim();
            
            try
            {
                if (!userInput.Contains(' '))
                {
                    throw new FieldException("Coordinates have to be written in this style: \"4 4\"");
                }
                var coordinates = userInput.Split(' ');
                if (!int.TryParse(coordinates[0], out var x) ||
                    !int.TryParse(coordinates[1], out var y))
                {
                    throw new FieldException("Not integers inputted!");
                }
            
                if (x <= 0 || y <= 0)
                {
                    throw new FieldException("Coordinates have to be greater than 0");
                }

                if (x > 3 || y > 3)
                {
                    throw new FieldException("Coordinates have to be less than 4");
                }

                if (_ticTacToe.Board[x - 1][y - 1] != Sign._)
                {
                    throw new FieldException("This field is already occupied");
                }

                return new[] { x - 1, y - 1 };
            }
            catch (FieldException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public void PrintWinEnd(string player)
    {
        Console.WriteLine($"{player} win !");
    }

    public void PrintDrawEnd()
    {
        Console.WriteLine("Nobody won! Everybody lose!");
    }
}