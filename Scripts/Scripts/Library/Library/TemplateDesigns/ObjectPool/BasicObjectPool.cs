using System;
using System.Collections.Generic;


namespace FSystem
{
    /// <summary>
    /// スタンダードなオブジェクトプール。プールが空になると新しい複製を作成する
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    /// <typeparam name="TObject">プールする対象のデータ型</typeparam>
    public class BasicObjectPool<TObject> : IObjectPool<TObject> where TObject : IPoolableObject<TObject>
    {
        private readonly Stack<TObject> POOL;

        private TObject _origin;

        /// <summary> 複製元のオブジェクト </summary>
        public TObject Origin { get => _origin; set => _origin = (value != null) ? value : throw new ArgumentNullException(); }
        /// <summary> プールに残っているオブジェクトの数 </summary>
        public int RemainCount { get => POOL?.Count ?? 0; }
        /// <summary> 現在のプールの大きさ </summary>
        public int PoolSize { get; private set; }



        /// <summary>
        /// スタンダードなオブジェクトプール。プールが空になると新しい複製を作成する
        /// </summary>
        /// <param name="origin">複製元のオブジェクト</param>
        /// <param name="initPoolSize">初期化時にプールとして確保するメモリサイズ</param>
        /// <exception cref="ArgumentNullException">originがnull</exception>
        public BasicObjectPool(TObject origin, int initPoolSize = 10)
        {
            _origin = origin ?? throw new ArgumentNullException("origin cannot be null");
            initPoolSize = (initPoolSize < 1) ? 1 : initPoolSize;
            POOL = new Stack<TObject>(initPoolSize);
        }

        /// <summary>
        /// プールからオブジェクトを取り出す
        /// </summary>
        /// <remarks>プールが空の場合は新たな複製を作成する</remarks>
        /// <returns>取り出されたオブジェクト</returns>
        public TObject Takeout()
        {
            bool isOk = POOL.TryPop(out TObject result);
            // 取り出しに失敗するか取り出したインスタンスがnullなら新しく複製を作る
            if (!isOk || (result == null))
            {
                if (Origin == null)
                    throw new ArgumentNullException("origin is null");

                result = Origin.Clone(++PoolSize) ?? throw new NullReferenceException("Clone failed");                    
            }
            // 有効化関数を呼んでから返す
            result.Enable((obj) => POOL.Push(obj));
            return result;
        }

        /// <summary>
        /// プールからオブジェクトを取り出す
        /// </summary>
        /// <remarks>プールが空の場合は新たな複製を作成する</remarks>
        /// <param name="value">取り出すオブジェクトの数</param>
        /// <returns>取り出されたオブジェクトの配列</returns>
        public TObject[] Takeout(int value)
        {
            // 配列の例外処理を避けるため、valueは1以上に補正
            value = (value < 1) ? 1 : value;
            TObject[] ret = new TObject[value];
            for (int i = 0; i < value; ++i)
            {
                ret[i] = Takeout();
            }
            return ret;
        }
    }
}