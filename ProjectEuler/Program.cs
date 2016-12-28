using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectEuler
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Out.Write("Problem Number : ");
            int problem = Int32.Parse(Console.ReadLine());

            Problems p = new Problems();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            long res = p.Problem(problem);
            watch.Stop();

            Console.Out.WriteLine("Result : " + res.ToString() + " in " + watch.ElapsedMilliseconds + " ms");

            Clipboard.SetText(res.ToString());

            Console.ReadLine();
        }
    }
}
