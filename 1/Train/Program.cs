using System.Text;

namespace Train
{
    enum Tiles {Empty, Train, Wagon, Wall, Door, Diamond}

    public struct Vect2D
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        public static Vect2D Up { get { return new Vect2D(-1, 0); } }

        public static Vect2D Left { get { return new Vect2D(0, -1); } }

        public static Vect2D Down { get { return new Vect2D(1, 0); } }

        public static Vect2D Right { get { return new Vect2D(0, 1); } }

        public Vect2D(int x, int y)
        {
            this.PosX = x;
            this.PosY = y;
        }

        public static Vect2D Add(Vect2D u, Vect2D v)
        {
            return new Vect2D(u.PosX + v.PosX, u.PosY + v.PosY);
        }

        public Vect2D MoveUp()
        {
            return Vect2D.Add(this, Vect2D.Up);
        }
        public Vect2D MoveLeft()
        {
            return Vect2D.Add(this, Vect2D.Left);
        }
        public Vect2D MoveDown()
        {
            return Vect2D.Add(this, Vect2D.Down);
        }
        public Vect2D MoveRight()
        {
            return Vect2D.Add(this, Vect2D.Right);
        }
    }

    class Wagon
    {
        public Vect2D Pos { get; set; }
        public Vect2D Dir { get; private set; }

        public Tiles Type { get; private set; }

    }

    class Train
    {
        public Vect2D Pos { get; set; }
        public bool Crashed { get; set; }
    }

    class Game
    {
        private Map Board { get; set; }
        public Vect2D LastMove { get; private set; }
        public bool Finished { get; private set; }

        public Game()
        {
            Board = new Map();
        }

        public void MoveUp()
        {
            if (CheckMove(Vect2D.Up))
            {
                return;
            }
            this.Board.MoveTrain(Vect2D.Up);
            LastMove = Vect2D.Up;
        }

        public void MoveLeft()
        {
            if (CheckMove(Vect2D.Left))
            {
                return;
            }
            this.Board.MoveTrain(Vect2D.Left);
            LastMove = Vect2D.Left;
        }

        public void MoveDown()
        {
            if (CheckMove(Vect2D.Down))
            {
                return;
            }
            this.Board.MoveTrain(Vect2D.Down);
            LastMove = Vect2D.Down;
        }

        public void MoveRight()
        {
            if (CheckMove(Vect2D.Right))
            {
                return;
            }
            this.Board.MoveTrain(Vect2D.Right);
            LastMove = Vect2D.Right;
        }

        public bool CheckMove(Vect2D v)
        {
            throw new NotImplementedException();
        }

        public string MapAsString()
        {
            StringBuilder sb = this.Board.MapToString();
            return sb.ToString();
        }
    }

    class Map
    {
        public Tiles[,] Board { get; private set; }
        public Train T { get; private set; }
        public List<Wagon> Wagons = new List<Wagon>();

        public bool Crashed { get { return T.Crashed; } }
        public bool DoorOpen { get; set; }

        public Map()
        {
            this.Board = new Tiles[12, 12];
            this.T = new Train();

            LoadStringMap(@"
                X X X X X X X X X X X
                X . . . . . . . . . X
                . . . . . . . . . . .
                . . . a . . . . . . .
                T . . . . . . . . . .
                ");
        }

        public void MoveTrain(Vect2D v)
        {
            ////check a valid vector
            //if ((v.PosX != 0 && v.PosY != 0) || v.PosX + v.PosY != 1)
            //{
            //    //incorrect vector
            //    return;
            //}
            if (!CanMove(v))
            {
                this.T.Crashed = true;
                return;
            }
            if (IsTileEmpty(v))
            {
                MoveTrainEmpty(v);
                return;
            }
            MoveTrainCargo(v);
        }

        public bool CanMove(Vect2D v)
        {
            throw new NotImplementedException();
        }

        public bool IsTileEmpty(Vect2D v)
        {
            throw new NotImplementedException();
        }

        public void MoveTrainEmpty(Vect2D v)
        {
            throw new NotImplementedException();
        }

        public void MoveTrainCargo(Vect2D v)
        {
            throw new NotImplementedException();
        }

        private void LoadStringMap(string StringMap)
        {
            string[] ArrayMap = StringMap.Split('\n');

            for (int x = 0; x < ArrayMap.Length; x++)
            {
                for (int y = 0; y < ArrayMap[x].Length; y++)
                {
                    switch (ArrayMap[x][y])
                    {
                        case '.':
                            this.Board[x, y] = Tiles.Empty;
                            break;
                        case 'X':
                            this.Board[x, y] = Tiles.Wall;
                            break;
                        case 'T':
                            this.T.Pos = new Vect2D(x, y);
                            break;
                        case 'D':
                            this.Board[x, y] = Tiles.Door;
                            break;
                        case 'a':
                            this.Board[x, y] = Tiles.Diamond;
                            break;

                        default:
                            this.Board[x, y] = Tiles.Empty;
                            break;
                    }

                }
            }
            return;
        }

        public StringBuilder MapToString()
        {
            return new StringBuilder();
        }
    }

    class Door
    {
        public char[] AnswerKey { get; private set; }
        
        public Door()
        {
            this.AnswerKey = new char[5] { 'D', 'F', 'M', 'A', 'H' };
        }
    }

    class Fruit
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();

            
            while (!g.Finished)
            {
                Console.Clear();
                Console.WriteLine(g.MapAsString());
                var inp = Console.ReadKey();
                Console.WriteLine();
                var input = inp.KeyChar.ToString();
                switch (input)
                {
                    case "w": case "W": g.MoveUp(); break;
                    case "a": case "A": g.MoveLeft(); break;
                    case "s": case "S": g.MoveDown(); break;
                    case "d": case "D": g.MoveRight(); break;
                }

                //g.UpdateGhosts();
                //g.UpdatePac(dir);
            }
        }
    }
}