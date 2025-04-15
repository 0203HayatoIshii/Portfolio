

namespace FSystem.Inputs
{
    /// <summary>
    /// 入力デバイスの管理クラスの外部インターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TInputType"></typeparam>
    public interface IInputManager<TInputType>
    {
		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 現在登録済みの入力デバイスの個数 </summary>
		public int DeviceCount { get; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 登録されている入力デバイスの中から指定された識別子の入力デバイスを検索し、インスタンスを返す
		/// </summary>
		/// <remarks>検索対象が見つからなかった際の動作は処理系依存</remarks>
		/// <param name="inputType">検索対象の識別子</param>
		/// <returns>見つかったとき => 指定された識別子の入力デバイス</returns>
		public IInputDevice<TInputType> FindDevice(TInputType inputType);
        /// <summary>
        /// 指定された入力デバイスを入力システムに登録する
        /// </summary>
        /// <remarks>既に同じ識別子の入力デバイスが登録されていた場合は登録しない</remarks>
        /// <param name="device">登録する入力デバイスのインスタンス</param>
        /// <returns>成功したとき => true | 失敗したとき => false</returns>
        public bool AddDevice(IInputComponent<TInputType> device);
        /// <summary>
        /// 指定された識別子の入力デバイスの登録を解除する
        /// </summary>
        /// <param name="inputType">解除対象の識別子</param>
        /// <returns>解除に成功したとき => ture | 解除に失敗したとき => false</returns>
        public bool RemoveDevice(TInputType inputType);
        /// <summary>
        /// 指定されたインスタンスの入力デバイスの登録を解除する
        /// </summary>
        /// <param name="device">解除対象のインスタンス</param>
        /// <returns>解除に成功したとき => ture | 解除に失敗したとき => false</returns>
        public bool RemoveDevice(IInputDevice<TInputType> device);
        /// <summary>
        /// 全ての入力デバイスを有効化する
        /// </summary>
        /// <returns>有効化に成功したデバイスの個数</returns>
        public int EnableAllDevice();
        /// <summary>
        /// 全ての入力デバイスを非有効化する
        /// </summary>
        /// <returns>非有効化に成功したデバイスの個数</returns>
        public int DisableAllDevice();
	} // IInputManager<TInputType>
} // FSystem.Inputs
