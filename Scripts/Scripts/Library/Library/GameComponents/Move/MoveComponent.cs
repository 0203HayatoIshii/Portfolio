#if UNITY_64

using UnityEngine;


namespace FSystem.GameComponents
{
    /// <summary>
    /// 移動用のコンポーネント
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rb;
        [SerializeField]
        private Rigidbody2D _rb2D;

        [SerializeField]
        private MoveEffector _headEffector;

        private VelocityData _inputVeclocityData;
        private VelocityData _curtVelocityData;

        /// <summary> 現在の移動量 </summary>
        public Vector3 Velocity { get => _curtVelocityData.Velocity; }
        /// <summary> 現在の移動方向 </summary>
        public Vector3 MoveDirection { get => _curtVelocityData.direction; }
        /// <summary> 現在の移動速度 </summary>
        public float MoveSpeed { get => _curtVelocityData.speedRate; }


        /// <summary>
        /// コンポーネントを更新する
        /// </summary>
        public void UpdateComponent()
        {
            if (_headEffector == null)
            {
                _curtVelocityData = _inputVeclocityData;
            }
            else
            {
                _curtVelocityData.speedRate = _headEffector.CalcSpeed(_inputVeclocityData, _curtVelocityData);
                _curtVelocityData.direction = _headEffector.CalcDirection(_inputVeclocityData, _curtVelocityData);
            }

            MoveObject();
            _inputVeclocityData.Reset();
        }
        /// <summary>
        /// オブジェクトを移動させる
        /// </summary>
        private void MoveObject()
        {
            // rigitbodyが設定されていればrigitbodyで移動
            if (_rb != null)
            {
                _rb.velocity = _curtVelocityData.Velocity / Time.fixedDeltaTime;
            }
            // rigitbody2Dがアタッチされていればrigitbody2Dで移動
            else if (_rb2D != null)
            {
                _rb2D.velocity = _curtVelocityData.Velocity / Time.fixedDeltaTime;
            }
            // 何もアタッチされていなければtransformで移動
            else
            {
                transform.position += _curtVelocityData.Velocity;
            }
        }

        /// <summary>
        /// 移動量を加算する
        /// </summary>
        /// <param name="input">入力された移動方向</param>
        /// <param name="speed">入力された移動速度</param>
        public void AddForce(Vector3 input, float speed = 1.0f)
        {
            _inputVeclocityData.direction = input;
            _inputVeclocityData.speedRate = speed;
        }

        /// <summary>
        /// 内部の移動量をリセットする
        /// </summary>
        public void ResetVelocity()
        {
            _inputVeclocityData.Reset();
            _curtVelocityData.Reset();
        }
    }
}

#endif