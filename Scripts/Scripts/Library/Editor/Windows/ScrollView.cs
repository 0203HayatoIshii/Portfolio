using UnityEngine;
using UnityEditor;


namespace FEditor
{
	/// <summary>
	/// スクロール可能なウィンドウ
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class ScrollView : View
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private Vector2 _scroll;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public ScrollView() : base("ScrollView", WidgetScale.HD)
			=> _scroll = Vector2.zero;
		public ScrollView(string name, WidgetScale scale, GUIStyle style = null) : base(name, scale, style)
			=> _scroll = Vector2.zero;

		public override void Draw()
        {
            if (!IsEnable)
                return;

            _scroll = EditorGUILayout.BeginScrollView(_scroll, Style ?? GUI.skin.window, OPTIONS);
            base.Draw();
            EditorGUILayout.EndScrollView();
        }
	} // ScrollView
} // FEditor
