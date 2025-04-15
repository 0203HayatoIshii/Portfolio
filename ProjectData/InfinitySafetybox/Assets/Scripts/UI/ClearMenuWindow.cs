using UnityEngine;
using UnityEngine.SceneManagement;

using FSystem;
using FSystem.GameComponents;


namespace UI
{
	/// <summary>
	/// クリアのウィンドウ
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	public class ClearMenuWindow : MenuWindow
    {
		public static int lastClearSatgeIndex = 0;

		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private ufloat _moveSpeed;
        [SerializeField] private int _sceneAmount;
        private SplineAnimator _anim;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public void LoadNextStage()
        {
            _anim ??= new SplineAnimator(GetComponent<SplineContainer>(), new ClampCounter(0, 1));
            if (lastClearSatgeIndex > _sceneAmount)
            {
				// ステージセレクト画面を読み込む
                _anim.LinearMoveToNextPos(transform, _moveSpeed, () => SceneManager.LoadScene("StageSelect"));
            }
            else
            {
				// 次のステージを読み込む
                _anim.LinearMoveToNextPos(transform, _moveSpeed, () => SceneManager.LoadScene("Stage" + (lastClearSatgeIndex + 1)));
            }
        }
        public void LoadStageSelect()
        {
            _anim ??= new SplineAnimator(GetComponent<SplineContainer>(), new ClampCounter(0, 1));
            _anim.LinearMoveToNextPos(transform, _moveSpeed, () => SceneManager.LoadScene("StageSelect"));
        }
	} // ClearMenuWindow
} // UI
