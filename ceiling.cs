using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

public class Program
{
  

    private static void tree(List<int> nodeVals, List<string> nodeList, string path)
    {
        List<int> rightTree = new List<int>();
        List<int> leftTree = new List<int>();
        int root = nodeVals[0];
        for (int i = 1; i < nodeVals.Count; i++)
        {
            if (nodeVals[i] > root)
            {
                rightTree.Add(nodeVals[i]);
            } else
            {
                leftTree.Add(nodeVals[i]);
            }
        }
        if (rightTree.Count > 0)
        {
            string rightPath = path + "R";
            nodeList.Add(rightPath);
            tree(rightTree, nodeList, rightPath);
        }
        if (leftTree.Count > 0)
        {
            string leftPath = path + "L";
            nodeList.Add(leftPath);
            tree(leftTree, nodeList, leftPath);
        }
    }

    private static bool areEqual(List<string> x, List<string> y)
    {
        if (x.Count != y.Count) return false;
        for (int i = 0; i < x.Count; i++)
        {
            if (x[i] != y[i]) return false;
        }
        return true;
    }

    public static void Main()
    {
        List<List<string>> uniqueTrees = new List<List<string>>();
        string input = Console.ReadLine();
        int lines;
        int.TryParse(input.Split(" ")[0], out lines);
        int nodes = int.Parse(input.Split(" ")[1]);
        if (nodes == 1 || lines == 1)
        {
            Console.WriteLine(1);
            return;
        }
        for (int i = 0;i < lines; i++)
        {
            bool inSet = false;
            List<string> order = new List<string>();
            List<int> nums = new List<int>();
            string inputVals = Console.ReadLine();
            string[] inputNums = inputVals.Split(" ");
            foreach (string x in inputNums)
            {
                nums.Add(int.Parse(x));
            }
            tree(nums, order,"");
            order.Sort();
            foreach (List<string> list in uniqueTrees)
            {
                if (areEqual(list, order))
                {
                    inSet = true;
                    break;
                }
            }
            if (!inSet)
            {
                uniqueTrees.Add(order);
            }
        }
        Console.WriteLine(uniqueTrees.Count);
    }
}