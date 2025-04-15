using UnityEngine.InputSystem;

using FSystem;
using FSystem.Inputs;


namespace InputSystem
{
	/// <summary>
	/// �|�[�Y�{�^��
	/// </summary>
	/// <remarks>����� : �Έ䔹�l</remarks>
	public class PauseButton : InputDevice<string>
    {
		//*************************************************************************************************
		// �v���C�x�[�g�ϐ�
		//*************************************************************************************************
		private Keyboard _keyboard;
        private Gamepad _gamepad;

		//*************************************************************************************************
		// �p�u���b�N�v���p�e�B
		//*************************************************************************************************
		public override string DeviceType { get => "PauseButton"; }

		//*************************************************************************************************
		// �p�u���b�N�֐�
		//*************************************************************************************************
		public override bool Enabled()
        {
            _keyboard = Keyboard.current;
            _gamepad = Gamepad.current;
            return base.Enabled();
        }

		//*************************************************************************************************
		// �p�������o
		//*************************************************************************************************
		protected override void UpdateInputValue()
        {
            if ((_keyboard?.escapeKey.wasPressedThisFrame ?? false) || (_gamepad?.startButton.wasPressedThisFrame ?? false))
            {
                Value = (union)true;
                InvokeCallbacks();
            }
            else
            {
                Value = (union)false;
            }
        }
	} // PauseButton
} // InputSystem
