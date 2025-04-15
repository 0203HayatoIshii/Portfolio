using UnityEngine;
using UnityEditor;


namespace FEditor
{
	/// <summary>
	/// テキストを入力するコンポーネント
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class TextFieldWidget : Widget
    {
		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public string Text { get; private set; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public TextFieldWidget() : base("TextArea", WidgetScale.HD)
			=> Text = string.Empty;
		public TextFieldWidget(WidgetScale scale, GUIStyle style = null) : base("TextArea", scale, style)
			=> Text = string.Empty;
		public TextFieldWidget(string name, WidgetScale scale, GUIStyle style = null) : base(name, scale, style)
			=> Text = string.Empty;

		public override void Draw() 
			=> Text = EditorGUILayout.TextField(Name, Text, (Style == GUIStyle.none) ? GUI.skin.textField : Style, OPTIONS);
	} // TextFieldWidget
} // FEditor
