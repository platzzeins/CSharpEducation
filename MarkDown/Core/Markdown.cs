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

    public static void FormatIntoOrderedList(List<string> userList)
    {
        for (var i = 0; i < userList.Count; i++)
        {
            userList[i] = $"\n{i + 1}. {userList[i]}";
        }
    }
    
    public static void FormatIntoUnorderedList(List<string> userList)
    {
        for (var i = 0; i < userList.Count; i++)
        {
            userList[i] = $"\n+ {userList[i]}";
        }
    }
}