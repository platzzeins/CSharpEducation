using MarkDown.Core;

namespace MarkDown.Ui;

public class Ui
{
    private static readonly string[] Formatters = new string[]
    {
        "plain",
        "bold",
        "italic",
        "header",
        "link",
        "inline-code",
        "ordered-list",
        "unordered-list",
        "new-line"
    };

    public bool IsUserExited;

    public void StartMenu()
    {
        Console.WriteLine("Choose a formatter:");
        var userInput = RequestUserInput();

        switch (userInput)
        {
            case "!exit":
                FileManager.WriteLogs("Standard", "command: Exit");
                IsUserExited = true;
                Console.WriteLine("Goodbye!");
                break;
            case "new-line":
                FileManager.WriteMarkdownDataToFile("\n");
                FileManager.WriteLogs("Command", $"command:new-line");
                break;
            case "!help":
                PrintHelpMenu();
                break;
            default:
            {
                if (Formatters[..5].Contains(userInput))
                {
                    RequestStandardMarkdownData(userInput);
                }
                else if (Formatters[5..].Contains(userInput))
                {
                    RequestListMarkdownData(userInput);
                }
                else
                {
                    FileManager.WriteLogs("UserError", $"command:{userInput}");
                    Console.WriteLine("Error happened. Input !help");
                }

                break;
            }
        }
    }

    private void RequestStandardMarkdownData(string command)
    {
        Console.WriteLine("Input your word(or word for link)");
        var userWord = RequestUserInput();
        
        switch (command)
        {
            case "plain":
                break;
            case "bold":
                userWord = Markdown.FormatIntoBold(userWord);
                break;
            case "italic":
                userWord = Markdown.FormatIntoItalic(userWord);
                break;
            case "header":
                Console.WriteLine("Input header level. From 1 to 6");
                var headerLevel = RequestNumber();
                userWord = Markdown.FormatIntoHeader(userWord, headerLevel);
                break;
            case "link":
                Console.WriteLine("Input link address");
                var link = RequestUserInput();
                userWord = Markdown.FormatIntoLink(userWord, link);
                break;
            case "inline-code":
                userWord = Markdown.FormatIntoInlineCode(userWord);
                break;
            default:
                FileManager.WriteLogs("Error", "Error at \"RequestStandardMarkdownData\" method");
                Console.WriteLine("Something went wrong");
                return;
        }
        FileManager.WriteMarkdownDataToFile(userWord);
        FileManager.WriteLogs("Standard", $"command:{command};word:{userWord}");
    }

    private void RequestListMarkdownData(string command)
    {
        var quantityOfStrings = RequestNumber();
        var userWords = new List<string>();
        
        
        for (var i = 0; i < quantityOfStrings; i++)
        {
            var userWord = RequestUserInput();
            userWords.Add(userWord);
        }

        switch (command)
        {
            case "ordered-list":
                Markdown.FormatIntoOrderedList(userWords);
                break;
            case "unordered-list":
                Markdown.FormatIntoUnorderedList(userWords);
                break;
            default:
                FileManager.WriteLogs("Error", "Error at \"RequestListMarkdownData\" method");
                Console.WriteLine("Something went wrong");
                break;
        }
        FileManager.WriteMarkDownDataToFile(userWords);
        FileManager.WriteLogs("Standard", $"command:{command};word:{string.Join('-' ,userWords)}");
    }

    private void PrintHelpMenu()
    {
        Console.WriteLine($"Available formatters: {String.Join(' ', Formatters)}");
    }
    
    private string RequestUserInput()
    {
        Console.Write(">");
        var userInput = Console.ReadLine()?.Trim() ?? "null";
        return userInput;
    }
    
    private int RequestNumber()
    {
        while (true)
        {
            var notParsedNumber = RequestUserInput();
            if (int.TryParse(notParsedNumber, out var parsedNumber))
            {
                return parsedNumber;
            }
            Console.WriteLine("Invalid value entered");
        }
    }
}