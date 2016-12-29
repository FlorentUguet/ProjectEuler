using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public static class Formulas
    {
        public static string GetDecimalCycle(int divisor)
        {
            int[] foundRemainders = new int[divisor];
            int value = 1;
            int position = 0;

            while (foundRemainders[value] == 0 && value != 0)
            {
                foundRemainders[value] = position;
                value *= 10;
                value %= divisor;
                position++;
            }

            if (position - foundRemainders[value] > sequenceLength)
            {
                sequenceLength = position - foundRemainders[value];
            }
        }

        public static bool IsPermutation(int m, int n)
        {
            int[] arr = new int[10];

            int temp = n;
            while (temp > 0)
            {
                arr[temp % 10]++;
                temp /= 10;
            }

            temp = m;
            while (temp > 0)
            {
                arr[temp % 10]--;
                temp /= 10;
            }

            for (int i = 0; i < 10; i++)
            {
                if (arr[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static List<long> PrimeSieve(long n)
        {
            List<long> numbers = new List<long>();
            bool[] A = new bool[n];

            for(int i = 2; i < n; i++) { A[i] = true; }

            for(int i=2;i<Math.Sqrt(n);i++)
            {
                if (A[i])
                {
                    for(int j = i*i;j<n;j+=i)
                    {
                        A[j] = false;
                    }
                }
            }

            for(int i=2;i<n;i++)
            {
                if(A[i])
                    numbers.Add(i);
            }

            return numbers;
        }
        public static int SumOfFactors(int number)
        {
            int sqrtOfNumber = (int)Math.Sqrt(number);
            int sum = 1;

            //If the number is a perfect square
            //Count the squareroot once in the sum of factors
            if (number == sqrtOfNumber * sqrtOfNumber)
            {
                sum += sqrtOfNumber;
                sqrtOfNumber--;
            }

            for (int i = 2; i <= sqrtOfNumber; i++)
            {
                if (number % i == 0)
                {
                    sum = sum + i + (number / i);
                }
            }

            return sum;
        }

        public static string NumberToString(int n)
        {
            string result = "";
            bool useAnd = false;

            if (n >= 1000)
            {
                result += NumberToString(n / 1000) + "thousand";
                n = n % 1000;
            }

            if (n >= 100)
            {
                result += NumberToString(n / 100) + "hundred";
                n = n % 100;

                if(n > 0)
                {
                    useAnd = true;
                }
            }

            if (useAnd)
            {
                result += "and";
            }

            if (n >= 20)
            {
                switch(n/10)
                {
                    case 2:
                        result += "twenty";
                        break;
                    case 3:
                        result += "thirty";
                        break;
                    case 4:
                        result += "forty";
                        break;
                    case 5:
                        result += "fifty";
                        break;
                    case 6:
                        result += "sixty";
                        break;
                    case 7:
                        result += "seventy";
                        break;
                    case 8:
                        result += "eighty";
                        break;
                    case 9:
                        result += "ninety";
                        break;
                }

                n = n % 10;
            }

            switch (n)
            {
                case 1:
                    result += "one";
                    break;
                case 2:
                    result += "two";
                    break;
                case 3:
                    result += "three";
                    break;
                case 4:
                    result += "four";
                    break;
                case 5:
                    result += "five";
                    break;
                case 6:
                    result += "six";
                    break;
                case 7:
                    result += "seven";
                    break;
                case 8:
                    result += "eight";
                    break;
                case 9:
                    result += "nine";
                    break;
                case 10:
                    result += "ten";
                    break;
                case 11:
                    result += "eleven";
                    break;
                case 12:
                    result += "twelve";
                    break;
                case 13:
                    result += "thirteen";
                    break;
                case 14:
                    result += "fourteen";
                    break;
                case 15:
                    result += "fifteen";
                    break;
                case 16:
                    result += "sixteen";
                    break;
                case 17:
                    result += "seventeen";
                    break;
                case 18:
                    result += "eighteen";
                    break;
                case 19:
                    result += "nineteen";
                    break;
            }

            return result;
        }

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
