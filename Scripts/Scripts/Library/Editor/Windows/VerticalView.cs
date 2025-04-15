using UnityEngine;
using UnityEditor;


namespace FEditor
{
	/// <summary>
	/// 垂直にコンポーネントを並べるウィンドウ
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class VerticalView : View
    {
		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public VerticalView() : base("VerticalView", WidgetScale.HD) { }
        public VerticalView(string name, WidgetScale scale, GUIStyle style = null) : base(name, scale, style) { }

		/// <summary>
		/// 描画処理を行う
		/// </summary>
		public override void Draw()
        {
            if (!IsEnable)
                return;

            EditorGUILayout.BeginVertical(Style ?? GUI.skin.window, OPTIONS);
            base.Draw();
            EditorGUILayout.EndVertical();
        }
	} // VerticalView
} // FEditor
