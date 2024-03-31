using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijsktra_Heap
{
    class Vertex
    {
        public List<Vertex> connections;
        public List<int> connectionsCosts;
        public int pathCost;
        public enum state { open, visited, closed }
        public state curState;
        public int ID;
        public Vertex(int id = -1)
        {
            connections = new List<Vertex>();
            connectionsCosts = new List<int>();
            pathCost = Int32.MaxValue;
            curState = Vertex.state.open;
            ID = id;
        }
    }

    class Graph
    {
        public List<Vertex> vertices;

        public Graph()
        {
            vertices = new List<Vertex>();
        }

        public static int Dijsktra(Graph graph, int startID, int endID)
        {
            Heap toVisit = new Heap();

            graph.vertices[startID].pathCost = 0;
            toVisit.Add(graph.vertices[startID]);

            while (!toVisit.Empty)
            {
                Vertex curV = toVisit.Pop();

                for (int i = 0; i < curV.connections.Count; i++)
                {
                    if (curV.connections[i].curState != Vertex.state.closed)
                    {
                        if (curV.connections[i].curState != Vertex.state.visited)
                        {
                            toVisit.Add(curV.connections[i]);
                            curV.connections[i].curState = Vertex.state.visited;
                        }
                        if (curV.connections[i].pathCost > curV.pathCost + curV.connectionsCosts[i])
                        {
                            curV.connections[i].pathCost = curV.pathCost + curV.connectionsCosts[i];
                            toVisit.Update(curV.connections[i]);
                        }
                    }
                }
                curV.curState = Vertex.state.closed;
            }

            Vertex endV = graph.vertices[endID];

            if (endV.pathCost == Int32.MaxValue)
            {
                endV.pathCost = -1;
            }

            return endV.pathCost;
        }

    }

    class Heap
    {
        public List<Vertex> list = new List<Vertex>();
        public Dictionary<Vertex, int> indices = new Dictionary<Vertex, int>();

        /// <summary>
        /// Returns if heap is empty.
        /// </summary>
        public bool Empty
        {
            get
            {
                return list.Count == 0;
            }
        }
        /// <summary>
        /// Returns number of elements in heap.
        /// </summary>
        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public Vertex Peek()
        {
            return list[0];
        }

        public void Add(Vertex v)
        {
            list.Add(v);
            indices.Add(v, list.Count - 1);

            BubbleUp(list.Count - 1);
        }


        public Vertex Pop()
        {
            Vertex t = list[0];

            Swap(0, list.Count - 1);
            list.RemoveAt(list.Count - 1);
            indices.Remove(t);

            BubbleDown(0);

            return t;
        }



        public void Update(Vertex v)
        {
            int i = indices[v];

            Bubble(i);
        }

        private void Bubble(int i)
        {
            if (i != 0 && list[Father(i)].pathCost > list[i].pathCost)
            {
                BubbleUp(i);
            }
            else
            {
                BubbleDown(i);
            }
        }

        private void BubbleUp(int v)
        {
            while (v != 0 && list[Father(v)].pathCost > list[v].pathCost)
            {
                Swap(Father(v), v);
                v = Father(v);
            }
        }

        private void BubbleDown(int v)
        {
            while (2 * v + 1 < list.Count && list[SmallerSon(v)].pathCost < list[v].pathCost)
            {
                Swap(SmallerSon(v), v);
                v = SmallerSon(v);
            }
        }

        private void Swap(int v1, int v2)
        {
            Vertex x = list[v1];
            list[v1] = list[v2];
            list[v2] = x;

            indices[list[v1]] = v1;
            indices[list[v2]] = v2;
        }


        private int Father(int v)
        {
            return (v - 1) / 2;
        }

        /// <summary>
        /// Check if how moany sons exist and return the one with the smallest pathCost
        /// </summary>
        private int SmallerSon(int v)
        {
            int smallerSonIndex = 2 * v + 1;
            if (2*v+2 < list.Count)
            {
                if (list[2*v+1].pathCost > list[2*v+2].pathCost)
                {
                    smallerSonIndex++;
                }
            }
            return smallerSonIndex;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            int nodeNum, edgeNum;

            string[] input = Console.ReadLine().Split();
            Int32.TryParse(input[0], out nodeNum);
            Int32.TryParse(input[1], out edgeNum);

            Graph graph = new Graph();

            // Initializes a list for each vertex ID
            for (int i = 0; i < nodeNum; i++)
            {
                graph.vertices.Add(new Vertex(i));
            }


            // Adds the new connection to each vertex ID
            for (int i = 0; i < edgeNum; i++)
            {
                input = Console.ReadLine().Split();
                int a = Int32.Parse(input[0]);
                int b = Int32.Parse(input[1]);
                int cost = Int32.Parse(input[2]);

                // Guarantees that the index of the cost is equal to the index of the vertex it leads to
                graph.vertices[a].connections.Add(graph.vertices[b]);
                graph.vertices[a].connectionsCosts.Add(cost);

                graph.vertices[b].connections.Add(graph.vertices[a]);
                graph.vertices[b].connectionsCosts.Add(cost);
            }



            int startID, endID;
            input = Console.ReadLine().Split();
            Int32.TryParse(input[0], out startID);
            Int32.TryParse(input[1], out endID);

            Console.WriteLine(Graph.Dijsktra(graph, startID, endID));


            Console.ReadLine();
        }
    }
}
