

namespace FSystem.Databases
{
    /// <summary>
    /// 既定のデータベースを作成するファクトリ
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class DefaultDatabaseFactory : IDatabaseFactory
    {
		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 指定された読み取り方を行うデータベースを作成する
		/// </summary>
		/// <typeparam name="TData">扱うデータの型</typeparam>
		/// <param name="stream">読み書きのインターフェース</param>
		/// <returns>作成されたデータベース</returns>
		public IDatabase<TData> GenerateDatabase<TData>(IDataStream<TData> stream) => new Database<TData>(stream);
	} // DefaultDatabaseFactory
} // FSystem.Databases
