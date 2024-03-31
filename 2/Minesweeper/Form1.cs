using System.Runtime.CompilerServices;

namespace Minesweeper
{
    // začátek nadstavby, aby byl program lépe formátovaný, abych mohl snadněji udělat rekurzivní objevování
    // (nechtělo se mi to dodělat, takže to bohužel není)
    public class Tile : Label
    {
        public int MineCount { get; set; }

        public Tile()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TabIndex = 1;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Text = "";
        }
    }
    public partial class Form1 : Form
    {
        public int n;
        public int m;
        public int mineCount;
        public int coveredMines;
        public bool gameOver = false;

        public List<List<Tile>> map = new List<List<Tile>>();
        public Form1()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Form1_Resize);

            this.n = 6;
            this.m = 6;

            MakeGrid();
            resizeAllLabels();
            Random rnd = new Random();
            // náhodná obtížnost podle náhodných konstant, který jsem vymyslel
            this.mineCount = rnd.Next(m*n/7, m*n/3);
            PlaceMines();
            UpdateTileTags();

            InitializeComponent();
        }

        private void MakeGrid()
        {
            for (int i = 0; i < this.n; i++)
            {
                this.map.Add(new List<Tile>());
                for (int j = 0; j < this.m; j++)
                {
                    this.map[i].Add(new Tile());
                    this.Controls.Add(this.map[i][j]);

                    Tile t = this.map[i][j];

                    t.Click += new System.EventHandler(TileClick);
                }
            }
        }

        private void PlaceMines()
        {
            for (int i = 0; i < this.mineCount; i++)
            {
                while (true)
                {
                    Random rnd = new Random();
                    int row = rnd.Next(0, this.n);
                    int col = rnd.Next(0, this.m);

                    if ((int)this.map[row][col].MineCount != -1)
                    {
                        this.map[row][col].MineCount = -1;
                        break;
                    }
                }
            }
            this.coveredMines = this.mineCount;
        }

        private void UncoverAllMines()
        {
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    if ((int)this.map[i][j].MineCount == -1)
                    {
                        this.map[i][j].Text = "💣";
                    }
                }
            }
        }

        private void UpdateTileTags()
        {
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    if ((int)this.map[i][j].MineCount != -1) this.map[i][j].MineCount = SurroundingMines(i, j);
                }
            }
        }

        private int SurroundingMines(int row, int col)
        {
            int mines = 0;

            if (row > 0)
            {
                if ((int)this.map[row - 1][col].MineCount == -1) mines++;
                if (col > 0) if ((int)this.map[row - 1][col - 1].MineCount == -1) mines++;
                if (col < this.m-1) if ((int)this.map[row - 1][col + 1].MineCount == -1) mines++;
            }
            if (row < this.n-1)
            {
                if ((int)this.map[row + 1][col].MineCount == -1) mines++;
                if (col > 0) if ((int)this.map[row + 1][col - 1].MineCount == -1) mines++;
                if (col < this.m - 1) if ((int)this.map[row + 1][col + 1].MineCount == -1) mines++;
            }
            if (col > 0) if ((int)this.map[row][col - 1].MineCount == -1) mines++;
            if (col < this.m-1) if ((int)this.map[row][col + 1].MineCount == -1) mines++;

            return mines;
        }

        private void resizeAllLabels()
        {
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    int smallerNum = Math.Min(ClientSize.Width / this.n, ClientSize.Height / this.m);
                    this.map[i][j].Size = new System.Drawing.Size(smallerNum, smallerNum);
                    this.map[i][j].Location = new System.Drawing.Point(this.map[i][j].Size.Width * (i), this.map[i][j].Size.Height * (j));
                    this.map[i][j].Font = new Font("Sans", this.map[i][j].Size.Width/5);
                }
            }
        }

        private void TileClick(object sender, EventArgs ev)
        {
            if (this.gameOver) return;
            Tile t = (Tile)sender;
            MouseEventArgs e = (MouseEventArgs)ev;
            if (e.Button == MouseButtons.Left)
            {
                if (t.Text == "🚩") return;
                if ((int)t.MineCount == -1)
                {
                    GameOver(false);
                    t.Text = "💥";
                }
                else
                {
                    if ((int)t.MineCount == 0)
                    {
                        // tady by bylo rekurivní odhalení
                    }
                    t.Text = ((int)t.MineCount).ToString();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // hra jenom sleduje, jestli jsi označil právě všechny miny
                // tudíž můžeš čistě jen ozačovat čtverce a jestli se náhodou trefíš, vyhraješ
                if (t.Text == "🚩")
                {
                    if ((int)t.MineCount == -1) coveredMines++;
                    t.Text = "";
                }
                else
                {
                    if ((int)t.MineCount == -1) coveredMines--;
                    t.Text = "🚩";
                }
                if (this.coveredMines == 0)
                {
                    GameOver(true);
                }
            }
        }

        private void GameOver(bool win)
        {
            Label l = new Label();
            this.Controls.Add(l);
            l.TabIndex = 1;
            l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            l.BringToFront();
            int smallerDim = Math.Min(ClientSize.Width / 40, ClientSize.Height / 20);
            l.Size = new System.Drawing.Size(smallerDim*10, smallerDim*10);
            l.BackColor = System.Drawing.Color.Transparent;
            l.Font = new Font("Sans", smallerDim);
            l.Location = new System.Drawing.Point(ClientSize.Width/2 - smallerDim*5, ClientSize.Height/10);
            if (win)
            {
                l.Text = "YOU WON!";
                this.gameOver = true;
            }
            else
            {
                l.Text = "YOU LOST!";
                UncoverAllMines();
                this.gameOver = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (this.gameOver) return;
            resizeAllLabels();
        }
    }
}