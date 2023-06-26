namespace DinnerParty
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input 1 for Classic version or 2 for Dictionary version");
            var answer = RequestVersionChoose();
            switch (answer)
            {
                case "1":
                    DinnerVersions.DinnerPartyClassic();
                    break;
                case "2":
                    DinnerVersions.DinnerPartyDictionary();
                    break;
            }
        }

        public static string RequestVersionChoose()
        {
            while (true)
            {
                Console.Write(">");
                var answer = Console.ReadLine().Trim().ToLower();
                if (answer is "1" or "2") { return answer;}
                Console.WriteLine("Enter 1 or 2 only");
            }
        }
    }
}

