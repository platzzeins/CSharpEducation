namespace TicTacToe.Entities;

public readonly record struct BoardPosition(int X, int Y) : IParsable<BoardPosition>
{
    public const int MinValue = 0;
    public const int MaxValue = 2;

    public static BoardPosition Parse(string? s, IFormatProvider? provider = null)
    {
        if (s is null)
        {
            throw new ArgumentNullException(nameof(s));
        }

        if (!s.Contains(' '))
        {
            throw new FormatException("Coordinates have to be written in this style: \"4 4\"");
        }

        var coordinates = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (coordinates is not [var xStr, var yStr])
        {
            throw new FormatException("Incorrect argument count");
        }

        if (!int.TryParse(xStr, out var x) || !int.TryParse(yStr, out var y))
        {
            throw new FormatException("Not integers inputted!");
        }

        if (x < 1 || x > 3 || y < 1 || y > 3)
        {
            throw new FormatException("Coordinates have to be in range 1..3");
        }

        return new BoardPosition(x - 1, y - 1);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out BoardPosition result)
    {
        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }
}