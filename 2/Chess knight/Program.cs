using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace Chess_knight
{
    internal class Program
    {
        private const int BOARD_WIDTH = 8;
        private const int BOARD_HEIGHT = 8;

        static void Main(string[] args)
        {
            int[,] Board = new int[BOARD_WIDTH, BOARD_HEIGHT];
            List<int[]> moves = new List<int[]>
            {
                new int[2] { -1, -2},  
                new int[2] { -1, 2},
                new int[2] { 1, -2},
                new int[2] { 1, 2},
                new int[2] { -2, -1},
                new int[2] { -2, 1},
                new int[2] { 2, -1},
                new int[2] { 2, 1},
            };
            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 0;

            StreamWriter setup = new StreamWriter("Input.txt");
            setup.Write("4\n1 2\n2 3\n2 5\n1 6\n1 1\n0 4\n");
            setup.Close();

            StreamReader sr = new StreamReader("Input.txt");
            int N = int.Parse(sr.ReadLine());
            string[] line;
            for (int i = 0; i < N; i++)
            {
                line = sr.ReadLine().Split();
                Board[int.Parse(line[0]), int.Parse(line[1])] = -1;
            }

            line = sr.ReadLine().Split();
            startX = int.Parse(line[0]);
            startY = int.Parse(line[1]);

            line = sr.ReadLine().Split();
            endX = int.Parse(line[0]);
            endY = int.Parse(line[1]);

            sr.Close();

            for (int i = 0; i < BOARD_WIDTH; i++)
            {
                for (int j = 0; j < BOARD_HEIGHT; j++)
                {
                    if (Board[i, j] == 0)
                    {
                        Board[i, j] = int.MaxValue;
                    }
                }
            }
            Board[startX, startY] = 0;

            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[2] { startX, startY });

            while (queue.Count > 0)
            {
                int[] curPos = queue.Dequeue();

                foreach (int[] mov in moves)
                {
                    int newX = curPos[0] + mov[0];
                    int newY = curPos[1] + mov[1];
                    if (newX > -1 && newY > -1 && newX < BOARD_WIDTH && newY < BOARD_HEIGHT)
                    {
                        if (Board[curPos[0], curPos[1]] < Board[newX, newY]-1) // This also takes care of walls, since they are -1
                        {
                            Board[newX, newY] = Board[curPos[0], curPos[1]] + 1;
                            queue.Enqueue( new int[2] {newX, newY } );
                        }
                    }
                }
            }
            StreamWriter sw = new StreamWriter("Output.txt");
            if (Board[endX, endY] == int.MaxValue) sw.WriteLine("-1");
            else sw.WriteLine(Board[endX, endY].ToString());
            sw.Close();

            if (Board[endX, endY] == int.MaxValue) Console.WriteLine("-1");
            else Console.WriteLine(Board[endX, endY].ToString());
        }
    }
}