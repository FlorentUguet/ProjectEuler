using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public static class Formulas
    {
        public static BigInteger ProperDivisorsSum(BigInteger n)
        {
            BigInteger result = 0;

            List<BigInteger> divisors = new List<BigInteger>();

            for(BigInteger i = 1; i< (n/2) + 1;i++)
            {
                if(n%i == 0)
                {
                    divisors.Add(i);
                }
            }

            foreach(BigInteger i in divisors)
            {
                result += i;
            }

            return result;
        }
        public static void Permute(char[] array, int i, int n, List<string> results)
        {
            int j;

            if (i == n)
                results.Add(new string(array));
            else
            {
                for (j = i; j <= n; j++)
                {
                    Swap(ref array[i], ref array[j]);
                    Permute(array, i + 1, n, results);
                    Swap(ref array[i], ref array[j]); //backtrack
                }
            }
        }

        public static void Swap(ref char a, ref char b)
        {
            char tmp;
            tmp = a;
            a = b;
            b = tmp;
        }

        public static BigInteger Fibonacci(BigInteger n)
        {
            BigInteger num1 = 0, num2 = 1, result = 0;

            if (n == 0) return 0;
            if (n == 1) return 1;
            for (BigInteger i = 2; i <= n; i++)
            {
                result = num1 + num2;
                num1 = num2;
                num2 = result;
            }
            return result;
        }

        public static BigInteger Factorial(BigInteger n)
        {
            BigInteger result = 1;
            for (BigInteger i = 1; i <= n; i++)
            {
                result = result * i;
            }
            return result;
        }
    }
}
