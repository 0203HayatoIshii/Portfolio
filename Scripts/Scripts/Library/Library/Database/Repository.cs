using System;
using System.Collections.Generic;


namespace FSystem.Databases
{
    /// <summary>
    /// 既定のリポジトリクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class Repository : Repository<DefaultDatabaseFactory> { }

    /// <summary>
    /// データベースの作成、削除、取得を行うクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class Repository<TFactory> : Singleton<Repository<TFactory>, IRepository<string>>, IRepository<string> where TFactory : IDatabaseFactory, new()
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private readonly Dictionary<string, IDatabase> DATABASE_LIST;
        private readonly IDatabaseFactory FACTORY;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 制作されたデータベースの個数 </summary>
		public int DatabaseCount { get => DATABASE_LIST.Count; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 既定のリポジトリクラス
		/// </summary>
		public Repository()
        {
            DATABASE_LIST = new Dictionary<string, IDatabase>();
            FACTORY = new TFactory();
        }
        /// <summary>
        /// 指定された識別子のデータベースを検索し、最初に見つけた対象を返す
        /// </summary>
        /// <typeparam name="TData">扱うデータ型</typeparam>
        /// <param name="databaseType">検索対象の識別子</param>
        /// <returns>見つかった場合 => 対象のインスタンス | 見つからなかった場合 => null</returns>
        public IDatabase<TData> FindDatabase<TData>(string databaseName)
        {
            databaseName ??= "";
            bool isOk = DATABASE_LIST.TryGetValue(databaseName, out IDatabase result);
            IDatabase<TData> ret = (isOk) ? result as IDatabase<TData> : null;
            return ret;
        }
        /// <summary>
        /// 新たなデータベースを作成する
        /// </summary>
        /// <typeparam name="TData">扱うデータの型</typeparam>
        /// <param name="databaseName">データベースの識別子</param>
        /// <param name="stream">データベースの使用するデータ読み書きのインターフェース</param>
        /// <returns>識別子が登録済み => 登録されているデータベース | 識別子が未登録 => 新しく作成されたデータベース</returns>
        /// <exception cref="ArgumentNullException">streamがnull</exception>
        /// <exception cref="ArgumentException">databaseTypeに指定した識別子が別のデータベースとして既に使用されている</exception>
        public IDatabase<TData> GenerateDatabase<TData>(string databaseName, IDataStream<TData> stream)
        {
            if (stream == null)
                throw new ArgumentNullException("'stream' cannot be null");

            databaseName ??= "";
            // 既に登録済みの識別子なら登録されているデータベースを返す
            bool isOk = DATABASE_LIST.TryGetValue(databaseName, out IDatabase result);
            if (isOk)
            {
                // データの型が異なった場合は例外を投げる
                if (result is not IDatabase<TData> ret)
                    throw new ArgumentException(databaseName + " is already in use other data type database");

                return ret;
            }

            // 未登録の識別子なら新しいデータベースを作成して返す
            IDatabase<TData> newDatabase = FACTORY.GenerateDatabase(stream);
            DATABASE_LIST.Add(databaseName, newDatabase);
            return newDatabase;
        }
        /// <summary>
        /// 指定された識別子のデータベースを削除する
        /// </summary>
        /// <param name="databaseType">削除するデータベースの識別子</param>
        /// <returns>削除に成功したとき => true | 削除に失敗したとき => false</returns>
        public bool DisposeDatabase(string databaseName) => DATABASE_LIST.Remove(databaseName ?? "");
	} // Repository<TFactory>
} // FSystem.Databases
