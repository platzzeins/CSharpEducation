namespace TicTacToe;

public class GameUI
{
    public string[][] Board;

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

                if (Board[x - 1][y - 1] != "_")
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