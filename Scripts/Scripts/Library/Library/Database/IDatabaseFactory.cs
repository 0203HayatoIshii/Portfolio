

namespace FSystem.Databases
{
    /// <summary>
    /// データベースを作成するインターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public interface IDatabaseFactory
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
		public IDatabase<TData> GenerateDatabase<TData>(IDataStream<TData> stream);
	} // IDatabaseFactory
} // FSystem.Databases
