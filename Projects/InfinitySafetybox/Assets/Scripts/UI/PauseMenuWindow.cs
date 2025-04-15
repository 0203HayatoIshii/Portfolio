using UnityEngine.SceneManagement;


namespace UI
{
	/// <summary>
	/// ポーズメニューのウィンドウ
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	public class PauseMenuWindow : MenuWindow
    {
		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public void ReloadStage()
        {
            var curtScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(curtScene.buildIndex);
        }

        public void ReturnStageSelect() => SceneManager.LoadScene("StageSelect");
	} // PauseMenuWindow
} // UI
