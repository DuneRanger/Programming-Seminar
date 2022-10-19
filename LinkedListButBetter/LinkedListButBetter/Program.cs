using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListButBetter
{
    class Node
    {
        public int value { get; set; }
        public Node next { get; set; }
        public Node(int val)
        {
            value = val;
            next = null;
        }
    }

    class LinkedList
    {
        private Node startNode { get; set; }
        private Node lastNode
        {
            get
            {
                Node curNode = startNode;
                while (curNode.next != null)
                {
                    curNode = curNode.next;
                }
                return curNode;
            }
        }

        public LinkedList(Node initialNode)
        {
            startNode = initialNode;
        }

        public void addNodeToEnd(int newValue)
        {
            lastNode.next = new Node(newValue);
            return;
        }

        public void addNodeToStart(int newValue)
        {
            Node newStart = new Node(newValue);
            newStart.next = startNode;
            startNode = newStart;
            return;
        }

        public Node returnFirst()
        {
            return startNode;
        }

        public void removeFirst()
        {
            startNode = startNode.next;
        }

        public void writeList()
        {
            Node curNode = startNode;
            Console.Write("[");
            while (curNode.next != null)
            {
                Console.Write(curNode.value + ", ");
                curNode = curNode.next;
            }
            Console.Write(curNode.value + "]");
            return;
        }

        public void
        
    }

    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine();
        }
    }

    //    private void add(Node newNode, int depth = -1)
    //    {
    //        Node currentNode = start;
    //        if (depth == -1)
    //        {
    //            getLast().next = newNode;
    //            return;
    //        }
    //        while(depth > 0)
    //        {
    //            currentNode = currentNode.next;
    //            depth--;
    //        }
    //        Node nextNode = currentNode.next;
    //        currentNode.next = newNode;
    //        newNode.next = nextNode;
    //        return;
    //    }

    //    public LinkedList(Node a)
    //    {
    //        start = a;
    //    }
    //}
}
