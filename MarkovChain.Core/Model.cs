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

        public int GetState()
        {
            if (_isTopPosition)
            {
                return 1;
            }
            else if (_isCurrentPosition)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }

        public static int CountStates
        {
            get
            {
                return 3;
            }
        }

        public static string ParseNextPosition(int position)
        {
            switch (position)
            {
                case 1:
                    {
                        return "Top";
                    }
                case 2:
                    {
                        return "Bottom";
                    }
                default:
                    {
                        return "Current";
                    }
            }
        }
    }
}
