using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spizL
{
    class Program
    {
        static void Main(string[] args)
        {
            //spizL.Run(args[0]);
            Console.WriteLine(".");
            spizL spizl = new spizL("spizl/_exp.spiz");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            spizl.Run();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms elapsed");
            Console.ReadLine();
        }
    }
}
