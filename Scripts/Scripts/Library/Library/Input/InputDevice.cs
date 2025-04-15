#if UNITY_64

using System;

using UnityEngine;


namespace FSystem.Inputs
{
    /// <summary>
    /// 入力システムの基礎入力デバイスクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TInputType">任意の入力デバイス識別子</typeparam>
    public abstract class InputDevice<TInputType> : MonoBehaviour, IInputComponent<TInputType>
    {
		//*************************************************************************************************
		// パブリックイベント
		//*************************************************************************************************
		/// <summary> コールバック関数 </summary>
		public event Action<union> Callbacks;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 自身の入力デバイス識別子 </summary>
		public abstract TInputType DeviceType { get; }
        /// <summary> 有効かどうかを表すフラグ </summary>
        public bool IsActive { get; private set; }
        /// <summary> 現在の入力値 </summary>
        public union Value { get; protected set; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 新しく入力システムに登録された後、1回だけ呼ばれる関数
		/// </summary>
		public virtual void Init() { Value = union.Zero; }
        /// <summary>
        /// このデバイスを有効化する
        /// </summary>
        public virtual bool Enabled() { Value = union.Zero; IsActive = true; return true; }
        /// <summary>
        /// このデバイスを非有効化する
        /// </summary>
        public virtual bool Disable() { Value = union.Zero; IsActive = false; return true; }
        /// <summary>
        /// 入力システムから登録解除される前、1回だけ呼ばれる関数
        /// </summary>
        public virtual void Final() { Value = union.Zero; IsActive = false; }

        /// <summary>
        /// 入力システムから呼ばれる更新関数
        /// </summary>
        public void UpdateDevice()
        {
            if (IsActive)
            {
                UpdateInputValue();
            }
        }

		//*************************************************************************************************
		// 継承メンバ
		//*************************************************************************************************
		/// <summary>
		/// 入力値の更新を行う
		/// </summary>
		protected abstract void UpdateInputValue();
        /// <summary>
        /// 現在設定されている入力値ですべての登録済みコールバックを呼び出す
        /// </summary>
        protected void InvokeCallbacks() { Callbacks?.Invoke(Value); }
	} // InputDevice<TInputType>
} // FSystem.Inputs

#endif