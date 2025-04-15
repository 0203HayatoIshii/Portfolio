#if UNITY_64

using System;
using System.IO;
using System.Text;
using UnityEngine;


namespace FSystem.Databases
{
    public abstract class DataStream<TData> : IDataStream<TData>
    {
        private string _directoryPath;
        private Encoding _encoding;


        /// <summary> 入出力を行うファイルのディレクトリ </summary>
        public string DirectoryPath
        {
            get => _directoryPath;
            set
            {
                // 値がnullか空文字なら既定のパスを使用
                if (string.IsNullOrEmpty(value))
                {
                    _directoryPath = Application.dataPath.Replace('/', Path.DirectorySeparatorChar);
                    return;
                }

                // 最後にパス区切り文字が含まれていたら最後のパス区切り文字を削除
                if (value.EndsWith(Path.DirectorySeparatorChar))
                {
                    value = value.Remove(value.Length - 1);
                }

                string path = value.Replace('/', Path.DirectorySeparatorChar);
                // パスが有効でないならディレクトリを作成
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                _directoryPath = path;
            }
        }


        /// <summary> 読み書きするファイルのエンコード方式 </summary>
        /// <remarks>既定ではUTF-8が使用されます</remarks>
        public Encoding Encoding { get => _encoding; set => _encoding = value ?? throw new ArgumentNullException("Encoding cannot be null"); }



        /// <summary>
        /// データを読み書きするクラス
        /// </summary>
        /// <remarks>Assetsフォルダがルートディレクトリとして設定される</remarks>
        public DataStream(string directoryPath = null)
        {
            DirectoryPath = directoryPath;
            Encoding = Encoding.UTF8;
        }
        /// <summary>
        /// データを読み書きするクラス
        /// </summary>
        public DataStream(string directoryPath, Encoding encoding)
        {
            DirectoryPath = directoryPath;
            Encoding = encoding;
        }


        /// <summary>
        /// 指定された名前のファイルからデータを読み取る
        /// </summary>
        /// <remarks>事前に設定されているディレクトリパスからの相対パスを指定してください</remarks>
        /// <param name="fileName">事前に設定されているディレクトリパスからの相対パス</param>
        /// <returns>読み取られたデータ</returns>
        public TData ReadFile(string fileName)
        {
            // パスを正規化して読み込み
            string targetFilePath = GetFullPath(fileName);
            using var reader = new StreamReader(targetFilePath, Encoding);
            string buffer = reader.ReadToEnd();

            TData ret = ConvertToData(buffer);
            return ret;
        }
        protected abstract TData ConvertToData(string fileData);

        /// <summary>
        /// 指定された名前のファイルとしてデータを書き出す
        /// </summary>
        /// <param name="fileName">事前に設定されているディレクトリパスからの相対パス</param>
        /// <param name="data">書き出すデータ</param>
        public void WriteData(string fileName, in TData data)
        {
            // パスを正規化して読み込み
            string targetFilePath = GetFullPath(fileName);
            using var writer = new StreamWriter(targetFilePath, false, Encoding);

            string temp = ConvertToString(data);
            writer.Write(temp);
            writer.Flush();
        }
        protected abstract string ConvertToString(in TData data);

        /// <summary>
        /// 指定されたファイルをディレクトリパスと結合する
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>ディレクトリパスと結合されたファイルの完全パス</returns>
        protected string GetFullPath(string fileName)
        {
            string ret = (Path.IsPathRooted(fileName)) ? fileName : string.Join(Path.DirectorySeparatorChar, DirectoryPath, fileName);
            return ret;
        }
    }
}

#endif