

namespace FSystem
{
    /// <summary>
    /// プロトタイプパターン用のプロトタイプ側の共通インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="T">複製する対象のデータ型</typeparam>
    public interface ICloneable<out T>
    {
        /// <summary>
        /// オブジェクトを複製する
        /// </summary>
        /// <returns>複製されたオブジェクト</returns>
        public T Clone();
    }
}