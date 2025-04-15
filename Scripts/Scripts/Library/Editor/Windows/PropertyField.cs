using UnityEngine;
using UnityEditor;


namespace FEditor
{
	/// <summary>
	/// インスペクターに表示されるデータを表示するコンポーネント
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class PropertyField : Widget
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private readonly SerializedObject PROPERTY;
        private bool _showFlag;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public PropertyField(UnityEngine.Object @object, GUIStyle style = null) : base(@object.GetType().Name , WidgetScale.HD, style)
			=> PROPERTY = new SerializedObject(@object);
		public PropertyField(UnityEngine.Object @object, string name, WidgetScale scale, GUIStyle style = null) : base(name, scale, style)
			=> PROPERTY = new SerializedObject(@object);

		public override void Draw()
        {
            // ドロップダウンを表示
            _showFlag = EditorGUILayout.Foldout(_showFlag, Name, true);
            if (_showFlag)
                return;

            // プロパティを表示
            PROPERTY.Update();
            var itr = PROPERTY.GetIterator();
            itr.NextVisible(true);
            while (itr.NextVisible(false))
            {
                EditorGUILayout.PropertyField(itr, true);
            }
            PROPERTY.ApplyModifiedProperties();
        }
	} // PropertyField
} // FEditor
