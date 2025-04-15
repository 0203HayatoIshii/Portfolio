

namespace FSystem
{
    /// <summary>
    /// ステートを運用するクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TStateType">ステートの識別子</typeparam>
    public interface IStateMachine<TStateType>
    {
        /// <summary>
        /// 新しいステートを追加する
        /// </summary>
        /// <param name="newState">追加するステートのインスタンス</param>
        public void AddState(IState<TStateType> newState);
        /// <summary>
        /// 更新するステートを変更する
        /// </summary>
        /// <param name="targetType">更新先のステートの識別子</param>
        /// <returns>変更に成功したとき => true | 変更に失敗したとき => false</returns>
        public bool ChangeState(TStateType targetType);
        /// <summary>
        /// 新しいステートを追加し、現在のステートを追加したステートに変更する
        /// </summary>
        /// <param name="targetState">追加するステートのインスタンス</param>
        /// <returns>変更に成功したとき => true | 変更に失敗したとき => false</returns>
        public bool ChangeState(IState<TStateType> targetState);
    }
}