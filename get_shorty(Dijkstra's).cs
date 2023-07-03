using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public class Program
{
    private static void AddToDict(string index, Corridor corridor, Dictionary<string, List<Corridor>> dict)
    {
        if (dict.ContainsKey(index))
        {
            dict[index].Add(corridor);
        }
        else
        {
            dict.Add(index, new List<Corridor>() { corridor });
        }
    }

    public static void Main()
    {
        Console.WriteLine("server = atr.eng.utah.edu;" +
                                "database=Chess;" +
                                "uid=steve;" +
                                "password=abc123");

    }

    private static double findPath(string endPoint, Dictionary<string, List<Corridor>> corridors, int numIntersections)
    {
        HashSet<string> checkedNodes = new HashSet<string>();
        double size = 1;

        PQ pq = new PQ(numIntersections);
        pq.insertOrChange(new Node("0", 1));
        checkedNodes.Add("0");

        while (pq.count > 0)
        {
            Node currentNode = pq.getMax();
            checkedNodes.Add(currentNode.data);
            size = currentNode.weight;

            if (currentNode.data == endPoint)
            {
                return size;
            }

            foreach (Corridor x in corridors[currentNode.data])
            {
              if (checkedNodes.Contains(x.endPoint))
                {
                    continue;
                }
                pq.insertOrChange(new Node(x.endPoint,(size * x.factor)));
            }
        }

        return size;
    }
}

public class Corridor
{
    public string startPoint;
    public string endPoint;
    public double factor;

    public Corridor(string startPoint, string endPoint, double factor)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.factor = factor;
    }
}


public class Node
{
    public string data;
    public double weight;

    public Node(string data, double weight)
    {
        this.data = data;
        this.weight = weight;
    }
}

public class PQ
{
    private Node[] endPoints;
    public int count;
    private Dictionary<string, int> index;

    public PQ(int numCorridors)
    {
        endPoints = new Node[numCorridors];
        count = 0;
        index = new Dictionary<string, int>();
    }

    public Node getMax()
    {
        Node max = endPoints[0];
        Node newRoot = endPoints[count - 1];
        endPoints[0] = newRoot;
        count--;
        index.Remove(max.data);
        index[newRoot.data] = 0;
        moveDown(0);
        return max;
    }

    private void moveDown(int v)
    {
        while (true)
        {
            if (2*v+1 >= count)
            {
                return;
            }
            int leftChild = 2 * v + 1;
            int rightChild = 2 * v + 2;
            if (rightChild >= count)
            {
                if (endPoints[v].weight < endPoints[leftChild].weight)
                {
                    Node temp = endPoints[v];
                    endPoints[v] = endPoints[leftChild];
                    endPoints[leftChild] = temp;
                    index[endPoints[v].data] = v;
                    index[endPoints[leftChild].data] = leftChild;
                    v = leftChild;
                } else
                {
                    return;
                }
            }
            else if (endPoints[leftChild].weight >= endPoints[rightChild].weight)
            {
                if (endPoints[v].weight < endPoints[leftChild].weight)
                {
                    Node temp = endPoints[v];
                    endPoints[v] = endPoints[leftChild];
                    endPoints[leftChild] = temp;
                    index[endPoints[v].data] = v;
                    index[endPoints[leftChild].data] = leftChild;
                    v = leftChild;
                } else
                {
                    return;
                }
            }
            else if (endPoints[leftChild].weight < endPoints[rightChild].weight)
            {
                if (endPoints[v].weight < endPoints[rightChild].weight)
                {
                    Node temp = endPoints[v];
                    endPoints[v] = endPoints[rightChild];
                    endPoints[rightChild] = temp;
                    index[endPoints[v].data] = v;
                    index[endPoints[rightChild].data] = rightChild;
                    v = rightChild;
                } else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    public void insertOrChange(Node node)
    {
        if (!index.ContainsKey(node.data))
        {
            index.Add(node.data, count);
            endPoints[count] = node;
            moveUp(count);
            count++;
        } else
        {
            Node currentNode = endPoints[index[node.data]];
            if (currentNode.weight >= node.weight)
            {
                return;
            }
            else
            {
                endPoints[index[node.data]] = node;
                moveUp(index[node.data]);
            }
        }
    }

    private void moveUp(int mover)
    {
        while (true)
        {
            if (mover == 0)
            {
                return;
            }
            int parent = (mover-1) / 2;
            if (endPoints[mover].weight > endPoints[parent].weight)
            {
                Node temp = endPoints[parent];
                endPoints[parent] = endPoints[mover];
                endPoints[mover] = temp;
                index[endPoints[mover].data] = mover;
                index[endPoints[parent].data] = parent;
                mover = parent;
            } else
            {
                return;
            }
        }
    }


}

