using System;


namespace FSystem
{
    /// <summary>
    /// 最小値から最大値を一定方向に進むカウンタ、最大値を越したときに最小値に戻る
    /// </summary>
    /// <remarks>製作者 : 石井隼人</remarks>
    public class LoopCounter : Counter
    {
        private int _additionValue;

        public int AdditionValue { get => _additionValue; set => _additionValue = Math.Abs(value); }

        /// <summary>
        /// 最小値から最大値を一定方向に進むカウンタ、最大値を越したときに最小値に戻る
        /// </summary>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        public LoopCounter(int min, int max, int addition = 1) : base(min, max) 
        { 
            AdditionValue = addition;
        }

        /// <summary>
        /// 一つ次の値を計算する
        /// </summary>
        /// <returns>計算後の値</returns>
        public override int ToNext()
        {
            Position += AdditionValue;
            while (Position > Max)
            {
                Position = Min + (Position - (Max + 1));
            }
            return Position;
        }

        /// <summary>
        /// 一つ前の値を計算する
        /// </summary>
        /// <returns>計算後の値</returns>
        public override int ToPrev()
        {
            Position -= AdditionValue;
            while (Position < Min)
            {
                Position = Max - Math.Abs(Position - (Min - 1));
            }
            return Position;
        }
    }
}