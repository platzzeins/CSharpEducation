using MarkDown.Exceptions;

namespace MarkDown.Core;

public static class Markdown
{
    public static string FormatIntoBold(string userString)
    {
        return $"**{userString}**";
    }

    public static string FormatIntoItalic(string userString)
    {
        return $"*{userString}*";
    }

    public static string FormatIntoHeader(string userString, int headingLevel)
    {
        if (headingLevel is < 1 or > 6)
        {
            throw new FormatterException("Header level is out of range");
        }
        
        return $"{new string('#', headingLevel)}{userString}";
    }

    public static string FormatIntoLink(string title, string link)
    {
        return $"[{title}]({link})";
    }

    public static string FormatIntoInlineCode(string userString)
    {
        return $"`{userString}`";
    }

    public static List<string> FormatIntoOrderedList(List<string> userList)
    {
        var copiedList = userList.Clone();
        
        for (var i = 0; i < copiedList.Count; i++)
        {
            copiedList[i] = $"\n{i + 1}. {copiedList[i]}";
        }

        return copiedList;
    }
    
    public static List<string> FormatIntoUnorderedList(List<string> userList)
    {
        var copiedList = userList.Clone();
        
        for (var i = 0; i < copiedList.Count; i++)
        {
            copiedList[i] = $"\n+ {copiedList[i]}";
        }

        return copiedList;
    }
}

public static class Extension
{
    public static List<T> Clone<T>(this List<T> source)
    {
        return source.GetRange(0, source.Count);
    }
}