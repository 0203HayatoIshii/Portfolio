#if UNITY_64

using UnityEngine;
using UnityEngine.UI;

namespace FSystem.UnityAdapters
{
    /// <summary>
    /// Imageコンポーネント用のアダプタークラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class UnityRendererAdapter_Image : UnityRendererAdapter
    {
        [SerializeField]
        private Image _renderer;

        public override Color color { get => Utility.IsAttached(ref _renderer, this) ? _renderer.color : default; set { if (Utility.IsAttached(ref _renderer, this)) _renderer.color = value; } }
        public override Sprite sprite { get => Utility.IsAttached(ref _renderer, this) ? _renderer.sprite : default; set { if (Utility.IsAttached(ref _renderer, this)) _renderer.sprite = value; } }
    }
}

#endif