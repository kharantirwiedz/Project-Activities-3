using System;
using System.Threading;

/*
namespace Snake.Logic
{
    static bool work = false;
    const int width = 20;
    const int height = 20;
    static int x, y, fx, fy, score;
    static Random r = new Random();
    enum eDir { stop = 0, left, right, up, down };
    static eDir direction;
    static int[] tailx = new int[100];
    static int[] taily = new int[100];
    static int ntail;

    static void End()
    {
        work = false;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("GAME OVER");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Press Spacebar to start or ESC to exit");
        while (true)
        {
            ConsoleKeyInfo k = Console.ReadKey();
            if (k.Key.ToString() == "Spacebar")
            {
                Init();
                break;
            }
            if (k.Key.ToString() == "Escape")
                break;
        }
    }

    static void Init()
    {
        work = true;
        for (int i = 0; i <= ntail; i++)
        {
            tailx[i] = 0;
            taily[i] = 0;
        }
        ntail = 0;
        direction = 0;
        x = width / 2;
        y = height / 2;
        fx = r.Next() % (width - 3) + 1;
        fy = r.Next() % (height - 3) + 1;
        score = 0;
        Draw();
    }
    static void Draw()
    {
        Console.Clear();
        for (int j = 0; j < height; j++)
            for (int i = 0; i < width; i++)
            {
                if ((j == 0) || (j == height - 1) || (i == 0) || (i == width - 1))
                    Console.Write("■");
                else if ((j == y) && (i == x))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("0");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if ((j == fy) && (i == fx))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("♥");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    bool empty = true;
                    for (int k = 0; k < ntail; k++)
                    {
                        if ((tailx[k] == i) && (taily[k] == j))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("o");
                            Console.ForegroundColor = ConsoleColor.White;
                            empty = false;
                        }
                    }
                    if (empty == true)
                        Console.Write(" ");
                }
                if (i == width - 1)
                    Console.Write("\n");
            }
        Console.WriteLine("Score = {0}", score);
    }

    static void Input(ConsoleKeyInfo k)
    {
        if ((k.KeyChar == 'a') || (k.KeyChar == 'A'))
            direction = eDir.left;
        else if ((k.KeyChar == 'd') || (k.KeyChar == 'D'))
            direction = eDir.right;
        else if ((k.KeyChar == 'w') || (k.KeyChar == 'W'))
            direction = eDir.up;
        else if ((k.KeyChar == 's') || (k.KeyChar == 'S'))
            direction = eDir.down;
        else if ((k.KeyChar == 'x') || (k.KeyChar == 'X'))
            End();
    }
    static void Logic()
    {
        int prevx = tailx[0];
        int prevy = taily[0];
        int prev2x, prev2y;
        tailx[0] = x;
        taily[0] = y;
        for (int i = 1; i < ntail; i++)
        {
            prev2x = tailx[i];
            prev2y = taily[i];
            tailx[i] = prevx;
            taily[i] = prevy;
            prevx = prev2x;
            prevy = prev2y;
        }
        switch (direction)
        {
            case eDir.left:
                x--;
                break;
            case eDir.right:
                x++;
                break;
            case eDir.up:
                y--;
                break;
            case eDir.down:
                y++;
                break;
        }
        if (x >= width - 1)
            x = 1;
        else if (x == 0)
            x = width - 2;
        if (y >= height - 1)
            y = 1;
        else if (y == 0)
            y = height - 2;

        for (int i = 0; i < ntail; i++)
        {
            if ((tailx[i] == x) && (taily[i] == y))
            {
                End();
            }
        }
        if ((x == fx) && (y == fy))
        {
            Console.Beep();
            score += 100;
            for (int i = 0; i < ntail; i++)
            {
                while ((fx == x) && (fy == y) || (tailx[i] == fx) && (taily[i] == fy))
                {
                    fx = r.Next() % (width - 3) + 1;
                    fy = r.Next() % (height - 3) + 1;
                }
            }
            ntail++;
        }
    }
    static void Main()
    {
        Init();
        while (work == true)
        {
            Draw();
            Thread.Sleep(50);
            if (Console.KeyAvailable == true)
                Input(Console.ReadKey());
            Logic();
        }
    }
}
*/