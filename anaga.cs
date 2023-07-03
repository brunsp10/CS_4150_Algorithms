using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

public class Program
{
    public static double msecs(Stopwatch sw)
    {
        return (((double)sw.ElapsedTicks) / Stopwatch.Frequency) * 1000;
    }

    private static string getRandomWord(int length)
    {
        StringBuilder word = new StringBuilder(length);
        Random rand = new Random();
        for (int i = 0; i < length; i++)
        {
            word.Append(rand.Next(97, 123));
        }
        return word.ToString();
    }

    private static string[] generateWordArray(int size, int wordLength)
    {
        string[] words = new string[size];
        for (int i = 0; i < size; i++)
        {
            words[i] = getRandomWord(wordLength);
        }
        return words;
    }

    /// <summary>
    /// Method to find words that are not anagrams
    /// </summary>
    /// <param name="wordArray"></param>
    private static void findAnagrams(string[] wordArray, int numberOfWords)
    {
        HashSet<String> nonAnagrams = new HashSet<String>();
        HashSet<String> rejected = new HashSet<String>();
        for (int i = 0; i < numberOfWords; i++)
        {
            char[] sortedArray = wordArray[i].ToCharArray();
            Array.Sort(sortedArray);
            String sortedWord = string.Join("", sortedArray);
            if (nonAnagrams.Contains(sortedWord))
            {
                nonAnagrams.Remove(sortedWord);
                rejected.Add(sortedWord);
            }
            else if (!rejected.Contains(sortedWord))
            {
                nonAnagrams.Add(sortedWord);
            }
        }
    }

    public static void Main()
    {
        Console.WriteLine("Is high resolution: " + Stopwatch.IsHighResolution);
        Console.WriteLine("Ticks per second: " + Stopwatch.Frequency);
        System.Threading.Thread.Sleep(3000);
        /*Console.WriteLine("Starting no. words test");
        int timesToRepeat = 10_000;

        for (int n = 50; n <= 26214400; n *= 2)
        {
            Stopwatch sw = new Stopwatch();

            string[] words = generateWordArray(n, 5);

            sw.Start();
            for (int i = 0; i < timesToRepeat; i++)
            {
                findAnagrams(words, n);
            }
            sw.Stop();

            double totalAverage = msecs(sw) / timesToRepeat;

            sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < timesToRepeat; i++)
            {
            }
            sw.Stop();

            double overheadAverage = msecs(sw) / timesToRepeat;

            double avgTime = totalAverage - overheadAverage;

            Console.WriteLine(n + "\t" + avgTime.ToString());
        }

        System.Threading.Thread.Sleep(3000);*/

        Console.WriteLine("Starting length words test");
        int timesToRepeat = 500;

        for (int k = 5; k <= 2621440; k *= 2)
        {
            Stopwatch sw = new Stopwatch();

            string[] words = generateWordArray(2000, k);

            sw.Start();
            for (int i = 0; i < timesToRepeat; i++)
            {
                findAnagrams(words, 2000);
            }
            sw.Stop();

            double totalAverage = msecs(sw) / timesToRepeat;

            sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < timesToRepeat; i++)
            {
            }
            sw.Stop();

            double overheadAverage = msecs(sw) / timesToRepeat;

            double avgTime = totalAverage - overheadAverage;

            Console.WriteLine(k + "\t" + avgTime.ToString());
        }
		Console.WriteLine(System.DateTime.Now.ToString());
    }
}