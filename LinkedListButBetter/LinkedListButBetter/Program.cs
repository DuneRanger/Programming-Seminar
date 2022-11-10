0using System;
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
        public int length
        {
            get
            {
                Node curNode = startNode;
                int index = 0;
                while (curNode.next != null)
                {
                    curNode = curNode.next;
                    index++;
                }
                return index+1;
            }
        }

        public LinkedList(Node initialNode)
        {
            startNode = initialNode;
        }

        public void addNodeToEnd(int newValue)
        {
            lastNode.next = new Node(newValue);
        }

        public void addNodeToStart(int newValue)
        {
            Node newStart = new Node(newValue);
            newStart.next = startNode;
            startNode = newStart;
        }

        public Node returnFirst()
        {
            return startNode;
        }

        public void removeFirst()
        {
            startNode = startNode.next;
        }


        public void addNodeDynamically(int newValue, int index)
        {
            if (index < length)
            {
                int curInd = 0;
                Node curNode = startNode;
                while(curInd < index-1)
                {
                    curNode = curNode.next;
                    curInd++;
                }
                Node newNode = new Node(newValue);
                newNode.next = curNode.next;
                curNode.next = newNode;
            }
        }

        public dynamic returnNodeDynamically(int index)
        {
            if (index < length)
            {
                int curInd = 0;
                Node curNode = startNode;
                while (curInd < index)
                {
                    curNode = curNode.next;
                    curInd++;
                }
                return curNode;
            }
            return null;
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
            Console.WriteLine();
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {
            LinkedList testing = new LinkedList(new Node(1));
            testing.addNodeToEnd(2);
            testing.addNodeToEnd(3);
            testing.addNodeToEnd(4);
            testing.addNodeToEnd(5);
            testing.writeList();

            testing.removeFirst();
            testing.writeList();

            testing.addNodeToStart(9);
            Console.WriteLine(testing.returnFirst().value.ToString());
            testing.writeList();

            testing.addNodeDynamically(8, 3);
            testing.writeList();

            Console.WriteLine(testing.returnNodeDynamically(3).value.ToString());

            Console.ReadLine();
        }
    }
}
