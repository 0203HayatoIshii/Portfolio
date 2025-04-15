using UnityEngine;
using UnityEngine.SceneManagement;

using FSystem;
using FSystem.GameComponents;


namespace UI
{
	/// <summary>
	/// タイトルのウィンドウ
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	public class TitleMenuWindow : MenuWindow
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private ufloat _moveSpeed;
        private SplineAnimator _anim;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public void LoadStageSelect()
        {
            _anim ??= new SplineAnimator(GetComponent<SplineContainer>(), new ClampCounter(0, 1));
            _anim.LinearMoveToNextPos(transform, _moveSpeed, () => SceneManager.LoadScene("StageSelect"));
        }
        public void ExitGame() => Application.Quit();
	} // TitleMenuWindow
} // UI
