

namespace FSystem.Inputs
{
    /// <summary>
    /// 入力デバイスの内部インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TInputType">任意の入力デバイス識別子</typeparam>
    public interface IInputComponent<TInputType> : IInputDevice<TInputType>
    {
		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 新しく入力システムに登録された後、1回だけ呼ばれる関数
		/// </summary>
		public void Init();
        /// <summary>
        /// 入力システムから呼ばれる更新関数
        /// </summary>
        public void UpdateDevice();
        /// <summary>
        /// 入力システムから登録解除される前、1回だけ呼ばれる関数
        /// </summary>
        public void Final();
	} // IInputComponent<TInputType>
} // FSystem.Inputs
