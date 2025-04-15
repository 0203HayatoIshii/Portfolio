using System;


namespace Actors
{
    /// <summary>
    /// 移動ギミック用のインターフェース
    /// </summary>
    /// <remarks>製作者 : 石井隼人</remarks>
    internal interface IMoveGimmick : IGimmick
    {
        /// <summary> 現在の座標のインデックス番号 </summary>
        public int CurtPointIndex { get; }
        /// <summary>
        /// 次の座標に移動する
        /// </summary>
        public void MoveToNextPoint(Action onProcessEnd);
        /// <summary>
        /// 前の座標に移動する
        /// </summary>
        public void MoveToPrevPoint(Action onProcessEnd);
    }
}