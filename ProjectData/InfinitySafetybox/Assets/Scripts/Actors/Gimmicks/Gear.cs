using System;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using FSystem;
using FSystem.GameComponents;


namespace Actors
{
	/// <summary>
	/// 回転するギア用のクラス
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	internal class Gear : MonoBehaviour, IGimmick, IMoveGimmick, IRollGimmick
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private ufloat _moveSpeed;
        private SplineAnimator _splineAnimator;

        [SerializeField] private Collider2D _affectArea;
        protected RotationComponent _rollComp;
        private List<Collider2D> _overlapColliderBuffer;
        protected List<IRollGimmick> _overlapRollGimmickBuffer;
        [SerializeField]
        private float _rotationTime;
        private bool _isMarked;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 有効状態を示すフラグ </summary>
		public bool Activate { get; set; }
		/// <summary> 動作中かを示すフラグ </summary>
		public bool IsBusy { get => ((_rollComp != null) && _rollComp.IsBusy) || (_splineAnimator?.IsBusy ?? false); }
		/// <summary> 現在居る位置のインデックス番号 </summary>
		public int CurtPointIndex { get => _splineAnimator?.CurtIndex ?? 0; }
		/// <summary> 現在の回転角のインデックス番号 </summary>
		public int CurtDegAngle { get => (_rollComp == null) ? 0 : (int)_rollComp.CurtDegAngle /* TODO : 回転角から扱いやすい値に変換 */ ; }

		/// <summary>
		/// 次の座標に移動する
		/// </summary>
		public void MoveToNextPoint(Action onProcessEnd)
        {
            if (IsBusy || !Activate)
            {
                onProcessEnd?.Invoke();
                return;
            }

            _splineAnimator ??= CreateSplineAnimator();
            _splineAnimator.LinearMoveToNextPos(transform, _moveSpeed, onProcessEnd);
        }
		/// <summary>
		/// 前の座標に移動する
		/// </summary>
		public void MoveToPrevPoint(Action onProcessEnd)
        {
            if (IsBusy || !Activate)
            {
                onProcessEnd?.Invoke();
                return;
            }

            _splineAnimator ??= CreateSplineAnimator();
            _splineAnimator.LinearMoveToPrevPos(transform, _moveSpeed, onProcessEnd);
        }

		/// <summary>
		/// 右回転
		/// </summary>
		public virtual void RollRight(int anglePartitionValue)
        {
            // すでに回転しているか回転できない状態なら何もせず無効値を返す
            if (IsBusy || !Activate)
                return;

            // 周囲の回転ギミックと自身を回転させる
            _rollComp.RollRight(anglePartitionValue);
            foreach (var c in _overlapRollGimmickBuffer)
            {
                c.RollLeft(anglePartitionValue);
            }
        }
		/// <summary>
		/// 左回転
		/// </summary>
		public virtual void RollLeft(int anglePartitionValue)
        {
            // すでに回転しているか回転できない状態なら何もせず無効値を返す
            if (IsBusy || !Activate)
                return;

            // 周囲の回転ギミックと自身を回転させる
            _rollComp.RollLeft(anglePartitionValue);
            foreach (var c in _overlapRollGimmickBuffer)
            {
                c.RollRight(anglePartitionValue);
            }
        }
		/// <summary>
		/// 回転できるかを確認する
		/// </summary>
		/// <returns>回転できるとき => true | 回転できないとき => false</returns>
		public bool CanRoll()
        {
            // 回転できない状態なら何もせず無効値を返す
            if (IsBusy || !Activate)
                return false;

            // このフレームですでに判定が住んでいるなら何もせずに有効値を返す
            if (_isMarked)
                return true;

            // 判定済みのフラグを立てて回転の判定を行う
            _isMarked = true;
            bool ret_canRoll = true;
            int hitCnt = FindOverlapRollGimmicks();
            for (int i = 0; i < hitCnt; ++i)
            {
                // 周囲の回転ギミックが回転できない状態なら回転しない
                bool isOk = _overlapRollGimmickBuffer[i].CanRoll();
                if (isOk)
                    continue;

                ret_canRoll = false;
                break;
            }

            _isMarked = false;
            return ret_canRoll;
        }

        /// <summary>
        /// 周囲の回転ギミックを取得する
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        protected int FindOverlapRollGimmicks()
        {
            if ((_affectArea == null) && !TryGetComponent(out _affectArea))
                return -1;

            // 周囲のコライダーを取得
            var filter = new ContactFilter2D() { useLayerMask = true, layerMask = (1 << 3), useTriggers = true };
            _overlapColliderBuffer ??= new List<Collider2D>(5);
            _overlapRollGimmickBuffer ??= new List<IRollGimmick>(5);
            int hitCnt = _affectArea.OverlapCollider(filter, _overlapColliderBuffer);

            // 回転ギミックを取得
            _overlapRollGimmickBuffer.Clear();
            int ret_listSize = 0;
            for(int i = 0; i < hitCnt; ++i)
            {
                if (!_overlapColliderBuffer[i].TryGetComponent(out IRollGimmick result))
                    continue;

                _overlapRollGimmickBuffer.Add(result);
                ++ret_listSize;
            }
            return ret_listSize;
        }

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
			if (TryGetComponent(out SplineContainer result) && result.Count > 0)
			{
				_splineAnimator = CreateSplineAnimator();
				transform.position = _splineAnimator.GetCurtPos();
			}
			_rollComp = new RotationComponent(transform, _rotationTime);
		}
		//********************************************************************************************************************************//


#if UNITY_EDITOR
		[ContextMenu("Create root")]
        public void CreateRoots()
        {
            if (!TryGetComponent(out SplineContainer result))
                throw new NullReferenceException("'SplineContainer' is not attached");

            // 親オブジェクトの作成
            var root = new GameObject("root");
            // 子オブジェクトの作成
            var point = CreatePoint(root.transform);
            var rail = CreateRail(root.transform);

            // 各ポイントの座標を取得
            Vector3[] points = result.Points;
            if ((points == null) || (points.Length <= 0))
                return;

            // 開始地点のポイントをインスタンス化
            Vector3 prevPos = points[0];
            Instantiate(point, prevPos, Quaternion.identity, root.transform);

            // 直線をインスタンス化
            int len = points.Length;
            for (int i = 1; i < len; ++i)
            {
                // 終了地点のインスタンス化
                Instantiate(point, points[i], Quaternion.identity, root.transform);

                // 一つ前のポイントと今のポイントの座標を比較して直線を作成
                Vector3 diff = prevPos - points[i];
                float degAngle = (Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg) + 90.0f;
                Vector3 centerPos = Vector3.Lerp(prevPos, points[i], 0.5f);
                Quaternion rot = Quaternion.Euler(0.0f, 0.0f, degAngle);
                var temp = Instantiate(rail, centerPos, rot, root.transform);
                temp.size = new Vector2(temp.size.x, diff.magnitude);

                prevPos = points[i];
            }

            // オリジナルを破棄
            DestroyImmediate(point.gameObject);
            DestroyImmediate(rail.gameObject);
        }
        private SpriteRenderer CreatePoint(Transform root)
        {
            // 回転地点のスプライトの作成
            var railPoint = AssetDatabase.LoadAssetAtPath<Sprite>("Assets\\Images\\rail_rollPoint.png");
            if (railPoint == null)
                throw new NullReferenceException();

            // ポイント用のゲームオブジェクトを動的に生成して返す
            var ret_point = new GameObject("point").AddComponent<SpriteRenderer>();
            ret_point.transform.parent = root;
            ret_point.sprite = railPoint;
            return ret_point;
        }
        private SpriteRenderer CreateRail(Transform root)
        {
            // 直線用のスプライトの作成
            var rail = AssetDatabase.LoadAssetAtPath<Sprite>("Assets\\Images\\rail.png");
            if (rail == null)
                throw new NullReferenceException();

            // ライン用のゲームオブジェクトを動的に生成して返す
            var ret_rail = new GameObject("rail").AddComponent<SpriteRenderer>();
            ret_rail.transform.parent = root;
            ret_rail.sprite = rail;
            ret_rail.drawMode = SpriteDrawMode.Tiled;
            ret_rail.sortingOrder = -1;
            return ret_rail;
        }
#endif

	} // Gear
} // Actors
