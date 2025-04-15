using System;


namespace FSystem
{
    /// <summary>
    /// オブジェクトプールパターン用のオブジェクト側の共通インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TObject">自分自身のデータ型</typeparam>
    public interface IPoolableObject<out TObject>
    {
        /// <summary>
        /// 自身を複製する
        /// </summary>
        /// <param name="instanceNumber">1から始まるインスタンス番号</param>
        /// <returns>自身の複製体</returns>
        public TObject Clone(int instanceNumber);
        /// <summary>
        /// このオブジェクトを有効化する
        /// </summary>
        /// <param name="disableCallback">非有効化するためのコールバック関数</param>
        public void Enable(Action<TObject> disableCallback);
    }
}