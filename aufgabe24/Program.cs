using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static int width = 20;
    static int height = 10;
    static int snakeX;
    static int snakeY;
    static int foodX;
    static int foodY;
    static int[,] gameField = new int[width, height];
    static List<int> snakeXPositions = new List<int>();
    static List<int> snakeYPositions = new List<int>();
    static int snakeLength = 1;
    static bool gameOver = false;

    static void Main(string[] args)
    {
        Console.Title = "Pseudo Snake";
        InitializeGame();
        RunGameLoop();
    }

    static void InitializeGame()
    {
        snakeX = width / 2;
        snakeY = height / 2;
        gameField[snakeX, snakeY] = 1;
        snakeXPositions.Add(snakeX);
        snakeYPositions.Add(snakeY);

        Random rand = new Random();
        foodX = rand.Next(0, width);
        foodY = rand.Next(0, height);
        gameField[foodX, foodY] = 2;
    }

    static void RunGameLoop()
    {
        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                HandleInput(key);
            }

            UpdateGame();
            DrawGame();

            Thread.Sleep(100); // Delay for smooth movement
        }

        Console.WriteLine("Spiel vorbei! Deine Punktzahl: " + (snakeLength - 1));
    }

    static void HandleInput(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (snakeY > 0)
                    MoveSnake(0, -1);
                break;
            case ConsoleKey.DownArrow:
                if (snakeY < height - 1)
                    MoveSnake(0, 1);
                break;
            case ConsoleKey.LeftArrow:
                if (snakeX > 0)
                    MoveSnake(-1, 0);
                break;
            case ConsoleKey.RightArrow:
                if (snakeX < width - 1)
                    MoveSnake(1, 0);
                break;
        }
    }

    static void MoveSnake(int deltaX, int deltaY)
    {
        int newSnakeX = snakeX + deltaX;
        int newSnakeY = snakeY + deltaY;

        if (gameField[newSnakeX, newSnakeY] == 0)
        {
            snakeXPositions.Add(newSnakeX);
            snakeYPositions.Add(newSnakeY);

            if (snakeXPositions.Count > snakeLength)
            {
                gameField[snakeXPositions[0], snakeYPositions[0]] = 0;
                snakeXPositions.RemoveAt(0);
                snakeYPositions.RemoveAt(0);
            }

            snakeX = newSnakeX;
            snakeY = newSnakeY;
            gameField[snakeX, snakeY] = 1;
        }
        else if (gameField[newSnakeX, newSnakeY] == 2)
        {
            snakeLength++;
            snakeXPositions.Add(newSnakeX);
            snakeYPositions.Add(newSnakeY);

            Random rand = new Random();
            do
            {
                foodX = rand.Next(0, width);
                foodY = rand.Next(0, height);
            } while (gameField[foodX, foodY] != 0);

            gameField[foodX, foodY] = 2;
        }
        else
        {
            gameOver = true;
        }
    }

    static void UpdateGame()
    {
        // No additional update logic needed in this basic example
    }

    static void DrawGame()
    {
        Console.Clear();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int cellValue = gameField[x, y];
                if (cellValue == 0)
                    Console.Write(".");
                else if (cellValue == 1)
                    Console.Write("O");
                else if (cellValue == 2)
                    Console.Write("X");
            }
            Console.WriteLine();
        }
    }
}