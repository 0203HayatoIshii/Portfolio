using UnityEngine;
using UnityEditor;

using FSystem;
using FSystem.GameComponents;


namespace FEditor
{
	/// <summary>
	/// スプライン用のエディタ
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	[CustomEditor(typeof(SplineContainer), true)]
    public class SplineContainerEditor : Editor
    {
		//*************************************************************************************************
		// プライベートデータ
		//*************************************************************************************************
		private enum EKeyState
        {
            Free    = 0b0001,
            Up      = 0b0011,
            Press   = 0b0100,
            Down    = 0b1100,
		} // EKeyState

		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private static readonly float POINT_SIZE = 0.5f;
        private static readonly string[] TEXTS = new string[] { "Edit spline", "Exit edit mode" };
        private static readonly WidgetScale BUTTON_SIZE = new (100.0f, 400.0f, 20.0f, 20.0f);

        private ICountable _indexCounter;
        private EKeyState _keyState;
        private int _editSplineIndex;
        private Button _editButton;
        private Button _copyMyPositionButton;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public override void OnInspectorGUI()
        {
            _editButton ??= CreateEditorButton();
            _editButton.Draw();
            _copyMyPositionButton ??= CreateCopyMyPositionButton();
            _copyMyPositionButton.Draw();

            base.OnInspectorGUI();
        }

		//*************************************************************************************************
		// プライベート関数
		//*************************************************************************************************
		private void Awake()
        {
            _indexCounter = CreateCounter();
            _editButton = CreateEditorButton();
            _keyState = EKeyState.Free;
            _editSplineIndex = 0;
        }
        private void OnSceneGUI()
        {
            DrawSpline();

            // エディタ状態でないならここで終了
            _indexCounter ??= CreateCounter();
            if (_indexCounter.Position == 0)
                return;

            if (Event.current.keyCode != KeyCode.Space)
                return;

            UpdateKeyState();
            EditSpline();
        }
        /// <summary>
        /// スプラインの描画を行う
        /// </summary>
        private void DrawSpline()
        {
            if ((target is not SplineContainer container) || (container.Count < 1))
                return;

            Vector3 prevPos = container[0];
            foreach (var c in container)
            {
                Handles.DrawLine(prevPos, c);
                Handles.SphereHandleCap(0, c, Quaternion.identity, POINT_SIZE, EventType.Repaint);
                prevPos = c;
            }
        }
        /// <summary>
        /// スプラインを編集する
        /// </summary>
        private void EditSpline()
        {
            switch (_keyState)
            {
                // 押されている間はスプラインの座標を更新
                case EKeyState.Press: UpdateSplinePoint(); return;

                // 押されたときに周囲にスプラインがあるかを確認し、無ければ追加
                case EKeyState.Down: FindEditSplinePoint(); return;

                // どれでもないときは何もしない
                default: return;
            }
        }
        /// <summary>
        /// スプラインの位置を更新する
        /// </summary>
        private void UpdateSplinePoint()
        {
            if ((target is not SplineContainer container) || (_editSplineIndex < 0) || (_editSplineIndex >= container.Count))
                return;

            Vector3 mousePos = GetMousePosition();
            container[_editSplineIndex] = mousePos;
        }
        /// <summary>
        /// 編集対象のスプラインを見つける
        /// </summary>
        private void FindEditSplinePoint()
        {
            if (target is not SplineContainer container)
                return;

            // マウス座標に近いスプラインがあればそのスプラインのインデックス番号を記録
            // 無ければ新しいスプラインを作成
            Vector3 mousePos = GetMousePosition();
            _editSplineIndex = FindOverlapPointIndex(container, mousePos);
            if (_editSplineIndex < 0)
            {
                container.Add(mousePos);
                _editSplineIndex = container.Count - 1;
            }
        }
        /// <summary>
        /// 指定座標周辺のスプラインのインデックス番号を取得する
        /// </summary>
        /// <param name="container">調査対象のスプラインコンテナ</param>
        /// <param name="point">基準座標</param>
        /// <returns>指定座標周辺のスプラインのインデックス番号 | 無い場合は -1 </returns>
        private int FindOverlapPointIndex(SplineContainer container, Vector3 point)
        {
            int len = (container == null) ? 0 : container.Count;
            for(int i = 0; i < len; ++i)
            {
                float sqrDist = (container[i] - point).sqrMagnitude;
                if (sqrDist <= (POINT_SIZE * POINT_SIZE))
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 現在のマウスの位置からワールド座標を取得する
        /// </summary>
        /// <returns>現在のマウスの位置をワールド変換した座標</returns>
        private Vector3 GetMousePosition()
        {
            // raycastを使用して座標を求める
            Vector3 mousePos = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);
            bool isHit = Physics.Raycast(ray, out RaycastHit result);
            if (isHit)
                return result.point;

            // raycastに引っかからなければ座標変換で座標を求める
            Camera sceneCamera = SceneView.currentDrawingSceneView.camera;
            mousePos.z = 1.0f;
            Vector3 world = sceneCamera.ScreenToWorldPoint(mousePos);
            world.y *= -1.0f;
            Vector3 ret = world + sceneCamera.transform.forward * 10.0f;
            return ret;
        }
        /// <summary>
        /// キーの入力状態を更新する
        /// </summary>
        private void UpdateKeyState()
        {
            Event curtEvent = Event.current;
            if (_keyState.HasFlag(EKeyState.Free))
            {
                _keyState = ((curtEvent.keyCode == KeyCode.Space) && (curtEvent.type == EventType.KeyDown)) ? EKeyState.Down : EKeyState.Free;
            }
            else
            {
                _keyState = ((curtEvent.keyCode == KeyCode.Space) && (curtEvent.type == EventType.KeyDown)) ? EKeyState.Press : EKeyState.Up;
            }
        }

        private void OnDisable() => Tools.hidden = false;

		/// <summary>
		/// カウンタを作成する
		/// </summary>
		private ICountable CreateCounter() => new LoopCounter(0, 1);
		/// <summary>
		/// ボタンを作成する
		/// </summary>
		private Button CreateEditorButton()
        {
            _indexCounter ??= CreateCounter();

            var ret = new Button(TEXTS[0], BUTTON_SIZE);
            ret.OnClick += () => { Tools.hidden = (_indexCounter.ToNext() == 1); ret.Text = TEXTS[_indexCounter.Position]; };
            return ret;
        }
        private Button CreateCopyMyPositionButton()
        {
            var ret = new Button("Copy position", BUTTON_SIZE);
            ret.OnClick += () =>
            {
                if (target is SplineContainer result)
                {
                    result.Add(result.transform.position);
                }
            };
            return ret;
        }
	} // SplineContainerEditor
} // FEditor
