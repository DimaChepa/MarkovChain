namespace MarkovChain.Core
{
    using System.Collections.Generic;
    
    public class ChainWorker
    {
        private IList<double> _data;
        private IList<Model> _predictSet;
        public ChainWorker(IList<double> data)
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
                if (_data[i] > _data[i - 1])
                {
                    _predictSet.Add(new Model(false, false, true));
                }
                else if (_data[i] < _data[i - 1])
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
