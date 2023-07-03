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
        String[] input = Console.ReadLine().Split(" ");
        int N = int.Parse(input[0]);
        int k = int.Parse(input[1]);
        int[,] values = new int[N,2];
        for (int i = 0; i < N; i++)
        {
            string[] row = Console.ReadLine().Split(" ");
            values[i, 0] = int.Parse(row[0]);
            values[i, 1] = int.Parse(row[1]);
        }
        Console.ReadLine();
        int result = getMaxValue(values, k, N);
        Console.WriteLine(result);
    }

    private static int getMaxValue(int[,] values, int k, int N)
    {
        Dictionary<String, int> maxima = new Dictionary<String, int>();
        return MaxValue(0, -1, k, maxima, values, N);
    }

    private static int MaxValue(int r, int unclosableRoom, int k, Dictionary<string, int> maxima, int[,] values, int N)
    {
        string thisKey = $"maxValue({r},{unclosableRoom},{k})";
        if (maxima.ContainsKey(thisKey))
        {
            return maxima[thisKey];
        }
        if (r >= N)
        {
            return 0;
        }
        if (k == N - r)
        {
            if (unclosableRoom == 0)
            {
                int val = values[r, 0] + MaxValue(r + 1, 0, k - 1, maxima, values, N);
                maxima[thisKey] =val;
                return val;
            }
            else if (unclosableRoom == 1)
            {
                int val  = values[r, 1] + MaxValue(r + 1, 1, k - 1, maxima, values, N);
                maxima[thisKey] = val;
                return val;
            }
            else
            {
                int maxPossible = Math.Max(values[r, 0] + MaxValue(r + 1, 0, k - 1, maxima, values, N), values[r, 1] + MaxValue(r + 1, 1, k - 1, maxima, values, N));
                maxima[thisKey] = maxPossible;
                return maxPossible;
            }
        } else if (k <= N - r)
        {
            if (unclosableRoom == 0)
            {
                int maxPossible = Math.Max(values[r, 0] + MaxValue(r + 1, 0, k - 1, maxima, values, N), values[r, 0] + values[r, 1] + MaxValue(r + 1, -1, k, maxima, values, N));
                maxima[thisKey] = maxPossible;
                return maxPossible;
            }
            else if (unclosableRoom == 1)
            {
                int maxPossible = Math.Max(values[r, 1] + MaxValue(r + 1, 1, k - 1, maxima, values, N), values[r, 0] + values[r, 1] + MaxValue(r + 1, -1, k, maxima, values, N));
                maxima[thisKey] = maxPossible;
                return maxPossible;
            }
            else
            {
                int maxClosed = Math.Max(values[r, 0] + MaxValue(r + 1, 0, k - 1, maxima, values, N), values[r, 1] + MaxValue(r + 1, 1, k - 1, maxima, values, N));
                int maxPossible = Math.Max(maxClosed, values[r, 0] + values[r, 1] + MaxValue(r + 1, -1, k, maxima, values, N));
                maxima[thisKey] = maxPossible;
                return maxPossible;
            }
        }
        return -1;
    }
}