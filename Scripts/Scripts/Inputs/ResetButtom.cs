using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using FSystem.Inputs;


namespace InputSystem
{
	/// <summary>
	/// リセットボタン
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class ResetButtom : InputDevice<string>
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private Keyboard _keyboard;
        private Gamepad _gamepad;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public override string DeviceType { get => "ResetButton"; }

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
            if ((_keyboard?.rKey.wasPressedThisFrame ?? false) || (_gamepad?.selectButton.wasPressedThisFrame ?? false))
            {
                var curtScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(curtScene.buildIndex);
            }
        }
	} // ResetButtom
} // InputSystem
