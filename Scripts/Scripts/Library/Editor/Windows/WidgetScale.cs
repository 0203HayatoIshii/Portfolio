

namespace FEditor
{
    /// <summary>
    /// ウィジェットの大きさ情報
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public struct WidgetScale
    {
		//*************************************************************************************************
		// パブリック定数
		//*************************************************************************************************
		public static readonly WidgetScale Zero = new(0.0f, 0.0f, 0.0f, 0.0f);
        public static readonly WidgetScale HD = new(7.2f, 1920.0f, 4.8f, 1080.0f);

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 最小の横幅 </summary>
		public float minX;
        /// <summary> 最大の横幅 </summary>
        public float maxX;
        /// <summary> 最小の縦幅 </summary>
        public float minY;
        /// <summary> 最大の縦幅 </summary>
        public float maxY;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public WidgetScale(float minX, float maxX, float minY, float maxY)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
        }
        public WidgetScale(float x, float y)
        {
            minX = x;
            maxX = x;
            minY = y;
            maxY = y;
        }
	} // WidgetScale
} // FEditor
