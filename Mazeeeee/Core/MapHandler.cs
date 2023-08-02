namespace ChatBot.Core;

public class MapHandler
{
    public char[,] Read(string path)
    {
        var map = new char[8, 8];

        using var reader = new StreamReader(path);
        string line;
        var counter = 0;
        while ((line = reader.ReadLine()) != null)
        {
            for (var i = 0; i < line.Length; i++)
            {
                map[counter, i] = line[i];
            }
            counter++;
        }

        return map;
    }
}