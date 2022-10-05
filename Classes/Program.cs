namespace Classes
{
    public class Government
    {
        public PrimeMinister PrMin;
        public Minister[] Ministers;
        string Land;

        public Governement(PrimeMinister prmin, string land)
        {
            Land = land;
            PrMin = prmin;
        }

        public void SetMinisters(Minister[] ministers)
        {
            Ministers = ministers
        }

        public void PrintMinisters(Minister[] ministers)
        {
            for (int i = 0; i < ministers.Length; i++)
            {
                Console.WriteLine(ministers[i].Name);
            }
        }

        private bool TryAnnex(string land)
        { //Returns wheter what we are tyring to Annex is Slovenia
            if (land == "Slovenia") return true;
            else return false;
        }

    }

    public class PrimeMinister
    {
        public string Name;
        public Government Gov;

        public PrimeMinister(string name, Government gov)
        {
            Name = name;
            Gov = gov;
        }

        public string StrongAnnouncement()
        {
            return "The current government is the best in the history of this land!!"
        }
    }

    public class Minister
    {
        public string Name;
        public PrimeMinister PrMin;

        public Minister(string name, PrimeMinister prmin)
        {
            Name = name;
            PrMin = prmin;
        }

        private void Resign()
        {
            for (int i = 0; i < PrMin.Gov.Ministers.Length; i++)
            {
                if (PrMin.Gov.Ministers[i].Name == Name)
                {
                    var foos = new List<Minister>(PrMin.Gov.Ministers);
                    foos.RemoveAt(i);
                    PrMin.Gov.Ministers = foos.ToArray();
                }
            }
        }

        public string WeakAnnouncement()
        {
            "I don't know, but the Prime Minister would say: " + PrMin.StrongAnnouncement();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");




            Government TestGov = new Government(new PrimeMinister("PrimeMime", Tester), "Czechia")

            Minister[] ministers;

            for (int i = 0; i < 10; i++)
            {
                ministers[i] = new Minister($"min_{i}", TestGov.PrMin);
            }

            TestGov.SetMinisters(ministers);

            TestGov.PrintMinisters();

            Console.Read();
        }
    }
}