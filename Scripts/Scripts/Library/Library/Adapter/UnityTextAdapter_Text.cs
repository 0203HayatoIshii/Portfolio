#if UNITY_64

using UnityEngine;
using UnityEngine.UI;


namespace FSystem.UnityAdapters
{
    /// <summary>
    /// Textコンポーネント用のアダプタークラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class UnityTextAdapter_Text : UnityTextAdapter
    {
        [SerializeField]
        private Text _text;

        public override string text { get => Utility.IsAttached(ref _text, this) ? _text.text : default; set { if (Utility.IsAttached(ref _text, this)) _text.text = value; } }
        public override Color color { get => Utility.IsAttached(ref _text, this) ? _text.color : default; set { if (Utility.IsAttached(ref _text, this)) _text.color = value; } }
        public override Font font { get => Utility.IsAttached(ref _text, this) ? _text.font : default; set { if (Utility.IsAttached(ref _text, this)) _text.font = value; } }
        public override FontStyle fontStyle { get => Utility.IsAttached(ref _text, this) ? _text.fontStyle : default; set { if (Utility.IsAttached(ref _text, this)) _text.fontStyle = value; } }
        public override int fontSize { get => Utility.IsAttached(ref _text, this) ? _text.fontSize : default; set { if (Utility.IsAttached(ref _text, this)) _text.fontSize = value; } }
    }
}

#endif