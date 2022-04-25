using System;
using HC.EF.Sqlite.Core5;

namespace HC.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new Runner().Run();

            System.Console.WriteLine("Finish");
            System.Console.ReadLine();
        }
    }
}