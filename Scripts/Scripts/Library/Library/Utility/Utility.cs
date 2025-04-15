using System;


namespace FSystem
{
    /// <summary>
    /// よく使うUtiliry関数をまとめたクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public static class Utility
    {

#if UNITY_64
        public static bool IsAttached<T>(ref T component, UnityEngine.MonoBehaviour script) where T : class
        {
            if (script == null)
                throw new ArgumentNullException("argument 'script' is not allowed to be null");

            if ((component == null) && !script.TryGetComponent(out component))
                throw new NullReferenceException(component.ToString() + " is not attached");

            return true;
        }
#endif
    }
}