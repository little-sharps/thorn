using System;
using System.Collections.Generic;
using System.Text;

namespace ThornPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Found the following arguments:");
            for(var i = 0; i < args.Length; i++)
            {
                Console.WriteLine("args[{0}] = {1}", i, args[i]);
            }
            Console.WriteLine();
        }
    }
}
