using System.Collections.Generic;


namespace FSystem.UI
{
    /// <summary>
    /// ウィンドウの履歴を管理するクラス
    /// </summary>
    /// <typeparam name="TWindowType">ウィンドウの識別子</typeparam>
    /// <remarks>製作者 : 石井隼人</remarks>
    public class WindowHistory<TWindowType>
    {
        /// <summary>
        /// nullオブジェクト
        /// </summary>
        private class NullWindow : IWindow<TWindowType>
        {
            public TWindowType Name { get => default; }
            public bool Enable { get => true; }

            public void OnEnter()     { /* NOTHING */ }
            public void OnStay()      { /* NOTHING */ }
            public void OnExit()      { /* NOTHING */ }
            public void OnBacked()    { /* NOTHING */ }
            public void OnOverlaped() { /* NOTHING */ }
        }


        private readonly Stack<IWindow<TWindowType>> HISTORY;
        private IWindow<TWindowType> _reservedWindow;

        /// <summary> 現在更新中のウィンドウ </summary>
        public IWindow<TWindowType> CurtWindow { get; private set; }



        /// <summary>
        /// ウィンドウの履歴を管理するクラス
        /// </summary>
        /// <typeparam name="TWindowType">ウィンドウの識別子</typeparam>
        /// <remarks>製作者 : 石井隼人</remarks>
        public WindowHistory(int initBufferSize = 5)
        {
            HISTORY = new Stack<IWindow<TWindowType>>(initBufferSize);
            _reservedWindow = null;
            CurtWindow = new NullWindow();
        }
        /// <summary>
        /// 最新のウィンドウを更新する
        /// </summary>
        public void UpdateWindow()
        {
            ChangeWindow();
            CurtWindow.OnStay();
        }

        /// <summary>
        /// 新しいウィンドウへの変更を予約する
        /// </summary>
        /// <param name="window">予約する新しいウィンドウのインスタンス</param>
        public void Push(IWindow<TWindowType> window)
        {
            _reservedWindow = window;
        }
        /// <summary>
        /// ウィンドウをひとつ前に戻すことを予約する
        /// </summary>
        public void Pop()
        {
            HISTORY.TryPeek(out _reservedWindow);
        }

        /// <summary>
        /// 可能なら新しいウィンドウに変更する
        /// </summary>
        private void ChangeWindow()
        {
            // 予約されたウィンドウが非有効なら何もしない
            if (_reservedWindow == null || !_reservedWindow.Enable)
            {
                _reservedWindow = null;
                return;
            }

            // ひとつ前のウィンドウと同じインスタンスなら戻った判定
            if (HISTORY.TryPeek(out IWindow<TWindowType> result) && (result == _reservedWindow))
            {
                _reservedWindow.OnBacked();
                CurtWindow.OnExit();
                CurtWindow = _reservedWindow;
                HISTORY.Pop();
                _reservedWindow = null;
                return;
            }

            // ひとつ前のウィンドウと違うインスタンスなら新しいウィンドウに行った判定
            _reservedWindow.OnEnter();
            CurtWindow.OnOverlaped();
            HISTORY.Push(CurtWindow);
            CurtWindow = _reservedWindow;
            _reservedWindow = null;
        }
	} // WindowHistory
} // FSystem.UI
