using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merge_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            int SIZE = 8;
            Console.WriteLine("Please enter the size of your array:");
            SIZE = int.Parse(Console.ReadLine());

            int[] arr = new int[SIZE];
            for (int x = 0; x < SIZE; x++)
            {
                arr[x] = rnd.Next(0, SIZE);
            }
            Console.WriteLine("Your array is:\n[{0}]", string.Join(", ", arr));
            Console.WriteLine("Press a key to start sorting");
            Console.Read();
            Sort(arr, 0, arr.Length-1);

            Console.WriteLine("\n\n\nSorted array:\n[{0}]\n\n\n", string.Join(", ", arr));
            Console.Read();
        }

        static void Merge(int[] Unsorted, int left, int middle, int right)
        {
            //Console.WriteLine("Merge: " + left + " " + middle + " " + right);

            int LSize = middle - left + 1;
            int RSize = right - middle;

            // Sub arrays with their respective sizes
            int[] L = new int[LSize]; // +1 to include left (or middle, depending on your thought process)
            int[] R = new int[RSize];

            for (int i = 0; i < LSize; i++)
            {
                L[i] = Unsorted[left + i];
            }

            for (int i = 0; i < RSize; i++)
            {   // middle + 1 because array L already includes middle
                R[i] = Unsorted[middle + 1 + i];
            }

            Console.WriteLine("Sorting: [{0}, {1}]", string.Join(", ", L), string.Join(", ", R));

            // Index of each array to be compared
            int LI = 0;
            int RI = 0;

            int OgI = left; // Index of where the lower number will be in the original array

            // Until we go through one of the arrays...
            while (LI < LSize && RI < RSize)
            {  
                if (L[LI] < R[RI])
                {
                    // Since arrays are parse-by-reference, this changes the original array 
                    Unsorted[OgI] = L[LI];
                    LI++;
                }
                else
                {
                    Unsorted[OgI] = R[RI];
                    RI++;
                }
                OgI++;
            }

            if (LI < LSize)
            {
                for (; LI < LSize; LI++, OgI++)
                {
                    Unsorted[OgI] = L[LI];
                }
            } else
            {
                for (; RI < RSize; RI++, OgI++)
                {
                    Unsorted[OgI] = R[RI];
                }
            }
        }

        static void Sort(int[] Unsorted, int left, int right)
        {
            if (left < right)
            {
                // int automatically floors the result
                int middle = left + (right - left) / 2; // left index + half the length
                // Console.WriteLine("Sort: " + left + " " + middle + " " + right);
                Sort(Unsorted, left, middle);
                Sort(Unsorted, middle + 1, right);

                Merge(Unsorted, left, middle, right);
            }
            //else { Console.WriteLine("End: " + left + " " + right); }
            return;
        }
    }
}