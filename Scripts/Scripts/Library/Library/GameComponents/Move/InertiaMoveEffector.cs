#if UNITY_64

using System;

using UnityEngine;


namespace FSystem.GameComponents
{
    /// <summary>
    /// 移動コンポーネントに対して慣性の計算を付け足すエフェクタークラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class InertiaMoveEffector : MoveEffector
    {
        private const float PARAM_MAX = 50.0f;

        [SerializeField, Range(0.0f, PARAM_MAX)]
        private float _acceleration;
        [SerializeField, Range(0.0f, 1.0f)]
        private float _friction;
        [SerializeField, Range(0.0f, PARAM_MAX)]
        private float _maxSpeed;
        [SerializeField, Range(0.0f, PARAM_MAX)]
        private float _ignoreSpeed;
        [SerializeField, Range(0.0f, 180.0f)]
        private float _accelerationDegAngle;
        [SerializeField, Range(0.0f, 1.0f)]
        private float _rotationRate;


        /// <summary> 加速度(加算) </summary>
        public float Acceleration         { get => _acceleration;         set => _acceleration         = Mathf.Clamp(value, 0.0f, float.MaxValue); }
        /// <summary> 摩擦(乗算) </summary>
        public float Friction             { get => _friction;             set => _friction             = Mathf.Clamp01(value); }
        /// <summary> 最大速度 </summary>
        public float MaxSpeed             { get => _maxSpeed;             set => _maxSpeed             = Mathf.Clamp(value, 0.0f, float.MaxValue); }
        /// <summary> 切り捨てられる最小速度 </summary>
        public float IgnoreSpeed          { get => _ignoreSpeed;          set => _ignoreSpeed          = Mathf.Clamp(value, 0.0f, float.MaxValue); }
        /// <summary> 切り捨てられる最小速度 </summary>
        public float AccelerationDegAngle { get => _accelerationDegAngle; set => _accelerationDegAngle = Mathf.Clamp(value, 0.0f, 180.0f        ); }
        /// <summary> 切り捨てられる最小速度 </summary>
        public float RotationRate         { get => _rotationRate;         set => _rotationRate         = Mathf.Clamp01(value); }



        /// <summary>
        /// 移動速度を計算する
        /// </summary>
        /// <param name="input">入力された移動量</param>
        /// <param name="current">現在の移動量</param>
        /// <returns>慣性計算された移動速度</returns>
        internal override float CalcSpeed(in VelocityData input, in VelocityData current)
        {
            // 入力が０、現在のスピードが最大速度を超えている、反転している、のどれかに当てはまれば減速、それ以外なら加速
            VelocityData temp = ((input.IsZero()) || (current.speedRate > (_maxSpeed * input.speedRate)) || IsTurn(input, current)) ? 
                Dicelerate(current) : Accelerate(input, current);

            float ret = base.CalcSpeed(input, temp);
            return ret;
        }
        /// <summary>
        /// 移動ベクトルが移動方向と反対かどうかを判別する
        /// </summary>
        /// <param name="input">入力ベクトル</param>
        /// <param name="curtData">現在の移動データ</param>
        /// <returns>反転しているとき => true | 前進しているとき => false</returns>
        private bool IsTurn(in VelocityData input, in VelocityData curtData)
        {
            float angle = Vector3.Angle(input.direction, curtData.direction);
            bool ret = (angle >= _accelerationDegAngle);
            return ret;
        }
        /// <summary>
        /// 加速する
        /// </summary>
        /// <param name="input">入力ベクトル</param>
        /// <param name="curtData">現在の移動データ</param>
        /// <returns>加速した後の移動データ</returns>
        private VelocityData Accelerate(in VelocityData input, in VelocityData curtData)
        {
            VelocityData ret = curtData;
            ret.speedRate += _acceleration * input.speedRate;
            float curtMaxSpeedRate = _maxSpeed * input.speedRate;
            if (ret.speedRate > curtMaxSpeedRate)
            {
                ret.speedRate = curtMaxSpeedRate;
            }
            return ret;
        }
        /// <summary>
        /// 減速する
        /// </summary>
        /// <param name="input">入力ベクトル</param>
        /// <param name="curtData">現在の移動データ</param>
        /// <returns>減速した後の移動データ</returns>
        private VelocityData Dicelerate(in VelocityData curtData)
        {
            VelocityData ret = curtData;
            ret.speedRate *= _friction;
            if (ret.speedRate < _ignoreSpeed)
            {
                ret.speedRate = 0.0f;
            }
            return ret;
        }

        /// <summary>
        /// 移動方向を計算する
        /// </summary>
        /// <param name="input">入力された移動量</param>
        /// <param name="current">現在の移動量</param>
        /// <returns>現在の移動方向と入力された移動方向の法線に対して割合回転したベクトル</returns>
        internal override Vector3 CalcDirection(in VelocityData input, in VelocityData current)
        {
            VelocityData temp = current;
            if (current.IsZero())
            {
                temp.direction = input.direction;
            }
            else if(!IsTurn(input, current))
            {
                // FIXME : 回転軸としている外積が０になったときに不具合を起こす
                // ロドリゲスの回転公式を使って法線を軸に回転
                Vector3 axis = Vector3.Cross(input.direction, current.direction).normalized;
                float angle = Vector3.Angle(input.direction, current.direction) * _rotationRate * -Mathf.Deg2Rad;
                float cos = Mathf.Cos(angle);
                float sin = Mathf.Sin(angle);
                temp.direction = (temp.direction * cos) + ((1 - cos) * Vector3.Dot(axis, temp.direction) * axis) + (Vector3.Cross(axis, temp.direction) * sin);
            }

            Vector3 ret = base.CalcDirection(input, temp);
            return ret;
        }
    }
}

#endif