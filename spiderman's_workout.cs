using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class Program
{
    public static void Main()
    {
        int numWorkouts = int.Parse(Console.ReadLine());
        for (int i = 0; i < numWorkouts; i++)
        {
            int numStops = int.Parse(Console.ReadLine());
            string[] movesString = Console.ReadLine().Split(" ");
            int[] moves = new int[movesString.Length];
            int sum = 0;
            for (int j = 0; j < movesString.Length; j++)
            {
                int curr = int.Parse(movesString[j]);
                sum += curr;
                moves[j] = curr;
            }
            int maxHeight = sum/2 + 1;
            int[,] heights = new int[numStops + 1, maxHeight];
            for (int j = 0; j <= numStops; j++)
            {
                for (int k = 0; k < maxHeight; k++)
                {
                    heights[j, k] = int.MaxValue;
                }
            }
            heights[0, 0] = 0;
            for (int currentStop = 0; currentStop < numStops; currentStop++)
            {
                for (int currentHeight = 0; currentHeight < maxHeight; currentHeight++)
                {
                    int heightNow = heights[currentStop, currentHeight];
                    if (heightNow > maxHeight)
                    {
                        continue;
                    }
                    int nextStop = currentStop + 1;
                    int nextDown = currentHeight - moves[currentStop];
                    int nextUp = currentHeight + moves[currentStop];
                    if (nextUp < maxHeight && nextUp < heights[nextStop, nextUp])
                    {
                        heights[nextStop, nextUp] = Math.Max(nextUp, heightNow);
                    }
                    if (nextDown >= 0 && heightNow < heights[nextStop, nextDown])
                    {
                        heights[nextStop, nextDown] = heightNow;
                    }
                }
            }
            if (heights[numStops, 0] > 200000)
            {
                Console.WriteLine("IMPOSSIBLE");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                int buildHeight = 0;
                for (int j = numStops; j > 0; j--)
                {
                    int currentMax = heights[j, buildHeight];
                    if (buildHeight - moves[j - 1] >= 0 && currentMax >= heights[j - 1, buildHeight - moves[j - 1]])
                    {
                        currentMax = heights[j - 1, buildHeight - moves[j - 1]];
                        sb.Insert(0, "U");
                        buildHeight = buildHeight - moves[j - 1];

                    }
                    else
                    {
                        currentMax = heights[j - 1, buildHeight + moves[j - 1]];
                        sb.Insert(0, "D");
                        buildHeight = buildHeight + moves[j - 1];
                    }
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }
}