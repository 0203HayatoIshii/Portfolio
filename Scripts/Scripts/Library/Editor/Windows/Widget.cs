using UnityEngine;


namespace FEditor
{
	/// <summary>
	/// UIコンポーネントのインターフェイス
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public interface IWidget
    {
		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> このウィジェットの名前 </summary>
		public string Name { get; }
        /// <summary> このウィジェットのアクティブ状態かのフラグ </summary>
        public bool IsEnable { get; set; }
        /// <summary> このウィジェットの管理者 </summary>
        public View Master { get; internal set; }
        /// <summary> このウィジェットに適用されているスタイル </summary>
        public GUIStyle Style { get; set; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 描画処理を行う
		/// </summary>
		public void Draw();
        /// <summary>
        /// 管理者に通知を出す
        /// </summary>
        /// <remarks>管理しているウィジェットからの通知を処理したい場合はこの関数をオーバーライドすること</remarks>
        /// <param name="sender">通知元のウィジェット</param>
        /// <param name="message">通知の内容</param>
        /// <returns>通知が処理されたとき => true | 通知が処理されなかったとき => false</returns>
        public bool Notify(IWidget sender, string message);
	} // IWidget


	/// <summary>
	/// エディターウィンドウに使用するウィジェットのベースクラス
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public abstract class Widget : IWidget
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		protected readonly GUILayoutOption[] OPTIONS;
        private GUIStyle _style;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> このウィジェットの名前 </summary>
		public string Name { get; set; }
        /// <summary> このウィジェットのアクティブ状態かのフラグ </summary>
        public bool IsEnable { get; set; }
        /// <summary> このウィジェットの管理者 </summary>
        View IWidget.Master { get; set; }
        /// <summary> このウィジェットに適用されているスタイル </summary>
        public GUIStyle Style { get => _style; set => _style = value ?? GUIStyle.none; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// エディターウィンドウに使用するウィジェットのベースクラス
		/// </summary>
		/// <param name="name">このウィジェット名前</param>
		/// <param name="scale">このウィジェットの大きさ</param>
		/// <param name="style">このウィジェットのスタイル</param>
		public Widget(string name, WidgetScale scale, GUIStyle style = null)
        {
            OPTIONS = new GUILayoutOption[] { GUILayout.MinWidth(scale.minX), GUILayout.MaxWidth(scale.maxX), GUILayout.MaxHeight(scale.maxY), GUILayout.MinHeight(scale.minY) };
            Name = name;
            IsEnable = true;
            Style = style;
        }
        /// <summary>
        /// 描画処理を行う
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// 管理者に通知を出す
        /// </summary>
        /// <remarks>管理しているウィジェットからの通知を処理したい場合はこの関数をオーバーライドすること</remarks>
        /// <param name="sender">通知元のウィジェット</param>
        /// <param name="message">通知のハンドル番号</param>
        /// <returns>通知が処理されたとき => true | 通知が処理されなかったとき => false</returns>
        public virtual bool Notify(IWidget sender, string message) => ((IWidget)this).Master?.Notify(sender, message) ?? false;
	} // Widget
} // FEditor
