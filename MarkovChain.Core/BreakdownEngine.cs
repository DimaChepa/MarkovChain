namespace MarkovChain.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BreakdownEngine
    {
        private const int _timePeriod = 5;
        private IList<double> _data;
        public BreakdownEngine(IList<double> data)
        {
            _data = data;
        }
        public virtual IList<double> GetBreakdowns()
        {
            var count = GetBreakDownsCount();
            var listOfBreakdowns = new List<double>();
            for (int i = 0; i < count; i++)
            {
                var listForCaclucate = new List<double>();
                for (int j = i * _timePeriod; j < (i + 1) * _timePeriod; j++)
                {
                    listForCaclucate.Add(_data[j]);
                }
                listOfBreakdowns.Add(CalculateBreakdown(listForCaclucate));
            }

            return listOfBreakdowns;
        }

        private double CalculateBreakdown(IList<double> data)
        {
            // coefficients of the equation of the line
            double a = data[0] - data[_timePeriod - 1];
            double b = _timePeriod - 1;
            double c =  data[_timePeriod - 1] - data[0]*_timePeriod;

            var listOfDistances = new List<double>();
            for (int i = 0; i < data.Count; i++)
            {
                listOfDistances.Add(Math.Abs(a * (i + 1) + b * data[i] + c) / (Math.Sqrt(a * a + b * b)));
            }
            return data.ElementAt(listOfDistances.IndexOf(listOfDistances.Max()));
        }

        private int GetBreakDownsCount()
        {
            return _data.Count / _timePeriod;
        }
    }
}
