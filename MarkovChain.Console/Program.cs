namespace MarkovChain.Console
{
    using MarkovChain.Core;
    using System;
    using System.Collections.Generic;
    class Program
    {
        static void Main(string[] args)
        {
            ChainWorker worker = new ChainWorker(GenerateTestData(100));
            worker.BuildChainMatrix();
            PredictionProvider predictionProvider = new PredictionProvider(worker);
            string result = Model.ParseNextPosition(predictionProvider.Predict());
            Console.WriteLine(result);
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
