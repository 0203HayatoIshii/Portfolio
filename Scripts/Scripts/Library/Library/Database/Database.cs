using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace FSystem.Databases
{
    /// <summary>
    /// 既定のデータベースクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TData">扱うデータの型</typeparam>
    public partial class Database<TData> : IDatabase<TData>
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private readonly Dictionary<string, TData> DATA_LIST;
        private IDataStream<TData> _dataStream;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 記録されているデータの個数 </summary>
		public int DataCount { get => DATA_LIST.Count; }
        /// <summary> データの読み取りを行うクラス </summary>
        public IDataStream<TData> DataStream { get => _dataStream; set { if (value != null) _dataStream = value; } }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 既定のデータベースクラス
		/// </summary>
		public Database(IDataStream<TData> stream)
        {
            DATA_LIST = new Dictionary<string, TData>();
            _dataStream = stream ?? throw new ArgumentNullException("'stream' cannot be null");
        }

        /// <summary>
        /// 指定されたファイルに紐づいているデータ
        /// </summary>
        /// <param name="fileName">データに紐づいているファイル名</param>
        public TData this[string fileName]  { get => DATA_LIST[fileName ?? ""]; set => DATA_LIST[fileName ?? ""] = value; }

        /// <summary>
        /// 指定されたファイルに紐づいたデータが記録済みかを調べる
        /// </summary>
        /// <param name="fileName">調べるファイル名</param>
        /// <returns>記録されているとき => true | 記録されていないとき => false</returns>
        public bool Contain(string fileName) => DATA_LIST.ContainsKey(fileName);

        /// <summary>
        /// ディレクトリ内のすべてのファイルを読み込む
        /// </summary>
        public int LoadData()
        {
            string[] files = Directory.GetFiles(_dataStream.DirectoryPath, "*", SearchOption.AllDirectories);
            if (files == null)
                return 0;

            foreach (var c in files)
            {
                LoadData(c);
            }
            return files.Length;
        }
        /// <summary>
        /// 指定されたファイルからデータを読み込む
        /// </summary>
        /// <param name="fileName">読み込むファイルの名前</param>
        /// <returns>読み込みに成功したとき => true | 読み込みに失敗したとき => false</returns>
        public bool LoadData(string fileName)
        {
            // nullなら空文字列に
            fileName ??= "";

            // 指定されたファイルに紐づいているデータが記録されていれば失敗
            if (DATA_LIST.ContainsKey(fileName))
                return false;

            // 指定されたファイルからデータを読み込む
            TData result = _dataStream.ReadFile(fileName);
            DATA_LIST.Add(fileName, result);
            return true;
        }

        /// <summary>
        /// 現在記録済みのデータをすべて破棄する
        /// </summary>
        public void UnloadData() => DATA_LIST.Clear();
        /// <summary>
        /// 指定されたファイルに紐づいているデータを破棄する
        /// </summary>
        /// <param name="fileName">解放するデータに紐づいたファイルの名前</param>
        /// <returns>解放に成功したとき => true | 解放に失敗したとき => false</returns>
        public bool UnloadData(string fileName) => DATA_LIST.Remove(fileName ?? "");

        /// <summary>
        /// 現在記録されているデータすべてを紐づいたファイルに書き出す
        /// </summary>
        public void WriteData()
        {
            // 記録済みのデータすべてを書き出す
            foreach((string fileName, TData data) in DATA_LIST)
            {
                _dataStream.WriteData(fileName, data);
            }
        }
        /// <summary>
        /// 指定されたファイルに紐づいているデータを書き出す
        /// </summary>
        /// <param name="fileName">書き出すデータに紐づいているファイルの名前</param>
        /// <returns>書き出しに成功したとき => true | 書き出しに失敗したとき => false</returns>
        public bool WriteData(string fileName)
        {
            // nullなら空文字列に
            fileName ??= "";

            // 指定されたファイルに紐づいているデータが記録されていなければ失敗
            bool isOk = DATA_LIST.TryGetValue(fileName, out TData result);
            if (!isOk)
                return false;

            // 指定されたファイルに紐づいたデータが記録済みなら書き出し
            _dataStream.WriteData(fileName, result);
            return true;
        }

        public IEnumerator<KeyValuePair<string, TData>> GetEnumerator() => DATA_LIST.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => DATA_LIST.GetEnumerator();
	} // Database<TData>
} // FSystem.Databases
