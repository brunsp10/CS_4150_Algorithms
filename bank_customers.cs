using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public class Program
{
    public static void Main()
    {
        string input = Console.ReadLine();
        int numPeople = int.Parse(input.Split(" ")[0]);
        int timeLeft = int.Parse(input.Split(" ")[1]);
        int moneyTaken = 0;
        int customerID = 0;
        PQ[] timePQs = new PQ[timeLeft];
        for (int i = 0; i < timeLeft; i++)
        {
            timePQs[i] = new PQ();
        }
        HashSet<int> servedCustomers = new HashSet<int>();
        for (int i = 0; i < numPeople; i++)
        {
            string customerData = Console.ReadLine();
            int customerMoney = int.Parse(customerData.Split(" ")[0]);
            int customerTime = int.Parse(customerData.Split(" ")[1]);
            Node node = new Node(customerID, customerMoney);
            for (int t = 0; t <= customerTime; t++)
            {
                timePQs[t].insertOrChange(node);
            }
            customerID++;
        }
        for (int i = timePQs.Length-1; i >= 0; i--)
        {
            while (timePQs[i].count > 0 && servedCustomers.Contains(timePQs[i].peekMax().data))
            {
                timePQs[i].getMax();
            }
            if (timePQs[i].count == 0)
            {
                continue;
            }
            Node customerAdded = timePQs[i].peekMax();
            moneyTaken += customerAdded.weight;
            servedCustomers.Add(customerAdded.data);
        }
        Console.WriteLine(moneyTaken);
    }

    private static void addToDict(Dictionary<int, PQ> customerTimeAndMoney, int customerTime, int customerMoney, int placeholder)
    {
        if (customerTimeAndMoney.ContainsKey(customerTime))
        {
            customerTimeAndMoney[customerTime].insertOrChange(new Node(placeholder, customerMoney));
        }
        else
        {
            customerTimeAndMoney.Add(customerTime, new PQ());
            customerTimeAndMoney[customerTime].insertOrChange(new Node(placeholder, customerMoney));
        }
    }

    public class Node
    {
        public int data;
        public int weight;

        public Node(int data, int weight)
        {
            this.data = data;
            this.weight = weight;
        }
    }

    public class PQ
    {
        private List<Node> endPoints;
        public int count;
        private Dictionary<int, int> index;

        public PQ()
        {
            endPoints = new List<Node>();
            count = 0;
            index = new Dictionary<int, int>();
        }

        public Node peekMax()
        {
            return endPoints[0];
        }

        public Node getMax()
        {
           
            Node max = endPoints[0];
            if (count == 1)
            {
                count--;
                return max;

            }
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
                if (2 * v + 1 >= count)
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
                    }
                    else
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
                    }
                    else
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
                    }
                    else
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
                endPoints.Add(node);
                moveUp(count);
                count++;
            }
            else
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
                int parent = (mover - 1) / 2;
                if (endPoints[mover].weight > endPoints[parent].weight)
                {
                    Node temp = endPoints[parent];
                    endPoints[parent] = endPoints[mover];
                    endPoints[mover] = temp;
                    index[endPoints[mover].data] = mover;
                    index[endPoints[parent].data] = parent;
                    mover = parent;
                }
                else
                {
                    return;
                }
            }
        }
    }
}

