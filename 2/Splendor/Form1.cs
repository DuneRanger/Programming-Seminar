using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;


namespace Splendor
{
    public partial class Form1 : Form
    {
        List<Label> Tokens = new List<Label>();
        public Form1()
        {
            InitializeComponent();
            Controller Game = new Controller(15);
            GenerateUI();
        }

        public void GenerateTokens()
        {
            int TokenSize = 40;
            int TokenX = 300;
            int TokenYStart = 80;

            Label label = new Label();
            label.BackColor = Color.Black;
            label.Size = new Size(TokenSize, TokenSize);
            label.Location = new Point(TokenX, TokenYStart);
            label.Tag = 0;
            label.Click += new System.EventHandler(TokenClick);
            Tokens.Add(label);
            this.Controls.Add(label);

            label = new Label();
            label.BackColor = Color.Red;
            label.Size = new Size(TokenSize, TokenSize);
            label.Location = new Point(TokenX, TokenYStart + (int)(TokenSize * 1.5));
            label.Tag = 1;
            label.Click += new System.EventHandler(TokenClick);
            Tokens.Add(label);
            this.Controls.Add(label);

            label = new Label();
            label.BackColor = Color.Green;
            label.Size = new Size(TokenSize, TokenSize);
            label.Location = new Point(TokenX, TokenYStart + (int)(TokenSize * 3));
            label.Tag = 2;
            label.Click += new System.EventHandler(TokenClick);
            Tokens.Add(label);
            this.Controls.Add(label);

            label = new Label();
            label.BackColor = Color.Blue;
            label.Size = new Size(TokenSize, TokenSize);
            label.Location = new Point(TokenX, TokenYStart + (int)(TokenSize * 4.5));
            label.Tag = 3;
            label.Click += new System.EventHandler(TokenClick);
            Tokens.Add(label);
            this.Controls.Add(label);

            label = new Label();
            label.BackColor = Color.White;
            label.Size = new Size(TokenSize, TokenSize);
            label.Location = new Point(TokenX, TokenYStart + (int)(TokenSize * 6));
            label.Tag = 4;
            label.Click += new System.EventHandler(TokenClick);
            Tokens.Add(label);
            this.Controls.Add(label);

            label = new Label();
            label.BackColor = Color.Yellow;
            label.Size = new Size(TokenSize, TokenSize);
            label.Location = new Point(TokenX, TokenYStart + (int)(TokenSize * 6));
            label.Tag = 5;
            Tokens.Add(label);
            this.Controls.Add(label);
        }

        private void TokenClick(object sender, EventArgs ev)
        {
            MouseEventArgs e = (MouseEventArgs)ev;

        }

        public void GenerateUI()
        {
            GenerateTokens();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Token : Label
    {
        int size = 40;
        int x = 300;
        int startY = 80;
        public Token(Color col, int i)
        {
            this.BackColor = col;
            this.Size = new Size(size, size);
            this.Location = new Point(x, startY + (int)(i*1.5*size));
        }
    }

    public class Card : Label
    {
        public int id;
        public int tier;
        public int type;
        public int Score;
        public List<int> Cost;

        public Card(int id, int type, int tier, int score, List<int> cost)
        {
            this.id = id;
            this.type = type;
            this.Score = score;
            this.Cost = cost;
        }
    }
    public class Player
    {
        public int id;
        public string name;
        public int Score;
        public List<int> Tokens;
        public List<int> Cards;
        public List<Card> HeldCards;

        public Player(int id, string name = "")
        {
            this.id = id;
            this.name = name != "" ? name : id.ToString();
            this.Score = 0;
            this.Tokens = new List<int>() { 0, 0, 0, 0, 0, 0 };
            this.Cards = new List<int>() { 0, 0, 0, 0, 0 };
            this.HeldCards = new List<Card>();
        }
    }

    public class Market
    {
        public List<List<Card>> Deck; // two dimensional, first array is tier 1, second tier 2, third tier 3
        public List<List<Card>> ForSale;
        public List<Card> BonusCards;
        public List<int> Tokens;
        private static Random rng = new Random();

        public Market()
        {
            // Initialize deck and cards here
            this.ForSale = new List<List<Card>>();
            this.Deck = new List<List<Card>>();
            this.BonusCards = new List<Card>()
            {
                new Card(0, -1, -1, 3, new List<int> { 3, 3, 3, 0, 0 }),
                new Card(0, -1, -1, 3, new List<int> { 0, 3, 4, 0, 3 }),
                new Card(0, -1, -1, 3, new List<int> { 0, 3, 3, 4, 0 })
            };
            this.Tokens = new List<int>() { 4, 4, 4, 4, 4, 4}; //black, red, green, blue, white, joker
        }

        public void ShuffleDeck()
        {
            for (int i = 0; i < Deck.Count; i++)
            {
                int n = Deck[i].Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    Card value = Deck[i][k];
                    Deck[i][k] = Deck[i][n];
                    Deck[i][n] = value;
                }
            }
        }

        public void InitDrawCards()
        {
            ShuffleDeck();
            for (int i = 0; i < Deck.Count; i++)
            {
                this.ForSale[i].Add(Deck[i][Deck[i].Count - 1]);
                Deck[i].RemoveAt(Deck[i].Count - 1);
                this.ForSale[i].Add(Deck[i][Deck[i].Count - 1]);
                Deck[i].RemoveAt(Deck[i].Count - 1);
                this.ForSale[i].Add(Deck[i][Deck[i].Count - 1]);
                Deck[i].RemoveAt(Deck[i].Count - 1);
            }
        }

        public void RemoveCard(Card card)
        {
            for (int i = 0; i < ForSale[card.tier].Count; i++)
            {
                if (ForSale[card.tier][i].id == card.id)
                {
                    ForSale[card.tier][i] = Deck[card.tier][Deck[card.tier].Count-1];
                    Deck[card.tier].RemoveAt(Deck[card.tier].Count - 1);
                }
            }
        }
    }

    public class Controller
    {
        public Player Player1;
        public Player Player2;
        public Market market;
        public int ScoreGoal;
        public Player CurPlayer;
        public bool ChoosingTokens;

        public Controller(int goal)
        {
            this.ScoreGoal = goal;
            this.Player1 = new Player(1);
            this.Player2 = new Player(2);
            this.market = new Market();
            this.CurPlayer = Player1;
            this.ChoosingTokens = false;
        }

        public void StartGame()
        {
            this.market.InitDrawCards();
            StartTurn();
        }

        public void StartTurn()
        {
            List<int> chosenTokens = new List<int>() { 0, 0, 0, 0, 0 };
            ChoosingTokens = true;
            while (true)
            {
                foreach (int tokens in chosenTokens)
                {
                    if (tokens > 1) break;
                }
                if (chosenTokens.Aggregate(0, (acc, x) => acc+x) > 2) break; // effectively Reduce
            }
            ChoosingTokens = false;
        }

        public void EndTurn()
        {
            if (CurPlayer == Player1) CurPlayer = Player2;
            else CurPlayer = Player1;
            StartTurn();
        }

        public bool CheckPrice(Card card)
        {
            int difference = 0;
            for (int i = 0; i < CurPlayer.Cards.Count; i++)
            {
                difference += Math.Min(card.Cost[i] - CurPlayer.Cards[i] - CurPlayer.Tokens[i], 0);
            }
            if (difference >= CurPlayer.Tokens[5])
            {
                return true;
            }
            return false;
        }

        public void BuyCard(Card card)
        {
            for (int i = 0; i < CurPlayer.Cards.Count; i++)
            {
                CurPlayer.Tokens[i] -= Math.Max(card.Cost[i] - CurPlayer.Cards[i], 0);
                if (CurPlayer.Tokens[i] < 0)
                {
                    CurPlayer.Tokens[5] += CurPlayer.Tokens[i];
                    CurPlayer.Tokens[i] = 0;
                }
            }
            CurPlayer.Cards[card.type] += 1;
            CurPlayer.Score += card.Score;
            this.market.RemoveCard(card);

            if (!CheckVictory()) EndTurn();
        }

        public void HeldCard(Player curPlayer, Card card)
        {
            if (curPlayer.HeldCards.Count < 3)
            {
                curPlayer.HeldCards.Add(card);
                this.market.RemoveCard(card);
            }
            EndTurn();
        }

        public bool CheckVictory()
        {
            return CurPlayer.Score >= ScoreGoal;
        }
    }
}