using System.Collections;
using System.Collections.Generic;

namespace GenericLinkedList
{
    public class Node<T>
    {
        public static Node<T> EmptyNode = new Node<T>(default(T));
        public T Value { get; set; }
        public Node<T>? Before { get; set; }
        public Node<T>? Next { get; set; }

        public Node(T val, Node<T> before = null, Node<T> next = null)
        {
            this.Value = val;
            this.Before = before;
            this.Next = next;
        }
    }
    public class LinkedList<T> : IEnumerable
    {
        Node<T> Start = Node<T>.EmptyNode;
        Node<T> End = Node<T>.EmptyNode;
        int Size { get; set; }

        public LinkedList()
        {
            Start.Next = End;
            End.Before = Start;
            Size = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public LinkedListEnum<T> GetEnumerator()
        {
            return new LinkedListEnum<T>(this.Start, this.End, this.Size);
        }

        public void AddFront(T val)
        {
            Node<T> newNode = new Node<T>(val, Start, Start.Next);
            Start.Next.Before = newNode;
            Start.Next = newNode;
            Size++;
        }

        public void AddBack(T val)
        {
            Node<T> newNode = new Node<T>(val, End.Before, End);
            End.Before.Next = newNode;
            End.Before = newNode;
            Size++;
        }

        public T Front()
        {
            return Start.Next.Value;
        }

        public void DeleteFront()
        {
            if (Size == 0) return;
            Start.Next = Start.Next.Next;
            Start.Next.Before = Start;
            Size--;
        }

        public void Print()
        {
            Node<T> curNode = Start.Next;
            while(curNode != End)
            {
                Console.Write(curNode.Value.ToString() + " ");
                curNode = curNode.Next;
            }
            Console.Write("\n");
        }
    }
    
    public class LinkedListEnum<T> : IEnumerator
    {
        Node<T> Start = Node<T>.EmptyNode;
        Node<T> End = Node<T>.EmptyNode;
        Node<T> CurN = Node<T>.EmptyNode;
        int Size;
        int Position;


        public LinkedListEnum(Node<T> s, Node<T> e, int size)
        {
            this.Start = s;
            this.End = e;
            this.Size = size;
            this.Position = -1;
            this.CurN = this.Start;
        }

        //public object Current => CurN.Value;
        public object Current { get { return CurN.Value; } }

        public bool MoveNext()
        {
            if (CurN == End)
            {
                //throw error
            }
            this.CurN = this.CurN.Next;
            this.Position++;
            return this.Position < this.Size;
        }

        public void Reset()
        {
            this.CurN = this.Start;
            this.Position = -1;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>();

            Console.WriteLine("here");
            list.Print();
            list.AddFront(5);
            list.Print();
            list.AddBack(2);
            list.AddBack(10);
            list.Print();
            list.AddFront(6);
            list.Print();
        }
    }
}