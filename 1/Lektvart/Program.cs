using System;
using System.Collections.Generic;

namespace Lektvary
{
    internal class Program
    {
        class Potion
        {
            public List<Potion> requiredFor = new List<Potion>();
            public int waitTime { get; set; }
            public int initialTime { get; set; }
            public int dependentOn;
        }
        static void Main(string[] args)
        {
            int potNum, depNum;
            var curLine = Console.ReadLine().Split(" ");
            potNum = Int32.Parse(curLine[0]);
            depNum = Int32.Parse(curLine[1]);

            List<Potion> Potions = new List<Potion>();

            curLine = Console.ReadLine().Split(" ");

            for (int i = 0; i < potNum; i++)
            {
                Potion temp = new Potion();
                temp.waitTime = 0;
                temp.initialTime = Int32.Parse(curLine[i]);
                Potions.Add(temp);
            }

            for (int i = 0; i < depNum; i++)
            {
                curLine = Console.ReadLine().Split(" ");
                int req = Int32.Parse(curLine[0]);
                int pot = Int32.Parse(curLine[1]);
                Potions[req].requiredFor.Add(Potions[pot]);
                Potions[pot].dependentOn += 1;
            }

            int totalTime = 0;

            Queue<Potion> potQ = new Queue<Potion>();

            for (int i = 0; i < potNum; i++)
            {
                if (Potions[i].dependentOn == 0) potQ.Enqueue(Potions[i]);
            }

            while (potQ.Count != 0)
            {
                Potion curPot = potQ.Dequeue();
                for (int i = 0; i < curPot.requiredFor.Count; i++)
                {
                    curPot.requiredFor[i].waitTime = Math.Max(curPot.requiredFor[i].waitTime, curPot.waitTime + curPot.initialTime);
                    curPot.requiredFor[i].dependentOn--;
                    if (curPot.requiredFor[i].dependentOn == 0) potQ.Enqueue(curPot.requiredFor[i]);
                }
            }

            for (int i = 0; i < Potions.Count; i++)
            {
                if (Potions[i].initialTime + Potions[i].waitTime > totalTime) totalTime = Potions[i].waitTime + Potions[i].initialTime;
            }

            Console.WriteLine(totalTime);

        }
    }
}