#if UNITY_64

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace FSystem
{
    public partial class FDebugger
    {
        /// <summary>
        /// 条件式がtrueの時にアサーションを起こす
        /// </summary>
        /// <param name="condition">条件式</param>
        /// <param name="message">出力メッセージ</param>
        /// <param name="path">システムによって決定する値 引数を指定しないこと</param>
        /// <param name="line">システムによって決定する値 引数を指定しないこと</param>
        [Conditional("UNITY_EDITOR")]
        public static void Assert(bool condition, string message = null,
            [CallerFilePath] string path = "", [CallerLineNumber] int line = 0)
        {
            // 条件がfalseなら何もせず抜ける
            if (!condition)
                return;

            // デフォルト時のメッセージ
            message ??= "asserting !!!";

            // ログの出力とエディタの停止
            UnityEngine.Debug.LogError($@"<a href=""{path}"" line=""{line}""><color=#C4E200>{message}{Environment.NewLine}{path}:{line}</color></a>");
            UnityEngine.Debug.Break();
        }
    }
}

#endif