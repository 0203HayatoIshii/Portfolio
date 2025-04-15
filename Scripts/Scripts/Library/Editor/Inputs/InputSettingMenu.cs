using UnityEditor;


namespace FEditor.Inputs
{
	/// <summary>
	/// 入力の設定画面
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class InputSettingMenu : BaseEditorWindow
    {
		/// <summary>
		/// ウィンドウの作成を行う
		/// </summary>
        [MenuItem("FSystem/InputBinds")]
        public static void ShowWindow() => GetWindow<InputSettingMenu>("Game input bind settings");

		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private View _mainView;
        private DevicePropertyView _propertyView;

		//*************************************************************************************************
		// 継承メンバ
		//*************************************************************************************************
		protected override void Draw() => _mainView.Draw();

		protected override void Init()
        {
            _mainView = new ScrollView("mainView", WidgetScale.HD);
            _propertyView = _mainView.AddChild<DevicePropertyView>();
        }

        protected override void Final()
        {
            if (_propertyView == null)
                return;

            _propertyView.End();
        }
	} // InputSettingMenu
} // FEditor.Inputs
