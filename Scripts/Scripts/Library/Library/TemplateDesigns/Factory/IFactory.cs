

namespace FSystem
{
    /// <summary>
    /// ファクトリパターン用のファクトリ側の共通インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TProduct">作成するデータの型</typeparam>
    public interface IFactory<out TProduct>
    {
        /// <summary>
        /// 新たなインスタンスを作成する
        /// </summary>
        /// <returns>作成されたインスタンス</returns>
        public TProduct Generate();
    }
}