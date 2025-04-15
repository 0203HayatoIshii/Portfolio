using System;


namespace FSystem.Inputs
{
    /// <summary>
    /// 入力デバイスの外部インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TInputType">任意の入力デバイス識別子</typeparam>
    public interface IInputDevice<TInputType>
    {
		//*************************************************************************************************
		// パブリックイベント
		//*************************************************************************************************
		/// <summary> コールバック関数 </summary>
		public event Action<union> Callbacks;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 自身の入力デバイス識別子 </summary>
		public TInputType DeviceType { get; }
        /// <summary> 有効かどうかを表すフラグ </summary>
        public bool IsActive { get; }
        /// <summary> 現在の入力値 </summary>
        public union Value { get; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// このデバイスを有効化する
		/// </summary>
		public bool Enabled();
        /// <summary>
        /// このデバイスを非有効化する
        /// </summary>
        public bool Disable();
	} // IInputDevice<TInputType>
} // FSystem.Inputs
