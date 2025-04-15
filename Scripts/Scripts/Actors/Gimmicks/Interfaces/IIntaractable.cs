

namespace Actors
{
    /// <summary>
    /// インタラクトできるオブジェクトのインターフェイス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    internal interface IIntaractable
    {
        /// <summary>
        /// ビジターを受け入れるためのメソッド
        /// </summary>
        /// <param name="visitor">インタラクトしたアクターの参照</param>
        public void Intaract(IVisitor visitor);

        /// <summary>
        /// プレイヤーが触れた瞬間呼ばれる
        /// </summary>
        public void OnEnter();
        /// <summary>
        /// プレイヤーが触れている間呼ばれる
        /// </summary>
        public void OnStay();
        /// <summary>
        /// プレイヤーが離れたとき呼ばれる
        /// </summary>
        public void OnExit();
	} // IIntaractable
} // Actors
