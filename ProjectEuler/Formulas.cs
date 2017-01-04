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
        public static long FactorialFromDigits(long n)
        {
            string s = n.ToString();

            long res = 0;

            foreach(char c in s)
            {
                res += (long)Factorial(Int32.Parse(c.ToString()));
            }

            return res;
        }

        public static bool isTriangular(long n)
        {
            double buf = Math.Sqrt(8 * n + 1);
            return (buf ==  Math.Floor(buf));
        }

        public static bool isTruncatable(long n)
        {
            string nString = n.ToString();

            for(int i=0;i<nString.Length;i++)
            {
                string s = nString.Substring(i);
                if (!isPrime(long.Parse(s)))
                    return false;
            }

            for (int i = 0; i < nString.Length; i++)
            {
                string s = nString.Substring(0,nString.Length - i);
                if (!isPrime(long.Parse(s)))
                    return false;
            }

            return true;
        }

        public static long NumberOfRightAngleTriangles(int p)
        {
            long result = 0;

            for(int a = 1; a < p; a++)
            {
                for (int b = 1; b < a; b++)
                {
                    int c = p - b - a;

                    if (c < b)
                        break;

  
                    if(a*a == b*b + c*c && a+b+c == p)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public static bool isPandigital(long n)
        {
            int l = n.ToString().Length;

            if(l <= 9)
            {
                return isPandigital(n, 1, l);
            }
            else if(l == 10)
            {
                return isPandigital(n, 0, 9);
            }
            else
            {
                return false;
            }
            
        }

        public static bool isPandigital(long n, int min, int max)
        {
            if (n.ToString().Length != (max-min+1))
                return false;

            for(int i=min; i<=max;i++)
                if (!n.ToString().Contains(i.ToString()))
                    return false;

            return true;
        }

        public static long LargestPandigital(int minDigit, int maxDigit)
        {
            string res = "0";
            int res_n;
            int res_i;
            int n = 1;

            int nDigits = maxDigit - minDigit + 1;

            while(n.ToString().Length <= nDigits / 2)
            {
                if(n.ToString().Length > nDigits)
                    break;

                string buf = "";
                int i = 1;

                do
                {
                    buf = buf + (i * n);
                    i++;
                } while ((buf + (i * n)).Length <= nDigits);

                if ( i>2 && Int64.Parse(buf) > Int64.Parse(res) && isPandigital(Int64.Parse(buf), minDigit, maxDigit))
                {
                    res = buf;
                    res_n = n;
                    res_i = i - 1;
                }

                n++;
            }

            return Int64.Parse(res);
        }

        public static bool isPentagonal(int number)
        {
            double penTest = (Math.Sqrt(1 + 24 * number) + 1.0) / 6.0;
            return penTest == ((int)penTest);
        }

        public static List<BigInteger> Pentagonal(long n, long m)
        {
            List<BigInteger> numbers = new List<BigInteger>();

            for(long i=n;i<m;i++)
            {
                numbers.Add(i * ((3 * i) - 1) / 2);
            }

            return numbers;
        }

        public static bool IsPalindrome(string value)
        {
            int min = 0;
            int max = value.Length - 1;
            while (true)
            {
                if (min > max)
                {
                    return true;
                }
                char a = value[min];
                char b = value[max];
                if (char.ToLower(a) != char.ToLower(b))
                {
                    return false;
                }
                min++;
                max--;
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

        public static bool isPrime(long n)
        {
            if (n <= 1)
                return false;
            else if( n <= 3)
                return true;
            else if (n % 2 == 0 || n % 3 == 0)
                return false;

            int i = 5;
            while(i*i <= n)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;
                i = i + 6;
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
            if (n <= 1)
                return 1;
            return n * Factorial(n - 1);
        }
    }
}
