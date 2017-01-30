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
        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }

        public static void OutputPoker(int i, int[] p1, int[] p2)
        {
            if (p1[i] > p2[i])
                Console.Write("P1 WITH ");
            else
                Console.Write("P2 WITH ");

            switch (i)
            {
                case 0:
                    Console.Write("RYL FLUSH : " + p1[i] + " vs " + p2[i]);
                    break;
                case 1:
                    Console.Write("STR FLUSH : " + p1[i] + " vs " + p2[i]);
                    break;
                case 2:
                    Console.Write("FOUR KIND : " + p1[i] + " vs " + p2[i]);
                    break;
                case 3:
                    Console.Write("FULL HOUSE: " + p1[i] + " vs " + p2[i]);
                    break;
                case 4:
                    Console.Write("FLUSH     : " + p1[i] + " vs " + p2[i]);
                    break;
                case 5:
                    Console.Write("STARIGHT  : " + p1[i] + " vs " + p2[i]);
                    break;
                case 6:
                    Console.Write("THREE KIND: " + p1[i] + " vs " + p2[i]);
                    break;
                case 7:
                    Console.Write("TWO PAIRS : " + p1[i] + " vs " + p2[i]);
                    break;
                case 8:
                    Console.Write("ONE PAIR  : " + p1[i] + " vs " + p2[i]);
                    break;
                case 9:
                    Console.Write("HIGH CARD : " + p1[i] + " vs " + p2[i]);
                    break;
            }

            Console.WriteLine("");
        }

        public static int[] GetPokerHandScore(string hand)
        {
            List<string> cards = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A" };
            List<string> colors = new List<string>() { "H", "C", "S", "D" };

            int[] res = new int[10];
            string[] c = hand.Split(' ');

            //9-High Card
            for (int i = 0; i < c.Length; i++)
                res[9] = Math.Max(res[9], cards.IndexOf(c.ElementAt(0).ElementAt(0).ToString()));

            //8-One pair
            foreach (string card in cards)
                if (hand.Count(f => f == card.ElementAt(0)) == 2)
                    res[8] = cards.IndexOf(card);

            //7-Two pairs
            foreach (string card in cards)
                if (hand.Count(f => f == card.ElementAt(0)) == 2)
                    foreach (string card2 in cards)
                        if (card2 != card)
                            if (hand.Count(f => f == card2.ElementAt(0)) == 2)
                                res[7] = cards.IndexOf(card2);

            //6-Three of a kind
            foreach (string card in cards)
                if (hand.Count(f => f == card.ElementAt(0)) == 3)
                    res[6] = cards.IndexOf(card);

            //3-Full House
            if (res[6] > 0 && res[8] > 0)
                res[3] = res[6];

            //4-Flush
            foreach (string color in colors)
                if (hand.Count(f => f == color.ElementAt(0)) == 5)
                    res[4] = 1;

            //5-Straight
            for (int i = 0; i < cards.Count - 4; i++)
                if (hand.Contains(cards.ElementAt(i)) && hand.Contains(cards.ElementAt(i + 1)) && hand.Contains(cards.ElementAt(i + 2)) && hand.Contains(cards.ElementAt(i + 3)) && hand.Contains(cards.ElementAt(i + 4)))
                    res[5] = i;
            //2-Four of a kind
            foreach (string card in cards)
                    if (hand.Count(f => f == card.ElementAt(0)) == 4)
                        res[2] = cards.IndexOf(card);

            //0-Royal Flush
            res[0] = 1;
            if (res[4] < 1)
                res[0] = 0;
            else
                for (int i = cards.Count - 1; i >= 8; i--)
                    if (!hand.Contains(cards.ElementAt(i)))
                        res[0] = 0;

            //1-Straight Flush
            if (res[4] > 0 && res[5] > 0)
                res[1] = res[5];
            

            return res;
        }

        public static int[] Analysis(int[] message, int keyLength)
        {
            int maxsize = 0;
            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] > maxsize) maxsize = message[i];
            }

            int[,] aMessage = new int[keyLength, maxsize + 1];
            int[] key = new int[keyLength];

            for (int i = 0; i < message.Length; i++)
            {
                int j = i % keyLength;
                aMessage[j, message[i]]++;
                if (aMessage[j, message[i]] > aMessage[j, key[j]])
                    key[j] = message[i];
            }

            int spaceAscii = 32;
            for (int i = 0; i < keyLength; i++)
            {
                key[i] = key[i] ^ spaceAscii;
            }

            return key;
        }

        public static int[] Encrypt(int[] message, int[] key)
        {
            int[] encryptedMessage = new int[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                encryptedMessage[i] = message[i] ^ key[i % key.Length];
            }
            return encryptedMessage;
        }

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

        public static bool isHexagonal(long n)
        {
            double buf = (Math.Sqrt(1 + 8 * n) + 1.0) / 4.0;
            return buf == ((long)buf);
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

        public static List<string> PandigitalNumbers(int minDigit, int maxDigit)
        {
            List<string> numbers = new List<string>();
            List<string> c = new List<string>();

            string input = "";

            for(int i=minDigit;i<= maxDigit;i++)
            {
                input += i.ToString();
            }

            Permute(input.ToArray(), minDigit, maxDigit, numbers);

            return numbers;


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

        public static bool isPentagonal(long number)
        {
            double penTest = (Math.Sqrt(1 + 24 * number) + 1.0) / 6.0;
            return penTest == ((long)penTest);
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

        public static void Permute(string input, List<string> results)
        {
            Permute(input.ToArray(), 0, input.Length - 1, results);
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
