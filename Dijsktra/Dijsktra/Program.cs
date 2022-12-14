using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijsktra
{
    class Vertex
    {
        public List<Vertex> connections;
        public List<int> connectionsCosts;
        public int pathCost;
        public string path;
        public int ID;
        public Vertex(int id = -1)
        {
            connections = new List<Vertex>();
            connectionsCosts = new List<int>();
            pathCost = 0;
            path = "";
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

        public static string Dijsktra(Graph graph, int startID, int endID)
        {
            List<Vertex> open = new List<Vertex>();
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                if (graph.vertices[i].ID == startID) open.Add(graph.vertices[i]);
            }

            while (open.Count != 0)
            {
                int smallestPathCost = Int32.MaxValue;
                Vertex curV = new Vertex();
                for (int i = 0; i < open.Count; i++)
                {
                    if (open[i].pathCost < smallestPathCost)
                    {
                        smallestPathCost = open[i].pathCost;
                        curV = open[i];
                    }
                }
                curV.path = $"";

                for (int i = 0; i < curV.connections.Count; i++)
                {
                    curV.connections[i].pathCost = curV.pathCost + curV.connectionsCosts[i];
                    open.Add(curV.connections[i]);
                }
                open.Remove(curV);
            }

            Vertex endV = new Vertex();
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                if (graph.vertices[i].ID == endID) endV = graph.vertices[i];
            }

            Queue<Vertex> pathQ = new Queue<Vertex>();
            pathQ.Enqueue(endV);


            return "";
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
            Graph graph = new Graph();

            // Initializes a list for each vertex ID
            for (int i = 0; i < nodeNum; i++)
            {
                graph.vertices.Add(new Vertex(i));
            }


            // Adds the new connection to each vertex ID
            for (int i = 0; i < edgeNum; i++)
            {
                string[] tokens = Console.ReadLine().Split();
                int a = Int32.Parse(tokens[0]);
                int b = Int32.Parse(tokens[1]);
                int cost = Int32.Parse(tokens[2]);

                // Guarantees that the index of the cost is equal to the index of the vertex it leads to
                graph.vertices[a].connections.Add(graph.vertices[b]);
                graph.vertices[a].connectionsCosts.Add(cost);

                graph.vertices[b].connections.Add(graph.vertices[a]);
                graph.vertices[b].connectionsCosts.Add(cost);
            }



            int startID, endID;
            Int32.TryParse(Console.ReadLine(), out startID);
            Int32.TryParse(Console.ReadLine(), out endID);

            Graph.Dijsktra(graph, startID, endID);


            Console.ReadLine();
        }
    }
}
