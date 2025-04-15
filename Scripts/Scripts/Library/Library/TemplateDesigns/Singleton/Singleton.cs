

namespace FSystem
{
    /// <summary>
    /// シングルトンオブジェクトを作成するためのジェネリッククラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="T">シングルトン化するクラス　対象はデフォルトコンストラクタを定義している必要がある</typeparam>
    public class Singleton<T> where T : new()
    {
        private static readonly T _singleton = new();

        /// <summary> 唯一のインスタンスを獲得する </summary>
        public static T Instance { get => _singleton; }

        static Singleton() { }
        protected Singleton()
        {
            // 既にインスタンスが存在する状態でnewされたらエラー
            FDebugger.Assert((_singleton != null), "singleton object was created by others");
        }
    }

    /// <summary>
    /// シングルトンオブジェクトを作成するためのジェネリッククラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TClass">シングルトン化するクラス　対象はデフォルトコンストラクタを定義している必要がある</typeparam>
    /// <typeparam name="TInterface">外部に公開されるインターフェース</typeparam>
    public class Singleton<TClass, TInterface> where TClass : TInterface, new()
    {
        private static readonly TClass _singleton = new();

        /// <summary> 唯一のインスタンスを獲得する </summary>
        public static TInterface Instance { get => _singleton; }

        static Singleton() { }
        protected Singleton()
        {
            // 既にインスタンスが存在する状態でnewされたらエラー
            FDebugger.Assert((_singleton != null), "singleton object was created by others");
        }
    }
}