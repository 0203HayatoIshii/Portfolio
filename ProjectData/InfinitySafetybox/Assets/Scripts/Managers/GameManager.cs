using UnityEngine;

using FSystem.Inputs;


namespace Managers
{
	/// <summary>
	/// ゲームシステムの管理を行う
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	internal class GameManager : MonoBehaviour
    {
        [SerializeField] private int FPS;

        private void Awake()
        {
            InputManager.Instance.EnableAllDevice();
            Application.targetFrameRate = FPS;
            QualitySettings.vSyncCount = 0;
        }
	} // GameManager
} // Managers
