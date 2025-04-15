#if UNITY_64

using UnityEngine;

namespace FSystem.UnityAdapters
{
    /// <summary>
    /// renderer系のコンポーネントを同一視するためのアダプタークラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public abstract class UnityRendererAdapter : MonoBehaviour
    {
        public abstract Color color { get; set; }
        public abstract Sprite sprite { get; set; }
    }
}

#endif