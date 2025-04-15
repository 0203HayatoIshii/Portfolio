#if UNITY_64

using System;

using UnityEngine;


namespace FSystem.GameComponents
{
    public class AreaLimitMoveEffector : MoveEffector
    {
        [SerializeField]
        private Shape _limit;

        internal override float CalcSpeed(in VelocityData input, in VelocityData current)
        {
            // FIXME : 交点を求めて速度を計算
            // 移動後の座標が指定されたエリア外になっていたら速度を0に
            Vector3 movedPoint = transform.position + current.Velocity;
            float ret = ((_limit == null) || _limit.IsOverlapped(movedPoint)) ? current.speedRate : 0.0f;
            return ret;
        }
    }
}

#endif