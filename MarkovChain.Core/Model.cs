namespace MarkovChain.Core
{
    public class Model
    {
        private bool _isCurrentPosition;
        private bool _isBottomPosition;
        private bool _isTopPosition;
        public Model(bool isCurrentPostion, bool isBottomPostion, bool isTopPostion)
        {
            _isCurrentPosition = isCurrentPostion;
            _isBottomPosition = isBottomPostion;
            _isTopPosition = isTopPostion;
        }
        public bool IsCurrentPosition
        {
            get
            {
                return _isCurrentPosition;
            }
        }
        public bool IsBottomPosition
        {
            get
            {
                return _isBottomPosition;
            }
        }
        public bool IsTopPosition
        {
            get
            {
                return _isTopPosition;
            }
        }
    }
}
