using UnityEngine;
using UnityEditor;


namespace FEditor
{
	/// <summary>
	/// 水平にコンポーネントを並べるウィンドウ
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class HorizontalView : View
    {
		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public HorizontalView() : base("HorizontalView", WidgetScale.HD) { }
        public HorizontalView(string name, WidgetScale scale, GUIStyle style = null) : base(name, scale, style) { }

        public override void Draw()
        {
            if (!IsEnable)
                return;

            EditorGUILayout.BeginHorizontal(Style, OPTIONS);
            base.Draw();
            EditorGUILayout.EndHorizontal();
        }
	} // HorizontalView
} // FEditor
