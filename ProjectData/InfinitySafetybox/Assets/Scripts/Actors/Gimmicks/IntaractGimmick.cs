using UnityEngine;


namespace Actors
{
	/// <summary>
	/// インタラクトできるギミックの抽象クラス
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	internal abstract class IntaractGimmick : MonoBehaviour, IIntaractable
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private Outliner _flash;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// ビジターを受け入れるためのメソッド
		/// </summary>
		/// <param name="visitor">インタラクトしたアクターの参照</param>
		public abstract void Intaract(IVisitor visitor);
		/// <summary>
		/// プレイヤーが触れた瞬間呼ばれる
		/// </summary>
		public void OnEnter()
        {
            if (_flash == null)
                return;

            _flash.StartComponent();
        }
		/// <summary>
		/// プレイヤーが触れている間呼ばれる
		/// </summary>
		public void OnStay()
        {
            if (_flash == null)
                return;

            _flash.UpdateComponent();
        }
		/// <summary>
		/// プレイヤーが離れたとき呼ばれる
		/// </summary>
		public void OnExit()
        {
            if (_flash == null)
                return;

            _flash.End();
        }
	} // IntaractGimmick
} // Actors
