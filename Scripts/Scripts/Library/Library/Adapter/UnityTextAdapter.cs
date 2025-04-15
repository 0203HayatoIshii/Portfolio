#if UNITY_64

using UnityEngine;


namespace FSystem.UnityAdapters
{
    /// <summary>
    /// text系のコンポーネントを同一視するためのアダプタークラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public abstract class UnityTextAdapter : MonoBehaviour
    {
        public abstract string text { get; set; }
        public abstract Color color { get; set; }
        public abstract Font font { get; set; }
        public abstract FontStyle fontStyle { get; set; }
        public abstract int fontSize { get; set; }
    }
}

#endif