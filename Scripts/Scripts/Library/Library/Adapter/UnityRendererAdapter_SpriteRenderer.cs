#if UNITY_64

using UnityEngine;

namespace FSystem.UnityAdapters
{
    /// <summary>
    /// Imageコンポーネント用のアダプタークラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>

    public class UnityRendererAdapter_SpriteRenderer : UnityRendererAdapter
    {
        [SerializeField]
        private SpriteRenderer _renderer;

        public override Color color { get => Utility.IsAttached(ref _renderer, this) ? _renderer.color : default; set { if (Utility.IsAttached(ref _renderer, this)) _renderer.color = value; } }
        public override Sprite sprite { get => Utility.IsAttached(ref _renderer, this) ? _renderer.sprite : default; set { if (Utility.IsAttached(ref _renderer, this)) _renderer.sprite = value; } }
    }
}

#endif