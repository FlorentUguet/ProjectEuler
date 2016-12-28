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
