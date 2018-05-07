namespace MarkovChain.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class PredictionProvider
    {
        protected readonly ChainWorker _chainWorker;
        protected double[,] _chainMatrix;
        public PredictionProvider(ChainWorker worker)
        {
            _chainWorker = worker;
            _chainMatrix = _chainWorker.BuildChainMatrix();
        }

        public virtual int Predict()
        {
            var lastItem = _chainWorker.GetData()[_chainWorker.GetData().Count - 1];
            var lastItemState = lastItem.GetState();
            var listNumbersPositions = new List<double>();
            for (int i = 0; i < _chainWorker.CountStates; i++)
            {
                listNumbersPositions.Add(_chainMatrix[lastItemState, i]);
            }
            var nextState = listNumbersPositions.IndexOf(listNumbersPositions.Max());
            return nextState;
        }
    }
}
