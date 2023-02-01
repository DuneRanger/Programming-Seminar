namespace PacMan
{
    internal class Program
    {

        public class Game
        {
            private Map Board { get; set; }

            public Game()
            {
                this.Board = new Map();

            }
            private char getInput()
            {
                var temp = Console.Read();
                char inp = (char)(temp.GetType().Equals(typeof(int)) ? '.' : temp);
                return inp;
            }  
        }

        public class Map
        {
            private int direction { get; set; }
            private char Pac { get; set; }
            private int[] pos { get; set; }
            private char[,] Board { get; set; }
            private int[] dimensions { get; set; }

            public Map()
            {
                this.Pac = '>';
                this.pos = new int[2] {5, 5};
                this.Board = new char[12, 12] {
                    { '.', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                };
                this.dimensions = new int[2] { 12, 12 };
            }

            private char ParseInput(string input)
            {
                switch (input.ToLower())
                {
                    case "w": return '^';
                    case "a": return '<';
                    case "s": return 'v'; 
                    case "d": return '>';
                }
                return this.Pac;
            }

            private void PacUp()
            {
                if (this.pos[1] > 0 && Board[this.pos[0], this.pos[1] - 1] != 'X')
                {
                    this.pos[1]--;
                }
            }
            private void PacLeft()
            {
                if (this.pos[0] > 0 && Board[this.pos[0] - 1, this.pos[1]] != 'X')
                {
                    this.pos[0]--;
                }
            }
            private void PacRight()
            {
                if (this.pos[1] < this.dimensions[1] && Board[this.pos[0], this.pos[1] + 1] != 'X')
                {
                    this.pos[1]++;
                }
            }

            private void PacDown()
            {
                if (this.pos[0] < this.dimensions[0] && Board[this.pos[0] + 1, this.pos[1]] != 'X')
                {
                    this.pos[0]++;
                }
            }

            private void MovePac()
            {
                switch (this.direction)
                {
                    case 1:
                        PacUp();
                        break;
                    case 2:
                        PacLeft();
                        break;
                    case 3:
                        PacRight();
                        break;
                    case 4:
                        PacDown();
                        break;
                }
            }

            private void UpdatePac(string input)
            {
                Pac = ParseInput(input);
                MovePac();
            }

            private string BoardToString()
            {
                string b = "";
                for (int i = 0; i < this.dimensions[0]; i++)
                {
                    for (int j = 0; j < this.dimensions[1]; j++)
                    {
                        b += Board[i, j];
                    }
                    b+= "\n";
                }
                return b;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}