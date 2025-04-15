

namespace Actors
{
	/// <summary>
	/// ギミック用のインターフェース
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	internal interface IGimmick
    {
        /// <summary> 有効状態かのフラグ </summary>
        public bool Activate { get; set; }
	} // IGimmick
} // Actors
