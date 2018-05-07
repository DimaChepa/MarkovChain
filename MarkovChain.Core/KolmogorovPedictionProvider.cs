using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChain.Core
{
    public class KolmogorovPedictionProvider : PredictionProvider
    {
        float[,] matrix;
        int numberColumns;
        int numberRows;
        public KolmogorovPedictionProvider(ChainWorker worker) : base(worker)
        {
        }

        public override int Predict()
        {
            var Numer = (_chainMatrix[0, 2] * _chainMatrix[1, 2] + _chainMatrix[0, 2] * _chainMatrix[2, 0] + _chainMatrix[1, 0] * _chainMatrix[2, 0]);
            var Denumer = _chainMatrix[0, 1] * _chainMatrix[1, 0] + _chainMatrix[0, 1] * _chainMatrix[1, 2] + _chainMatrix[0, 2] * _chainMatrix[1, 2]
                + _chainMatrix[0, 1] * _chainMatrix[2, 0] + _chainMatrix[0, 2] * _chainMatrix[2, 0] + _chainMatrix[1, 0] * _chainMatrix[2, 0]
                + _chainMatrix[0, 2] * _chainMatrix[2, 1] + _chainMatrix[1, 0] * _chainMatrix[2, 1] + _chainMatrix[1, 2] * _chainMatrix[2, 1];

            var p1 = Numer / (double)Denumer;

            Numer = (_chainMatrix[0, 1] - _chainMatrix[1, 2]) * _chainMatrix[2, 1] + _chainMatrix[0, 1] * (_chainMatrix[1, 2] + _chainMatrix[2, 0] + _chainMatrix[2, 1]);
            Denumer = (_chainMatrix[0, 1] - _chainMatrix[1, 2]) * (_chainMatrix[2, 1] - _chainMatrix[1, 0]) - (_chainMatrix[0, 1] + _chainMatrix[0, 2] + _chainMatrix[1, 0]) * (_chainMatrix[1, 2] + _chainMatrix[2, 0] + _chainMatrix[2, 1]);
            var p0 = Numer / (double)Denumer;

            Numer = (_chainMatrix[0, 1] * _chainMatrix[1, 0] + _chainMatrix[0, 2] * _chainMatrix[2, 1] + _chainMatrix[1, 0] * _chainMatrix[2, 1]);
            Denumer = _chainMatrix[0, 1] * _chainMatrix[1, 0] + _chainMatrix[0, 1] * _chainMatrix[1, 2] + _chainMatrix[0, 2] * _chainMatrix[1, 2]
                + _chainMatrix[0, 1] * _chainMatrix[2, 0] + _chainMatrix[0, 2] * _chainMatrix[2, 0] + _chainMatrix[1, 0] * _chainMatrix[2, 0]
                + _chainMatrix[0, 2] * _chainMatrix[2, 1] + _chainMatrix[1, 0] * _chainMatrix[2, 1] + _chainMatrix[1, 2] * _chainMatrix[2, 1];

            var p2 = Numer / (double)Denumer;

            var listProbs = new List<double> { p0, p1, p2 };

            return listProbs.IndexOf(listProbs.Max());
        }
    }
}

