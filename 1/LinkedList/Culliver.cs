using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        static void pushNode(Node start, int addedValue)
        {
            Node lastNode = start;
            while (lastNode.next != null)
            {
                lastNode = lastNode.next;
            }
            lastNode.next = new Node(addedValue);
            return;
        }
        static void Main(string[] args)
        {
            Node firstNode = new Node(0);
            firstNode.next = new Node(1);
            pushNode(firstNode, 2);
            pushNode(firstNode, 3);
            pushNode(firstNode, 4);
            pushNode(firstNode, 9);
            pushNode(firstNode, 5);

            Node currentNode = firstNode;
            while (currentNode.next != null)
            {
                Console.WriteLine(currentNode.value);
                currentNode = currentNode.next;
            }
            Console.WriteLine(currentNode.value);

            Console.ReadLine();
        }
    }

    //class LinkedList
    //{
    //    public Node start;

    //    private Node getLast()
    //    {
    //        Node lastNode = start;
    //        while (lastNode.next != null)
    //        {
    //            lastNode = lastNode.next;
    //        }
    //        return lastNode;
    //    }

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

    class Node
    {
        public int value;
        public Node next;
        public Node(int val)
        {
            value = val;
            next = null;
        }
    }
}