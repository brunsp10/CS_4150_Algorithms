using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

public class Program
{

    private static bool isInGalaxy(long star1X, long star1Y, long star2X, long star2Y, long d)
    {
        if ((star1X - star2X) * (star1X - star2X) + (star1Y - star2Y) * (star1Y - star2Y) <= (d * d))
        {
            return true;
        }
        return false;
    }

    private static List<long> findMajority(List<List<long>> list, long diameter, Dictionary<List<long>, int> possibleStars)
    {
        if (list.Count == 0) { return null; }
        else if (list.Count == 1) {
            addToDict(list[0], possibleStars, 1);
            return list[0];
        }
        else
        {
            List<long> x = findMajority(list.GetRange(0, list.Count / 2), diameter, possibleStars);
            List<long> y = findMajority(list.GetRange(list.Count/2, list.Count/2), diameter, possibleStars);
            if (x == null && y == null)
            {
                return null;
            }
            else if (x == null)
            {
                int value = checkStars(y[0], y[1], diameter, list);
                if (value > list.Count / 2)
                {
                    addToDict(y, possibleStars, value);
                    return y;
                }
                else
                {
                    possibleStars.Remove(y);
                    return null;
                }
            }
            else if (y == null)
            {
                int value = checkStars(x[0], x[1], diameter, list);
                if (value > list.Count / 2)
                {
                    addToDict(x, possibleStars, value);
                    return x;
                }
                else
                {
                    possibleStars.Remove(x);
                    return null;
                }
            }
            else
            {
                int value = checkStars(x[0], x[1], diameter, list);
                if (value > list.Count / 2)
                {
                    addToDict(x, possibleStars, value);
                    return x;
                }
                else
                {
                    possibleStars.Remove(x);
                }
                value = checkStars(y[0], y[1], diameter, list);
                if (value > list.Count / 2)
                {
                    addToDict(y, possibleStars, value);
                    return y;
                }
                else
                {
                    possibleStars.Remove(y);
                }
                return null;
            }
            }
    }


    private static void addToDict(List<long> list, Dictionary<List<long>, int> possibleStars, int value)
    {
        if (possibleStars.ContainsKey(list)) { possibleStars[list] = value; }
        else { possibleStars.Add(list, value); }
    }

    private static int checkStars(long starX, long starY, long diameter, List<List<long>> stars)
    {
        int count = 0;
        for (int i = 0; i < stars.Count; i++)
        {
            List<long> other = stars[i];
            long otherX = other[0];
            long otherY = other[1];
            if (isInGalaxy(starX, starY, otherX, otherY, diameter))
            {
                count++;
            }
        }
        if (count > stars.Count / 2)
        {
            return count;
        }
        return 0;
    }

    public static void Main()
    {
        string input = Console.ReadLine();
        long diameter = long.Parse(input.Split(" ")[0]);
        int starCount = int.Parse(input.Split(" ")[1]);
        List<List<long>> allStars = new List<List<long>>();
        for (int i = 0; i < starCount; i++)
        {
            string starInput = Console.ReadLine();
            List<long> starCoordinates = new List<long>();
            starCoordinates.Add(long.Parse(starInput.Split(" ")[0]));
            starCoordinates.Add(long.Parse(starInput.Split(" ")[1]));
            allStars.Add(starCoordinates);
        }
        Dictionary<List<long>, int> possibleStars = new Dictionary<List<long>, int>();
        findMajority(allStars, diameter, possibleStars);
        if (starCount == 1)
        {
            Console.WriteLine("1");
            return;
        }
        foreach (int value in possibleStars.Values)
        {
            if (value > allStars.Count/2)
            {
                Console.WriteLine(value);
                return;
            }
        }
        Console.WriteLine("NO");
    }
}