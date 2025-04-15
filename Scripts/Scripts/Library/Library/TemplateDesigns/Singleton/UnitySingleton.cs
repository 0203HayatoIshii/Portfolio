#if UNITY_64

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace FSystem
{ 
    /// <summary>
     /// シングルトンオブジェクトを作成するためのジェネリッククラス
     /// </summary>
     /// <remarks>制作者 : 石井隼人</remarks>
     /// <typeparam name="T">シングルトン化するクラス　対象はUnitySingleton<T>クラスを継承している必要がある</typeparam>
    public class UnitySingleton<T> : MonoBehaviour where T : UnitySingleton<T>
    {
        private static readonly object @lock = new();
        private static T _singleton;

        /// <summary> シーン内唯一のインスタンス </summary>
        public static T Instance
        {
            get
            {
                if (_singleton != null)
                    return _singleton;

                lock (@lock)
                {
                    if (_singleton == null)
                    {
                        _singleton = FindObjectOfType<T>();             
                        if (_singleton == null)
                        {
                            _singleton = new GameObject("SingletonOf" + typeof(T).Name).AddComponent<T>();
                        }

                        DontDestroyOnLoad(_singleton.transform.root.gameObject);
                        _singleton.Init();
                    }
                }
                return _singleton;
            }
        }

        /// <summary> 空の初期化関数 必要に応じて初期化処理を書き足せる </summary>
        protected virtual void Init() { }

        /// <summary> シーン遷移のタイミングで呼ばれるコールバックを設定する </summary>
        protected void SetCallbackOnChangeScene(UnityAction<Scene, LoadSceneMode> callback) => SceneManager.sceneLoaded += callback;

        protected void Start()
        {
            if (_singleton != null)
                return;

            _singleton = FindObjectOfType<T>();
            if (_singleton == null)
            {
                _singleton = new GameObject("SingletonOf" + typeof(T).Name).AddComponent<T>();
            }

            DontDestroyOnLoad(_singleton.transform.root.gameObject);
            _singleton.Init();
        }
    }

    /// <summary>
    /// シングルトンオブジェクトを作成するためのジェネリッククラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TClass">シングルトン化するクラス　対象はUnitySingleton<T>クラスを継承している必要がある</typeparam>
    /// <typeparam name="TInterface">外部に公開されるインターフェース</typeparam>
    public class UnitySingleton<TClass, TInterface> : MonoBehaviour where TClass : UnitySingleton<TClass, TInterface>, TInterface
    {
        private static readonly object @lock = new();
        private static TClass _singleton;

        /// <summary> シーン内唯一のインスタンス </summary>
        public static TInterface Instance
        {
            get
            {
                if (_singleton != null)
                    return _singleton;

                lock (@lock)
                {
                    if (_singleton == null)
                    {
                        _singleton = FindObjectOfType<TClass>() ?? new GameObject("SingletonOf" + typeof(TClass).Name).AddComponent<TClass>();
                        DontDestroyOnLoad(_singleton.transform.root.gameObject);
                        _singleton.Init();
                    }
                }
                return _singleton;
            }
        }

        /// <summary> 必要に応じて初期化処理を書き足せる </summary>
        protected virtual void Init() { }

        /// <summary> シーン遷移のタイミングで呼ばれるコールバックを設定する </summary>
        protected void SetCallbackOnChangeScene(UnityAction<Scene, LoadSceneMode> callback) => SceneManager.sceneLoaded += callback;

        protected void Start()
        {
            if (_singleton != null)
                return;

            lock (@lock)
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<TClass>() ?? new GameObject("SingletonOf" + typeof(TClass).Name).AddComponent<TClass>();
                    DontDestroyOnLoad(_singleton.transform.root.gameObject);
                    _singleton.Init();
                }
            }
        }
    }
}

#endif