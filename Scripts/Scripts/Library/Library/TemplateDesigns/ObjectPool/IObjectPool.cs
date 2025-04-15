

namespace FSystem
{
    /// <summary>
    /// オブジェクトプールパターン用のプール側の共通インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TObject">プールする対象のデータ型</typeparam>
    public interface IObjectPool<out TObject>
    {
        /// <summary> 複製元のオブジェクト </summary>
        public TObject Origin { get; }
        /// <summary> プールに残っているオブジェクトの数 </summary>
        public int RemainCount { get; }
        /// <summary> 現在のプールの大きさ </summary>
        public int PoolSize { get; }

        /// <summary>
        /// プールからオブジェクトを取り出す
        /// </summary>
        /// <remarks>プールが空の場合は新たな複製を作成する</remarks>
        /// <returns>取り出されたオブジェクト</returns>
        public TObject Takeout();
        /// <summary>
        /// プールからオブジェクトを取り出す
        /// </summary>
        /// <remarks>プールが空の場合は新たな複製を作成する</remarks>
        /// <param name="value">取り出すオブジェクトの数</param>
        /// <returns>取り出されたオブジェクトの配列</returns>
        public TObject[] Takeout(int value);
    }
}