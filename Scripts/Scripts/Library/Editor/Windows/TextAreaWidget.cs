using UnityEngine;
using UnityEditor;


namespace FEditor
{
	/// <summary>
	/// 複数行のテキスト入力用のコンポーネント
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class TextAreaWidget : Widget
    {
		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public string Text { get; private set; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public TextAreaWidget() : base("TextArea", WidgetScale.HD)
			=> Text = string.Empty;
		public TextAreaWidget(WidgetScale scale, GUIStyle style = null)
			: base("TextArea", scale, style) => Text = string.Empty;
		public TextAreaWidget(string name, WidgetScale scale, GUIStyle style = null) : base(name, scale, style)
			=> Text = string.Empty;

		public override void Draw()
			=> Text = EditorGUILayout.TextArea(Text, (Style == GUIStyle.none) ? GUI.skin.textArea : Style, OPTIONS);
	} // TextAreaWidget
} // FEditor
