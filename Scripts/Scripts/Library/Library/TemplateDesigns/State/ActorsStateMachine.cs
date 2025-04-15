#if UNITY_64

using System.Collections.Generic;

using UnityEngine;


namespace FSystem
{
    /// <summary>
    /// ステートマシン用のステートクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TStateType">任意のステート識別子</typeparam>
    /// <typeparam name="TEqualityComparer">任意のステート識別子を等価比較演する算子</typeparam>
    public abstract class ActorsStateMachine<TStateType, TEqualityComparer> : MonoBehaviour, IStateMachine<TStateType> where TEqualityComparer : IEqualityComparer<TStateType>, new()
    {
        [SerializeField]
        private List<ActorsState<TStateType>> _stateList;
        private List<IState<TStateType>> _runtimeStateList;
        private IState<TStateType> _reservedState;
        private TEqualityComparer _operator;

        /// <summary> 現在更新中のステート </summary>
        public IState<TStateType> CurrentState { get; private set; }



        /// <summary>
        /// 更新するステートを変更する
        /// </summary>
        /// <param name="targetType">更新先のステートの識別子</param>
        /// <returns>変更に成功したとき => true | 変更に失敗したとき => false</returns>
        public bool ChangeState(TStateType stateType)
        {
            _operator ??= new TEqualityComparer();

            // 登録リストから検索し、なければ失敗
            int hitIdx = _runtimeStateList?.FindIndex((obj) => _operator.Equals(obj.StateType, stateType)) ?? -1;
            if (hitIdx < 0)
                return false;

            // ヒットしたステートを予約キャッシュに入れる
            _reservedState = _runtimeStateList[hitIdx];
            return true;
        }
        /// <summary>
        /// 新しいステートを追加し、現在のステートを追加したステートに変更する
        /// </summary>
        /// <param name="targetState">追加するステートのインスタンス</param>
        /// <returns>変更に成功したとき => true | 変更に失敗したとき => false</returns>
        public bool ChangeState(IState<TStateType> state)
        {
            if ((state == null) || (CurrentState == state))
                return false;

            // 指定されたステートを予約キャッシュに入れる
            _reservedState = state;
            return true;
        }
        /// <summary>
        /// 新しいステートを追加する
        /// </summary>
        /// <param name="newState">追加するステートのインスタンス</param>
        public void AddState(IState<TStateType> newState)
        {
            _operator ??= new TEqualityComparer();

            // 同じ識別子のステートがあれば失敗
            if ((newState != null) && !(_runtimeStateList?.Exists((obj) => _operator.Equals(obj.StateType, newState.StateType)) ?? false))
            {
                // 引数のステートをリストに追加
                _runtimeStateList ??= new List<IState<TStateType>>(1);
                _runtimeStateList.Add(newState);
            }
        }

        /// <summary>
        /// ステートマシンの更新関数
        /// </summary>
        protected void UpdateState()
        {
            Init();
            ChangeState();

            CurrentState?.OnStay();
        }
        /// <summary>
        /// ステートマシンを初期化する
        /// </summary>
        /// <remarks>必ず開始時に一回だけ呼ぶこと</remarks>
        /// <param name="operator">ステートの一致を確認する等価演算子</param>
        /// <exception cref="ArgumentNullException">"operator"がnull</exception>
        private void Init()
        {
            if (_runtimeStateList != null)
                return;

            // unityEditor用のステートリストをランタイムのステートリストに追加
            _runtimeStateList = _stateList?.ConvertAll((old) => (IState<TStateType>)old) ?? new List<IState<TStateType>>();
            if (_runtimeStateList.Count > 0)
            {
                CurrentState = _runtimeStateList[0];
                CurrentState.OnEnter(this);
            }

            _operator ??= new TEqualityComparer();
        }
        /// <summary>
        /// 変更が予約されているときにステートを変更する
        /// </summary>
        private void ChangeState()
        {
            if (_reservedState == null)
                return;

            CurrentState?.OnExit();
            _reservedState.OnEnter(this);
            CurrentState = _reservedState;
            _reservedState = null;
        }
    }
}

#endif