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
            PredictionProvider predictionProvider = new PredictionProvider(worker);
            string result = Model.ParseNextPosition(predictionProvider.Predict());
            Console.WriteLine($"Next breakdown is: {result}");
            Console.ReadLine();
        }

        static List<double> GenerateTestData(int count)
        {

             var randomList = new List<double>();

             Random randNum = new Random();
             for (int i = 0; i < count; i++)
             {
                 randomList.Add(randNum.NextDouble() * 100);
             }
            return randomList;
        }
    }
}
