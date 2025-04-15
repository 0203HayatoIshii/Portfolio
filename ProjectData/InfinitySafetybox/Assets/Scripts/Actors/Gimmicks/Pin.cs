using System;

using UnityEngine;

using FSystem;
using FSystem.GameComponents;


namespace Actors
{
    internal class Pin : MonoBehaviour, IGimmick
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] protected ufloat _moveSpeed;
        [SerializeField] private Collider2D _myCol;
        [SerializeField] private SpriteRenderer _stopRenderer;
        protected SplineAnimator _splineAnimator;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		public bool Activate { get; set; }
        public bool IsBusy { get => (_splineAnimator?.IsBusy ?? false); }
        public int CurtPointIndex { get => _splineAnimator?.CurtIndex ?? 0; }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// ピンを引き抜く
		/// </summary>
		public void Pull()
        {
			// 有効状態でなければ無視
            if (IsBusy || !Activate)
                return;

			// スプラインを使ってピンの位置を移動させる
			_splineAnimator ??= CreateSplineAnimator();
            _myCol.enabled = false;
            _stopRenderer.enabled = false;
            _splineAnimator.LinearMoveToNextPos(transform, _moveSpeed);
        }

		//*************************************************************************************************
		// プライベート関数
		//*************************************************************************************************
		/// <summary>
		/// スプラインアニメーターを作成する
		/// </summary>
		protected SplineAnimator CreateSplineAnimator()
        {
            if (!TryGetComponent(out SplineContainer result))
                throw new NullReferenceException("'SplineContainer' is not attached");

            return new SplineAnimator(result, new RepeatCounter(0, result.Count - 1));
        }

		private void Awake()
		{
			Activate = true;
			_splineAnimator = CreateSplineAnimator();
			transform.position = _splineAnimator.GetCurtPos();
		}
	} // Pin
} // Actors
