using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hEAP
{
    class Heap
    {
        public List<int> tree;

        public Heap()
        {
            tree = new List<int>();
        }

        public void Swap(int ind1, int ind2)
        {
            int temp = this.tree[ind1];
            this.tree[ind1] = this.tree[ind2];
            this.tree[ind2] = temp;
        }

        public int returnMin()
        {
            if (tree.Count > 0)
            {
                return tree[0];
            }
            else
            {
                // Some error here or smth
                return -1;
            }
        }

        public int delMin()
        {
            return 0;
        }

        public void Add(int val)
        {

        }

        public void BubbleUp(int ind)
        {
            while (ind > 0)
            {
                if (this.tree[ind] < this.tree[Math.Floor(ind/2)])
                {
                    this.Swap(ind, Math.Floor(ind / 2));
                    ind = Math.Floor(ind / 2);
                }
                else
                {
                    break;
                }
            }
        }

}

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
