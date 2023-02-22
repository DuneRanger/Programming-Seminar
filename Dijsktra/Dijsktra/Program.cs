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
        public enum state {open, visited, closed}
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
            List<Vertex> toVisit = new List<Vertex>();

            graph.vertices[startID].pathCost = 0;
            toVisit.Add(graph.vertices[startID]);

            while (toVisit.Count != 0)
            {
                int smallestPathCost = Int32.MaxValue;
                Vertex curV = new Vertex();
                for (int i = 0; i < toVisit.Count; i++)
                {
                    if (toVisit[i].pathCost < smallestPathCost)
                    {
                        smallestPathCost = toVisit[i].pathCost;
                        curV = toVisit[i];
                    }
                }

                for (int i = 0; i < curV.connections.Count; i++)
                {
                    if (curV.connections[i].curState != Vertex.state.closed)
                    {
                        if (curV.connections[i].pathCost > curV.pathCost + curV.connectionsCosts[i])
                        {
                            curV.connections[i].pathCost = curV.pathCost + curV.connectionsCosts[i];
                        }
                        if (curV.connections[i].curState != Vertex.state.visited)
                        {
                            toVisit.Add(curV.connections[i]);
                            curV.connections[i].curState = Vertex.state.visited;
                        }
                    }
                }
                curV.curState = Vertex.state.closed;
                toVisit.Remove(curV);
            }

            Vertex endV = graph.vertices[endID];

            if (endV.pathCost == Int32.MaxValue)
            {
                endV.pathCost = -1;
            }

            return endV.pathCost;
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
