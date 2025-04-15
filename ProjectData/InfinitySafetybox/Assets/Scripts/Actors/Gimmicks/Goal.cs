using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using FSystem;


namespace Actors
{
	/// <summary>
	/// �S�[��
	/// </summary>
	/// <remarks>����� : �Έ䔹�l</remarks>
	internal class Goal : IntaractGimmick
    {
		//*************************************************************************************************
		// �v���C�x�[�g�ϐ�
		//*************************************************************************************************
		[SerializeField] private Animator _clearAnim;
        [SerializeField] private string _clearSceneName;
        private Thread _clearAnimThread;

		//*************************************************************************************************
		// �p�u���b�N�֐�
		//*************************************************************************************************
		/// <summary>
		/// �N���A�����u�ԂɌĂ΂��
		/// </summary>
		public void OnClear()
        {
            _clearAnimThread ??= new Thread();
            _clearAnimThread.StartCoroutine(OnClearHelper());
        }
        public override void Intaract(IVisitor visitor) => visitor.Visit(this);

		//*************************************************************************************************
		// �v���C�x�[�g�֐�
		//*************************************************************************************************
		/// <summary>
		/// �N���A�������s��
		/// </summary>
		private IEnumerator OnClearHelper()
        {
			// �N���A�̃A�j���[�V����������ΏI���܂ő҂�
            if (_clearAnim != null)
            {
                _clearAnim.Play("Clear");
                yield return null;

                while (_clearAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                    yield return null;
            }

			// ���̃X�e�[�W�V�[����ǂݍ���
            bool isOK = int.TryParse(SceneManager.GetActiveScene().name[^1].ToString(), out int result);
            if (isOK)
            {
                UI.ClearMenuWindow.lastClearSatgeIndex = result;
                SceneManager.LoadScene(_clearSceneName);
                yield break;
            }
            SceneManager.LoadScene("StageSelect");
        }
	} // Goal
} // Actors
