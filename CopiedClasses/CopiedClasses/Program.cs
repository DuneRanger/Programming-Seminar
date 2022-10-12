using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopiedClasses
{
    class Vlada
    {
        public string zeme;
        public Premier premier;
        public List<Ministr> ministri;

        bool ZkusAnektovat(string co)
        {
            if (co == "Slovensko")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Premier
    {
        public string jmeno;
        public Vlada vlada;

        public string SilneProhlaseni()
        {
            return "Premier " + jmeno + " říká: Nevim o cem to mluvite.";
        }
    }

    class Ministr
    {
        public string jmeno;
        public Premier premier;

        public string SlabeProhlaseni()
        {
            return jmeno + " říká: Ja nevim co bych na to rekl, ale pan premier by na to rekl: " + premier.SilneProhlaseni();
        }

        private void Demise()
        {
            int index = -1;
            for (int i = 0; i < premier.vlada.ministri.Count; i++)
            {
                if (premier.vlada.ministri[i].jmeno == jmeno)
                {
                    index = i;
                }
            }
            if (index != -1)
            {
                premier.vlada.ministri.RemoveAt(index);
            }
        }
    }

    class Hacker
    {
        public void InfiltrateGov(Vlada Gov)
        {
            Gov.premier = new Premier();
            Gov.premier.jmeno = "Ondrej";
            Gov.premier.vlada = Gov;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Vlada Government = new Vlada();
            Premier PrimeMinister = new Premier();
            List<Ministr> Ministers = new List<Ministr>() {};

            Government.zeme = "Česko";
            Government.premier = PrimeMinister;
            Government.ministri = Ministers;

            PrimeMinister.jmeno = "Ondřej";
            PrimeMinister.vlada = Government;

            int ministerAmount = 10;

            for (int i = 0; i < ministerAmount; i++)
            {
                Ministers.Add(new Ministr());
                Ministers[i].jmeno = "Minister_" + i.ToString();
                Ministers[i].premier = PrimeMinister;
            }

            Console.WriteLine(Government.premier.SilneProhlaseni());
            Console.WriteLine(Government.ministri[0].SlabeProhlaseni());

            Console.Read();
        }
    }
}

