namespace MarkovChain.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class PredictionProvider
    {
        private readonly ChainWorker _chainWorker;
        public PredictionProvider(ChainWorker worker)
        {
            _chainWorker = worker;
        }

        public virtual int Predict()
        {
            var lastItem = _chainWorker.GetData()[_chainWorker.GetData().Count - 1];
            var lastItemState = lastItem.GetState();
            var listNumbersPositions = new List<double>();
            for (int i = 0; i < _chainWorker.CountStates; i++)
            {
                listNumbersPositions.Add(_chainWorker.BuildChainMatrix()[lastItemState, i]);
            }
            var nextState = listNumbersPositions.IndexOf(listNumbersPositions.Max());
            return nextState;
        }
    }
}
