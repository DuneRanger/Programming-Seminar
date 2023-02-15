using System.Runtime.CompilerServices;
using System.Text;

namespace PacMan
{
    public class Game
    {
        private Map Board { get; set; }
        public int score { get { return this.Board.score; } }
        public bool Finished { get { return this.Board.score == this.Board.coinNum; }}

        public Game()
        {
            this.Board = new Map();
        }

        public void UpdatePac(char inp)
        {
            this.Board.UpdatePac(inp);
        }

        public void PrintBoard()
        {
            StringBuilder b = this.Board.BoardToString();
            // overwrite map with the positions of the ghosts
            Console.WriteLine(b);

        }
    }

    class Ghost
    {
        public int posX { get; private set; }
        public int posY { get; private set; }
        private int MoveType { get; set; }
        
        public Ghost(int x, int y, int Type)
        {
            this.posX = x;
            this.posY = y;
            this.MoveType = Type;
        }

        public void Move()
        {
            switch(this.MoveType)
            {
                case 0: MoveTypeOne(); break;
                case 1: MoveTypeTwo(); break;
                case 2: MoveTypeThree(); break;
            }
        }
        public void MoveTypeOne()
        {
            Random rnd = new Random();
            int dir = rnd.Next(0, 5);
            switch(dir)
            {
                case 0:
                    break;
            }
        }
        public void MoveTypeTwo()
        {

        }
        public void MoveTypeThree()
        {

        }

    }

    public class Map
    {
        private char Pac { get; set; }
        private int PacRow { get; set; }
        private int PacCol { get; set; }
        private char[,] Board { get; set; }
        private int MapWid { get { return this.Board.GetLength(1); } }
        private int MapHei { get { return this.Board.GetLength(0); } }
        public int score { get; private set; }
        public int coinNum { get; private set; }
        public Map()
        {
            this.Pac = '>';
            this.PacRow = 5;
            this.PacCol = 5;
            this.Board = new char[12, 12] {
                { '.', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '.'},
                { '.', 'G', '.', '.', '.', '.', '.', '.', 'X', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', 'X', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', 'X', '.', '.', '.'},
                { '.', '.', '.', 'o', '.', '.', '.', '.', 'X', '.', 'G', '.'},
                { '.', '.', '.', 'o', '.', '>', '.', '.', 'X', '.', '.', '.'},
                { '.', '.', '.', 'o', '.', '.', '.', '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', 'o', '.', 'o', '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                { '.', '.', '.', 'X', 'X', 'X', 'X', 'X', '.', '.', '.', '.'},
            };
            //this.Board[PacRow, PacCol] = this.Pac;

            this.score = 0;
            for (int i = 0; i < MapHei; i++)
            {
                for (int j = 0; j < MapWid; j++)
                {
                    if (this.Board[i, j] == 'o') coinNum++;
                }
            }
        }
        private void PacUp()
        {
            if (this.PacRow > 0 && Board[this.PacRow - 1, this.PacCol] != 'X')
            {
                this.PacRow--;
            }
        }
        private void PacLeft()
        {
            if (this.PacCol > 0 && Board[this.PacRow, this.PacCol - 1] != 'X')
            {
                this.PacCol--;
            }
        }
        private void PacRight()
        {
            if (this.PacCol < this.MapHei-1 && Board[this.PacRow, this.PacCol + 1] != 'X')
            {
                this.PacCol++;
            }
        }

        private void PacDown()
        {
            if (this.PacRow < this.MapWid-1 && Board[this.PacRow + 1, this.PacCol] != 'X')
            {
                this.PacRow++;
            }
        }

        private void MovePac()
        {
            this.Board[PacRow, PacCol] = '.';
            switch (this.Pac)
            {
                case '^':
                    PacUp();
                    break;
                case '<':
                    PacLeft();
                    break;
                case '>':
                     PacRight();
                    break;
                case 'v':
                    PacDown();
                    break;
            }
            if (this.Board[PacRow, PacCol] == 'o')
            {
                score++;
            }
            this.Board[PacRow, PacCol] = this.Pac;
        }



        public bool CheckVictory()
        {
            return this.score == coinNum;
        }

        public void UpdatePac(char inp)
        {
            this.Pac = inp == '.' ? this.Pac : inp;
            MovePac();
            return;
        }

        public StringBuilder BoardToString()
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < this.MapWid; i++)
            {
                for (int j = 0; j < this.MapHei; j++)
                {
                    b.Append(Board[i, j].ToString());
                }
                b.AppendLine();
            }
            return b;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();

            while (!g.Finished)
            {
                Console.Clear();
                g.PrintBoard();
                Console.WriteLine("Score: " + g.score);
                var inp = Console.ReadKey();
                Console.WriteLine();
                var input = inp.KeyChar.ToString();
                char dir = '.';
                switch (input)
                {
                    case "w": case "W": dir = '^'; break;
                    case "a": case "A": dir = '<'; break;
                    case "s": case "S": dir = 'v'; break;
                    case "d": case "D": dir = '>'; break;
                }
                g.UpdatePac(dir);
            }
        }
    }
}