using TicTacToe.Entities;
using TicTacToe.Exceptions;
using TicTacToe.Types;

namespace TicTacToe.UI;

public class GameUi
{
    private readonly Core.TicTacToe _ticTacToe;

    public GameUi(Core.TicTacToe ticTacToe) => _ticTacToe = ticTacToe;

    public void PrintBoard()
    {
        Console.Clear();
        PrintBar();
        for (var j = 0; j < _ticTacToe.Board.GetLength(0); j++)
        {
            for (var i = 0; i < _ticTacToe.Board.GetLength(1); i++)
            {
                var userSign = _ticTacToe.UserSign;
                var computerSign = _ticTacToe.ComputerSign;

                if (_ticTacToe.IsWinCell(j, i))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    if (_ticTacToe.Board[j, i] == userSign)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (_ticTacToe.Board[j, i] == computerSign)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                }

                var cellSymbol = _ticTacToe.Board[j, i] switch
                {
                    Sign.Empty => "_",
                    Sign.X => "X",
                    Sign.O => "O",
                    _ => throw new ArgumentOutOfRangeException()
                };

                switch (i)
                {
                    case 0:
                        Console.Write($"| {cellSymbol} ");
                        break;
                    case 2:
                        Console.Write($" {cellSymbol} |");
                        break;
                    default:
                        Console.Write(cellSymbol);
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
    public BoardPosition InputCoordinate()
    {
        while (true)
        {
            Console.Write(">");
            var userInput = Console.ReadLine()?.Trim() ?? "";

            try
            {
                var position = BoardPosition.Parse(userInput);
                if (_ticTacToe.Board[position.X, position.Y] != Sign.Empty)
                {
                    throw new FieldException("This field is already occupied");
                }

                return position;
            }
            catch (Exception e)
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