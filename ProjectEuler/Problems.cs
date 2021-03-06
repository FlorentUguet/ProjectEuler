﻿using System;
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

        public static long Problem28()
        {
            /*
            Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is formed as follows:

            21 22 23 24 25
            20  7  8  9 10
            19  6  1  2 11
            18  5  4  3 12
            17 16 15 14 13

            It can be verified that the sum of the numbers on the diagonals is 101.

            What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?
            */

            int l = 1001;
            long result = 1L;
            long buf = 1L;

            for (int i = 3; i <= l; i += 2)
            {
                for (int j = 1; j <= 4; j++)
                {
                    buf += (i - 1);
                    result += buf;
                }
            }

            return result;
        }

        public static long Problem29()
        {
            /*
            Consider all integer combinations of ab for 2 ≤ a ≤ 5 and 2 ≤ b ≤ 5:

            2^2=4, 2^3=8, 2^4=16, 2^5=32
            3^2=9, 3^3=27, 3^4=81, 3^5=243
            4^2=16, 4^3=64, 4^4=256, 4^5=1024
            5^2=25, 5^3=125, 5^4=625, 5^5=3125
            If they are then placed in numerical order, with any repeats removed, we get the following sequence of 15 distinct terms:

            4, 8, 9, 16, 25, 27, 32, 64, 81, 125, 243, 256, 625, 1024, 3125

            How many distinct terms are in the sequence generated by ab for 2 ≤ a ≤ 100 and 2 ≤ b ≤ 100
            */

            int aMax = 100;
            int bMax = 100;
            int aMin = 2;
            int bMin = 2;

            List<BigInteger> results = new List<BigInteger>();

            for(int a = aMin; a <= aMax; a++)
            {
                for (int b = bMin; b <= bMax; b++)
                {
                    BigInteger bi = (BigInteger)Math.Pow(a, b);
                    if(!results.Contains(bi))
                        results.Add(bi);
                }
            }



            return results.Count;
        }

        public static long Problem30()
        {
            /*
            Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:

1634 = 14 + 64 + 34 + 44
8208 = 84 + 24 + 04 + 84
9474 = 94 + 44 + 74 + 44
As 1 = 14 is not a sum it is not included.

The sum of these numbers is 1634 + 8208 + 9474 = 19316.

Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
*/
            long res = 0L;
            long buf = 2L;

            do
            {

                long buf_res = 0L;
                for (int i = 0; i < buf.ToString().Length; i++)
                {
                    buf_res += (long)Math.Pow((double)(buf.ToString().ElementAt(i) - 48), 5.0);
                }

                if (buf_res == buf)
                    res += buf;
                buf++;

            } while (buf < 1000000);


            return res;
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

        public static long Problem33()
        {
            int denominator = 1;
            int numerator = 1;

            for (int i=10;i<100;i++)
            {
                for (int j = i; j < 100; j++)
                {
                    for(int k=0;k<2;k++)
                    {
                        string istr = i.ToString();
                        string jstr = j.ToString();

                        if((i%10 != 0 && j % 10 != 0) && i != j)
                        {
                            if (istr.Contains(jstr.ElementAt(k)))
                            {
                                istr = istr.Remove(istr.IndexOf(jstr.ElementAt(k)),1);
                                jstr = jstr.Remove(k, 1);

                                int i2 = int.Parse(istr);
                                int j2 = int.Parse(jstr);

                                if ((double)i / (double)j == (double)i2 / (double)j2)
                                {
                                    denominator *= j2;
                                    numerator *= i2;
                                }
                            }
                        }                        
                    }
                }
            }

            Console.WriteLine(numerator + "/" + denominator);

            return numerator / denominator;
        }

        public static long Problem34()
        {
            long res = 0;

            int i = 3;

            long limit = 10;
            long fact = (long)Formulas.Factorial(9);

            while(limit < fact)
            {
                limit *= 10;
                fact += (long)Formulas.Factorial(9);
            }

            Console.WriteLine(limit + " > " + fact);

            while(i < fact)
            {
                if (i == Formulas.FactorialFromDigits(i))
                    res += i;

                i++;
            }

            return res;
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

        public static long Problem37()
        {
            /*
            The number 3797 has an interesting property. Being prime itself, it is possible to continuously remove digits from left to right, and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.

            Find the sum of the only eleven primes that are both truncatable from left to right and right to left.

            NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.
            */
            long res = 0L;

            List<long> primes = Formulas.PrimeSieve(1000000);
            List<long> truncatablePrimes = new List<long>();

            int i = 8;
            int j = 0;

            while (j < 11)
            {
                if (i > primes.Last())
                    return -2;

                if (Formulas.isTruncatable(i))
                {
                    j++;
                    res += i;
                }
                    
                i++;
            }

            return res;
        }

        public static long Problem38()
        {
            return Formulas.LargestPandigital(1,9);
        }

        public static long Problem39()
        {
            /*
            If p is the perimeter of a right angle triangle with integral length sides, {a,b,c}, there are exactly three solutions for p = 120.

            {20,48,52}, {24,45,51}, {30,40,50}

            For which value of p ≤ 1000, is the number of solutions maximised?
            */
            long result = 0L;
            long resId = 0L;

            for(int i = 1;i<=1000;i++)
            {
                long buf = Formulas.NumberOfRightAngleTriangles(i);

                if(buf > result)
                {
                    resId = i;
                    result = buf;
                }
            }
            

            return resId;
        }

        public static long Problem40()
        {
            long current = 0;
            long size = 0;
            long prev_size = 0;
            long divisor = 1;

            long res = 1;
            

            do
            {
                current++;
                prev_size = size;
                size += current.ToString().Length;

                if(size >= divisor)
                {
                    
                    res *= current.ToString().ElementAt((current.ToString().Length - 1) - (int)(size - divisor)) - 48;
                    divisor *= 10;
                }

            } while (size < 1000000);

            return res;
        }

        public static long Problem41()
        {
            /*
            We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. For example, 2143 is a 4-digit pandigital and is also prime.
            What is the largest n-digit pandigital prime that exists?
            */

            List<long> primes = Formulas.PrimeSieve(10000000);

            long result = 0L;

            foreach(long p in primes)
            {
                if(Formulas.isPandigital(p))
                {
                    result = p;
                }
            }

            return result;
        }

        public static long Problem42()
        {
            long result = 0;

            string text = System.IO.File.ReadAllText(@"res\p042_words.txt");

            foreach(string word in text.Split(','))
            {
                string w = word.Replace("\"", "");

                long val = 0;

                foreach(char c in w)
                {
                    val += c - 64;
                }

                if (Formulas.isTriangular(val))
                    result++;
            }

            return result;
        }

        public static long Problem43()
        {
            /*
            The number, 1406357289, is a 0 to 9 pandigital number because it is made up of each of the digits 0 to 9 in some order, but it also has a rather interesting sub-string divisibility property.

            Let d1 be the 1st digit, d2 be the 2nd digit, and so on. In this way, we note the following:

            d2d3d4=406 is divisible by 2
            d3d4d5=063 is divisible by 3
            d4d5d6=635 is divisible by 5
            d5d6d7=357 is divisible by 7
            d6d7d8=572 is divisible by 11
            d7d8d9=728 is divisible by 13
            d8d9d10=289 is divisible by 17
            Find the sum of all 0 to 9 pandigital numbers with this property.
            */

            List<string> numbers = Formulas.PandigitalNumbers(0, 9);
            List<long> primes = Formulas.PrimeSieve(20);

            long result = 0L;

            foreach(string str in numbers)
            {
                bool buf = true;

                for (int i = 1; i <= 7; i++)
                {
                    if(Int64.Parse(String.Concat(String.Concat(str[i], str[i+1]), str[i+2])) % primes.ElementAt(i-1) != 0)
                    {
                        buf = false;
                    }
                }

                if(buf)
                {
                    result += Int64.Parse(str);
                }
            }

            

            return result;
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

        public static long Problem45()
        {
            /*
            Triangle, pentagonal, and hexagonal numbers are generated by the following formulae:

            Triangle	 	Tn=n(n+1)/2	 	1, 3, 6, 10, 15, ...
            Pentagonal	 	Pn=n(3n−1)/2	 	1, 5, 12, 22, 35, ...
            Hexagonal	 	Hn=n(2n−1)	 	1, 6, 15, 28, 45, ...
            It can be verified that T285 = P165 = H143 = 40755.

            Find the next triangle number that is also pentagonal and hexagonal.
             */

            long n = 286L;
            long l = 0L;

            do
            {
                l = (n * (n + 1)) / 2;
                n = n + 1;
            } while (!Formulas.isTriangular(l) || !Formulas.isPentagonal(l) || !Formulas.isHexagonal(l));



            return l;
        }

        public static long Problem46()
        {
            long res = 0L;

            return res;
        }

        public static long Problem48()
        {
            double d = Math.Pow(1000, 1000) % 1000000;
            BigInteger b = (BigInteger)d;
            Console.WriteLine(b.ToString());
            return -1;
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

        public static long Problem50()
        {
            long result = 0L;
            int resCount = 0;
            long limit = 1000000;

            List<long> primes = Formulas.PrimeSieve(limit);

            for(int i=primes.Count-1;i>=0;i--)
            {
                /*
                The prime 41, can be written as the sum of six consecutive primes:

                41 = 2 + 3 + 5 + 7 + 11 + 13
                This is the longest sum of consecutive primes that adds to a prime below one-hundred.

                The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, and is equal to 953.

                Which prime, below one-million, can be written as the sum of the most consecutive primes?  
                */

                long l = 0L;
                long j = i;
                int c = 0;

                while(l < limit && j >= 0)
                {
                    l += primes.ElementAt((int)j);
                    j--;
                    c++;

                    if(c > resCount && Formulas.isPrime(l))
                    {
                        resCount = c ;
                        result = l;
                    }
                }
            }

            return result;
        }

        public static long Problem52()
        {
            /*
            It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, but in a different order.
            Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits
            */
            int x = 0;
            bool ok = true;

            do
            {
                x++;
                ok = true;
                for (int i = 2; i < 7; i++)
                {
                    if (!Formulas.IsPermutation(x * i,x))
                    {
                        ok = false;
                    }
                }

            } while (!ok);

            return x;
        }

        public static long Problem54()
        {
            int count = 0;
            int j = 1;
            foreach (string game in System.IO.File.ReadLines(@"res\p054_poker.txt"))
            {
                int[] p1 = Formulas.GetPokerHandScore(game.Substring(0, 14).Trim());
                int[] p2 = Formulas.GetPokerHandScore(game.Substring(14).Trim()) ;

                Console.Write(j + " ");
                j++;

                for(int i=0;i<p1.Length;i++)
                {
                    if(p1[i] > p2[i])
                    {
                        count++;
                        Formulas.OutputPoker(i, p1, p2);
                        break;
                    }
                    else if (p1[i] < p2[i])
                    {
                        Formulas.OutputPoker(i, p1, p2);
                        break;
                    }
                }
            }

            return count;

        }

        public static long Problem59()
        {
            int[] message = System.IO.File.ReadAllText(@"res\p059_cipher.txt").Split(',').Select(int.Parse).ToArray();
            int[] key = Formulas.Analysis(message, 3);

            int[] res = Formulas.Encrypt(message, key);
            long r = 0L;
            for(int i=0;i<res.Length;i++)
            {
                Console.Write((char)res[i]);
                r += res[i];
            }

            return r;
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

        public static long Problem76()
        {
            int sum = 5;
            long count = 0L;


            return count;
        }

        public static long Problem79()
        {
            /*
            A common security method used for online banking is to ask the user for three random characters from a passcode.For example, if the passcode was 531278, they may ask for the 2nd, 3rd, and 5th characters; the expected reply would be: 317.

            The text file, keylog.txt, contains fifty successful login attempts.

            Given that the three characters are always asked for in order, analyse the file so as to determine the shortest possible secret passcode of unknown length.
            */
            IEnumerable<string> passcodes = System.IO.File.ReadLines(@"res\p079_keylog.txt");
            Dictionary<string, int> count = new Dictionary<string, int>();

            long res = 0L;
            string password = "";

            foreach(string pass in passcodes)
            {
                int idx = 0;
                foreach (char c in pass)
                {
                    int i = int.Parse(c.ToString());
                    if(!password.Contains(c.ToString()))
                    {
                        password = password + c.ToString();
                        idx = password.Length - 1;
                    }
                    else
                    {
                        idx = password.IndexOf(c,idx);

                        if(idx < 0)
                        {
                            password = password+= c.ToString();
                            idx = password.Length - 1;
                        }
                    }
                }
            }

            Console.WriteLine(password);

            return 0;
        }

        public static long Problem81()
        {
            /*
            In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, by only moving to the right and down, is indicated in bold red and is equal to 2427.
            Find the minimal path sum, in matrix.txt (right click and "Save Link/Target As..."), a 31K text file containing a 80 by 80 matrix, from the top left to the bottom right by only moving right and down.
            */
            IEnumerable<string> m = System.IO.File.ReadLines(@"res\p081_matrix.txt");
            int l = m.Count();
            int c = m.ElementAt(0).Split(',').Count();
            int[,] matrix = new int[l,c];

            //Matrice
            for(int i=0;i<l;i++)
                for(int j=0;j<c;j++)
                    matrix[i,j] = int.Parse(m.ElementAt(i).Split(',').ElementAt(j));


            //Chemin
            for (int i = l - 1; i >= 0; i--)
                for (int j = c - 1; j >= 0; j--)
                    if ((i + 1 >= l) && (j + 1 >= c))
                        matrix[i, j] = matrix[i, j];
                    else if (i + 1 >= l)
                        matrix[i, j] += matrix[i, j + 1];
                    else if (j + 1 >= c)
                        matrix[i, j] += matrix[i + 1, j];
                    else
                        matrix[i, j] += Math.Min(matrix[i + 1, j], matrix[i, j + 1]);


            return matrix[0, 0];
        }        

        public static long Problem92()
        {
            /*
            A number chain is created by continuously adding the square of the digits in a number to form a new number until it has been seen before.

            For example,

            44 → 32 → 13 → 10 → 1 → 1
            85 → 89 → 145 → 42 → 20 → 4 → 16 → 37 → 58 → 89

            Therefore any chain that arrives at 1 or 89 will become stuck in an endless loop. What is most amazing is that EVERY starting number will eventually arrive at 1 or 89.

            How many starting numbers below ten million will arrive at 89?
            */
            Dictionary<int, int> loopingNumbers = new Dictionary<int, int>();
            loopingNumbers.Add(1,1);
            loopingNumbers.Add(89,89);

            long res = 0L;

            for(int i=1;i<10000000;i++)
            {
                int buf = i;
                List<int> bufferNums = new List<int>();

                while (!loopingNumbers.ContainsKey(buf))
                {
                    int j = 0;
                    foreach (char c in buf.ToString())
                    {
                        if (j == 0)
                            buf = 0;
                        int k = c - 48;
                        buf += k * k;
                        j++;
                    }

                    bufferNums.Add(buf);
                }

                if(buf != i)
                    loopingNumbers.Add(i,buf);

                if (buf == 89)
                {
                    res++;
                }
                
            }

            return res;
        }

        public static long Problem206()
        {
            //Smarter bruteforce (387 420 489) possibilities
            Stopwatch sw = new Stopwatch();
            sw.Start();

            decimal start = Math.Floor(Formulas.Sqrt(10203040506070809.0m) / 10.0m);
            decimal end =   Math.Floor(Formulas.Sqrt(19293949596979899.0m) / 10.0m);
            long dr = -1L;

            RegexUtil.setExpression("1.2.3.4.5.6.7.8.9");

            for (long i = (long)start; i < (long)end; i++)
            {
                dr = i * 10 + 3;
                if (RegexUtil.isMatching((dr * dr).ToString()))
                    break;

                dr = i * 10 + 7;
                if (RegexUtil.isMatching((dr * dr).ToString()))
                    break;

                if(i % 10000 == 0)
                    Console.WriteLine(((i - start) / (end - start) * 100.0m) + "%");
            }
            sw.Stop();

            return (long)(dr*10.0m);
        }
    }
}
