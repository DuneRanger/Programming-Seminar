namespace GraphQueue
{
    class Vertex
    {
        public List<Vertex> connections;
        public int depth;
        public List<int> path;
        public int ID;
        public Vertex()
        {
            connections= new List<Vertex>();
            path= new List<int>();
            depth= Int32.MaxValue;
            ID = -1;
        }
    }

    class Node
    {
        public Vertex value;
        public Node next;
        public Node before;
        public Node(Vertex v)
        {
            value = v;
        }
    }

    class Graph
    {
        public List<Vertex> vertices;

        public Graph()
        {
            vertices = new List<Vertex>();
        }

        public List<int> Search(int startID, int endID)
        {
            Queue Q = new Queue();

            this.vertices[startID].path.Add(startID);
            this.vertices[startID].depth = 0;
            Q.Add(this.vertices[startID]);

            while (!Q.Empty())
            {
                if (Q.First().ID == endID)
                {
                    return Q.First().path;
                }
                for (int i = 0; i < Q.First().connections.Count; i++)
                {
                    if (Q.First().connections[i].depth > Q.First().depth+1)
                    {
                        Q.First().connections[i].path.AddRange(Q.First().path);
                        Q.First().connections[i].path.Add(Q.First().connections[i].ID);
                        Q.First().connections[i].depth = Q.First().depth+1;
                        Q.Add(Q.First().connections[i]);
                    }
                }
                Q.Shift();
            }
            return new List<int> {-1};
        }
    }

    class Queue
    {
        public Node startNode;
        public Node lastNode;
        public int length;

        public Vertex First()
        {
            return startNode.value;
        }

        public void Shift()
        {
            startNode = startNode.next;
            startNode.before = null;
            length--;
        }

        public void Add(Vertex newValue)
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
    }


    class Program
    {
        static void Main(string[] args)
        {

            int nodeNum, edgeNum; 
            Int32.TryParse(Console.ReadLine(), out nodeNum);
            Int32.TryParse(Console.ReadLine(), out edgeNum);
            
            List<List<int>> edges = new List<List<int>>();

            // Initializes a list for each vertex ID
            for (int i = 0; i < nodeNum; i++)
            {
                edges.Add(new List<int>());
            }

            // Adds the new connection to each vertex ID
            for (int i = 0; i < edgeNum; i++)
            {
                string[] tokens = Console.ReadLine().Split();
                int a = Int32.Parse(tokens[0]);
                int b = Int32.Parse(tokens[1]);

                edges[a].Add(b);
                edges[b].Add(a);
            }

            Graph graph = new Graph();

            // Initializes each vertex
            for (int i = 0; i < nodeNum; i++)
            {
                graph.vertices.Add(new Vertex());
                graph.vertices[i].ID = i;
            }

            // Adds the saved connections to the vertex
            for (int i = 0; i < nodeNum; i++)
            {
                for (int j = 0; j < edges[i].Count; j++)
                {
                    graph.vertices[i].connections.Add(graph.vertices[edges[i][j]]);
                }
            }

            int startID, endID;
            Int32.TryParse(Console.ReadLine(), out startID);
            Int32.TryParse(Console.ReadLine(), out endID);

            List<int> path = graph.Search(startID, endID);

            for (int i = 0; i < path.Count; i++)
            {
                Console.Write(path[i] + " ");
            }

            Console.ReadLine();
        }
    }
}