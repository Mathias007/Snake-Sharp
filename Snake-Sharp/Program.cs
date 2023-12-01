using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;

        int screenWidth = Console.WindowWidth;
        int screenHeight = Console.WindowHeight;

        Random randomNumber = new Random();

        Pixel head = new Pixel();
        head.xPos = screenWidth / 2;
        head.yPos = screenHeight / 2;
        head.schermKleur = ConsoleColor.Red;

        List<int> teljePositie = new List<int>();

        teljePositie.Add(head.xPos);
        teljePositie.Add(head.yPos);

        DateTime time = DateTime.Now;
        string obstacle = "*";

        int obstacleXPos = randomNumber.Next(1, screenWidth);
        int obstacleYPos = randomNumber.Next(1, screenHeight);

        int score = 0;

        Game game = new Game(screenWidth, screenHeight, randomNumber, head, teljePositie, obstacle, obstacleXPos, obstacleYPos, score);
        game.Run();
    }
}
