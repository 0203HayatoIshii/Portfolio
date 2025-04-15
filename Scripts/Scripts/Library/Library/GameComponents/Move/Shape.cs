#if UNITY_64

using UnityEngine;


namespace FSystem.GameComponents
{
    public abstract class Shape : MonoBehaviour
    {
        public Vector3 Center { get; set; }

        public abstract bool IsOverlapped(Vector3 point);
    }
}

#endif