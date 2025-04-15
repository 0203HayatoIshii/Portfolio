using System.Collections.Generic;


namespace FSystem.Databases
{
    /// <summary>
    /// データの読み込み、書き込み、取得、代入を行うインターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public interface IDatabase
    {
		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 記録されているデータの個数 </summary>
		public int DataCount { get; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 指定されたファイルに紐づいたデータが記録済みかを調べる
		/// </summary>
		/// <param name="fileName">調べるファイル名</param>
		/// <returns>記録されているとき => true | 記録されていないとき => false</returns>
		public bool Contain(string fileName);

        /// <summary>
        /// ディレクトリ内のすべてのファイルを読み込む
        /// </summary>
        public int LoadData();
        /// <summary>
        /// 指定されたファイルからデータを読み込む
        /// </summary>
        /// <param name="fileName">読み込むファイルの名前</param>
        /// <returns>読み込みに成功したとき => true | 読み込みに失敗したとき => false</returns>
        public bool LoadData(string fileName);
        /// <summary>
        /// 現在記録済みのデータをすべて破棄する
        /// </summary>
        public void UnloadData();
        /// <summary>
        /// 指定されたファイルに紐づいているデータを破棄する
        /// </summary>
        /// <param name="fileName">解放するデータに紐づいたファイルの名前</param>
        /// <returns>解放に成功したとき => true | 解放に失敗したとき => false</returns>
        public bool UnloadData(string fileName);

        /// <summary>
        /// 現在記録されているデータすべてを紐づいたファイルに書き出す
        /// </summary>
        public void WriteData();
        /// <summary>
        /// 指定されたファイルに紐づいているデータを書き出す
        /// </summary>
        /// <param name="fileName">書き出すデータに紐づいているファイルの名前</param>
        /// <returns>書き出しに成功したとき => true | 書き出しに失敗したとき => false</returns>
        public bool WriteData(string fileName);
	} // IDatabase

	/// <summary>
	/// データの読み書きを行うインターフェース
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	/// <typeparam name="TData">扱うデータの型</typeparam>
	public interface IDatabase<TData> : IDatabase, IEnumerable<KeyValuePair<string, TData>>
    {
		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> データの読み取りを行うクラス </summary>
		public IDataStream<TData> DataStream { get; set; }
        /// <summary>
        /// 指定されたファイルに紐づいているデータ
        /// </summary>
        /// <param name="fileName">データに紐づいているファイル名</param>
        public TData this[string fileName] { get; set; }
	} // IDatabase<TData>
} // FSystem.Databases