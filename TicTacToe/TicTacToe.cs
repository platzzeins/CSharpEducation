using System.Security.AccessControl;

namespace TicTacToe;

public class TicTacToe
{
    public string[][] Board = new string[3][];
    private string _userSign;
    public string ComputerSign;
    private string _status;
    private readonly Random _rand = new Random();
    
    public TicTacToe()
    {
        CreateTheBoard();
        AssignStatusAndSigns();
    }

    public void Game()
    {
        switch (_status)
        {
            case "User":
                EnterTheCoordinations();
                _status = "Computer";
                break;
            case "Computer":
                MakeComputerMove();
                _status = "User";
                break;
        }
    }
    
    private void AssignStatusAndSigns()
    {
        var players = new string[] {"User", "Computer" };
        var index = _rand.Next(players.Length);
        _status = players[index];
        if (_status == "User")
        {
            _userSign = "X";
            ComputerSign = "O";
        }
        else
        {
            _userSign = "O";
            ComputerSign = "X";
        }
        
    }

    private void MakeComputerMove()
    {
        while (true)
        {
            var x = _rand.Next(0, 2);
            var y = _rand.Next(0, 2);
            if (Board[x][y] == "_")
            {
                Board[x][y] = ComputerSign;
                break;
            }
        }
        
    }

    private void EnterTheCoordinations()
    {
        while (true)
        {
            Console.Write(">");
            var userInput = Console.ReadLine().Trim();
            if (!userInput.Contains(' '))
            {
                Console.WriteLine("Coordinates have to be written in this style: \"4 4\"");
                continue;
            }
            var coordinates = userInput.Split(' ');
            if (!int.TryParse(coordinates[0], out var x) ||
                !int.TryParse(coordinates[1], out var y))
            {
                Console.WriteLine("Not integers inputted!");
                continue;
            }
            try
            {
                if (x <= 0 || y <= 0)
                {
                    throw new FieldException("Coordinates have to be greater than 0");
                }

                if (x > 3 || y > 3)
                {
                    throw new FieldException("Coordinates have to be less than 4");
                }
                Board[x - 1][y - 1] = _userSign;
                break;
            }
            catch (FieldException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    
    private void CreateTheBoard()
    {
        for (var i = 0; i < Board.Length; i++)
        {
            Board[i] = new string[3];
            for (var j = 0; j < Board[i].Length; j++)
            {
                Board[i][j] = "_";
            }
        }
    }
    
    public void PrintBoard()
    {
        Console.WriteLine("---------");
        foreach (var line in Board)
        {
            for (var i = 0; i < line.Length; i++)
            {
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
            }
            Console.WriteLine();
        }
        Console.WriteLine("---------");
    }
}