using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection;

namespace ProjectEuler
{
    class Problems
    {
        public long Problem(int n)
        {
            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod("Problem"+n);
            return (long)theMethod.Invoke(this,null);
        }

        public static long Problem15()
        {
            /*
             * Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, there are exactly 6 routes to the bottom right corner.
             * How many such routes are there through a 20×20 grid?
             */

            int sideX = 20;
            int sideY = 20;

            long[,] points = new long[sideX+1,sideY+1];

            points[0, 0] = 1;

            for (int x = 0; x <= sideX; x++)
            {
                for (int y = 0; y <= sideY; y++)
                {
                    points[x, y] = 1;
                }
            }

            for (int x=1;x<=sideX;x++)
            {
                for (int y = 1; y <= sideY; y++)
                {
                    points[x,y] = points[x, y-1] + points[x-1,y];
                }
            }

            //Output
            /*
            for (int x = 0; x < sideX; x++)
            {
                for (int y = 0; y < sideY; y++)
                {
                    Console.Write(points[x, y] + " ");
                }
                Console.WriteLine();
            }
            */

            return points[sideX, sideY];
        }

        public static long Problem16()
        {
            /*
            215 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.

            What is the sum of the digits of the number 21000?
            */
            int result = 0;

            BigInteger number = BigInteger.Pow(2, 1000);

            while (number > 0)
            {
                result += (int)(number % 10);
                number /= 10;
            }

            return result;
        }

        public static long Problem17()
        {
            /*
            If the numbers 1 to 5 are written out in words: one, two, three, four, five, then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.
            If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used?
            */
            long result = 0;

            for (int i=1; i<=1000;i++)
            {
                result += Formulas.NumberToString(i).Length;
            }

            return result;
        }

        public static long Problem18()
        {
            /*
            By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23.

            3
            7 4
            2 4 6
            8 5 9 3

            That is, 3 + 7 + 4 + 9 = 23.

            Find the maximum total from top to bottom of the triangle below:
            */

            List<List<int>> numbers = new List<List<int>>();
            string[] lines = System.IO.File.ReadAllLines(@"res\p018_data.txt");

            //Load the list
            foreach(string line in lines)
            {
                string[] n = line.Split(' ');

                numbers.Add(new List<int>());

                foreach(string item in n)
                {
                    numbers.Last().Add(Int32.Parse(item));
                }
            }

            while(numbers.Count > 1)
            {
                List<int> last1 = numbers[numbers.Count() - 2];
                List<int> last = numbers.Last();

                for(int i=0;i<last1.Count();i++)
                {
                    last1[i] = last1[i] + Math.Max(last[i], last[i + 1]);
                }

                numbers[numbers.Count() - 2] = last1;
                numbers.Remove(numbers.Last());
            }
            return numbers[0][0];
        }

        public static long Problem19()
        {
            long result = 0L;

            DateTime t = new DateTime(1901, 1, 1);
            DateTime limit = new DateTime(2000, 12, 31);

            while(t < limit)
            {
                if(t.DayOfWeek == DayOfWeek.Sunday)
                {
                    result++;
                }

                t = t.AddMonths(1);
                Console.Out.WriteLine(t.ToShortDateString());
            }

            return result;
        }

        public static long Problem20()
        {
            /*
            n! means n × (n − 1) × ... × 3 × 2 × 1

            For example, 10! = 10 × 9 × ... × 3 × 2 × 1 = 3628800,
            and the sum of the digits in the number 10! is 3 + 6 + 2 + 8 + 8 + 0 + 0 = 27.

            Find the sum of the digits in the number 100!
            */
            int result = 0;
            BigInteger fact = 1;

            for(int i=1;i<=100;i++)
            {
                fact = fact * i;
            }

            while (fact > 0)
            {
                result += (int)(fact % 10);
                fact /= 10;
            }

            return result;
        }

        public static long Problem21()
        {
            /*
            Let d(n)be defined as the sum of proper divisors of n(numbers less than n which divide evenly into n).
            If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.

            For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284.The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.

            Evaluate the sum of all the amicable numbers under 10000.
            */

            BigInteger result = 0;

            List<BigInteger> values = new List<BigInteger>();

            for(int i=0;i<10000;i++)
            {
                BigInteger val = Formulas.ProperDivisorsSum(i);

                if(val < i)
                {
                    if(values.ElementAt((int)val) == i)
                    {
                        result += val + i;
                    }
                }

                values.Add(val);
            }

            return (long)result;
        }

        public static long Problem22()
        {
            Stopwatch watch = new Stopwatch();
            List<String> names = new List<String>();

            //Parsing
            string text = System.IO.File.ReadAllText(@"res\p022_names.txt");
            int index = 0;
            int loop = 0;

            watch.Start();

            Console.Out.WriteLine("File has " + text.Length + " characters");

            while (index >= 0)
            {
                int start, end = 0;

                //Start quotes
                if (index > 0)
                    start = text.IndexOf(",", index) + 2;
                else
                    start = 1;

                //End quotes
                end = text.IndexOf(",",start + 1);

                if (end < 0)
                {
                    names.Add(text.Substring(start, text.Length - 1 - start));
                    break;
                }
                    

                names.Add(text.Substring(start,end - 1 - start));

                if(names.Last().Contains("\""))
                {
                    Console.Out.WriteLine("Error in name : Start " + start + ", End " + end + ", String (" + names.Last() + ")");
                    break;
                }

                //Failsafe
                loop++;

                if(loop > 10000){ Console.Out.WriteLine("Too many loops"); break;}

                index = end;
            }
            watch.Stop();
            Console.Out.WriteLine("Processed " + loop + " lines in " + watch.ElapsedMilliseconds + " ms");

            //Sorting
            watch.Restart();
            names = names.OrderBy(x => x).ToList();
            watch.Stop();
            Console.Out.WriteLine("Ordered in " + watch.ElapsedMilliseconds + " ms");

            //Result
            watch.Restart();

            long result = 0L;
            long i = 1L;

            foreach (String str in names)
            {
                long nameValue = 0;
                foreach(Char c in str)
                {
                    nameValue += (c - 64);
                }

                result += nameValue * i;                
                i++;
            }
            
            watch.Stop();
            Console.Out.WriteLine("Calculated in " + watch.ElapsedMilliseconds + " ms");

            return result;
        }

        public static long Problem23()
        {
            /*
            vA perfect number is a number for which the sum of its proper divisors is exactly equal to the number. For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28, which means that 28 is a perfect number.
            A number n is called deficient if the sum of its proper divisors is less than n and it is called abundant if this sum exceeds n.
            As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be written as the sum of two abundant numbers is 24. By mathematical analysis, it can be shown that all integers greater than 28123 can be written as the sum of two abundant numbers. However, this upper limit cannot be reduced any further by analysis even though it is known that the greatest number that cannot be expressed as the sum of two abundant numbers is less than this limit.
            Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.
            */
            Stopwatch w = new Stopwatch();
            long result = 0;
            int sum = 0;

            int limit = 28123;

            List<int> abundantNumbers = new List<int>();

            w.Start();

            for(int i=1;i< limit; i++)
            {
                sum = 0;

                for(int j=1;j<(i/2)+1;j++)
                {
                    if(i % j == 0)
                    {
                        sum += j;
                    }
                }

                if(sum > i)
                {
                    abundantNumbers.Add(i);
                }
            }
            Console.WriteLine("Processed the abundant numbers after " + w.ElapsedMilliseconds +" ms");

            bool[] canBeWrittenasAbundent = new bool[limit + 1];


            for (int i = 0; i < abundantNumbers.Count; i++)
            {
                for (int j = i; j < abundantNumbers.Count; j++)
                {
                    if (abundantNumbers[i] + abundantNumbers[j] <= limit)
                    {
                        canBeWrittenasAbundent[abundantNumbers[i] + abundantNumbers[j]] = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Processed the abundant sums after " + w.ElapsedMilliseconds + " ms");

            for (int i=0;i< limit; i++)
            {
                if(!canBeWrittenasAbundent[i])
                {
                    result += i;
                }
            }
            Console.WriteLine("Processed the non-abundant sum numbers after " + w.ElapsedMilliseconds + " ms");

            w.Stop();

            return result;
        }

        public static long Problem24()
        {
            /*
            A permutation is an ordered arrangement of objects. For example, 3124 is one possible permutation of the digits 1, 2, 3 and 4. If all of the permutations are listed numerically or alphabetically, we call it lexicographic order. The lexicographic permutations of 0, 1 and 2 are:

            012   021   102   120   201   210

            What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?
            */
            List<string> permutations = new List<string>();

            string seed = "0123456789";

            Formulas.Permute(seed.ToArray(),0,9,permutations);
            permutations.Sort();

            Console.Out.WriteLine("Found " + permutations.Count() + " permutations");

            return Int64.Parse(permutations[999999]);
        }

        public static long Problem25()
        {
            /*
            The Fibonacci sequence is defined by the recurrence relation:
            Fn = Fn−1 + Fn−2, where F1 = 1 and F2 = 1.
            Hence the first 12 terms will be:
            F1 = 1
            F2 = 1
            F3 = 2
            F4 = 3
            F5 = 5
            F6 = 8
            F7 = 13
            F8 = 21
            F9 = 34
            F10 = 55
            F11 = 89
            F12 = 144
            The 12th term, F12, is the first term to contain three digits.
            What is the index of the first term in the Fibonacci sequence to contain 1000 digits ?
            */

            string str = "";

            List<BigInteger> terms = new List<BigInteger>();
            terms.Add(1);
            terms.Add(1);

            while (str.Length < 1000)
            {
                int i = terms.Count;
                BigInteger buf = terms.ElementAt(i - 1) + terms.ElementAt(i - 2);
                str = buf.ToString();
                terms.Add(buf);
            }

            return terms.Count;
        }

        public static long Problem26()
        {
            return -1;
        }

        public static long Problem31()
        {
            /*
            In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:
            1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
            It is possible to make £2 in the following way:
            1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
            How many different ways can £2 be made using any number of coins?
            */

            List<int> coins = new List<int>() { 1, 2, 5, 10, 20, 50, 100, 200 };
            long result = 0L;
            int limit = 200;
            int sum = 0;

            for(int i = 0;i<= limit; i+=200)
            {
                for (int j = 0; j <= limit; j += 100)
                {
                    for (int k = 0; k <= limit; k += 50)
                    {
                        for (int l = 0; l <= limit; l += 20)
                        {
                            for (int m = 0; m <= limit; m += 10)
                            {
                                for (int n = 0; n <= limit; n += 5)
                                {
                                    for (int o = 0; o <= limit; o += 2)
                                    {
                                        for (int p = 0; p <= limit; p += 1)
                                        {
                                            sum = i + j + k + l + m + n + o + p;

                                            if(sum == 200)
                                            {
                                                result++;
                                            }
                                            else if(sum > 200)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static long Problem36()
        {
            /*
            The decimal number, 585 = 1001001001 (binary), is palindromic in both bases.
            Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.
            (Please note that the palindromic number, in either base, may not include leading zeros.)
            */

            long result = 0L;

            for(int i=0;i<1000000;i++)
            {
                string b = Convert.ToString(i, 2);
                if (Formulas.IsPalindrome(b) && Formulas.IsPalindrome(Convert.ToString(i)))
                {
                    Console.Out.WriteLine(i + " -> " + b);
                    result += i;
                }
            }

            return result;
        }

        public static long Problem39()
        {
            for(int i=0;i<1000;i++)
            {

            }
        }

        public static long Problem44()
        {
            int result = 0;
            bool notFound = true;
            int i = 1;

            while (notFound)
            {
                i++;
                int n = i * (3 * i - 1) / 2;

                for (int j = i - 1; j > 0; j--)
                {
                    int m = j * (3 * j - 1) / 2;
                    if (Formulas.isPentagonal(n - m) && Formulas.isPentagonal(n + m))
                    {
                        result = n - m;
                        notFound = false;
                        break;
                    }
                }
            }

            return result;
        }

        public static long Problem49()
        {
            /*
            The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, is unusual in two ways: (i) each of the three terms are prime, and, (ii) each of the 4-digit numbers are permutations of one another.
            There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, but there is one other 4-digit increasing sequence.
            What 12-digit number do you form by concatenating the three terms in this sequence?
            */

            List<long> primes = Formulas.PrimeSieve(10000);
            List<long> numbers = new List<long>();
            List<string> results = new List<string>();

            int limit = 10000;

            foreach (long n in primes)
            {
                if (n > 1000)
                    numbers.Add(n);
            }

            for(int i=0;i<numbers.Count();i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    long k = numbers[j] + (numbers[j] - numbers[i]);

                    if (k < limit && numbers.Contains(k))
                        if (Formulas.IsPermutation((int)numbers[i], (int)numbers[j]) && Formulas.IsPermutation((int)numbers[i], (int)k))
                            if(numbers[i] != 1487)
                                results.Add(String.Concat(numbers[i], numbers[j], k));
                }
            }
            return Int64.Parse(results[0]);
        }



        public static long Problem67()
            {
                List<List<int>> numbers = new List<List<int>>();
                string[] lines = System.IO.File.ReadAllLines(@"res\p067_triangle.txt");

                //Load the list
                foreach (string line in lines)
                {
                    string[] n = line.Split(' ');

                    numbers.Add(new List<int>());

                    foreach (string item in n)
                    {
                        numbers.Last().Add(Int32.Parse(item));
                    }
                }

                while (numbers.Count > 1)
                {
                    List<int> last1 = numbers[numbers.Count() - 2];
                    List<int> last = numbers.Last();

                    for (int i = 0; i < last1.Count(); i++)
                    {
                        last1[i] = last1[i] + Math.Max(last[i], last[i + 1]);
                    }

                    numbers[numbers.Count() - 2] = last1;
                    numbers.Remove(numbers.Last());
                }
                return numbers[0][0];
            }

    }
}
