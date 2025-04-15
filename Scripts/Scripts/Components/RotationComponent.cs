using System;
using System.Collections;

using UnityEngine;

using FSystem;


namespace Actors
{	
    /// <summary>
    /// 回転用のコンポーネント
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    internal class RotationComponent
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private readonly Transform TARGET_TRANSFORM;
        private readonly float INIT_DEG_ANGLE;
        private readonly Thread ROTATION_THREAD;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 回転にかかる時間 </summary>
		public float RotationTime { get; set; }
		/// <summary> 実行中であるかを示すフラグ </summary>
		public bool IsBusy { get => ROTATION_THREAD.IsBusy; }
		/// <summary> 現在の回転角度 </summary>
		public float CurtDegAngle { get; private set; }


		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public RotationComponent(Transform targetTransform, float rorationTime)
        {
            TARGET_TRANSFORM = (targetTransform != null) ? targetTransform : throw new ArgumentNullException("'targetTransform cannot be null'");
            INIT_DEG_ANGLE = targetTransform.eulerAngles.z;
            ROTATION_THREAD = new Thread();
            RotationTime = rorationTime;
        }

        /// <summary>
        /// 右回転
        /// </summary>
        /// <param name="anglePartitionValue">回転の分割数</param>
        /// <param name="endCallback">回転終了時に呼ばれるコールバック</param>
        public void RollRight(int anglePartitionValue, Action endCallback = null)
        {
            if (ROTATION_THREAD.IsBusy)
                return;

            ToNext(anglePartitionValue);
            ROTATION_THREAD.StartCoroutine(LinearRoll(), endCallback);
        }
        /// <summary>
        /// 左回転
        /// </summary>
        /// <param name="anglePartitionValue">回転の分割数</param>
        /// <param name="endCallback">回転終了時に呼ばれるコールバック</param>
        public void RollLeft(int anglePartitionValue, Action endCallback = null)
        {
            if (ROTATION_THREAD.IsBusy)
                return;

            ToPrev(anglePartitionValue);
            ROTATION_THREAD.StartCoroutine(LinearRoll(), endCallback);
        }

		//*************************************************************************************************
		// プライベート関数
		//*************************************************************************************************
		/// <summary>
		/// 線形補間を使って滑らかに回転させる
		/// </summary>
		private IEnumerator LinearRoll()
        {
            float targetDegAngleZ = INIT_DEG_ANGLE + CurtDegAngle;
            Quaternion from = TARGET_TRANSFORM.rotation;
            Quaternion to = Quaternion.Euler(new Vector3(0.0f, 0.0f, targetDegAngleZ));

			// 徐々に指定されている回転角に近づける
            for(float time = 0.0f; time < RotationTime; time += Time.deltaTime)
            {
                TARGET_TRANSFORM.rotation = Quaternion.Lerp(from, to, (time / RotationTime));
                yield return null;
            }

			// 回転角を指定された値に設定する
            TARGET_TRANSFORM.rotation = to;
        }

        /// <summary>
        /// 次の回転角を求める
        /// </summary>
        /// <param name="anglePartitionValue">回転の分割数</param>
        private void ToNext(int anglePartitionValue)
        {
            CurtDegAngle +=  360.0f / ((anglePartitionValue == 0) ? 1 : anglePartitionValue);
            while (CurtDegAngle >= 360.0f)
            {
                CurtDegAngle -= 360.0f;
            }
        }
        /// <summary>
        /// 前の回転角を求める
        /// </summary>
        /// <param name="anglePartitionValue">回転の分割数</param>
        private void ToPrev(int anglePartitionValue)
        {
			CurtDegAngle -= 360.0f / ((anglePartitionValue == 0) ? 1 : anglePartitionValue);
			while (CurtDegAngle < 0)
            {
                CurtDegAngle += 360.0f;
            }
        }
	} // RotationComponent
} // Actors
