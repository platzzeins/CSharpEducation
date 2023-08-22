namespace CreditCalculator;

public static class Parser
{
    public static Dictionary<string, int> ParseArguments(string[] args)
    {
        var dictionaryOfArgs = new Dictionary<string, int>();

        foreach (var arg in args)
        {
            var splitArgs = arg.Split('=');

            if (splitArgs[0].ToLower() == "type")
            {
                var type = splitArgs[^1].ToLower();
                if (type == "diff")
                {
                    dictionaryOfArgs.Add(splitArgs[0].ToLower(), 1);
                } else if (type == "annuity")
                {
                    dictionaryOfArgs.Add(splitArgs[0].ToLower(), 2);
                }
                else
                {
                    throw new Exception("Wrong type of calculation!");
                }
                
            }
            else
            {
                dictionaryOfArgs.Add(splitArgs[0].ToLower(), int.Parse(splitArgs[^1]));
            }
        }

        return dictionaryOfArgs;
    }
}