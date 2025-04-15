using UnityEngine;
using UnityEngine.SceneManagement;

using FSystem;
using FSystem.GameComponents;


namespace UI
{
	/// <summary>
	/// ステージセレクトのウィンドウ
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	public class StageSelectWindow : MenuWindow
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private ufloat _moveSpeed;
        private SplineAnimator _anim;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public void StartStage(int stageNumber)
        {
            _anim ??= new SplineAnimator(GetComponent<SplineContainer>(), new ClampCounter(0, 1));
            _anim.LinearMoveToNextPos(transform, _moveSpeed, () => SceneManager.LoadScene("Stage" + stageNumber));
        }
        public void ReturnTitle()
        {
            _anim ??= new SplineAnimator(GetComponent<SplineContainer>(), new ClampCounter(0, 1));
            _anim.LinearMoveToNextPos(transform, _moveSpeed, () => SceneManager.LoadScene("Title"));
        }
	} // StageSelectWindow
} // UI
