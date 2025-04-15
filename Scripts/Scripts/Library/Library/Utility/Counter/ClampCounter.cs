using System;


namespace FSystem
{
    /// <summary>
    /// 値が最小値と最大値の範囲で固定されるカウンタ、最大値を越しても最大値までの値で止まる
    /// </summary>
    /// <remarks>製作者 : 石井隼人</remarks>
    public class ClampCounter : Counter
    {
        private int _additionValue;

        /// <summary> カウントの追加量 </summary>
        public int AdditionValue { get => _additionValue; set => _additionValue = Math.Abs(value); }


        /// <summary>
        /// 値が最小値と最大値の範囲で固定されるカウンタ、最大値を越しても最大値までの値で止まる
        /// </summary>
        /// <remarks>製作者 : 石井隼人</remarks>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        /// <param name="additionValue">カウントの追加量</param>
        public ClampCounter(int min, int max, int additionValue = 1) : base(min, max)
        {
            AdditionValue = additionValue;
        }

        /// <summary>
        /// 一つ次の値を計算する
        /// </summary>
        /// <returns>計算後の値</returns>
        public override int ToNext()
        {
            Position += AdditionValue;
            if (Position > Max)
            {
                Position = Max;
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
            if (Position < Min)
            {
                Position = Min;
            }
            return Position;
        }
    }
}