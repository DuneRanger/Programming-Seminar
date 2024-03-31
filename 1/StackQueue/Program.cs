namespace StackQueue
{
    class Node
    {
        public int value { get; set; }
        public Node next { get; set; }
        public Node before { get; set; }
        public Node(int val)
        {
            value = val;
            next = null;
            before = null;
        }
    }

    class LinkedList
    {
        public Node startNode;
        public Node lastNode;
        public int length;

        public void Add(int newValue)
        {
            Node temp = new Node(newValue);
            if (this.Empty())
            {
                startNode = temp;
                lastNode = temp;
            }
            else
            {
                temp.before = lastNode;
                lastNode.next = temp;
                lastNode = temp;
            }
            length++;
        }

        public bool Empty()
        {
            if (length == 0) return true;
            return false;
        }

        public void WriteList()
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

    class Stack : LinkedList
    {
        public int First()
        {
            return lastNode.value;
        }

        public void Pop()
        {
            lastNode = lastNode.before;
//            lastNode.next = null;
            length--;
        }
    }

    class Queue : LinkedList
    {
        public int First()
        {
            return startNode.value;
        }

        public void Shift()
        {
            startNode = startNode.next;
            startNode.before = null;
            length--;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            Stack S = new Stack();
            Console.WriteLine("Is the stack empty? " + S.Empty());
            S.Add(1);
            S.Add(2);
            Console.WriteLine(S.First() + " is on top of the stack");
            S.Pop();
            S.Add(8);
            S.WriteList();
            S.Pop();
            S.Pop();
            Console.WriteLine("Is the stack empty? " + S.Empty());

            Queue Q = new Queue();
            Console.WriteLine("Is the queue empty? " + Q.Empty());
            Q.Add(1);
            Q.Add(2);
            Console.WriteLine(Q.First() + " is on top of the queue");
            Q.Shift();
            Q.Add(8);
            Q.WriteList();
            Q.Shift();
            Q.Shift();
            Console.WriteLine("Is the queue empty? " + Q.Empty());


            
            Console.ReadLine();
        }
    }
}