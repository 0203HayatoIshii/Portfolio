#if UNITY_64

using UnityEngine;


namespace FSystem.GameComponents
{
    /// <summary>
    /// �ړ��p�̃R���|�[�l���g
    /// </summary>
    /// <remarks>����� : �Έ䔹�l</remarks>
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

        /// <summary> ���݂̈ړ��� </summary>
        public Vector3 Velocity { get => _curtVelocityData.Velocity; }
        /// <summary> ���݂̈ړ����� </summary>
        public Vector3 MoveDirection { get => _curtVelocityData.direction; }
        /// <summary> ���݂̈ړ����x </summary>
        public float MoveSpeed { get => _curtVelocityData.speedRate; }


        /// <summary>
        /// �R���|�[�l���g���X�V����
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
        /// �I�u�W�F�N�g���ړ�������
        /// </summary>
        private void MoveObject()
        {
            // rigitbody���ݒ肳��Ă����rigitbody�ňړ�
            if (_rb != null)
            {
                _rb.velocity = _curtVelocityData.Velocity / Time.fixedDeltaTime;
            }
            // rigitbody2D���A�^�b�`����Ă����rigitbody2D�ňړ�
            else if (_rb2D != null)
            {
                _rb2D.velocity = _curtVelocityData.Velocity / Time.fixedDeltaTime;
            }
            // �����A�^�b�`����Ă��Ȃ����transform�ňړ�
            else
            {
                transform.position += _curtVelocityData.Velocity;
            }
        }

        /// <summary>
        /// �ړ��ʂ����Z����
        /// </summary>
        /// <param name="input">���͂��ꂽ�ړ�����</param>
        /// <param name="speed">���͂��ꂽ�ړ����x</param>
        public void AddForce(Vector3 input, float speed = 1.0f)
        {
            _inputVeclocityData.direction = input;
            _inputVeclocityData.speedRate = speed;
        }

        /// <summary>
        /// �����̈ړ��ʂ����Z�b�g����
        /// </summary>
        public void ResetVelocity()
        {
            _inputVeclocityData.Reset();
            _curtVelocityData.Reset();
        }
    }
}

#endif