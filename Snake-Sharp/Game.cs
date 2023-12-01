using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Game
{
    private int screenWidth;
    private int screenHeight;
    private Random randomNumber;
    private Pixel head;
    private List<int> teljePositie;
    private string obstacle;
    private int obstacleXPos;
    private int obstacleYPos;
    private int score;

    public Game(int screenWidth, int screenHeight, Random randomNumber, Pixel head, List<int> teljePositie, string obstacle, int obstacleXPos, int obstacleYPos, int score)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.randomNumber = randomNumber;
        this.head = head;
        this.teljePositie = teljePositie;
        this.obstacle = obstacle;
        this.obstacleXPos = obstacleXPos;
        this.obstacleYPos = obstacleYPos;
        this.score = score;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();

            // Draw Obstacle
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(obstacleXPos, obstacleYPos);
            Console.Write(obstacle);

            // Draw Snake
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(head.xPos, head.yPos);
            Console.Write("■");

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }

            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, screenHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }

            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Score: " + score);

            Console.Write("H");

            for (int i = 0; i < teljePositie.Count(); i += 2)
            {
                Console.SetCursorPosition(teljePositie[i], teljePositie[i + 1]);
                Console.Write("■");
            }

            //Game Logic
            ConsoleKeyInfo info = Console.ReadKey();
            switch (info.Key)
            {
                case ConsoleKey.UpArrow:
                    head.yPos--;
                    break;
                case ConsoleKey.DownArrow:
                    head.yPos++;
                    break;
                case ConsoleKey.LeftArrow:
                    head.xPos--;
                    break;
                case ConsoleKey.RightArrow:
                    head.xPos++;
                    break;
            }

            // Collision with Obstacle
            if (head.xPos == obstacleXPos && head.yPos == obstacleYPos)
            {
                score++;
                obstacleXPos = randomNumber.Next(1, screenWidth);
                obstacleYPos = randomNumber.Next(1, screenHeight);
            }

            teljePositie.Insert(0, head.xPos);
            teljePositie.Insert(1, head.yPos);

            teljePositie.RemoveAt(teljePositie.Count - 1);
            teljePositie.RemoveAt(teljePositie.Count - 1);

            // Collision with Walls or Itself
            if (head.xPos == 0 || head.xPos == screenWidth - 1 || head.yPos == 0 || head.yPos == screenHeight - 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
                Console.WriteLine("Game Over");
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
                Console.WriteLine("Your Score is: " + score);
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 2);
                Environment.Exit(0);
            }

            for (int i = 0; i < teljePositie.Count(); i += 2)
            {
                if (head.xPos == teljePositie[i] && head.yPos == teljePositie[i + 1])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
                    Console.WriteLine("Game Over");
                    Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
                    Console.WriteLine("Your Score is: " + score);
                    Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 2);
                    Environment.Exit(0);
                }
            }

            Thread.Sleep(50);
        }
    }
}
