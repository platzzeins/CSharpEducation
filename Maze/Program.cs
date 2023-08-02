using Maze.Core;
using Maze.Ui;

namespace Maze
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var maze = new MazeCore();
            var ui = new UserInterface(maze);
            var chosenLevel = ui.SelectLevelScreen();
            
            maze.Start(chosenLevel);
            
            var autoMovingThread = new Thread(maze.ChangePlayerPosition);
            var changeDirectionThread = new Thread(ui.ReadUserMove);
            
            autoMovingThread.Start();
            changeDirectionThread.Start();
            var printThread = new Thread(() =>
            {
                var tempX = 0;
                var tempY = 0;
            
                while (true)
                {
                    if (maze.Player.XPosition == tempX && maze.Player.YPosition == tempY) continue;
                    tempX = maze.Player.XPosition;
                    tempY = maze.Player.YPosition;
                    ui.PrintMaze();
                }
            });
            printThread.Start();
            autoMovingThread.Join();
            changeDirectionThread.Join();
            ui.PrintFinalScreen();
        }
    }
}