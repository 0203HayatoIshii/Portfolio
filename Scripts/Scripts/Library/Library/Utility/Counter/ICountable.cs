

namespace FSystem
{
    /// <summary>
    /// 数値のカウントを行うインターフェース
    /// </summary>
    /// <remarks>製作者 : 石井隼人</remarks>
    public interface ICountable
    {
        /// <summary> 現在の数値 </summary>
        public int Position { get; }
        /// <summary>
        /// 一つ次の値を計算する
        /// </summary>
        /// <returns>計算後の数値</returns>
        public int ToNext();
        /// <summary>
        /// 一つ前の値を計算する
        /// </summary>
        /// <returns>計算後の数値</returns>
        public int ToPrev();
    }
}