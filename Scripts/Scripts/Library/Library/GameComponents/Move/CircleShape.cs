#if UNITY_64

using UnityEngine;


namespace FSystem.GameComponents
{
    public class CircleShape : Shape
    {
        [SerializeField]
        private float _radius;


        public override bool IsOverlapped(Vector3 point)
        {
            Vector2 diff = Center - point;
            bool ret = (diff.sqrMagnitude <= _radius * _radius);
            return ret;
        }
    }
}

#endif