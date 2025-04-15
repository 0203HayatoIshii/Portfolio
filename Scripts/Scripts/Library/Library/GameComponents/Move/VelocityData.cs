#if UNITY_64

using UnityEngine;


namespace FSystem.GameComponents
{
    /// <summary>
    /// 移動量のデータ
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    internal struct VelocityData
    {
        /// <summary> 移動方向 </summary>
        public Vector3 direction;
        /// <summary> 移動速度 </summary>
        public float speedRate;

        /// <summary> 現在の移動量 </summary>
        public Vector3 Velocity { get => direction.normalized * speedRate; }

        /// <summary>
        /// 内部データを初期化する
        /// </summary>
        public void Reset()
        {
            direction = Vector3.zero;
            speedRate = 0.0f;
        }
        /// <summary>
        /// 記録されている移動量が０か確認する
        /// </summary>
        /// <returns>移動量が0のとき => true | 移動量が0以外 => false</returns>
        public readonly bool IsZero()
        {
            bool ret = ((direction == Vector3.zero) || (speedRate == 0.0f));
            return ret;
        }
    }
}

#endif