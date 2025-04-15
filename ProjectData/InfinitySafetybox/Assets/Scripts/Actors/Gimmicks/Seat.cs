using System;

using UnityEngine;


namespace Actors
{

    internal class Seat : Gear
    {
        [SerializeField]
        private Pin _targetPin;
        [SerializeField]
        private int _clearAngle;

        private Action _unlock;

        public override void RollLeft(int anglePartitionValue)
        {
            // すでに回転しているか回転できない状態なら何もせず無効値を返す
            if (IsBusy || !Activate)
                return;

            // 周囲の回転ギミックと自身を回転させる
            _unlock ??= Unlock;
            _rollComp.RollLeft(anglePartitionValue, _unlock);
            foreach (var c in _overlapRollGimmickBuffer)
            {
                c.RollRight(anglePartitionValue);
            }
        }

        public override void RollRight(int anglePartitionValue)
        {
            // すでに回転しているか回転できない状態なら何もせず無効値を返す
            if (IsBusy || !Activate)
                return;

            // 周囲の回転ギミックと自身を回転させる
            _unlock ??= Unlock;
            _rollComp.RollRight(anglePartitionValue, _unlock);
            foreach (var c in _overlapRollGimmickBuffer)
            {
                c.RollLeft(anglePartitionValue);
            }
        }

        private void Unlock()
        {
            if ((CurtDegAngle == _clearAngle) && (_targetPin != null))
            {
                _targetPin.Pull();
                Activate = false;
            }
        }
	} // Seat
} // Actors
