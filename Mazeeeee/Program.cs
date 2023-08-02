using ChatBot.Core;
using Maze.Ui;

namespace Maze
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var mapHandler = new MapHandler();
            var ui = new UserInterface();
            ui.PrintMaze(mapHandler.Read("/Users/nikitakuznecov/RiderProjects/CSharpEducation/Mazeeeee/Levels/Level1.txt"));
        }
    }
}