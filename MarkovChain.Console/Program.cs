namespace MarkovChain.Console
{
    using MarkovChain.Core;
    using System;
    using System.Collections.Generic;
    class Program
    {
        static void Main(string[] args)
        {
            List<double> list = new List<double>() { 1, 2, 3, 4, 5 };
            ChainWorker worker = new ChainWorker(list);
            worker.Activate();
            worker.BuildChainMatrix();
            Console.WriteLine("Hello World!");
        }
    }
}
