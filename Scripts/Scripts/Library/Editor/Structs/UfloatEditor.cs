using UnityEngine;
using UnityEditor;

using FSystem;


namespace FEditor
{
	/// <summary>
	/// 符号なし実数用のエディター
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	[CustomPropertyDrawer(typeof(ufloat))]
    public class UfloatEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            property.NextVisible(true);
            EditorGUI.PropertyField(fieldRect, property, GUIContent.none);
        }
	} // UfloatEditor
} // FEditor
