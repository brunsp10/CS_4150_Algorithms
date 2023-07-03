using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public class Program
{

    public static void Main()
    {
        String input = Console.ReadLine();
        while(input != "" && input != null)
        {
            String[] task = input.Split(" ");
            if (task[0] == "gcd")
            {
                Console.WriteLine(getGCD(BigInteger.Parse(task[1]), BigInteger.Parse(task[2])));
            }
            else if (task[0] == "exp")
            {
                int x = int.Parse(task[1]);
                int y = int.Parse(task[2]);
                int N = int.Parse(task[3]);
                Console.WriteLine(modExp(x, y, N));
            }
            else if (task[0] == "inverse")
            {
                int a = int.Parse(task[1]);
                int N = int.Parse(task[2]);
                BigInteger inverse = getInverse(a, N);
                if (inverse > N)
                {
                    Console.WriteLine("none");
                } else if (inverse < 0) {
                    Console.WriteLine(N + inverse);
                }
                else
                {
                    Console.WriteLine(inverse);
                }
            }
            else if (task[0] == "isprime")
            {
                Console.WriteLine(isPrime(int.Parse(task[1])));
            }
            else if (task[0] == "key")
            {
                BigInteger p = int.Parse(task[1]);
                BigInteger q = int.Parse(task[2]);
                BigInteger[] nums = getRSA(p, q);
                Console.WriteLine($"{nums[0]} {nums[1]} {nums[2]}");
            }
            input = Console.ReadLine();
        }

    }

    private static BigInteger[] getRSA(BigInteger p, BigInteger q)
    {
        BigInteger[] result = new BigInteger[3];
        result[0] = p * q;
        BigInteger phi = (p - 1) * (q - 1);
        int e = 2;
        while (getGCD(e, phi) != 1)
        {
            e++;
        }
        result[1] = e;
        BigInteger d = getInverse(e, phi);
        result[2] = d;
        return result;
    }

    private static string isPrime(int N)
    {
        int[] test = new int[] { 2, 3, 5 };
        foreach (int x in test)
        {
            BigInteger mod = modExp(x, N-1, N);
            if (mod != 1)
            {
                return "no";
            }
        }
        return "yes";
    }

    private static BigInteger getInverse(int a, BigInteger N)
    {
        BigInteger[] xyd = extendedEuclid(a,N);
        if (xyd[2] == 1)
        {
            BigInteger result = xyd[0] % N;
            if (result < 0)
            {
                return result + N;
            }
            else
            {
                return result;
            }
        }
        else
        {
            return N + 1;
        }
    }

    private static BigInteger[] extendedEuclid(BigInteger a, BigInteger b)
    {
        if (b == 0)
        {
            return new BigInteger[] { 1, 0, a };
        }
        else
        {
            BigInteger[] xydPrime = extendedEuclid(b, a % b);
            return new BigInteger[] { xydPrime[1], xydPrime[0] - (a / b) * xydPrime[1], xydPrime[2] };
        }
    }

    private static BigInteger modExp(BigInteger x, BigInteger y, BigInteger N)
    {
        if (y == 0)
        {
            return 1;
        } else
        {
            BigInteger z = modExp(x, y / 2, N);
            if (y % 2 == 0)
            {
                return (z*z) % N;
            } else
            {
                return (x * z * z) % N;
            }
        }
    }

    private static BigInteger getGCD(BigInteger a, BigInteger b)
    {
        while (b > 0)
        {
            BigInteger aModB = a % b;
            a = b;
            b = aModB;
        }
        return a;
    }
}
