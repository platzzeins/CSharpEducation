using System;

namespace ChatBot
{
    public class ChatBot
    {
        private readonly int _yearOfCreation;
        private readonly string _chatBotName;
        private string _userName;

        public ChatBot(string chatBotName, int yearOfCreation)
         {
             _chatBotName = chatBotName;
             _yearOfCreation = yearOfCreation;
         }
        
        public void PrintGreeting()
        {
            Console.WriteLine($"Hello! My name is {_chatBotName}");
            Console.WriteLine($"I was created in {_yearOfCreation}");
        }

        public void UserNameWelcome()
        {
            Console.WriteLine("Please, remind me your name.");
            _userName = RequestUserInput();
            Console.WriteLine($"What a great name you have, {_userName}!");
        }

        public void GuessAge()
        {
            
            Console.WriteLine("Input remainder of your age divided by 3");
            var remainder3 = RequestNumber();
            
            Console.WriteLine("Input remainder of your age divided by 5");
            var remainder5 = RequestNumber();
            
            Console.WriteLine("Input remainder of your age divided by 7");
            var remainder7 = RequestNumber();
            
            var age = (remainder3 * 70 + remainder5 * 21 + remainder7 * 15) % 105;
            
            Console.WriteLine($"Your age is {age}; that's a good time to start programming!");
        }

        public void Counting()
        {
            Console.WriteLine("Now I will prove to you that I can count to any number you want.");
            var repetitions = RequestNumber();
            
            for (var i = 1; i <= repetitions; i++)
            {
                Console.WriteLine($"{i}!");
            }
        }

        public void TestFromBot()
        {
            var answers = new string[]
            {
                "1. To repeat a statement multiple times.",
                "2. To decompose a program into several small subroutines.",
                "3. To determine the execution time of a program.",
                "4. To interrupt the execution of a program."
            };
            var userAnswer = RequestNumber();
            
            Console.WriteLine("Let's test your programming knowledge.");
            Console.WriteLine("Why do we use methods?");
            foreach (var answer in answers)
            {
                Console.WriteLine(answer);
            }


            while (true)
            {
                switch (userAnswer)
                {
                    case < 1:
                        Console.WriteLine("Your answer have to be greater than 0");
                        userAnswer = RequestNumber();
                        break;
                    case > 4:
                        Console.WriteLine("Your answer have to be less than 5");
                        userAnswer = RequestNumber();
                        break;
                    case 2:
                        Console.WriteLine("Completed, have a nice day!");
                        Console.WriteLine("Congratulations, have a nice day!");
                        return;
                }
            }
            
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

        private string RequestUserInput()
        {
            Console.Write(">");
            var userInput = Console.ReadLine();
            return userInput;
        }
        
    }
}