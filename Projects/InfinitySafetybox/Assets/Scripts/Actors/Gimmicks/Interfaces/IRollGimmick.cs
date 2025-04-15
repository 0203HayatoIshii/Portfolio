

namespace Actors
{
    /// <summary>
    /// 回転ギミック用のインターフェイス
    /// </summary>
    /// <remarks>製作者 : 石井隼人</remarks>
    internal interface IRollGimmick : IGimmick
    {
        /// <summary> 現在の回転角のインデックス番号 </summary>
        public int CurtDegAngle{ get; }
        /// <summary>
        /// 右回転
        /// </summary>
        public void RollRight(int anglePartitionValue);
        /// <summary>
        /// 左回転
        /// </summary>
        public void RollLeft(int anglePartitionValue);
		/// <summary>
		/// 回転できるかを確認する
		/// </summary>
		/// <returns>回転できるとき => true | 回転できないとき => false</returns>
        public bool CanRoll();
	} // IRollGimmick
} // Actors
