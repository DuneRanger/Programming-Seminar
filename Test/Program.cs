using System.Diagnostics.Metrics;

namespace Classes
{
    public class Government
    {
        public PrimeMinister PrMin;
        public List<Minister> Ministers;
        public string Land;

        public Government(string land)
        {
            Land = land;
        }
        public void SetPrimeMinister(PrimeMinister prmin)
        {
            PrMin = prmin;
        }
        public void SetMinisters(List<Minister> ministers)
        {
            Ministers = ministers;
        }

        public void PrintMinisters()
        {
            foreach (var min in Ministers)
            {
                Console.WriteLine(min.Name);
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
            return "The current government is the best in the history of this land!!";
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
            int counter = 0;
            foreach (var min in PrMin.Gov.Ministers)
            {
                if (min.Name == Name)
                {
                    PrMin.Gov.Ministers.RemoveAt(counter);
                }
                counter++;
            }
        }

        public string WeakAnnouncement()
        {
            return "I don't know, but the Prime Minister would say: " + PrMin.StrongAnnouncement();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");




            Government TestGov = new Government("Czechia");

            TestGov.SetPrimeMinister(new PrimeMinister("PrimeMime", TestGov));

            List<Minister> ministers = new List<Minister>();

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