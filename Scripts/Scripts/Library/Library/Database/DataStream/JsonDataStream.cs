#if UNITY_64

using System.Text;

using UnityEngine;


namespace FSystem.Databases
{
    /// <summary>
    /// Jsonフォーマットのデータを読み書きするクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public partial class JsonDataStream<TData> : DataStream<TData>
    {
        /// <summary>
        /// Jsonフォーマットのデータを読み書きするクラス
        /// </summary>
        /// <remarks>Assetsフォルダがルートディレクトリとして設定される</remarks>
        public JsonDataStream(string directoryPath = null) : base(directoryPath) { /* NOTHING */ }

        /// <summary>
        /// Jsonフォーマットのデータを読み書きするクラス
        /// </summary>
        public JsonDataStream(string directoryPath, Encoding encoding) : base(directoryPath, encoding) { /* NOTHING */ }

        protected override TData ConvertToData(string fileData) => JsonUtility.FromJson<TData>(fileData);
        protected override string ConvertToString(in TData data) => JsonUtility.ToJson(data);
    }
}

#endif