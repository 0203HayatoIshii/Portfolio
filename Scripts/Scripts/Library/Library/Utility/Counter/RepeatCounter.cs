

namespace FSystem
{
    /// <summary>
    /// 最小値から最大値の間を往復するカウンタ
    /// </summary>
    /// <remarks>製作者 : 石井隼人</remarks>
    public class RepeatCounter : Counter
    {
        private int _addNumber;


        /// <summary>
        /// 最小値から最大値の間を往復するカウンタ
        /// </summary>
        /// <remarks>製作者 : 石井隼人</remarks>
        public RepeatCounter(int min, int max) : base(min, max)
        {
            _addNumber = -1;
        }

        /// <summary>
        /// 一つ次の値を計算する
        /// </summary>
        /// <returns>計算後の値</returns>
        public override int ToNext()
        {
            if ((Position >= Max) || (Position <= Min))
            {
                _addNumber *= -1;
            }
            Position += _addNumber;
            return Position;
        }

        /// <summary>
        /// 一つ前の値を計算する
        /// </summary>
        /// <returns>計算後の値</returns>
        public override int ToPrev()
        {
            if ((Position >= Max) || (Position <= Min))
            {
                _addNumber *= -1;
            }
            Position -= _addNumber;
            return Position;
        }
    }
}