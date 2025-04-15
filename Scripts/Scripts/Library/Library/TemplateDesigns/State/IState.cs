

namespace FSystem
{
    /// <summary>
    /// ステートパターン用の共通インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TStateType">ステートの識別子</typeparam>
    public interface IState<TStateType>
    {
        /// <summary> このステートの識別子 </summary>
        public TStateType StateType { get; }

        /// <summary>
        /// このステートに侵入したときに一回だけ呼ばれる関数
        /// </summary>
        /// <param name="master">このステートを運用しているマシンのインスタンス</param>
        public void OnEnter(IStateMachine<TStateType> master);
        /// <summary>
        /// このステートにいる間呼ばれる更新関数
        /// </summary>
        public void OnStay();
        /// <summary>
        /// このステートから抜けるときに一度だけ呼ばれる関数
        /// </summary>
        public void OnExit();
    }
}