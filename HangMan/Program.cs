namespace HangMan
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var user = new User();
            var hangMan = new HangMan(user);
            hangMan.PrintGreeting();
            
            while (true)
            {
                hangMan.PrintUserInterface();
                hangMan.RequestCharacterFromUser();

                if (hangMan.IsPreviousLetterTheSame(user.CurrentLetter)
                    || !hangMan.IsWordContainsLetter(user.CurrentLetter)) { continue; }
                
                if (hangMan.IsTriesLeft)
                {
                    hangMan.PrintLoseEndUserInterface();
                    break;
                }
                
                if (hangMan.CheckEqualityOfWords())
                {
                    hangMan.PrintWinEndUserInterface();
                    break;
                }
                
            }
        }
    }
}

