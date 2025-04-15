#if UNITY_64


using System.Text;

namespace FSystem.Databases
{
    public class StringDataStream : DataStream<string>
    {
        /// <summary>
        /// データを読み書きするクラス
        /// </summary>
        /// <remarks>Assetsフォルダがルートディレクトリとして設定される</remarks>
        public StringDataStream(string directoryPath = null) : base(directoryPath) { /* NOTHING */ }

        /// <summary>
        /// データを読み書きするクラス
        /// </summary>
        public StringDataStream(string directoryPath, Encoding encoding) : base(directoryPath, encoding) { /* NOTHING */ }



        protected override string ConvertToData(string fileData) => fileData;
        protected override string ConvertToString(in string data) => data;
    }

}

#endif