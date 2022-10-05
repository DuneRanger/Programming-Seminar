using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessboard
{
    class Program
    {
        static string[,] getInputChessboard()
        {
            string[,] chessboard = new string[8, 8];

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    chessboard[y, x] = Console.ReadLine();
                }
            }

            return chessboard;
        }

        static Tuple<int[], int[]> findCoords(string[,] chessboard)
        {
            int[] kingCoords = new int[2];
            int[] goalCoords = new int[2];

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (chessboard[y, x] == "K")
                    {
                        kingCoords[0] = y;
                        kingCoords[1] = x;
                    }
                    if (chessboard[y, x] == "C")
                    {
                        goalCoords[0] = y;
                        goalCoords[1] = x;
                    }
                }
            }
            return Tuple.Create(kingCoords, goalCoords);
        }

        static int processPositionAndMove(string[,] chessboard, int[] kingCoords, int steps)
        {
            if (chessboard[kingCoords[0], kingCoords[1]] == "C")
            {
                return steps + 1;
            }
            else if (chessboard[kingCoords[0], kingCoords[1]] != ".")
            {
                return 65;
            }
            else
            {

                return 0;
            }
        }

        static void Main(string[] args)
        {
            string[,] chessboard = getInputChessboard();
            var coords = findCoords(chessboard);
            int[] kingCoords = coords.Item1;
            int[] goalCoords = coords.Item2;

            processPositionAndMove(chessboard, kingCoords, 0);

            return;
        }
    }
}
