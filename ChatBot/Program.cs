using System;

namespace ChatBot
{
    internal class Program
    {
        
        
        public static void Main(string[] args)
        {
            ChatBot chatBot = new ChatBot(chatBotName: "Andryushka i Tyoma", yearOfCreation: 2023);
            chatBot.PrintGreeting();
            chatBot.UserNameWelcome();
            chatBot.GuessAge();
            chatBot.Counting();
            chatBot.TestFromBot();
        }
    }
}