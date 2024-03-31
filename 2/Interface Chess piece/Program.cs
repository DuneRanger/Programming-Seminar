using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Interface_Chess_piece
{
    public class Position
    {
        public int X { get; }
        public int Y { get; }
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    interface IPiece
    {
        Position Pos { get; set; }
        List<Position> PossibleMoves(int width, int height);
    }

    interface IBoard
    {
        int Width { get; set;  }
        int Height { get; set; }

        int GetTile(Position pos);

        void SetTile(Position pos, int val);

        /// <summary>
        /// Sets obstacles to -2, other tiles to -1 and [0, 0] to 0
        /// </summary>
        void ResetBoard();
    }

    public class Board : IBoard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        List<List<int>> map;

        public Board(int w = 1, int h = 1)
        {
            this.Width = w;
            this.Height = h;
            this.map = new();
        }
        public int GetTile(Position pos)
        {
            if (pos.X < this.Width && pos.X > -1 && pos.Y < this.Height && pos.Y > -1)
            {
                return this.map[pos.X][pos.Y];
            }
            else
            {
                return -2;
            }
        }

        public void SetTile(Position pos, int val)
        {
            if (pos.X < this.Width && pos.X > -1 && pos.Y < this.Height && pos.Y > -1)
            {
                this.map[pos.X][pos.Y] = val;
            }
        }

        public void ResetBoard()
        {
            this.map = new List<List<int>>();
            for (int i = 0; i < this.Width; i++)
            {
                List<int> list = new();
                for (int j = 0; j < this.Height; j++)
                {
                    list.Add(-1);
                }
                this.map.Add(list);
            }
            this.map[0][0] = 0;
        }
    }

    public class Knight : IPiece
    {
        public Position Pos { get; set; }
        public Knight()
        {
            Pos = new Position(0, 0);
        }
        public List<Position> PossibleMoves(int width, int height)
        {
            List<int> OffsetX = new() { -2, -2, 2, 2, -1, -1, 1, 1};
            List<int> OffsetY = new() { -1, 1, -1, 1, -2, 2, -2, 2};
            List<Position> result = new();
            for (int i = 0; i < OffsetX.Count; i++)
            {
                result.Add(new(this.Pos.X + OffsetX[i], this.Pos.Y + OffsetY[i]));
            }
            return result;
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            
            Knight p = new();
            Board b = new(4, 4);

            Queue<Position> Q = new();
            b.ResetBoard();
            Console.WriteLine(b.GetTile(new Position(0, 0)));
            Q.Enqueue(new Position(0, 0));

            while (Q.Count > 0)
            {
                Position pos = Q.Dequeue();
                p.Pos = pos;
                foreach (Position move in p.PossibleMoves(b.Width, b.Height))
                {
                    if (b.GetTile(move) == -1)
                    {
                        b.SetTile(move, b.GetTile(pos) + 1);
                        Q.Enqueue(move);
                    }
                }
            }
            Console.WriteLine(b.GetTile(new Position(b.Width-1, b.Height-1)));
        }
    }
}