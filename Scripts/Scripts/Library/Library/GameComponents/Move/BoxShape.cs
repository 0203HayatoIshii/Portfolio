#if UNITY_64

using UnityEngine;


namespace FSystem.GameComponents
{
    public class BoxShape : Shape
    {
        [SerializeField]
        private Vector2 _scale;


        public override bool IsOverlapped(Vector3 point)
        {
            Vector2 scale = _scale / 2.0f;
            Vector2 rightUp = (Vector2)Center + scale;
            Vector2 leftDown = (Vector2)Center - scale;

            bool ret = (point.x <= rightUp.x ) &&
                       (point.x >= leftDown.x) &&
                       (point.y <= rightUp.y ) &&
                       (point.y >= leftDown.y);

            return ret;
        }
    }
}

#endif