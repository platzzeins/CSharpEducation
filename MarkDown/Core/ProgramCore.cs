using MarkDown.Exceptions;
using MarkDown.Types;

namespace MarkDown.Core;

public class ProgramCore
{
    private readonly LogsHandler _logsHandler = new LogsHandler(Environment.CurrentDirectory);
    private readonly FileManager _fileManager = new FileManager(Environment.CurrentDirectory);
    
    public void Format(string command, string userWord)
    {
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
            case "inline-code":
                userWord = Markdown.FormatIntoInlineCode(userWord);
                break;
            default:
                _logsHandler.Write(LogType.Error, "Error at \"Format\" method");
                Console.WriteLine("Something went wrong");
                return;
        }
        _fileManager.WriteMarkdownDataToFile(userWord);
        _logsHandler.Write(LogType.Command, $"command:{command};word:{userWord}");
    }

    public void Format(string command, List<string> userWords)
    {
        if (command is not "ordered-list" and not "unordered-list")
        {
            _logsHandler.Write(LogType.Error, "Error at \"Format(List variation)\" method");
            throw new CoreException("Command is not suit for this method");
        }

        List<string> formattedUserWords;

        if (command == "ordered-list")
        {
            formattedUserWords = Markdown.FormatIntoOrderedList(userWords);
        }
        else
        {
            formattedUserWords = Markdown.FormatIntoUnorderedList(userWords);
        }

        _fileManager.WriteMarkDownDataToFile(formattedUserWords);
        _logsHandler.Write(LogType.Command, string.Join(", ", formattedUserWords));
    }
    
    public void AddNewLine(string command)
    {
        if (command != "new-line")
        {
            _logsHandler.Write(LogType.Error, "Error at \"AddNewLine\" method");
            throw new CoreException("Command is not suit for this method");
        }
        
        _fileManager.WriteMarkdownDataToFile("\n");
        _logsHandler.Write(LogType.Command, $"command:new-line");
    }
    
    public void FormatLink(string command, string userWord, string link)
    {
        if (command != "link")
        {
            _logsHandler.Write(LogType.Error, "Error at \"FormatLink\" method");
            throw new CoreException("Command is not suit for this method");
        }

        userWord = Markdown.FormatIntoLink(userWord, link);
        
        _fileManager.WriteMarkdownDataToFile(userWord);
        _logsHandler.Write(LogType.Command, $"command:{command};word:{userWord}");
    }
    
    public void FormatHeader(string command, string userWord, int headerLevel)
    {
        if (command != "header")
        {
            _logsHandler.Write(LogType.Error, "Error at \"FormatHeader\" method");
            throw new CoreException("Command is not suit for this method");
        }
        
        userWord = Markdown.FormatIntoHeader(userWord, headerLevel);
        
        _fileManager.WriteMarkdownDataToFile(userWord);
        _logsHandler.Write(LogType.Command, $"command:{command};word:{userWord}");
    }
    
    public void Exit()
    {
        _logsHandler.Write(LogType.Command, "command: Exit");
    }
}