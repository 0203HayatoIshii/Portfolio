#if UNITY_64

using UnityEngine;


namespace FSystem.GameComponents
{
    public abstract class MoveEffector : MonoBehaviour
    {
        [SerializeField]
        private MoveEffector _nextEffector;

        /// <summary>
        /// 移動速度を計算する
        /// </summary>
        /// <param name="input">入力された移動量</param>
        /// <param name="current">現在の移動量</param>
        /// <returns>計算された移動速度</returns>
        internal virtual float CalcSpeed(in VelocityData input, in VelocityData current)
        {
            float ret = (_nextEffector == null) ? current.speedRate : _nextEffector.CalcSpeed(input, current);
            return ret;
        }
        /// <summary>
        /// 移動方向を計算する
        /// </summary>
        /// <param name="input">入力された移動量</param>
        /// <param name="current">現在の移動量</param>
        /// <returns>計算後の移動方向</returns>
        internal virtual Vector3 CalcDirection(in VelocityData input, in VelocityData current)
        {
            Vector3 ret = (_nextEffector == null) ? current.direction : _nextEffector.CalcDirection(input, current);
            return ret;
        }
    }
}

#endif