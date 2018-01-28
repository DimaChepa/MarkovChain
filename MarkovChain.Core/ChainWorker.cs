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

        }

        public void Calulate()
        {

        }

        public IList<Model> GetData()
        {
            return _predictSet;
        }
    }
}
