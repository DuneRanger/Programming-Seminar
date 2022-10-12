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
        public string SilneProhlaseni()
        {
            return "Nevim o cem to mluvite.";
        }
        public Vlada vlada;
    }

    class Ministr
    {
        public string jmeno;
        public string SlabeProhlaseni()
        {
            return "Ja nevim co bych na to rekl, ale pan premier by na to rekl: " + premier.SilneProhlaseni();
        }
        public Premier premier;
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

    class Program
    {
        static void Main(string[] args)
        {
            Vlada Government = new Vlada();
            Premier PrimeMinister = new Premier();
            List<Ministr> Ministers = new List<Ministr>() { new Ministr(), new Ministr(), new Ministr(), new Ministr(), new Ministr(), new Ministr() };

            Government.zeme = "Česko";
            Government.premier = PrimeMinister;
            Government.ministri = Ministers;
            PrimeMinister.jmeno = "Ondřej";
            PrimeMinister.vlada = Government;

            for (int i = 0; i < Ministers.Count; i++)
            {
                Ministers[i] = new Ministr();
                Ministers[i].jmeno = "guy" + i.ToString();
                Ministers[i].premier = PrimeMinister;
            }

        }
    }
}

