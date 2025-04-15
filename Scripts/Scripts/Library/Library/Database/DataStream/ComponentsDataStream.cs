#if UNITY_64

using System.Text;

using UnityEngine;


namespace FSystem.Databases
{
    public class ComponentsDataStream<TComponent> : DataStream<TComponent>
        where TComponent : MonoBehaviour
    {
        private readonly GameObject ATTACH_TARGET;


        /// <summary>
        /// Jsonフォーマットのデータを読み書きするクラス
        /// </summary>
        /// <remarks>Assetsフォルダがルートディレクトリとして設定される</remarks>
        public ComponentsDataStream(GameObject attachTarget, string directoryPath = null) : base(directoryPath)
        {
            ATTACH_TARGET = attachTarget;
        }

        /// <summary>
        /// Jsonフォーマットのデータを読み書きするクラス
        /// </summary>
        public ComponentsDataStream(GameObject attachTarget, string directoryPath, Encoding encoding) : base(directoryPath, encoding)
        {
            ATTACH_TARGET = attachTarget;
        }


        protected override TComponent ConvertToData(string fileData)
        {
            var ret = ATTACH_TARGET.AddComponent<TComponent>();
            JsonUtility.FromJsonOverwrite(fileData, ret);
            return ret;
        }

        protected override string ConvertToString(in TComponent data) => JsonUtility.ToJson(data);
    }
}

#endif