#if UNITY_64

using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;


namespace FSystem
{
    /// <summary>
    /// コルーチンを再生するためのクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class Thread
    {
        private static EmptyMonoBehaviour invoker;

        /// <summary> コルーチンが終了したときに呼ばれるコールバック </summary>
        public event Action OnCoroutineEnd;
        private IEnumerator _coroutine;


        public bool IsBusy { get => (_coroutine != null); }


        public Thread()
        {
            _coroutine = null;

            if (invoker == null)
            {
                invoker = new GameObject("CoroutinePlayer").AddComponent<EmptyMonoBehaviour>();
                SceneManager.sceneUnloaded += (scene) => invoker = null;
            }
        }
        private IEnumerator Update()
        {
            while(true)
            {
                yield return null;
                if (_coroutine?.MoveNext() ?? true)
                    continue;

                _coroutine = null;
                if (OnCoroutineEnd != null)
                {
                    OnCoroutineEnd.Invoke();
                    OnCoroutineEnd = null;
                }
                yield break;
            }
        }

        /// <summary>
        /// コルーチンを再生する
        /// </summary>
        /// <param name="coroutine">再生するコルーチン</param>
        public void StartCoroutine(IEnumerator coroutine, Action endCallback = null)
        {
            if ((coroutine == null) || IsBusy)
                return;

            _coroutine = coroutine;
            OnCoroutineEnd = endCallback;
            invoker.StartCoroutine(Update());
        }
        /// <summary>
        /// コルーチンを中止する
        /// </summary>
        /// <param name="coroutine"></param>
        public void StopCoroutine(bool isCallEndCallback = false)
        {
            _coroutine = null;
            if (isCallEndCallback)
            {
                OnCoroutineEnd?.Invoke();
            }
        }
    }
}

#endif