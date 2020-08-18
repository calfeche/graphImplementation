using System;
using System.Collections.Generic;

namespace AlfecheExer3
{
    class Vertex
    {
        public String name;
        public int state;

        public Vertex(String name)
        {
            this.name = name;

        }
    }

    class Stack
    {
        public Array stack;
        int pointer;

    }

    class Queue
    {
        public Queue queue;
        int pointer;
    }

    class DirectedGraph
    {
        public readonly int MAX_VERTICES = 30;

        int n; //no. of vertices in the graph
        int e; //no. of edges in the graph
        bool[,] adj; //2-d array representing the adjacency matrix of the graph
        Vertex[] vertexList; //stores the vertices of the graph

        //constants for different states of the vertex
        private readonly int INITIAL = 0;
        private readonly int WAITING = 1;
        private readonly int VISITED = 2;

        //initiation of the directed graph
        public DirectedGraph()
        {
            adj = new bool[MAX_VERTICES, MAX_VERTICES];
            vertexList = new Vertex[MAX_VERTICES];
        }

        public int Vertices()
        {
            return n; //returns the total no. of actual vertices in the graph
        }

        public int Edges()
        {
            return e; //returns the total no. of actual edges in the graph
        }

        public void Display() //prints the adjacency maytrix
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if (adj[i, j]) //if true, prints 1
                        Console.Write("1, ");
                    else
                        Console.Write("0, ");
                Console.WriteLine();
            }
        }

        public void InsertVertex(String name)
        {
            vertexList[n++] = new Vertex(name); //creates a new object and is stored inside the array
        }

        private int GetIndex(String s)
        //takes a name of a vertex and return the index of the vertex
        //helper method in order to implement the edges of the adjacency matrix
        {
            for (int i = 0; i < n; i++)
                if (s.Equals(vertexList[i].name))
                    return i;
            throw new System.InvalidOperationException("Invalid Vertex");  //if not found inside the list
        }

        private bool IsAdjacent(int u, int v)
        {
            //helper method to check neighbor vertices.
            return adj[u, v];
        }

        public bool EdgeExist(String s1, String s2)
        {
            //helper method to check extisnce of edge
            return IsAdjacent(GetIndex(s1), GetIndex(s2));
        }

        public void InsertEdge(String s1, String s2)
        {
            int u = GetIndex(s1);
            int v = GetIndex(s2);

            if (u == v)
                throw new System.InvalidOperationException("Not a valid edge");
            if (adj[u, v] == true)
                Console.Write("Edge already present");
            else
            {
                adj[u, v] = true;
                e++;
            }
        }

        public void BfsTraversal() //traverses the graph only reachable by the initial vertex
        {
            for (int v = 0; v < n; v++)
                vertexList[v].state = INITIAL;
            Console.Write("Enter the starting vertex for the Breadth First Search : ");
            String s = Console.ReadLine();
            Bfs(GetIndex(s));
        }

        private void Bfs(int v)
        {
            Queue<int> qu = new Queue<int>();
            qu.Enqueue(v);
            vertexList[v].state = WAITING;

            while (qu.Count != 0)
            {
                v = qu.Dequeue();
                Console.Write(vertexList[v].name + " ");
                vertexList[v].state = VISITED;

                for (int i = 0; i < n; i++)
                {
                    if (IsAdjacent(v, i) && vertexList[i].state == INITIAL)
                    {
                        qu.Enqueue(i); //adds the adjacent vertices that are in initial state for visitation
                        vertexList[i].state = WAITING;
                    }
                }
            }
            Console.Write("");
        }

        public void BfsTraversal_All(String s)
        {
            //traverses the entrire graph even those unreachable by the initial vertex
            int v;
            for (v = 0; v < n; v++)
                vertexList[v].state = INITIAL;

            Bfs(GetIndex(s));

            for (v = 0; v < n; v++)
                if (vertexList[v].state == INITIAL)
                    Bfs(v);
        }


        public void DfsTraversal()
        {
            for (int v = 0; v < n; v++)
                vertexList[v].state = INITIAL;

            Console.Write("Enter starting vertex for Depth First Search: ");
            String s = Console.ReadLine();
            Dfs(GetIndex(s));
        }

        private void Dfs(int v)
        {
            Stack<int> st = new Stack<int>();
            st.Push(v);

            while (st.Count != 0)
            {
                v = st.Pop();

                if (vertexList[v].state == INITIAL)
                {
                    Console.Write(vertexList[v].name + " ");
                    vertexList[v].state = VISITED;
                }

                for (int i = n - 1; i >= 0; i--)
                {
                    if (IsAdjacent(v, i) && vertexList[i].state == INITIAL)
                        st.Push(i);
                }
            }
            Console.Write("");
        }

        public void DfsTraversal_All(String s)
        {
            int v;
            for (v = 0; v < n; v++)
                vertexList[v].state = INITIAL;

            Dfs(GetIndex(s));

            for (v = 0; v < n; v++)
                if (vertexList[v].state == INITIAL)
                    Dfs(v);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            #region Graph One (Int)
            DirectedGraph graphOne = new DirectedGraph();
            graphOne.InsertVertex("1");
            graphOne.InsertVertex("2");
            graphOne.InsertVertex("3");
            graphOne.InsertVertex("4");
            graphOne.InsertVertex("5");
            graphOne.InsertVertex("6");
            graphOne.InsertVertex("7");

            graphOne.InsertEdge("1", "2");
            graphOne.InsertEdge("1", "3");
            graphOne.InsertEdge("2", "4");
            graphOne.InsertEdge("3", "6");
            graphOne.InsertEdge("4", "5");
            graphOne.InsertEdge("4", "6");
            graphOne.InsertEdge("5", "2");
            graphOne.InsertEdge("6", "7");
            #endregion

            #region Graph Two (Strings)
            DirectedGraph graphTwo = new DirectedGraph();
            graphTwo.InsertVertex("A");
            graphTwo.InsertVertex("B");
            graphTwo.InsertVertex("C");
            graphTwo.InsertVertex("D");
            graphTwo.InsertVertex("E");

            graphTwo.InsertEdge("A", "B");
            graphTwo.InsertEdge("B", "E");
            graphTwo.InsertEdge("E", "C");
            graphTwo.InsertEdge("E", "D");
            #endregion

            string[] notebooks = new string[5];

            string[] menu = new string[6];
            menu[0] = "Graphs";
            menu[1] = "[1] Perform Depth First Search Traversal";
            menu[2] = "[2] Perform Breadth First Search Traversal";
            menu[3] = "[3] Search Graph 1";
            menu[4] = "[4] Search Graph 2";
            menu[5] = "[5] Exit";
            foreach (string i in menu)
            {
                Console.WriteLine(i);
            }

            int menu_choice;
            int graph_choice;
            do
            {
                Console.Write("\nSelect an action: ");
                menu_choice = Convert.ToInt32(Console.ReadLine());
                switch (menu_choice)
                {
                    case 1:
                        Console.Write("Choose a graph to perform a DFS with: ");
                        graph_choice = Convert.ToInt32(Console.ReadLine());
                        if(graph_choice == 1)
                        {
                            graphOne.DfsTraversal_All("1");
                        }
                        if (graph_choice == 2)
                        {
                            graphTwo.DfsTraversal_All("A");
                        }
                        break;
                        
                    case 2:
                        Console.Write("Choose a graph to perform a BFS with: ");
                        graph_choice = Convert.ToInt32(Console.ReadLine());
                        if (graph_choice == 1)
                        {
                            graphOne.BfsTraversal_All("1");
                        }
                        if (graph_choice == 2)
                        {
                            graphTwo.BfsTraversal_All("A");
                        }
                        break;
                    case 3: //DFS on graph 1
                        Console.WriteLine("\nYou're about to perform a DFS on graph 1");
                        graphOne.DfsTraversal();
                        break;
                    case 4: //BFS on graph 2
                        Console.WriteLine("\nYou're about to perform a BFS on graph 2");
                        graphTwo.BfsTraversal();
                        break;
                    case 5:
                        break;
                   
                }

            }
            while (menu_choice != 5 || menu_choice > 5);



        }
    }
}
