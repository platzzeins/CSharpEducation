﻿using Maze.Core;
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
                ui.PrintMap();
                
                while (true)
                {
                    if (maze.Player.X == tempX && maze.Player.Y == tempY) continue;
                    ui.ChangePlayerIconPosition(tempX, tempY);
                    tempX = maze.Player.X;
                    tempY = maze.Player.Y;
                    
                }
                // ReSharper disable once FunctionNeverReturns
            });
            printThread.Start();
            autoMovingThread.Join();
            changeDirectionThread.Join();
            ui.PrintFinalScreen();
        }
    }
}