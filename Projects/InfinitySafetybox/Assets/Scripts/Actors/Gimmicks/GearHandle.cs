using System;

using UnityEngine;


namespace Actors
{
	/// <summary>
	/// �M�A�n���h���p�̃N���X
	/// </summary>
	/// <remarks>����� : �Έ䔹�l</remarks>
	internal class GearHandle : IntaractGimmick, IGimmick
    {
		//*************************************************************************************************
		// �v���C�x�[�g�f�[�^
		//*************************************************************************************************
		[Serializable]
        private struct GearData
        {
            public Gear target;
            public int gearToothCount;
        }

		//*************************************************************************************************
		// �v���C�x�[�g�ϐ�
		//*************************************************************************************************
		[SerializeField] private GearData[] _rollTargetGears;
        [SerializeField, Range(0.0f, 180.0f)] private float _rotateIncidenceAngle;

		//*************************************************************************************************
		// �p�u���b�N�v���p�e�B
		//*************************************************************************************************
		/// <summary> �L����Ԃ��̃t���O </summary>
		public bool Activate { get; set; }

		/// <summary>
		/// �r�W�^�[���󂯓���邽�߂̃��\�b�h
		/// </summary>
		/// <param name="visitor">�C���^���N�g�����A�N�^�[�̎Q��</param>
		public override void Intaract(IVisitor visitor) => visitor.Visit(this);

        /// <summary>
        /// �M�A����]������
        /// </summary>
        /// <param name="invoker">�C���^���N�g�����Ώۂ�transform</param>
        public void Roll(Transform invoker)
        {
            if ((_rollTargetGears == null) || !Activate)
                return;

            // ��ɉ�]�ł����Ԃ����m�F
            int len = _rollTargetGears?.Length ?? 0;
            for(int i = 0; i < len; ++i)
            {
                if (!_rollTargetGears[i].target.CanRoll())
                    return;
            }

            // ��]�ł���Ȃ�ڑ�����Ă��邷�ׂẴM�A����]������
            for (int i = 0; i < len; ++i)
            {
                // �ړ������̃x�N�g���ƃM�A�ƑΏۂƂ̍��̃x�N�g�������]�������v�Z
                Vector3 diff = transform.position - invoker.position;
                float rot = (invoker.eulerAngles.z + 90.0f) * Mathf.Deg2Rad;
                var forward = new Vector3
                {
                    x = Mathf.Cos(rot),
                    y = Mathf.Sin(rot),
                };

                // �}�p�x�Ȃ��]���Ȃ�
                float angle = Vector3.Angle(forward, diff);
                if (angle > _rotateIncidenceAngle)
                    return;

                // �N���X�ς��g�p���ĉ�]���������߂�
                float cross = Vector3.Cross(forward, diff).z;
                if (cross < 0)
                {
                    _rollTargetGears[i].target.RollLeft(_rollTargetGears[i].gearToothCount);
                }
                else
                {
                    _rollTargetGears[i].target.RollRight(_rollTargetGears[i].gearToothCount);
                }
            }
        }

		private void Awake() => Activate = true;
	} // GearHandle
} // Actors
