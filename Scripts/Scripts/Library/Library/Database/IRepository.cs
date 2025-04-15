

namespace FSystem.Databases
{
    /// <summary>
    /// データベースの生成、保管、削除を行うためのインターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TDatabaseType">データベースの識別子</typeparam>
    public interface IRepository<TDatabaseType>
    {
		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 制作されたデータベースの個数 </summary>
		public int DatabaseCount { get; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 新たなデータベースを作成する
		/// </summary>
		/// <typeparam name="TData">扱うデータの型</typeparam>
		/// <param name="databaseType">作成するデータベースの識別子</param>
		/// <param name="stream">データベースがデータの読み書きを行うクラス</param>
		/// <returns>作成されたデータベース</returns>
		public IDatabase<TData> GenerateDatabase<TData>(TDatabaseType databaseType, IDataStream<TData> stream);
        /// <summary>
        /// 指定された識別子のデータベースを削除する
        /// </summary>
        /// <param name="databaseType">削除するデータベースの識別子</param>
        /// <returns>削除に成功したとき => true | 削除に失敗したとき => false</returns>
        public bool DisposeDatabase(TDatabaseType databaseType);
        /// <summary>
        /// 指定された識別子のデータベースを検索し、最初に見つけた対象を返す
        /// </summary>
        /// <typeparam name="TData">扱うデータ型</typeparam>
        /// <param name="databaseType">検索対象の識別子</param>
        /// <returns>見つかった場合 => 対象のインスタンス | 見つからなかった場合 => null</returns>
        public IDatabase<TData> FindDatabase<TData>(TDatabaseType databaseType);
	} // IRepository<TDatabaseType>
} // FSystem.Databases
