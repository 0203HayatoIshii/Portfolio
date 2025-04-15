using System;

using UnityEngine;

using FSystem;
using FSystem.Inputs;
using FSystem.GameComponents;

namespace Actors
{
	/// <summary>
	/// プレイヤー
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	internal class Player : MonoBehaviour, IVisitor
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private MoveComponent _moveComp;
        [SerializeField] private Collider2D _interactArea;
        [SerializeField] private GameObject _menuScrean;
        private GameObject _menuScreanBuffer;

        private IIntaractable _intaractObjectBuffer;

        private IInputDevice<string> _intaract;
        private IInputDevice<string> _move;
        private IInputDevice<string> _resetButton;
        private IInputDevice<string> _pauseButton;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public void Visit(GearHandle gear)
        {
            transform.parent = gear.transform.root;
            gear.Roll(transform);
        }
        public void Visit(Lever @switch)
        {
            @switch.OnClick();
        }
        public void Visit(Goal goal)
        {
            Disable();
            goal.OnClear();
        }

		/// <summary>
		/// 入力システムの有効化を行う
		/// </summary>
        public void Enable()
        {
            _move ??= InputManager.Instance.FindDevice("PlayerMove");
            _move.Enabled();
            _intaract ??= InputManager.Instance.FindDevice("Intaract");
            _intaract.Enabled();
            _resetButton ??= InputManager.Instance.FindDevice("ResetButton");
            _resetButton.Enabled();
            _pauseButton ??= InputManager.Instance.FindDevice("PauseButton");
            _pauseButton.Enabled();
        }
		/// <summary>
		/// 入力システムの非有効化を行う
		/// </summary>
		public void Disable()
        {
            _move?.Disable();
            _intaract?.Disable();
            _resetButton?.Disable();
            _pauseButton?.Disable();
        }

		//*************************************************************************************************
		// プライベート関数
		//*************************************************************************************************
		private void Awake() => Enable();
		private void Update()
		{
			move(_move.Value);

			findGimmick();
			interact(_intaract.Value);

			pause(_pauseButton.Value);
		}
		/// <summary>
		/// 移動する
		/// </summary>
		/// <param name="input">入力値</param>
		private void move(union input)
        {
            if (_moveComp == null)
                return;

            _moveComp.AddForce((Vector2)input);
            _moveComp.UpdateComponent();

            // 入力が0以外ならプレイヤーの向きを入力値の方向に変える
            if ((Vector2)input != Vector2.zero)
            {
                transform.parent = null;
                var angle = new Vector3 { z = Mathf.Atan2(_moveComp.MoveDirection.y, _moveComp.MoveDirection.x) * Mathf.Rad2Deg - 90.0f };
                transform.eulerAngles = angle;
            }
        }
		/// <summary>
		/// 周囲のギミックを捜索する
		/// </summary>
        private void findGimmick()
        {
            // 周囲のインタラクトできるコライダーを探す
            var overlapColliderAry = new Collider2D[3];
            var filter = new ContactFilter2D() { useTriggers = true, useLayerMask = true, layerMask = (1 << 3) };
            int hitCnt = _interactArea.OverlapCollider(filter, overlapColliderAry);
            for (int i = 0; i < hitCnt; ++i)
            {
                if (!overlapColliderAry[i].TryGetComponent(out IIntaractable result))
                    continue;

                // 同じものに触れていたら更新関数を呼ぶ
                if (_intaractObjectBuffer == result)
                {
                    result.OnStay();
                    return;
                }
                // 違うものに触れたらバッファを入れ替える
                else
                {
                    _intaractObjectBuffer?.OnExit();
                    result?.OnEnter();
                    _intaractObjectBuffer = result;                    
                    return;
                }
            }

            // 何にも触れていなければバッファを空に
            if (_intaractObjectBuffer != null)
            {
                _intaractObjectBuffer.OnExit();
                _intaractObjectBuffer = null;
            }
        }
        /// <summary>
        /// 周囲のギミックにインタラクトを試みる
        /// </summary>
        /// <param name="input">入力値</param>
        /// <exception cref="NullReferenceException">コライダーがアタッチされていない</exception>
        private void interact(union input)
        {
            if (!(bool)input)
                return;

			// インタラクトできるエリアが見つからなかったらエラーを出す
            if ((_interactArea == null) && !TryGetComponent(out _interactArea))
            {
                Debug.LogError("'intaractArea is not attached'");
                return;
            }

            _intaractObjectBuffer?.Intaract(this);
        }

		/// <summary>
		/// キャラクターの一時停止を行う
		/// </summary>
		/// <param name="input"></param>
        private void pause(union input)
        {
            if (!(bool)input)
                return;

            if (_menuScreanBuffer == null)
            {
                _menuScreanBuffer = Instantiate(_menuScrean);
                _move?.Disable();
                _intaract?.Disable();
                _resetButton?.Disable();
            }
            else
            {
                Destroy(_menuScreanBuffer);
                Enable();
            }
        }
	} // Player
} // Actors
