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
        }

        public void Activate()
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

        public void Calculate()
        {

        }

        public IList<Model> GetData()
        {
            return _predictSet;
        }
    }
}
