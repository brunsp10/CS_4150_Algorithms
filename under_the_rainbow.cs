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
        String input = Console.ReadLine();
        int numHotels = int.Parse(input)+1;
        int[] hotelsDistances = new int[numHotels];
        for (int i = 0; i < numHotels; i++)
        {
            int hotelDist = int.Parse(Console.ReadLine());
            hotelsDistances[i] = hotelDist;
        }
        int[] minCosts = new int[numHotels];
        Console.WriteLine(getMinCost(hotelsDistances, minCosts));
    }

    private static int getMinCost(int[] hotelsDistances, int[] minCosts)
    {
        for (int i = 1; i < minCosts.Length; i++)
        {
            int currentHotel = hotelsDistances[i];
            minCosts[i] = int.MaxValue;
            for (int j = 0; j < i; j++)
            {
                int penaltyDist = 400-(currentHotel - hotelsDistances[j]);
                int penalty = penaltyDist * penaltyDist;
                if (minCosts[j] + penalty < minCosts[i])
                {
                    minCosts[i] = minCosts[j] + penalty;
                }
            }
        }
        return minCosts[minCosts.Length - 1];
    }
}