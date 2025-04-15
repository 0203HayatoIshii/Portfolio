using UnityEngine.InputSystem;

using FSystem;
using FSystem.Inputs;


namespace InputSystem
{
	/// <summary>
	/// ポーズボタン
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class PauseButton : InputDevice<string>
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private Keyboard _keyboard;
        private Gamepad _gamepad;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public override string DeviceType { get => "PauseButton"; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public override bool Enabled()
        {
            _keyboard = Keyboard.current;
            _gamepad = Gamepad.current;
            return base.Enabled();
        }

		//*************************************************************************************************
		// 継承メンバ
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
