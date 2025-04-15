#if !UNITY_64

using System;
using System.Diagnostics;

namespace FSystem
{
    public partial class FDebugger
    {
        public static void Assert(bool condition) => Debug.Assert(condition);
        public static void Assert(bool condition, string message) => Debug.Assert(condition, message);
    }
}

#endif