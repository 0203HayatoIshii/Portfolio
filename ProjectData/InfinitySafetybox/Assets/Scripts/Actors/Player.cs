using System;

using UnityEngine;

using FSystem;
using FSystem.Inputs;
using FSystem.GameComponents;

namespace Actors
{
	/// <summary>
	/// �v���C���[
	/// </summary>
	/// <remarks>����� : �Έ䔹�l</remarks>
	internal class Player : MonoBehaviour, IVisitor
    {
		//*************************************************************************************************
		// �v���C�x�[�g�ϐ�
		//*************************************************************************************************
		[SerializeField] private MoveComponent _moveComp;
        [SerializeField] private Collider2D _interactArea;
        [SerializeField] private GameObject _menuScrean;
        private GameObject _menuScreanBuffer;

        private IIntaractable _intaractObjectBuffer;

        private IInputDevice<string> _intaract;
        private IInputDevice<string> _move;
        private IInputDevice<string> _resetButton;
        private IInputDevice<string> _pauseButton;

		//*************************************************************************************************
		// �p�u���b�N�֐�
		//*************************************************************************************************
		public void Visit(GearHandle gear)
        {
            transform.parent = gear.transform.root;
            gear.Roll(transform);
        }
        public void Visit(Lever @switch)
        {
            @switch.OnClick();
        }
        public void Visit(Goal goal)
        {
            Disable();
            goal.OnClear();
        }

		/// <summary>
		/// ���̓V�X�e���̗L�������s��
		/// </summary>
        public void Enable()
        {
            _move ??= InputManager.Instance.FindDevice("PlayerMove");
            _move.Enabled();
            _intaract ??= InputManager.Instance.FindDevice("Intaract");
            _intaract.Enabled();
            _resetButton ??= InputManager.Instance.FindDevice("ResetButton");
            _resetButton.Enabled();
            _pauseButton ??= InputManager.Instance.FindDevice("PauseButton");
            _pauseButton.Enabled();
        }
		/// <summary>
		/// ���̓V�X�e���̔�L�������s��
		/// </summary>
		public void Disable()
        {
            _move?.Disable();
            _intaract?.Disable();
            _resetButton?.Disable();
            _pauseButton?.Disable();
        }

		//*************************************************************************************************
		// �v���C�x�[�g�֐�
		//*************************************************************************************************
		private void Awake() => Enable();
		private void Update()
		{
			move(_move.Value);

			findGimmick();
			interact(_intaract.Value);

			pause(_pauseButton.Value);
		}
		/// <summary>
		/// �ړ�����
		/// </summary>
		/// <param name="input">���͒l</param>
		private void move(union input)
        {
            if (_moveComp == null)
                return;

            _moveComp.AddForce((Vector2)input);
            _moveComp.UpdateComponent();

            // ���͂�0�ȊO�Ȃ�v���C���[�̌�������͒l�̕����ɕς���
            if ((Vector2)input != Vector2.zero)
            {
                transform.parent = null;
                var angle = new Vector3 { z = Mathf.Atan2(_moveComp.MoveDirection.y, _moveComp.MoveDirection.x) * Mathf.Rad2Deg - 90.0f };
                transform.eulerAngles = angle;
            }
        }
		/// <summary>
		/// ���͂̃M�~�b�N��{������
		/// </summary>
        private void findGimmick()
        {
            // ���͂̃C���^���N�g�ł���R���C�_�[��T��
            var overlapColliderAry = new Collider2D[3];
            var filter = new ContactFilter2D() { useTriggers = true, useLayerMask = true, layerMask = (1 << 3) };
            int hitCnt = _interactArea.OverlapCollider(filter, overlapColliderAry);
            for (int i = 0; i < hitCnt; ++i)
            {
                if (!overlapColliderAry[i].TryGetComponent(out IIntaractable result))
                    continue;

                // �������̂ɐG��Ă�����X�V�֐����Ă�
                if (_intaractObjectBuffer == result)
                {
                    result.OnStay();
                    return;
                }
                // �Ⴄ���̂ɐG�ꂽ��o�b�t�@�����ւ���
                else
                {
                    _intaractObjectBuffer?.OnExit();
                    result?.OnEnter();
                    _intaractObjectBuffer = result;                    
                    return;
                }
            }

            // ���ɂ��G��Ă��Ȃ���΃o�b�t�@�����
            if (_intaractObjectBuffer != null)
            {
                _intaractObjectBuffer.OnExit();
                _intaractObjectBuffer = null;
            }
        }
        /// <summary>
        /// ���͂̃M�~�b�N�ɃC���^���N�g�����݂�
        /// </summary>
        /// <param name="input">���͒l</param>
        /// <exception cref="NullReferenceException">�R���C�_�[���A�^�b�`����Ă��Ȃ�</exception>
        private void interact(union input)
        {
            if (!(bool)input)
                return;

			// �C���^���N�g�ł���G���A��������Ȃ�������G���[���o��
            if ((_interactArea == null) && !TryGetComponent(out _interactArea))
            {
                Debug.LogError("'intaractArea is not attached'");
                return;
            }

            _intaractObjectBuffer?.Intaract(this);
        }

		/// <summary>
		/// �L�����N�^�[�̈ꎞ��~���s��
		/// </summary>
		/// <param name="input"></param>
        private void pause(union input)
        {
            if (!(bool)input)
                return;

            if (_menuScreanBuffer == null)
            {
                _menuScreanBuffer = Instantiate(_menuScrean);
                _move?.Disable();
                _intaract?.Disable();
                _resetButton?.Disable();
            }
            else
            {
                Destroy(_menuScreanBuffer);
                Enable();
            }
        }
	} // Player
} // Actors
