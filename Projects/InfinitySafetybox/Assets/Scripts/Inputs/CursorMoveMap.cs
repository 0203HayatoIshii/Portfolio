using UnityEngine;
using UnityEngine.InputSystem;

using FSystem;
using FSystem.Inputs;


namespace InputSystem
{
	/// <summary>
	/// �J�[�\���̈ړ��{�^��
	/// </summary>
	/// <remarks>����� : �Έ䔹�l</remarks>
	public class CursorMoveMap : InputDevice<string>
    {
		//*************************************************************************************************
		// �v���C�x�[�g�ϐ�
		//*************************************************************************************************
		private Keyboard _keyboard;
        private Gamepad _gamepad;
        private Timer _inputInterval;
        [SerializeField] private ufloat _inputIntervalTimeOfSecond;

		//*************************************************************************************************
		// �p�u���b�N�v���p�e�B
		//*************************************************************************************************
		public override string DeviceType { get => "CursorMove"; }

		//*************************************************************************************************
		// �p�u���b�N�֐�
		//*************************************************************************************************
		public override bool Enabled()
        {
            _inputInterval ??= new Timer();
            _keyboard ??= Keyboard.current;
            _gamepad ??= Gamepad.current;
            return base.Enabled();
        }

		//*************************************************************************************************
		// �p�������o
		//*************************************************************************************************
		protected override void UpdateInputValue()
        {
            if (!_inputInterval.IsTimeOver(_inputIntervalTimeOfSecond))
            {
                Value = union.Zero;
                return;
            }

            Vector2Int input = UpdateKeyboard();
            input += UpdateGamepad();
            Value = (union)input;
            InvokeCallbacks();
            if (input != Vector2Int.zero)
            {
                _inputInterval.ResetTimer();
            }
        }

		//*************************************************************************************************
		// �v���C�x�[�g�֐�
		//*************************************************************************************************
		private Vector2Int UpdateKeyboard()
        {
            if (_keyboard == null)
                return Vector2Int.zero;

            var input = new Vector2Int();
            if (_keyboard.upArrowKey.isPressed)
            {
                input.y += 1;
            }
            if (_keyboard.downArrowKey.isPressed)
            {
                input.y -= 1;
            }
            if (_keyboard.rightArrowKey.isPressed)
            {
                input.x += 1;
            }
            if (_keyboard.leftArrowKey.isPressed)
            {
                input.x -= 1;
            }
            return input;
        }
        private Vector2Int UpdateGamepad()
        {
            if (_gamepad == null)
                return Vector2Int.zero;

            const float ERROR_RANGE = 0.8f;
            var input = new Vector2Int();
            if (_gamepad.leftStick.value.y > ERROR_RANGE)
            {
                input.y += 1;
            }
            if (_gamepad.leftStick.value.y < -ERROR_RANGE)
            {
                input.y -= 1;
            }
            if (_gamepad.leftStick.value.x > ERROR_RANGE)
            {
                input.x += 1;
            }
            if (_gamepad.leftStick.value.x < -ERROR_RANGE)
            {
                input.x -= 1;
            }
            return input;
        }
	} // CursorMoveMap
} // InputSystem
