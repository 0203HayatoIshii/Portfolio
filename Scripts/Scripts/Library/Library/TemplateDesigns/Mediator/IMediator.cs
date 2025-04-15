

namespace FSystem
{
    /// <summary>
    /// メディエイターパターン用のメディエイター側の共通インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public interface IMediator
    {
        /// <summary>
        /// コンポーネントから通知を受け取る
        /// </summary>
        /// <param name="component">通知元のコンポーネント</param>
        /// <param name="messageHandle">メッセージの種類</param>
        public void Notice(IMediatorComponent component, HANDLE messageHandle);
    }

    /// <summary>
    /// メディエイターパターン用のメディエイター側の共通インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TMessage">メッセージに使用するデータ型</typeparam>
    public interface IMediator<TMessage>
    {
        /// <summary>
        /// コンポーネントから通知を受け取る
        /// </summary>
        /// <param name="component">通知元のコンポーネント</param>
        /// <param name="message">メッセージの種類</param>
        public void Notice(IMediatorComponent component, TMessage message);
    }
}