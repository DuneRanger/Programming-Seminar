namespace StackQueue
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
        private Node lastNode { get; set; }
  

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

    class Stack
    {
        public Node bottom;
        public Node top;
        public Node secondLast;
        public int length;
        public void Add(int val)
        {
            if (length == 0)
            {
                Node temp = new Node(val);
                bottom = temp;
                top = temp;
                length++;
            }
            else
            {
                Node temp = new Node(val);
                top.next = temp;
                secondLast = top;
                top = temp;
                length++;
            }
        }

        public void Pop()
        {
            
        }

        public int First()
        {
            return top.value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }
    }
}
