namespace Trie
{
    public class Node
    {
        public char symb { get; private set; }
        public bool isWord { get; set; }
        public int usedIn { get; set; }
        public Dictionary<char, Node> children;

        public Node(char symb)
        {
            this.symb = symb;
            this.isWord = false;
            this.usedIn = 0;
            this.children= new Dictionary<char, Node>();
        }
    }
    public class Trie
    {
        public Node Root { get; private set; }

        public Trie()
        {
            Root = new Node(' ');
        }

        public void Insert(string word)
        {
            Node currentNode = Root;
            currentNode.usedIn++;
            
            foreach (char c in word)
            {

                if (!currentNode.children.ContainsKey(c))
                {
                    Node newNode = new Node(c);
                    currentNode.children.Add(c, newNode);
                    currentNode = newNode;
                }
                else
                {
                    currentNode = currentNode.children[c];
                }
                currentNode.usedIn++;
            }
            currentNode.isWord= true;
        }

        public bool Contains(string word)
        {
            Node currentNode = Root;

            foreach (char c in word)
            {

                if (!currentNode.children.ContainsKey(c))
                {
                    return false;
                }
                else
                {
                    currentNode = currentNode.children[c];
                }
            }
            return currentNode.isWord;
        }

        public void Remove(string word)
        {
            if (!this.Contains(word)) return;

            Node currentNode = Root;
            currentNode.usedIn--;

            foreach (char c in word)
            {
                Node oldNode = currentNode;
                currentNode = currentNode.children[c];

                currentNode.usedIn--;
                if (currentNode.usedIn == 0)
                {
                    oldNode.children.Remove('c');
                    return;
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}