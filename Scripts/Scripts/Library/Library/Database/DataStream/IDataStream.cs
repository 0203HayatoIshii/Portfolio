

namespace FSystem.Databases
{
    /// <summary>
    /// データの入出力を行うインターフェース
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public interface IDataStream<TData>
    {
        /// <summary> 入出力を行うファイルのディレクトリ </summary>
        public string DirectoryPath { get; set; }

        /// <summary>
        /// 指定された名前のファイルからデータを読み取る
        /// </summary>
        /// <remarks>事前に設定されているディレクトリパスからの相対パスを指定してください</remarks>
        /// <param name="fileName">事前に設定されているディレクトリパスからの相対パス</param>
        /// <returns>読み取られたデータ</returns>
        public TData ReadFile(string fileName);
        /// <summary>
        /// 指定された名前のファイルとしてデータを書き出す
        /// </summary>
        /// <param name="fileName">事前に設定されているディレクトリパスからの相対パス</param>
        /// <param name="data">書き出すデータ</param>
        public void WriteData(string fileName, in TData data);
    }
}