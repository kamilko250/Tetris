using System;
using System.Threading;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new();
            
            game.StartGame();

            while (game.CanPlay)
            {
                switch (Console.ReadKey(false).Key.ToString())
                {
                    case "Enter":
                        if (game.IsFigureAbleToRotate())
                            game.Rotate(Rotation.Right);
                        break;
                    case "LeftArrow":
                        if (game.IsFigureAbleToMoveLeft())
                            game.MoveLeft();
                        break;
                    case "RightArrow":
                        if (game.IsFigureAbleToMoveRight())
                            game.MoveRight();
                        break;
                    case "DownArrow": 
                        game.MoveFigureDownAndCheckFullRows();
                        break;
                }
                Thread.Sleep(200);
            }
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.WriteLine($"Score: {game.Points}");


            Console.WriteLine("\nPress enter to exit game");
            while (Console.ReadKey(false).Key != ConsoleKey.Enter);

        }
    }
}