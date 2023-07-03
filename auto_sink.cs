using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

public class Program
{

    private static void addToDict(Dictionary<string, List<String>> connections, string origin, string end)
    {
        if (connections.ContainsKey(origin))
        {
            connections[origin].Add(end);
        }
        else
        {
            connections.Add(origin, new List<String> { end });
        }
    }

    public static void Main()
    {
        // Make all required variables
        List<String> costs = new List<String>();
        Dictionary<string, List<String>> connections = new Dictionary<string, List<String>>();
        Dictionary<string, int> cities = new Dictionary<string, int>();

        int numCities = int.Parse(Console.ReadLine());
        for (int i = 0; i < numCities; i++)
        {
            string input = Console.ReadLine();
            string name = input.Split(" ")[0];
            int toll = int.Parse(input.Split(" ")[1]);
            cities.Add(name, toll);
        }
        int highways = int.Parse(Console.ReadLine());
        for (int i = 0; i < highways; i++)
        {
            string input = Console.ReadLine();
            string origin = input.Split(" ")[0];
            string end = input.Split(" ")[1];
            addToDict(connections, origin, end);
        }
        int trips = int.Parse(Console.ReadLine());
        for (int i = 0; i < trips; i++)
        {
            LinkedList<int> tripCost = new LinkedList<int>();
            Dictionary<string, int> visited= new Dictionary<string, int>();
            string input = Console.ReadLine();
            string origin = input.Split(" ")[0];
            string end = input.Split(" ")[1];
            if (origin == end)
            {
                Console.WriteLine("0");
                continue;
            }
            findTrips(origin, end, 0, tripCost, cities, connections, visited);
            if (tripCost.Count > 0)
            {
                Console.WriteLine(tripCost.First.Value);
            }
            else
            {
                Console.WriteLine("NO");
            }
        }
    }

    private static void findTrips(string origin, string end, int costSoFar, LinkedList<int> tripCost, Dictionary<string, int> cities, Dictionary<string, List<String>> connections,
        Dictionary<string, int> visited)
    {
        if (origin == end)
        {
            if (tripCost.Count== 0)
            {
                tripCost.AddFirst(costSoFar);
                return;
            } else if (costSoFar < tripCost.First.Value)
            {
                tripCost.AddFirst(costSoFar);
                return;
            } else
            {
                return;
            }
        }
        visited.Add(origin, costSoFar);
        if (!connections.ContainsKey(origin))
        {
            return;
        }
        foreach (string connection in connections[origin])
        {
            if (visited.ContainsKey(connection) && visited[connection] <= costSoFar + cities[connection])
            {
                continue;
            } else if (visited.ContainsKey(connection) && visited[connection] > costSoFar + cities[connection])
            {
                visited.Remove(connection);
            }
            findTrips(connection, end, costSoFar + cities[connection], tripCost, cities, connections, visited);
        }

    }
}