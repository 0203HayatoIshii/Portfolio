using System;

using UnityEngine;
using UnityEngine.Events;

using FSystem;


namespace Actors
{
	/// <summary>
	/// インタラクトすることでギミックの状態を変えるレバー
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	internal class Lever : IntaractGimmick, IGimmick
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private UnityEvent<Action> _onClick;
        [SerializeField] private ufloat _intarvalTime;
        private int _processCount;
        private Timer _timer;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public bool Activate { get; set; }

		//*************************************************************************************************
		// 説明
		//*************************************************************************************************
		/// <summary>
		/// ビジターを受け入れるためのメソッド
		/// </summary>
		/// <param name="visitor">インタラクトしたアクターの参照</param>
		public override void Intaract(IVisitor visitor) => visitor.Visit(this);

		/// <summary>
		/// インタラクトされたときに呼ばれる
		/// </summary>
        public void OnClick()
        {
			// アクティブでないときやCT以内にインタラクトされているときは無視
            _timer ??= new Timer();
            if (!Activate || (_processCount > 0) || !_timer.IsTimeOver(_intarvalTime))
                return;

			// クリックイベントを発火させ、レバーの向きを反転させる
            _processCount = _onClick.GetPersistentEventCount();
            _onClick?.Invoke(() => --_processCount);
            transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 180.0f);
            _timer.ResetTimer();
        }

		//*************************************************************************************************
		// 説明
		//*************************************************************************************************
		private void Awake()
		{
			Activate = true;
			_timer = new Timer();
		}
	} // Lever
} // Actors
