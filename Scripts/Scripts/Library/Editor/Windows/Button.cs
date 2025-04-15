using System;

using UnityEngine;


namespace FEditor
{
	/// <summary>
	/// ボタン機能を持つコンポーネント
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class Button : Widget
    {
		//*************************************************************************************************
		// パブリックイベント
		//*************************************************************************************************
		public event Action OnClick;

		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private string _text;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public string Text { get => _text; set => _text = value ?? ""; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public Button() : base("Button", WidgetScale.HD, GUI.skin.button)
			=> Text = "Button";
		public Button(string text) : base(text, WidgetScale.HD, GUI.skin.button)
			=> Text = text;
		public Button(string text, WidgetScale scale) : base(text, scale)
			=> Text = text;
		public Button(string text, string name, WidgetScale scale, GUIStyle style = null) : base(name, scale, style)
			=> Text = text;

		/// <summary>
		/// 描画処理を行う
		/// </summary>
		public override void Draw()
        {
            bool isClick = GUILayout.Button(Text, (Style == GUIStyle.none) ? GUI.skin.button : Style, OPTIONS);
            if (isClick)
            {
                OnClick?.Invoke();
                Notify(this, "OnClick");
            }
        }
    }
}