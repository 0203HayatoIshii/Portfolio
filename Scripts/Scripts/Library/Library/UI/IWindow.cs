using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;


namespace FSystem.UI
{
    /// <summary>
    /// ウィンドウ用のインターフェイス
    /// </summary>
    /// <typeparam name="TWindowType">ウィンドウの識別子</typeparam>
    /// <remarks>製作者 : 石井隼人</remarks>
    public interface IWindow<TWindowType>
    {
        /// <summary> このウィンドウの名前 </summary>
        public TWindowType Name { get; }
        /// <summary> このウィンドウが有効かのフラグ </summary>
        public bool Enable { get; }

        /// <summary>
        /// ウィンドウに初めて入ったときに呼ばれる
        /// </summary>
        public void OnEnter();
        /// <summary>
        /// 描画されているときに呼ばれる
        /// </summary>
        public void OnStay();
        /// <summary>
        /// ウィンドウが破棄されるときに呼ばれる
        /// </summary>
        public void OnExit();
        /// <summary>
        /// 別ウィンドウから戻ってきたときに呼ばれる
        /// </summary>
        public void OnBacked();
        /// <summary>
        /// 別ウィンドウに変更される前に呼ばれる
        /// </summary>
        public void OnOverlaped();
    }
}