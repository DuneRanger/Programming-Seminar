using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace PacMan
{
    public class Game
    {
        public enum GameState { Running, Won, Lost}
        private Map Board { get; set; }
        public int score { get { return this.Board.score; } }
        public GameState State { get; set; }
        private Ghost[] Ghosts { get; set; }

        public Game()
        {
            this.Board = new Map();
            // Since ghost positions are hard-coded, check the board before changing them
            Ghosts = new Ghost[4];
            Ghosts[0] = new Ghost(0, 0, 0, this.Board.Board);
            Ghosts[1] = new Ghost(0, 14, 0, this.Board.Board);
            Ghosts[2] = new Ghost(14, 14, 1, this.Board.Board);
            Ghosts[3] = new Ghost(0, 7, 2, this.Board.Board);
            foreach(Ghost g in Ghosts)
            {
                this.Board.Board[g.posX, g.posY] = 'G';
            }
        }

        // Updates Pacmans sprite and then moves him accordingly, also changes this.State when neccessary
        public void UpdatePac(char inp)
        {
            this.Board.UpdatePac(inp);
            if (this.Board.CheckVictory()) this.State = Game.GameState.Won;
        }

        // Updates each ghosts position and changes this.State when neccessary
        public void UpdateGhosts()
        {
            foreach(Ghost g in Ghosts)
            {
                this.Board.RewriteTile(g.MovingOnto, g.posX, g.posY);
                if (g.Move(this.Board.PacRow, this.Board.PacCol))
                {
                    this.State = Game.GameState.Lost;
                }
                this.Board.RewriteTile('G', g.posX, g.posY);
            }
        }

        // Directly prints the board with colours
        public void PrintBoard()
        {
            StringBuilder b = this.Board.BoardToString();
            for (int i = 0; i < b.Length; i++)
            {
                switch(b[i])
                {
                    case '.':
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 'o':
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case 'X':
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case 'G':
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
                Console.Write(b[i] + " ");
                Console.ResetColor();
            }
        }
    }

    class Ghost
    {
        public int posX { get; private set; }
        public int posY { get; private set; }
        private int MoveType { get; set; }
        // Board is required for checking valid moves
        private char[,] Board { get; set; }
        // MovingOnto is required so that coins and other ghosts don't disappear
        public char MovingOnto { get; private set; }
        
        public Ghost(int x, int y, int Type, char[,] Board)
        {
            this.posX = x;
            this.posY = y;
            this.MoveType = Type;
            this.Board = Board;
            this.MovingOnto = this.Board[this.posX, this.posY];
        }

        //This isn't working, so I'll just do normal comments

        /// <summary>
        ///     Updates the ghosts position based on its movement type
        ///     Updates MovingOnto based on whatever the gost just moved onto
        /// </summary>
        /// <param name="PacX">Pacmans row</param>
        /// <param name="PacY">Pacmans column</param>
        /// <returns>Colision</returns>
        public bool Move(int PacX, int PacY)
        {
            //If Pacman moved onto the ghost
            if (this.posX == PacX && this.posY == PacY) return true;
            switch(this.MoveType)
            {
                case 0: MoveTypeOne(); break;
                case 1: MoveTypeTwo(PacX, PacY); break;
                case 2: MoveTypeThree(PacX, PacY); break;
            }
            this.MovingOnto = this.Board[posX, posY];
            //Stops extra ghost symbols from generating when they cross paths
            if (this.MovingOnto == 'G') { this.MovingOnto = '.'; }
            return this.posX == PacX && this.posY == PacY;
        }

        // Completely random movement, but never no movement
        public void MoveTypeOne()
        {
            Random rnd = new Random();
            bool moved = false;
            while (!moved)
            {
                int dir = rnd.Next(0, 4);
                switch(dir)
                {
                    case 0:
                        if (this.MoveUp()) moved = true;
                        break;
                    case 1:
                        if (this.MoveLeft()) moved = true;
                        break;
                    case 2:
                        if (this.MoveRight()) moved = true;
                        break;
                    case 3:
                        if (this.MoveDown()) moved = true;
                        break;
                }
            }
        }

        // Closing in on Pacmans x (row) and y (column)
        // Can get stuck, but will always be there to block off a path
        public void MoveTypeTwo(int PacX, int PacY)
        {
            if (this.posX == PacX)
            {
                if (this.posY > PacY) this.MoveLeft();
                else this.MoveRight();
            }
            else if (this.posY == PacY)
            {
                if (this.posX > PacX) this.MoveUp();
                else this.MoveDown();
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

        // Node class for A*
        class Node
        {
            public enum MoveDir { Up, Left, Down, Right}
            public int posX { get; private set; }
            public int posY { get; private set; }
            public Node parent { get; set; }
            public MoveDir move { get; set; }
            public int cost { get; set; }
            public int costH { get; set; }
            public int costTotal { get { return this.cost + this.costH; } }

            public Node(int x, int y)
            {
                this.posX = x;
                this.posY = y;
                this.cost = 1;
                this.costH = 0;
                this.parent = null;
            }
        }

        // A* with Manhattan distance
        public void MoveTypeThree(int PacX, int PacY)
        {
            // Inspiration, since I couldn't be bothered doing it from scratch and fixing bugs (as if there weren't some anyway)
            // https://github.com/Hoyby/AStar/blob/main/src/aStar.py

            PriorityQueue<Node, int> Q = new PriorityQueue<Node, int>();
            Node Start = new Node(this.posX, this.posY);
            Node Goal = new Node(PacX, PacY);
            //Start cost can stay as 1 (default cost) and not affect anything
            Q.Enqueue(Start, Start.costTotal);

            // Since we are creating new nodes every time, we can only check if they have been closed by checking the x and y value
            // The other option would be rewriting the board as Nodes, or just converting it specially for ghost type 3
            List<Tuple<int, int>> closed = new List<Tuple<int, int>>();

            while (Q.Count > 0)
            {
                Node currentNode = Q.Dequeue();
                closed.Add(new Tuple<int, int>(currentNode.posX, currentNode.posY));

                if (currentNode.posX == PacX && currentNode.posY == PacY)
                {
                    Node current = currentNode;
                    Node.MoveDir lastMove = current.move;

                    // Since we only need the first move taken
                    while (current.parent != null)
                    {
                        lastMove = current.move;
                        current = current.parent;
                    }
                    switch(lastMove)
                    {
                        case Node.MoveDir.Up:
                            this.MoveUp();
                            break;
                        case Node.MoveDir.Left:
                            this.MoveLeft();
                            break;
                        case Node.MoveDir.Down:
                            this.MoveDown();
                            break;
                        case Node.MoveDir.Right:
                            this.MoveRight();
                            break;
                    }
                    return;
                }

                List<(int, int)> moves = new List<(int, int)>() { (1, 0), (-1, 0), (0, 1), (0, -1)};

                for (int i = 0; i < moves.Count; i++) {
                    Node child = new Node(currentNode.posX + moves[i].Item1, currentNode.posY + moves[i].Item2);

                    //Checks if child.posX and child.posY is within the bounds of the map and isn't a wall ('X')
                    if (child.posX < 0 || child.posX >= this.Board.GetLength(0) || child.posY < 0 || child.posY >= this.Board.GetLength(1) || this.Board[child.posX, child.posY] == 'X')
                    {
                        continue;
                    }
                    // if the current child has already been closed
                    if (closed.Exists(c => c.Item1 == child.posX && c.Item2 == child.posY)) {
                        continue;
                    }

                    child.cost += currentNode.cost;
                    child.costH = Math.Abs(Goal.posX-child.posX) + Math.Abs(Goal.posY-child.posY); // Manhattan distance
                    child.parent = currentNode;
                    switch(i)
                    {
                        case 0:
                            child.move = Node.MoveDir.Down;
                            break;
                        case 1:
                            child.move = Node.MoveDir.Up;
                            break;
                        case 2:
                            child.move = Node.MoveDir.Right;
                            break;
                        case 3:
                            child.move = Node.MoveDir.Left;
                            break;                            
                    }

                    bool validEnqueue = true;
                    // Checks if the current square is already in the queue and if the one in the queue has a smaller cost
                    foreach ((Node, int) n in Q.UnorderedItems)
                    {
                        if (child.posX == n.Item1.posX && child.posY == n.Item1.posY && child.cost > n.Item1.cost)
                        {
                            validEnqueue = false;
                            break;
                        }
                    }
                    if (validEnqueue)
                    {
                        Q.Enqueue(child, child.costTotal);
                    }
                }
            }
        }

        // All ghost movement functions return whether the move is valid or not
        // If the move was valid, it will also execute (before returning true)

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
            this.PacRow = 14;
            this.PacCol = 0;
            // When resizing the board, do not forget to update ghost and Pac starting positions
            this.Board = new char[15, 15] {
                { 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                { 'o', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'X', 'X', 'X', 'o', 'X', 'o'},
                { 'o', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'o', 'o', 'o', 'o', 'o', 'X', 'o'},
                { 'o', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'o'},
                { 'o', 'X', 'o', 'X', 'X', 'o', 'o', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'o'},
                { 'o', 'X', 'o', 'o', 'X', 'X', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'o'},
                { 'o', 'X', 'X', 'o', 'X', 'o', 'o', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'o'},
                { 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                { 'o', 'X', 'X', 'o', 'X', 'o', 'o', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'o'},
                { 'o', 'X', 'o', 'o', 'X', 'X', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'o'},
                { 'o', 'X', 'o', 'X', 'X', 'o', 'o', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'o'},
                { 'o', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'o'},
                { 'o', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'o', 'o', 'o', 'o', 'o', 'X', 'o'},
                { 'o', 'X', 'o', 'X', 'X', 'o', 'X', 'o', 'X', 'X', 'X', 'X', 'o', 'X', 'o'},
                { '.', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
            };
            this.Board[PacRow, PacCol] = this.Pac;

            this.score = 0;
            // calculates max score (coinNum)
            for (int i = 0; i < MapHei; i++)
            {
                for (int j = 0; j < MapWid; j++)
                {
                    if (this.Board[i, j] == 'o') coinNum++;
                }
            }
        }

        // All Pacman movement functions simply move Pacman, if the move is valid
        // There has been no need to return whether the move is valid or not (like with the ghosts)

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

        // Move Pacman depending on the sprite this.Pac (the sprite is updated in UpdatePac())
        // Simply rewrites wherever Pacman has been to '.', since there is no case where he can leave any non '.' or 'o' square
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

        // returns whether the current score and coinNum match up
        public bool CheckVictory()
        {
            return this.score == coinNum;
        }

        // Changes Pacmans sprite and moves accordingly (should be improved once a visual overhaul is done)
        public void UpdatePac(char inp)
        {
            this.Pac = inp == '.' ? this.Pac : inp;
            MovePac();
            return;
        }

        // Used to rewrite board tiles after ghosts move off of them
        public void RewriteTile(char sprite, int x, int y)
        {
            this.Board[x, y] = sprite;
            return;
        }

        // Converts the board to a StringBuilder object
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

            Console.WriteLine("Are you ready? (y/n)");
            var play = Console.ReadKey();
            Console.WriteLine();
            var playing = play.KeyChar.ToString();

            // Loops the main game
            while (playing == "y")
            {
                Game g = new Game();

                // Loops the in-game turns
                while (g.State == Game.GameState.Running)
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
                    g.UpdateGhosts();
                }

                if (g.State == Game.GameState.Won)
                {
                    Console.WriteLine("Congrats");
                }
                else
                {
                    Console.WriteLine("You lost...");
                }

                Console.WriteLine("Play again? (y/n)");
                play = Console.ReadKey();
                Console.WriteLine();
                playing = play.KeyChar.ToString();

            }
        }
    }
}