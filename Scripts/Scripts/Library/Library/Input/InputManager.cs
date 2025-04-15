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
    /// ���̓V�X�e���̊�b�}�l�[�W���[�N���X
    /// </summary>
    /// <remarks>����� : �Έ䔹�l</remarks>
    public class InputManager : UnitySingleton<InputManager, IInputManager<string>>, IInputManager<string> 
    {
		//*************************************************************************************************
		// �p�u���b�N�f�[�^
		//*************************************************************************************************
		public enum EUpdateType
        {
            Update,
            FixedUpdate,
            LateUpdate,
		} // UpdateType

		//*************************************************************************************************
		// �v���C�x�[�g�ϐ�
		//*************************************************************************************************
		private List<IInputComponent<string>> _deviceList;
        private EUpdateType _curtUpdateType;
        private YieldInstruction _coroutine;

		//*************************************************************************************************
		// �p�u���b�N�v���p�e�B
		//*************************************************************************************************
		/// <summary> ���ݓo�^�ς݂̓��̓f�o�C�X�̌� </summary>
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
		// �p�u���b�N�֐�
		//*************************************************************************************************
		/// <summary>
		/// �o�^�ς݂̓��̓f�o�C�X�̍X�V�������Ă�
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
        /// �o�^����Ă�����̓f�o�C�X�̒�����w�肳�ꂽ���ʎq�̓��̓f�o�C�X���������A�C���X�^���X��Ԃ�
        /// </summary>
        /// <remarks>�����Ώۂ�������Ȃ������ۂ̓���͏����n�ˑ�</remarks>
        /// <param name="inputType">�����Ώۂ̎��ʎq</param>
        /// <returns>���������Ƃ� => �w�肳�ꂽ���ʎq�̓��̓f�o�C�X</returns>
        public IInputDevice<string> FindDevice(string inputType)
        {
            // ������Ȃ��������O�𑗏o
            int hitIdx = _deviceList?.FindIndex((obj) => obj.DeviceType == inputType) ?? -1;
            if (hitIdx < 0)
                throw new NotSupportedException("unredistered inputType" + inputType);

            return _deviceList[hitIdx];
        }

        /// <summary>
        /// �w�肳�ꂽ���̓f�o�C�X����̓V�X�e���ɓo�^����
        /// </summary>
        /// <remarks>���ɓ������ʎq�̓��̓f�o�C�X���o�^����Ă����ꍇ�͓o�^���Ȃ�</remarks>
        /// <param name="device">�o�^������̓f�o�C�X�̃C���X�^���X</param>
        /// <returns>���������Ƃ� => true | ���s�����Ƃ� => false</returns>
        public bool AddDevice(IInputComponent<string> device)
        {
            // �������ʎq�̃X�e�[�g������Ύ��s
            if ((device == null) || (_deviceList?.Exists((obj) => obj.DeviceType == device.DeviceType) ?? false))
                return false;

            // �����̃X�e�[�g�����X�g�ɒǉ�
            _deviceList ??= new List<IInputComponent<string>>(1);
            _deviceList.Add(device);
            device.Init();
            return true;
        }
        /// <summary>
        /// �w�肳�ꂽ���ʎq�̓��̓f�o�C�X�̓o�^����������
        /// </summary>
        /// <param name="inputType">�����Ώۂ̎��ʎq</param>
        /// <returns>�����ɐ��������Ƃ� => ture | �����Ɏ��s�����Ƃ� => false</returns>
        public bool RemoveDevice(string inputType)
        {
            // �o�^���X�g���猟�����A�Ȃ���Ύ��s
            int hitIdx = _deviceList?.FindIndex((obj) => obj.DeviceType == inputType) ?? -1;
            if (hitIdx < 0)
                return false;

            // �q�b�g�����X�e�[�g������
            _deviceList[hitIdx].Final();
            _deviceList.RemoveAt(hitIdx);
            return true;
        }
        /// <summary>
        /// �w�肳�ꂽ�C���X�^���X�̓��̓f�o�C�X�̓o�^����������
        /// </summary>
        /// <param name="device">�����Ώۂ̃C���X�^���X</param>
        /// <returns>�����ɐ��������Ƃ� => ture | �����Ɏ��s�����Ƃ� => false</returns>
        public bool RemoveDevice(IInputDevice<string> device)
        {
            // �o�^���X�g���猟�����A�Ȃ���Ύ��s
            int hitIdx = _deviceList?.FindIndex((obj) => obj == device) ?? -1;
            if (hitIdx < 0)
                return false;

            // �q�b�g�����X�e�[�g������
            _deviceList[hitIdx].Final();
            _deviceList.RemoveAt(hitIdx);
            return true;
        }

        /// <summary>
        /// �S�Ă̓��̓f�o�C�X��L��������
        /// </summary>
        /// <returns>�L�����ɐ��������f�o�C�X�̌�</returns>
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
        /// �S�Ă̓��̓f�o�C�X���L��������
        /// </summary>
        /// <returns>��L�����ɐ��������f�o�C�X�̌�</returns>
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
		// �p�������o
		//*************************************************************************************************
		protected override void Init()
		{
			base.Init();

			// �f�[�^�x�[�X�̍쐬
			IDatabase<string> database = Repository.Instance.FindDatabase<string>(CONSTANTS.DATABASE_NAME) ??
				Repository.Instance.GenerateDatabase(CONSTANTS.DATABASE_NAME, new StringDataStream(CONSTANTS.DIRECTORY_PATH));

			// �t�@�C���̃��[�h�ƃ��X�g�̏�����
			int count = database.LoadData();
			_deviceList = new List<IInputComponent<string>>((count <= 0) ? 1 : count);

			// �t�@�C���f�[�^����f�o�C�X���쐬
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

			// �o�^�ς݂̓��̓f�o�C�X�̗L�����Ə��������s��
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