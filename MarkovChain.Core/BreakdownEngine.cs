namespace MarkovChain.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BreakdownEngine
    {
        private IList<double> _data;
        private Dictionary<A, B> _dictionary;
        private StatisticService _statisticService;
        public BreakdownEngine(IList<double> data) : this(data, new StatisticService(data))
        {
            _data = data;
            _dictionary = new Dictionary<A, B>();
        }

        public BreakdownEngine(IList<double> data, StatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public virtual IList<BreakDownModel> GetBreakdowns()
        {
            var startLimit = 0;
            var endLimit = _data.Count - 1;
            AddMaxBreakdownToDictionary(startLimit, endLimit, 1, 1);
            var listPos = new List<int>() { 1 };
            for (int i = 2; i < _data.Count; i++)
            {
                var count = listPos.Last();
                int countParentNodes = 0;
                for (int j = 1; j <= count; j += 1)
                {
                    try
                    {
                        var kvp = _dictionary.Where(x => x.Key.B.Contains($"L{i - 1}P{j}")).First();

                        var array = ParseKey(kvp.Key.B);
                        for (int k = 0; k < 2; k++)
                        {
                            if (k == 0)
                            {
                                startLimit = array[0];
                                endLimit = array[0] + kvp.Value.A;
                            }
                            else
                            {
                                startLimit = array[0] + kvp.Value.A;
                                endLimit = array[1];
                            }
                            if (array[1] - array[0] > 1)
                            {
                                countParentNodes++;
                                AddMaxBreakdownToDictionary(startLimit, endLimit, i, countParentNodes);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                listPos.Add(countParentNodes);
            }

            var dictionaryWithoutAnomals = GetDictionaryWithoutAnomalValues();
            var listBreakdowns = SortBreakDownDictionary(dictionaryWithoutAnomals);
            return listBreakdowns;
        }

        private List<BreakDownModel> SortBreakDownDictionary(Dictionary<A, B> dictionary)
        {
            var dict = dictionary.OrderBy(x => ParseKey(x.Key.B)[0] + x.Value.A);
            var newList = new List<BreakDownModel>();
            foreach (var item in dict)
            {
                newList.Add(new BreakDownModel { Perpendicular = item.Value.C, Position = ParseKey(item.Key.B)[0] + item.Value.A });
            }
            return newList;
        }

        private Dictionary<A, B> GetDictionaryWithoutAnomalValues()
        {
            var deviation = 0.005;
            Dictionary<A, B> resultDictionary = new Dictionary<A, B>();
            foreach (var kp in _dictionary)
            {
                if (kp.Value.C > deviation)
                {
                    resultDictionary.Add(kp.Key, kp.Value);
                }
            }
            return resultDictionary;
        }

        private int[] ParseKey(string key)
        {
            key = key.Split('L')[0];
            string endPos = key.Split('E')[1];
            string startPos = key.Split('E')[0];
            startPos = startPos.Split('S')[1];
            var intArray = new int[2];
            intArray[0] = Convert.ToInt32(startPos);
            intArray[1] = Convert.ToInt32(endPos);
            return intArray;
        }

        private void AddMaxBreakdownToDictionary(int startLimit, int endLimit, int level, int position)
        {
            var listBreakdowns = CalculateBreakdown(startLimit, endLimit);
            var max = listBreakdowns.IndexOf(listBreakdowns.Max());
            _dictionary.Add(new A { B = $"S{startLimit}E{endLimit}L{level}P{position}" }, new B { A = max, C = listBreakdowns.Max() });
        }
        private List<double> CalculateBreakdown(int start, int end)
        {
            // coefficients of the equation of the line
            double a = _data[end] - _data[start];
            double b = start - end;
            double c = -start * (_data[end] + _data[start]) + _data[start] * (start + end);

            var listOfDistances = new List<double>();
            for (int i = start; i <= end; i++)
            {
                listOfDistances.Add(Math.Abs(a * i + b * _data[i] + c) / (Math.Sqrt(a * a + b * b)));
            }

            return listOfDistances;
        }
    }

    class A
    {
        public string B { get; set; }
    }

    public class B
    {
        public int A { get; set; }
        public double C { get; set; }
    }

    public class BreakDownModel
    {
        public double Perpendicular { get; set; }
        public int Position { get; set; }
    }
}
