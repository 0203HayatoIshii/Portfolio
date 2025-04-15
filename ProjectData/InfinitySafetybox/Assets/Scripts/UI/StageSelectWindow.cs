using UnityEngine;
using UnityEngine.SceneManagement;

using FSystem;
using FSystem.GameComponents;


namespace UI
{
	/// <summary>
	/// �X�e�[�W�Z���N�g�̃E�B���h�E
	/// </summary>
	/// <remarks>����� : �Έ䔹�l</remarks>
	public class StageSelectWindow : MenuWindow
    {
		//*************************************************************************************************
		// �v���C�x�[�g�ϐ�
		//*************************************************************************************************
		[SerializeField] private ufloat _moveSpeed;
        private SplineAnimator _anim;

		//*************************************************************************************************
		// �p�u���b�N�֐�
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
