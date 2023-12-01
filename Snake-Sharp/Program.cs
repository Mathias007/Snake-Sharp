using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var tickRate = TimeSpan.FromMilliseconds(100);
        var snakeGame = new SnakeGame();

        using (var cts = new CancellationTokenSource())
        {
            async Task MonitorKeyPresses()
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(intercept: true).Key;
                        snakeGame.OnKeyPress(key);
                    }

                    await Task.Delay(10);
                }
            }

            var monitorKeyPresses = MonitorKeyPresses();

            do
            {
                snakeGame.OnGameTick();
                snakeGame.Render();
                await Task.Delay(tickRate);
            } while (!snakeGame.GameOver);

            for (var i = 0; i < 3; i++)
            {
                Console.Clear();
                await Task.Delay(500);
                snakeGame.Render();
                await Task.Delay(500);
            }

            cts.Cancel();
            await monitorKeyPresses;
        }
    }
}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}


readonly struct Position
{
    public Position(int top, int left)
    {
        Top = top;
        Left = left;
    }
    public int Top { get; }
    public int Left { get; }

    public Position RightBy(int n) => new Position(Top, Left + n);
    public Position DownBy(int n) => new Position(Top + n, Left);
}
