#if UNITY_64

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FSystem.Databases;


namespace FSystem.Inputs
{
    /// <summary>
    /// 入力システムの基礎マネージャークラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class InputManager : UnitySingleton<InputManager, IInputManager<string>>, IInputManager<string> 
    {
		//*************************************************************************************************
		// パブリックデータ
		//*************************************************************************************************
		public enum EUpdateType
        {
            Update,
            FixedUpdate,
            LateUpdate,
		} // UpdateType

		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private List<IInputComponent<string>> _deviceList;
        private EUpdateType _curtUpdateType;
        private YieldInstruction _coroutine;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> 現在登録済みの入力デバイスの個数 </summary>
		public int DeviceCount { get => _deviceList?.Count ?? 0; }
        public EUpdateType CurtUpdateType 
        { 
            get => _curtUpdateType; 
            set 
            {
                _curtUpdateType = value;
                switch (value)
                {
                    case EUpdateType.Update:         _coroutine = null;                      break;
                    case EUpdateType.FixedUpdate:    _coroutine = new WaitForFixedUpdate();  break;
                    case EUpdateType.LateUpdate:     _coroutine = new WaitForEndOfFrame();   break;
                    default:                        _coroutine = null;                      break;
                }
            }
        }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		/// <summary>
		/// 登録済みの入力デバイスの更新処理を呼ぶ
		/// </summary>
		public IEnumerator UpdateDevices()
        {
            while (true)
            {
                if (_deviceList != null)
                {
                    foreach (var device in _deviceList)
                    {
                        device?.UpdateDevice();
                    }
                    yield return _coroutine;
                }
            }
        }

        /// <summary>
        /// 登録されている入力デバイスの中から指定された識別子の入力デバイスを検索し、インスタンスを返す
        /// </summary>
        /// <remarks>検索対象が見つからなかった際の動作は処理系依存</remarks>
        /// <param name="inputType">検索対象の識別子</param>
        /// <returns>見つかったとき => 指定された識別子の入力デバイス</returns>
        public IInputDevice<string> FindDevice(string inputType)
        {
            // 見つからなかったら例外を送出
            int hitIdx = _deviceList?.FindIndex((obj) => obj.DeviceType == inputType) ?? -1;
            if (hitIdx < 0)
                throw new NotSupportedException("unredistered inputType" + inputType);

            return _deviceList[hitIdx];
        }

        /// <summary>
        /// 指定された入力デバイスを入力システムに登録する
        /// </summary>
        /// <remarks>既に同じ識別子の入力デバイスが登録されていた場合は登録しない</remarks>
        /// <param name="device">登録する入力デバイスのインスタンス</param>
        /// <returns>成功したとき => true | 失敗したとき => false</returns>
        public bool AddDevice(IInputComponent<string> device)
        {
            // 同じ識別子のステートがあれば失敗
            if ((device == null) || (_deviceList?.Exists((obj) => obj.DeviceType == device.DeviceType) ?? false))
                return false;

            // 引数のステートをリストに追加
            _deviceList ??= new List<IInputComponent<string>>(1);
            _deviceList.Add(device);
            device.Init();
            return true;
        }
        /// <summary>
        /// 指定された識別子の入力デバイスの登録を解除する
        /// </summary>
        /// <param name="inputType">解除対象の識別子</param>
        /// <returns>解除に成功したとき => ture | 解除に失敗したとき => false</returns>
        public bool RemoveDevice(string inputType)
        {
            // 登録リストから検索し、なければ失敗
            int hitIdx = _deviceList?.FindIndex((obj) => obj.DeviceType == inputType) ?? -1;
            if (hitIdx < 0)
                return false;

            // ヒットしたステートを解除
            _deviceList[hitIdx].Final();
            _deviceList.RemoveAt(hitIdx);
            return true;
        }
        /// <summary>
        /// 指定されたインスタンスの入力デバイスの登録を解除する
        /// </summary>
        /// <param name="device">解除対象のインスタンス</param>
        /// <returns>解除に成功したとき => ture | 解除に失敗したとき => false</returns>
        public bool RemoveDevice(IInputDevice<string> device)
        {
            // 登録リストから検索し、なければ失敗
            int hitIdx = _deviceList?.FindIndex((obj) => obj == device) ?? -1;
            if (hitIdx < 0)
                return false;

            // ヒットしたステートを解除
            _deviceList[hitIdx].Final();
            _deviceList.RemoveAt(hitIdx);
            return true;
        }

        /// <summary>
        /// 全ての入力デバイスを有効化する
        /// </summary>
        /// <returns>有効化に成功したデバイスの個数</returns>
        public int EnableAllDevice()
        {
            int ret_successCount = 0;
            foreach(var device in _deviceList)
            {
                if (device.Enabled())
                {
                    ++ret_successCount;
                }
            }
            return ret_successCount;
        }
        /// <summary>
        /// 全ての入力デバイスを非有効化する
        /// </summary>
        /// <returns>非有効化に成功したデバイスの個数</returns>
        public int DisableAllDevice()
        {
            int ret_successCount = 0;
            foreach (var device in _deviceList)
            {
                if (device.Disable())
                {
                    ++ret_successCount;
                }
            }
            return ret_successCount;
        }

		//*************************************************************************************************
		// 継承メンバ
		//*************************************************************************************************
		protected override void Init()
		{
			base.Init();

			// データベースの作成
			IDatabase<string> database = Repository.Instance.FindDatabase<string>(CONSTANTS.DATABASE_NAME) ??
				Repository.Instance.GenerateDatabase(CONSTANTS.DATABASE_NAME, new StringDataStream(CONSTANTS.DIRECTORY_PATH));

			// ファイルのロードとリストの初期化
			int count = database.LoadData();
			_deviceList = new List<IInputComponent<string>>((count <= 0) ? 1 : count);

			// ファイルデータからデバイスを作成
			foreach ((string name, string jsonTxt) in database)
			{
				if (!name.EndsWith(".json"))
					continue;

				string className = Path.GetFileNameWithoutExtension(name);
				Type deviceType = Type.GetType(className);
				var device = gameObject.AddComponent(deviceType) as IInputComponent<string> ?? throw new InvalidProgramException();
				JsonUtility.FromJsonOverwrite(jsonTxt, device);
				_deviceList.Add(device);
			}

			// 登録済みの入力デバイスの有効化と初期化を行う
			EnableAllDevice();
			foreach (var device in _deviceList)
			{
				device?.Init();
			}

			_curtUpdateType = EUpdateType.Update;
			StartCoroutine(UpdateDevices());
		}
	} // InputManager
} // FSystem.Inputs

#endif