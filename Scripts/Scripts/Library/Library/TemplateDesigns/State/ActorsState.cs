#if UNITY_64

using UnityEngine;


namespace FSystem
{
    /// <summary>
    /// アクター用のステートのベースクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TStateType">任意のステート識別子</typeparam>
    public abstract class ActorsState<TStateType> : MonoBehaviour, IState<TStateType>
    {
        /// <summary> このステートを運用しているインスタンス </summary>
        public IStateMachine<TStateType> Master { get; private set; }
        /// <summary> このステートの識別子 </summary>
        public abstract TStateType StateType { get; }

        /// <summary>
        /// このステートに侵入したときに一回だけ呼ばれる関数
        /// </summary>
        /// <param name="master">このステートを運用しているマシンのインスタンス</param>
        public virtual void OnEnter(IStateMachine<TStateType> master) { Master = master; }
        /// <summary>
        /// このステートにいる間呼ばれる更新関数
        /// </summary>
        public virtual void OnStay() { }
        /// <summary>
        /// このステートから抜けるときに一度だけ呼ばれる関数
        /// </summary>
        public virtual void OnExit() { }

        /// <summary>
        /// 指定された識別子のステートに変更する
        /// </summary>
        /// <param name="targetType">変更先の識別子</param>
        /// <returns>変更に成功したとき => true | 変更に失敗したとき => false</returns>
        protected bool ChangeState(TStateType targetType) => Master?.ChangeState(targetType) ?? false;
    }
}

#endif