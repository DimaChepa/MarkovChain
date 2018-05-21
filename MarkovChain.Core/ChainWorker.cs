namespace MarkovChain.Core
{
    using System.Collections.Generic;
    
    public class ChainWorker
    {
        private IList<BreakDownModel> _data;
        private IList<Model> _predictSet;
        public ChainWorker(IList<BreakDownModel> data)
        {
            _data = data;
            Activate();
        }

        private void Activate()
        {
            _predictSet = new List<Model>();
            _predictSet.Add(new Model(true, false, false));
            for (int i = 1; i < _data.Count; i++)
            {
                if (_data[i].Perpendicular > _data[i - 1].Perpendicular)
                {
                    _predictSet.Add(new Model(false, false, true));
                }
                else if (_data[i].Perpendicular < _data[i - 1].Perpendicular)
                {
                    _predictSet.Add(new Model(false, true, false));
                }
                else
                {
                    _predictSet.Add(new Model(true, false, false));
                }
            }
        }

        public virtual double[,] BuildChainMatrix()
        {
            double[,] array = new double[3, 3];
            array[0, 0] = 1;
            for (int i = 1; i < _predictSet.Count; i++)
            {
                array[_predictSet[i - 1].GetState(), _predictSet[i].GetState()]++;
            }

            for (int i = 0; i < 3; i++)
            {
                double sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum += array[i, j];
                }
                for (int j = 0; j < 3; j++)
                {
                    array[i, j] = array[i, j] / sum;
                    if (double.IsNaN(array[i, j]))
                    {
                        array[i, j] = 0;
                    }
                }
            }

            return array;
        }

        public virtual int CountStates
        {
            get
            {
                return Model.CountStates;
            }
        }

        public virtual IList<Model> GetData()
        {
            return _predictSet;
        }
    }
}
