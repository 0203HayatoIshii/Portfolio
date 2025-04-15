using System;

using UnityEngine;


namespace Actors
{
	/// <summary>
	/// ギアハンドル用のクラス
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	internal class GearHandle : IntaractGimmick, IGimmick
    {
		//*************************************************************************************************
		// プライベートデータ
		//*************************************************************************************************
		[Serializable]
        private struct GearData
        {
            public Gear target;
            public int gearToothCount;
        }

		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private GearData[] _rollTargetGears;
        [SerializeField, Range(0.0f, 180.0f)] private float _rotateIncidenceAngle;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 有効状態かのフラグ </summary>
		public bool Activate { get; set; }

		/// <summary>
		/// ビジターを受け入れるためのメソッド
		/// </summary>
		/// <param name="visitor">インタラクトしたアクターの参照</param>
		public override void Intaract(IVisitor visitor) => visitor.Visit(this);

        /// <summary>
        /// ギアを回転させる
        /// </summary>
        /// <param name="invoker">インタラクトした対象のtransform</param>
        public void Roll(Transform invoker)
        {
            if ((_rollTargetGears == null) || !Activate)
                return;

            // 先に回転できる状態かを確認
            int len = _rollTargetGears?.Length ?? 0;
            for(int i = 0; i < len; ++i)
            {
                if (!_rollTargetGears[i].target.CanRoll())
                    return;
            }

            // 回転できるなら接続されているすべてのギアを回転させる
            for (int i = 0; i < len; ++i)
            {
                // 移動方向のベクトルとギアと対象との差のベクトルから回転方向を計算
                Vector3 diff = transform.position - invoker.position;
                float rot = (invoker.eulerAngles.z + 90.0f) * Mathf.Deg2Rad;
                var forward = new Vector3
                {
                    x = Mathf.Cos(rot),
                    y = Mathf.Sin(rot),
                };

                // 急角度なら回転しない
                float angle = Vector3.Angle(forward, diff);
                if (angle > _rotateIncidenceAngle)
                    return;

                // クロス積を使用して回転方向を決める
                float cross = Vector3.Cross(forward, diff).z;
                if (cross < 0)
                {
                    _rollTargetGears[i].target.RollLeft(_rollTargetGears[i].gearToothCount);
                }
                else
                {
                    _rollTargetGears[i].target.RollRight(_rollTargetGears[i].gearToothCount);
                }
            }
        }

		private void Awake() => Activate = true;
	} // GearHandle
} // Actors
