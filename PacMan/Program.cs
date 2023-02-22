using System.Runtime.CompilerServices;
using System.Text;

namespace PacMan
{
    public class Game
    {
        private Map Board { get; set; }
        public int score { get { return this.Board.score; } }
        public bool Finished { get { return this.Board.score == this.Board.coinNum; }}
        private Ghost[] Ghosts { get; set; }

        public Game()
        {
            this.Board = new Map();
            Ghosts = new Ghost[3];
            Ghosts[0] = new Ghost(0, 0, 0, this.Board.Board);
            Ghosts[1] = new Ghost(11, 0, 1, this.Board.Board);
            Ghosts[2] = new Ghost(0, 11, 2, this.Board.Board);
            foreach(Ghost g in Ghosts)
            {
                this.Board.Board[g.posX, g.posY] = 'G';
            }
        }

        public void UpdatePac(char inp)
        {
            this.Board.UpdatePac(inp);
        }

        public void UpdateGhosts()
        {
            foreach(Ghost g in Ghosts)
            {
                this.Board.RewriteTile('.', g.posX, g.posY);
                if (g.Move(this.Board.PacRow, this.Board.PacCol))
                {
                    //Except now Ghosts move before Pac, so it is wrong
                    //Ghost ate Pac
                    //Do something
                }
                this.Board.RewriteTile('G', g.posX, g.posY);
            }
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



        private char[,] Board { get; set; }
        
        public Ghost(int x, int y, int Type, char[,] Board)
        {
            this.posX = x;
            this.posY = y;
            this.MoveType = Type;
            this.Board = Board;
        }

        public bool Move(int PacX, int PacY)
        {
            switch(this.MoveType)
            {
                case 0: MoveTypeOne(); break;
                case 1: MoveTypeTwo(PacX, PacY); break;
                case 2: MoveTypeThree(PacX, PacY); break;
            }
            return this.posX == PacX && this.posY == PacY;
        }

        public void MoveTypeOne()
        {
            Random rnd = new Random();
            int dir = rnd.Next(0, 4);
            switch(dir)
            {
                case 0:
                    this.MoveUp();
                    break;
                case 1:
                    this.MoveLeft();
                    break;
                case 2:
                    this.MoveRight();
                    break;
                case 3:
                    this.MoveDown();
                    break;
            }
        }
        public void MoveTypeTwo(int PacX, int PacY)
        {
            if (this.posX == PacX)
            {
                if (this.posY > PacY) this.MoveLeft();
                else this.MoveRight();
            }
            else if (new Random().Next(0, 2) == 1) {
                if (this.posX > PacX)
                {
                    if (!this.MoveUp())
                    {
                        if (this.posY > PacY) this.MoveLeft();
                        else this.MoveRight();
                    }
                }
                else
                {
                    if (!this.MoveDown())
                    {
                        if (this.posY > PacY) this.MoveLeft();
                        else this.MoveRight();
                    }
                }
            }
            else
            {
                if (this.posY > PacY)
                {
                    if (!this.MoveLeft())
                    {
                        if (this.posX > PacX) this.MoveUp();
                        else this.MoveDown();
                    }
                }
                else
                {
                    if (!this.MoveRight())
                    {
                        if (this.posX > PacX) this.MoveUp();
                        else this.MoveDown();
                    }
                }
            }
        }
        public void MoveTypeThree(int PacX, int PacY)
        {

        }

        private bool MoveUp()
        {
            if (this.posX > 0 && this.Board[this.posX - 1, this.posY] != 'X')
            {
                this.posX--;
                return true;
            }
            return false;
        }
        private bool MoveLeft()
        {
            if (this.posY > 0 && Board[this.posX, this.posY - 1] != 'X')
            {
                this.posY--;
                return true;
            }
            return false;
        }
        private bool MoveRight()
        {
            if (this.posY < this.Board.GetLength(1) - 1 && this.Board[this.posX, this.posY + 1] != 'X')
            {
                this.posY++;
                return true;
            }
            return false;
        }

        private bool MoveDown()
        {
            if (this.posX < this.Board.GetLength(0) - 1 && this.Board[this.posX + 1, this.posY] != 'X')
            {
                this.posX++;
                return true;
            }
            return false;
        }

    }

    public class Map
    {
        private char Pac { get; set; }
        public int PacRow { get; private set; }
        public int PacCol { get; private set; }
        public char[,] Board { get; private set; }
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
                { '.', '.', '.', '.', '.', '.', '.', '.', 'X', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', 'X', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', 'X', '.', '.', '.'},
                { '.', '.', '.', 'o', '.', '.', '.', '.', 'X', '.', '.', '.'},
                { '.', '.', '.', 'o', '.', '.', '.', '.', 'X', '.', '.', '.'},
                { '.', '.', '.', 'o', '.', '.', '.', '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', 'o', '.', 'o', '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                { '.', '.', '.', 'X', 'X', 'X', 'X', 'X', '.', '.', '.', '.'},
            };
            this.Board[PacRow, PacCol] = this.Pac;

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

        public void RewriteTile(char sprite, int x, int y)
        {
            this.Board[x, y] = sprite;
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
                g.UpdateGhosts();
                g.UpdatePac(dir);
            }
        }
    }
}