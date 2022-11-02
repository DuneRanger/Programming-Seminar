namespace Binary_Tree
{
    class TreeNode
    {
        public int value { get; set; }
        public TreeNode left { get; set; }
        public TreeNode right { get; set; }

        public TreeNode(int val = 100)
        {
            value = val;
        }
    }
    class BinaryTree
    {
        public TreeNode Root;
        
        public BinaryTree(TreeNode start)
        {
            Root = start;
        }

        public void addVal(int val)
        {
            TreeNode currentNode = Root;

            while (currentNode.right != null && currentNode.left != null)
            {
                if (currentNode.value == val) return;
                if (val > currentNode.right.value)
                {
                    currentNode = currentNode.right;
                }
                else
                {
                    currentNode = currentNode.left;
                }
            }
            if (currentNode.left != null)
            {
                if (val < currentNode.left.value)
                {
                    currentNode.right = currentNode.left;
                    currentNode.left = new TreeNode(val);
                }
                else
                {
                    currentNode.right = new TreeNode(val);
                }
            }
            else if (currentNode.right != null)
            {
                if (val > currentNode.right.value)
                {
                    currentNode.left = currentNode.right;
                    currentNode.right = new TreeNode(val);
                }
                else
                {
                    currentNode.left = new TreeNode(val);
                }
            }
            else
            {
                if (val > currentNode.value)
                {
                    currentNode.right = new TreeNode(val);
                }
                else
                {
                    currentNode.left = new TreeNode(val);
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