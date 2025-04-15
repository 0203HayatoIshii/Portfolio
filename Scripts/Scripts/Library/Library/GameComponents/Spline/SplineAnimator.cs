using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace FSystem.GameComponents
{
    /// <summary>
    /// スプラインに従って座標を順にめぐるクラス
    /// </summary>
    /// <remarks>製作者 : 石井隼人</remarks>
    public partial class SplineAnimator
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private readonly SplineContainer SPLINE_CONTAINER;
        private readonly Queue<ITask> TASKS;
        private readonly ICountable POSITION_COUNTER;
        private readonly Thread TASK_THREAD;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 現在のポイントのインデックス番号 </summary>
		public int CurtIndex { get => POSITION_COUNTER.Position; }
		/// <summary> アニメーション中 </summary>
		public bool IsBusy { get => TASK_THREAD.IsBusy; }


		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// スプラインに従って座標を順にめぐるクラス
		/// </summary>
		/// <param name="container">スプラインコンテナ</param>
		/// <param name="countable">めぐり順を決めるカウンター</param>
		/// <exception cref="NullReferenceException">引数にnullが含まれる</exception>
		public SplineAnimator(SplineContainer container, ICountable countable = null)
        {
            SPLINE_CONTAINER = (container != null) ? container : throw new NullReferenceException("'container' can not null");
            POSITION_COUNTER = countable ?? new LoopCounter(0, container.Count);
            TASKS = new Queue<ITask>();
            TASK_THREAD = new Thread();
        }

        /// <summary>
        /// 次のポイントの座標を取得する
        /// </summary>
        /// <returns>次のポイントの座標</returns>
        public Vector3 GetNextPos()
        {
            POSITION_COUNTER.ToNext();
            return SPLINE_CONTAINER[POSITION_COUNTER.Position];
        }
        /// <summary>
        /// 前のポイントの座標を取得する
        /// </summary>
        /// <returns>前のポイントの座標</returns>
        public Vector3 GetPrevPos()
        {
            POSITION_COUNTER.ToPrev();
            return SPLINE_CONTAINER[POSITION_COUNTER.Position];
        }
        /// <summary>
        /// 現在地の座標を取得する
        /// </summary>
        /// <returns></returns>
        public Vector3 GetCurtPos() => SPLINE_CONTAINER[POSITION_COUNTER.Position];

		/// <summary>
		/// 次のポイントへなめらかに移動する
		/// </summary>
		/// <param name="transform">移動対象のtransform</param>
		/// <param name="moveSpeedPerSecond">移動速度(m/s)</param>
		public void LinearMoveToNextPos(Transform transform, ufloat moveSpeedPerSecond, Action onMoveEnd = null)
        {
            Vector3 nextPos = GetNextPos();
            ITask newTask = new MoveTask(transform, nextPos, moveSpeedPerSecond);
            TASKS.Enqueue(newTask);
            TASK_THREAD.StartCoroutine(ExecuteTask(), onMoveEnd);
        }

        /// <summary>
        /// 前のポイントへなめらかに移動する
        /// </summary>
        /// <param name="transform">移動対象のtransform</param>
        /// <param name="moveSpeedPerSecond">移動速度(m/s)</param>
        public void LinearMoveToPrevPos(Transform transform, ufloat moveSpeedPerSecond, Action onMoveEnd = null)
        {
            Vector3 prevPos = GetPrevPos();
            ITask newTask = new MoveTask(transform, prevPos, moveSpeedPerSecond);
            TASKS.Enqueue(newTask);
            TASK_THREAD.StartCoroutine(ExecuteTask(), onMoveEnd);
        }

        /// <summary>
        /// タスクをコルーチンを使って実行する
        /// </summary>
        private IEnumerator ExecuteTask()
        {
            // タスクがある間繰り返し
            while(TASKS.TryPeek(out ITask result))
            {
                // 実行が完了するまで繰り返し
                while (result.Do()) yield return null;

                // 実行が完了したらタスクを破棄
                TASKS.Dequeue();
            }
        }
	} // SplineAnimator


	/// <summary>
	/// スプラインに従って座標を順にめぐるクラス
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	public partial class SplineAnimator
    {
        /// <summary>
        /// アニメーションのタスク
        /// </summary>
        /// <remarks>製作者 : 石井隼人</remarks>
        private interface ITask
        {
            /// <summary>
            /// 既定の動作を実行する
            /// </summary>
            /// <returns>実行が完了していないとき => true | 実行が完了したとき => false</returns>
            public bool Do();
		} // ITask

		/// <summary>
		/// 移動アニメーションのタスク
		/// </summary>
		/// <remarks>製作者 : 石井隼人</remarks>
		public class MoveTask : ITask
        {
            private readonly Transform TARGET_TRANSFORM;
            private readonly Vector3 TARGET_POINT;
            private readonly ufloat MOVESPEED;

            /// <summary>
            /// 移動アニメーションのタスク
            /// </summary>
            /// <remarks>製作者 : 石井隼人</remarks>
            public MoveTask(Transform target, Vector3 targetPoint, ufloat moveSpeed)
            {
                TARGET_TRANSFORM = (target != null) ? target : throw new ArgumentNullException("'target' cannot null");
                TARGET_POINT = targetPoint;
                MOVESPEED = moveSpeed;
            }

            /// <summary>
            /// 既定の動作を実行する
            /// </summary>
            /// <returns>実行が完了していないとき => true | 実行が完了したとき => false</returns>
            public bool Do()
            {
                // 対象との距離が移動範囲内になるまで少しずつ移動
                // 現在地と目標地点から移動方向を計算
                Vector3 diff = TARGET_POINT - TARGET_TRANSFORM.position;
                ufloat moveSpeed = MOVESPEED * Time.deltaTime;

                // 十分近ければ抜ける
                if ((TARGET_TRANSFORM == null) || (diff.sqrMagnitude <= (moveSpeed * moveSpeed)))
                {
                    // 座標を補正
                    TARGET_TRANSFORM.position = TARGET_POINT;
                    return false;
                }

                // 移動
                Vector3 moveVec = diff.normalized * (float)moveSpeed;
                TARGET_TRANSFORM.position += moveVec;
                return true;
            }
		} // MoveTask
	} // SplineAnimator
} // FSystem.GameComponents