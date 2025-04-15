

namespace FSystem
{
    /// <summary>
    /// 数値のカウントを行うクラスのベースクラス
    /// </summary>
    /// <remarks>製作者 : 石井隼人</remarks>
    public abstract class Counter : ICountable
    {
        private int _max;
        private int _min;


        /// <summary> 現在の数値 </summary>
        public int Position { get; protected set; }

        /// <summary> 最大値 </summary>
        public int Max 
        {
            get => _max; 
            set
            {
                if (value < _min)
                {
                    _max = _min;
                    _min = value;
                }
                else
                {
                    _max = value;
                }

                if (Position > _max)
                {
                    Position = _max;
                }
            }
        }

        /// <summary> 最小値 </summary>
        public int Min
        {
            get => _min;
            set
            {
                if (value > _max)
                {
                    _min = _max;
                    _max = value;
                }
                else
                {
                    _min = value;
                }

                if (Position < _min)
                {
                    Position = _min;
                }
            }
        }


        /// <summary>
        /// 数値のカウントを行うクラスのベースクラス
        /// </summary>
        public Counter(int min, int max)
        {
            Min = min;
            Max = max;
            Position = Min;
        }

        /// <summary>
        /// 一つ次の値を計算する
        /// </summary>
        /// <returns>計算後の値</returns>
        public abstract int ToNext();
        /// <summary>
        /// 一つ前の値を計算する
        /// </summary>
        /// <returns>計算後の値</returns>
        public abstract int ToPrev();
    }
}