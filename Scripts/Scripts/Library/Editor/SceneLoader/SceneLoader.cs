using System.IO;

using UnityEditor;
using UnityEditor.SceneManagement;


namespace FEditor
{
	/// <summary>
	/// シーンの読み込みを行うウィンドウ
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class SceneLoader : BaseEditorWindow
    {
		[MenuItem("FSystem/SceneLoader")]
        public static void ShowWindow() => GetWindow<SceneLoader>();

		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private View _mainView;

		//*************************************************************************************************
		// 継承メンバ
		//*************************************************************************************************
		protected override void Init()
        {
			// シーンファイルの検索
            _mainView = new VerticalView();
			string[] guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets\\Scenes" });
            foreach(var c in guids)
            {
				// シーンファイルの名前を取り出してボタンを生成
                string path = AssetDatabase.GUIDToAssetPath(c);
                string sceneName = Path.GetFileNameWithoutExtension(path);
                var button = _mainView.AddChild(new Button(sceneName, "sceneLoadButton", new WidgetScale(500.0f, 25.0f)));
                button.OnClick += () => { EditorSceneManager.SaveOpenScenes(); EditorSceneManager.OpenScene(path); };
            }
        }
        protected override void Draw() => _mainView.Draw();
		protected override void Final() { }
	} // SceneLoader
} // FEditor
