using UnityEngine.InputSystem;

using FSystem;
using FSystem.Inputs;


namespace InputSystem
{
	/// <summary>
	/// インタラクトボタン
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	internal class PlayerIntaractMap : InputDevice<string>
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private Keyboard _key;
        private Gamepad _pad;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public override string DeviceType { get => "Intaract"; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public override void Init()
        {
            base.Init();
            _key = Keyboard.current;
            _pad = Gamepad.current;
        }

		//*************************************************************************************************
		// 継承メンバ
		//*************************************************************************************************
		protected override void UpdateInputValue()
        {
            Value = (union)false;
            InputKeyboard();
            InputGamepad();
        }

		//*************************************************************************************************
		// プライベート関数
		//*************************************************************************************************
		private void InputKeyboard()
        {
            if (_key?.spaceKey.isPressed ?? false)
            {
                Value = (union)true;
                InvokeCallbacks();
            }
        }
        private void InputGamepad()
        {
            if (_pad?.aButton.isPressed ?? false)
            {
                Value = (union)true;
                InvokeCallbacks();
            }
        }
	} // PlayerIntaractMap
} // InputSystem
