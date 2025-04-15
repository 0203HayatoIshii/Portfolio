using UnityEngine.InputSystem;
using FSystem.Inputs;


namespace InputSystem
{
	/// <summary>
	/// メニュー画面でのインタラクトボタン
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class IntarctMenu : InputDevice<string>
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private Keyboard _keyboard;
        private Gamepad _gamepad;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public override string DeviceType { get => "InteractMenu"; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public override bool Enabled()
        {
            _keyboard ??= Keyboard.current;
            _gamepad ??= Gamepad.current;
            return base.Enabled();
        }

		//*************************************************************************************************
		// 継承メンバ
		//*************************************************************************************************
		protected override void UpdateInputValue()
        {
            if ((_keyboard?.spaceKey.wasPressedThisFrame ?? false) || (_gamepad?.aButton.wasPressedThisFrame ?? false))
            {
                InvokeCallbacks();
            }
        }
	} // IntarctMenu
} // InputSystem
