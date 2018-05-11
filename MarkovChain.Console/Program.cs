namespace MarkovChain.Console
{
    using MarkovChain.Core;
    using System;
    using System.Collections.Generic;
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("number of elements to generate: ");
            int count = Convert.ToInt32(Console.ReadLine());            

            BreakdownEngine engine = new BreakdownEngine(GenerateTestData(count));
            var breakdowns = engine.GetBreakdowns();
            ChainWorker worker = new ChainWorker(breakdowns);
            worker.BuildChainMatrix();
            var predictionProvider = new KolmogorovPedictionProvider(worker);
            string result = Model.ParseNextPosition(predictionProvider.Predict());
            Console.WriteLine($"Next breakdown is: {result}");
            Console.ReadLine();
        }

        static List<double> GenerateTestData(int count)
        {

             var randomList = new List<double>();

             Random randNum = new Random();
            /*for (int i = 0; i < count; i++)
            {
               randomList.Add(i);
            }*/
            randomList.Add(1);
            randomList.Add(5);
            randomList.Add(3);
            randomList.Add(4);
            randomList.Add(2);
            return randomList;
        }
    }
}
