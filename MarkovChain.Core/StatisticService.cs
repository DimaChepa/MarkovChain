using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChain.Core
{
    public class StatisticService
    {
        private IList<double> _data;
        public StatisticService(IList<double> data)
        {
            _data = data;
        }

        public virtual double GetDeviation()
        {
            double sum = 0;
            var average = _data.Average();
            for (int i = 0; i < _data.Count; i++)
            {
                sum += Math.Pow(_data[i] - average, 2);
            }
            sum = sum / (double)_data.Count;

            return Math.Sqrt(sum);
        }
    }
}
