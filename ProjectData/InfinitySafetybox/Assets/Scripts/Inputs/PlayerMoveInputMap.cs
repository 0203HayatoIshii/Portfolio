using UnityEngine;
using UnityEngine.InputSystem;

using FSystem;
using FSystem.Inputs;


namespace InputSystem
{
	/// <summary>
	/// 移動ボタン
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	internal class PlayerMoveInputMap : InputDevice<string>
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private float _deadZone;
        private Keyboard _keyboard;
        private Gamepad _gamepad;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public override string DeviceType { get => "PlayerMove"; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public override void Init()
        {
            base.Init();
            _keyboard = Keyboard.current;
            _gamepad = Gamepad.current;
        }

		//*************************************************************************************************
		// 継承メンバ
		//*************************************************************************************************
		protected override void UpdateInputValue()
        {
            Vector2 input = GetKeyboardInput();
            input += GetGamepatInput();

            Value = (union)input;
            InvokeCallbacks();
        }
        /// <summary>
        /// キーボードの入力を取得する
        /// </summary>
        /// <returns></returns>
        private Vector2 GetKeyboardInput()
        {
            if (_keyboard == null)
                return Vector2.zero;

            var ret = new Vector2();
            if (_keyboard.rightArrowKey.isPressed)
            {
                ret.x += 1.0f;
            }
            if (_keyboard.leftArrowKey.isPressed)
            {
                ret.x -= 1.0f;
            }
            if (_keyboard.upArrowKey.isPressed)
            {
                ret.y += 1.0f;
            }
            if (_keyboard.downArrowKey.isPressed)
            {
                ret.y -= 1.0f;
            }
            return ret;
        }
        /// <summary>
        /// ゲームパッドの入力を取得する
        /// </summary>
        /// <returns></returns>
        private Vector2 GetGamepatInput()
        {
            if (_gamepad == null)
                return Vector2.zero;

            Vector2 ret = _gamepad.leftStick.value;
            if (ret.sqrMagnitude <= (_deadZone * _deadZone))
            {
                ret = Vector2.zero;
            }
            return ret;
        }
	} // PlayerMoveInputMap
} // InputSystem
