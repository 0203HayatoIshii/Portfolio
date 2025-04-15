using System.Collections.Generic;

using UnityEngine;


namespace FEditor
{
    /// <summary>
    /// エディターウィンドウのビュー用のベースクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public abstract class View : Widget
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		/// <summary> 管理しているウィジェットのリスト </summary>
		private readonly List<IWidget> WIDGET_LIST;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// エディターウィンドウのビュー用のベースクラス
		/// </summary>
		/// <param name="name">このビューの名前</param>
		/// <param name="scale">このビューの大きさ</param>
		/// <param name="style">このビューに適用されるスタイル</param>
		public View(string name, WidgetScale scale, GUIStyle style = null) : base(name, scale, style)
        {
            WIDGET_LIST = new List<IWidget>();
        }

		/// <summary>
		/// 描画処理を行う
		/// </summary>
		public override void Draw()
        {
            foreach (var c in WIDGET_LIST)
            {
                c.Draw();
            }
        }

        /// <summary>
        /// 管理するウィジェットを追加する
        /// </summary>
        /// <param name="child">追加するウィジェット</param>
        public TWidget AddChild<TWidget>(TWidget child) where TWidget : IWidget
        {
            // null なら追加しない
            if (child == null)
                return default;

            WIDGET_LIST.Add(child);
            child.Master = this;
            return child;
        }
        /// <summary>
        /// 管理するウィジェットを追加する
        /// </summary>
        public TWidget AddChild<TWidget>() where TWidget : IWidget, new()
        {
            var child = new TWidget();
            WIDGET_LIST.Add(child);
            child.Master = this;
            return child;
        }
        /// <summary>
        /// 指定されたウィジェットを管理内から外す
        /// </summary>
        /// <param name="target">管理内から外すウィジェット</param>
        /// <returns>成功時 => true | 失敗時 => false</returns>
        public bool RemoveChild(IWidget target)
        {
            if (target == null)
                return false;

            bool isOk = WIDGET_LIST.Remove(target);
            if (!isOk)
                return false;

            target.Master = null;
            return true;
        }
        /// <summary>
        /// 指定されたウィジェットが管理内にあるかを調べる
        /// </summary>
        /// <param name="target">管理内にあるか調べるウィジェット</param>
        /// <returns>管理内にあるとき => true | 管理内にないとき => false</returns>
        public bool Contain(IWidget target) => WIDGET_LIST.Contains(target);

        public IWidget GetChild(int index) => WIDGET_LIST[index];
        public IReadOnlyCollection<IWidget> GetChildren() => WIDGET_LIST.AsReadOnly();
	} // View
} // FEditor
