using UnityEngine;


namespace Actors
{
	/// <summary>
	/// アウトラインを表示する
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class Outliner : MonoBehaviour
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private Renderer _targetRenderer;
        [SerializeField] private Color _outlineColor;
        private bool _colorAdditionFlag;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 初期化処理
		/// </summary>
		public void StartComponent()
        {
            if (_targetRenderer == null)
                return;

            _colorAdditionFlag = true;
            _outlineColor.a = 0;
            _targetRenderer.material.SetColor("_SolidOutline", _outlineColor);
        }
		/// <summary>
		/// 更新処理
		/// </summary>
        public void UpdateComponent()
        {
            if (_targetRenderer == null)
                return;

            UpdateColor();
            _targetRenderer.material.SetColor("_SolidOutline", _outlineColor);
        }
		/// <summary>
		/// 終了処理
		/// </summary>
		public void End()
		{
			if (_targetRenderer == null)
				return;

			_outlineColor.a = 0;
			_targetRenderer.material.SetColor("_SolidOutline", _outlineColor);
		}

		//*************************************************************************************************
		// プライベート関数
		//*************************************************************************************************
		/// <summary>
		/// 色の更新を行う
		/// </summary>
		private void UpdateColor()
        {
			// アウトラインを点滅させる
            const float FLASH_SPEED = 0.05f;
            if (_colorAdditionFlag)
            {
                _outlineColor.a += FLASH_SPEED;
                if (_outlineColor.a >= 1.0f)
                {
                    _colorAdditionFlag = false;
                }
            }
            else
            {
                _outlineColor.a -= FLASH_SPEED;
                if (_outlineColor.a <= 0.0f)
                {
                    _colorAdditionFlag = true;
                }
            }
        }
    } // FlashComponent
} // Actors
