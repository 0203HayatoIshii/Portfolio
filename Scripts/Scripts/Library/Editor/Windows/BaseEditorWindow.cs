using UnityEditor;


namespace FEditor
{
	/// <summary>
	/// エディターウィンドウの抽象クラス
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public abstract class BaseEditorWindow : EditorWindow
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private bool _onInit;

		//*************************************************************************************************
		// 継承メンバ
		//*************************************************************************************************
		/// <summary>
		/// 描画処理を行う
		/// </summary>
		protected abstract void Draw();
		/// <summary>
		/// 初期化処理を行う
		/// </summary>
		protected abstract void Init();
		/// <summary>
		/// 終了処理を行う
		/// </summary>
		protected abstract void Final();

		//*************************************************************************************************
		// プライベート関数
		//*************************************************************************************************
		private void OnGUI()
        {
            try
            {
                if (!_onInit)
                {
                    Init();
                    _onInit = true;
                }
                Draw();
            }
            catch
            {
                Close();
                throw;
            }
        }
        private void OnDestroy() => Final();
	} // BaseEditorWindow
} // FEditor
