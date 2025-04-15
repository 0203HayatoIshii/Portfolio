using System;

using UnityEngine;
using UnityEngine.Events;

using FSystem;
using FSystem.Inputs;


namespace UI
{
	/// <summary>
	/// ���C�����j���[�̃E�B���h�E
	/// </summary>
	/// <remarks>����� : �Έ䔹�l</remarks>
	public class MenuWindow : MonoBehaviour
    {
		//*************************************************************************************************
		// �v���C�x�[�g�f�[�^
		//*************************************************************************************************
		[Serializable]
        private struct ButtonData
        {
            public Transform transform;
            public Vector3 cursorScale;
            public UnityEvent onClick;
        }
        [Serializable]
        private struct ButtonList
        {
            public ButtonData[] x;
        }

		//*************************************************************************************************
		// �v���C�x�[�g�ϐ�
		//*************************************************************************************************
		[SerializeField] private ButtonList[] _buttonArray;

        [SerializeField] private Transform _cursorObject;
        private Vector2Int _cursorPosition;

        private IInputDevice<string> _cursorMove;
        private IInputDevice<string> _interactMenu;

		//*************************************************************************************************
		// �p�u���b�N�֐�
		//*************************************************************************************************
		public void SetCursorPosition(union input)
        {
            var vec = (Vector2Int)input;
            _cursorPosition.y -= vec.y;
            _cursorPosition.y = Mathf.Clamp(_cursorPosition.y, 0, (_buttonArray.Length - 1));
            _cursorPosition.x += vec.x;
            _cursorPosition.x = Mathf.Clamp(_cursorPosition.x, 0, (_buttonArray[_cursorPosition.y].x.Length  - 1));
            _cursorObject.position = _buttonArray[_cursorPosition.y].x[_cursorPosition.x].transform.position;
            _cursorObject.localScale = _buttonArray[_cursorPosition.y].x[_cursorPosition.x].cursorScale;
        }
        public void Invoke(union input)
        {
            _buttonArray[_cursorPosition.y].x[_cursorPosition.x].onClick?.Invoke();
        }

		//*************************************************************************************************
		// �v���C�x�[�g�ϐ�
		//*************************************************************************************************
		private void Awake()
		{
			_cursorMove = InputManager.Instance.FindDevice("CursorMove");
			_cursorMove.Callbacks += SetCursorPosition;
			_interactMenu = InputManager.Instance.FindDevice("InteractMenu");
			_interactMenu.Callbacks += Invoke;
		}
		private void OnDestroy()
		{
			if (_cursorMove != null)
			{
				_cursorMove.Callbacks -= SetCursorPosition;
			}
			if (_interactMenu != null)
			{
				_interactMenu.Callbacks -= Invoke;
			}
		}
	} // MenuWindow
} // UI