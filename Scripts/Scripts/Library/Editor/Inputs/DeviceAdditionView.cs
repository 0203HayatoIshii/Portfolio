using UnityEngine;


namespace FEditor.Inputs
{
	/// <summary>
	/// デバイスを追加するウィンドウ
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class DeviceAdditionView : VerticalView
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private readonly TextFieldWidget @assembly;
        private readonly TextFieldWidget @namespace;
        private readonly TextFieldWidget @class;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public DeviceAdditionView() : base("DeviceAdditionButton", new WidgetScale(800.0f, 100.0f), GUI.skin.window)
        {
            // デバイスの追加を行うためのボタンを子として作成
            @assembly = AddChild(new TextFieldWidget("Assembly", new WidgetScale(800.0f, 25.0f)));
            @namespace = AddChild(new TextFieldWidget("Namespace", new WidgetScale(800.0f, 25.0f)));
            @class = AddChild(new TextFieldWidget("Class", new WidgetScale(800.0f, 25.0f)));
            AddChild(new Button("AddComponent", new WidgetScale(800.0f, 25.0f)));
        }
		/// <summary>
		/// 管理者に通知を出す
		/// </summary>
		/// <remarks>管理しているウィジェットからの通知を処理したい場合はこの関数をオーバーライドすること</remarks>
		/// <param name="sender">通知元のウィジェット</param>
		/// <param name="message">通知のハンドル番号</param>
		/// <returns>通知が処理されたとき => true | 通知が処理されなかったとき => false</returns>
		public override bool Notify(IWidget sender, string message)
        {
            if (sender.Name == "AddComponent")
            {
                base.Notify(sender, (@namespace.Text + "." + @class.Text + ", " + @assembly.Text));
                return true;
            }
            return base.Notify(sender, message);
        }
	} // DeviceAdditionView
} // FEditor.Inputs
