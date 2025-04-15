#if UNITY_EDITOR

using System;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;


namespace Test
{
    public class TEST : MonoBehaviour
    {
        [MenuItem("jfief/afuoe")]
        public static void AA()
        {
            Debug.Log(typeof(TEST).Assembly);
        }
    }
}

#endif