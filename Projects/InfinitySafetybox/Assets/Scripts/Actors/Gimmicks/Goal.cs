using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using FSystem;


namespace Actors
{
	/// <summary>
	/// ゴール
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	internal class Goal : IntaractGimmick
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private Animator _clearAnim;
        [SerializeField] private string _clearSceneName;
        private Thread _clearAnimThread;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// クリアした瞬間に呼ばれる
		/// </summary>
		public void OnClear()
        {
            _clearAnimThread ??= new Thread();
            _clearAnimThread.StartCoroutine(OnClearHelper());
        }
        public override void Intaract(IVisitor visitor) => visitor.Visit(this);

		//*************************************************************************************************
		// プライベート関数
		//*************************************************************************************************
		/// <summary>
		/// クリア処理を行う
		/// </summary>
		private IEnumerator OnClearHelper()
        {
			// クリアのアニメーションがあれば終わるまで待つ
            if (_clearAnim != null)
            {
                _clearAnim.Play("Clear");
                yield return null;

                while (_clearAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                    yield return null;
            }

			// 次のステージシーンを読み込む
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
