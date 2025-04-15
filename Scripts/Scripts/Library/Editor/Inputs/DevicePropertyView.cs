using System;
using System.IO;
using System.Collections.Generic;

using UnityEngine;

using FSystem.Inputs;
using FSystem.Databases;


namespace FEditor.Inputs
{
	/// <summary>
	/// デバイスのプロパティを設定するウィンドウ
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public class DevicePropertyView : VerticalView
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private readonly IDatabase<string> DATABASE;
        private readonly GameObject DUMMY;
        private readonly CollectionView COLLECTION;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public DevicePropertyView() : base("DevicePropertyView", WidgetScale.HD)
        {
            // データベースの作成
            DATABASE = Repository.Instance.FindDatabase<string>(CONSTANTS.DATABASE_NAME) ??
                Repository.Instance.GenerateDatabase(CONSTANTS.DATABASE_NAME, new StringDataStream(CONSTANTS.DIRECTORY_PATH));

			// コンポーネントをつける用のダミーオブジェクトを作成
            DUMMY = new GameObject("dummyObjectFromInputSettingMenu");

            // ファイルのロードとリストの初期化
            int count = DATABASE.LoadData();
            var dataList = new List<UnityEngine.Object>((count <= 0) ? 1 : count);

            // ファイルデータからデバイスを作成
            foreach ((string name, string jsonTxt) in DATABASE)
            {
                // メタファイルは無視
                if (name.EndsWith(".meta"))
                    continue;

                // ファイル名からクラス名を取得し、中身のjsonデータからデバイスの作成、アタッチを行う
                string className = Path.GetFileNameWithoutExtension(name);
                Type deviceType = Type.GetType(className);
                if (DUMMY.AddComponent(deviceType) is not InputDevice<string> device)
                    continue;

                JsonUtility.FromJsonOverwrite(jsonTxt, device);
                dataList.Add(device);
            }
            DATABASE.UnloadData();

            COLLECTION = AddChild(new CollectionView(dataList));
            AddChild(new DeviceAdditionView());
        }

		/// <summary>
		/// 終了処理
		/// </summary>
        public void End()
        {
			// 現在保存されているファイルをいったん削除
            string[] files = Directory.GetFiles(DATABASE.DataStream.DirectoryPath, "*", SearchOption.AllDirectories);
            if (files != null)
            {
                foreach (string c in files)
                {
                    File.Delete(c);
                }
            }

            // 記録されているデバイスをすべて書き出し
            foreach (UnityEngine.Object c in COLLECTION)
            {
                if (c == null)
                    continue;

                string name = c.GetType().AssemblyQualifiedName;
                string json = JsonUtility.ToJson(c);

                DATABASE[name + ".json"] = json;
            }

            DATABASE.WriteData();
            DATABASE.UnloadData();

            UnityEngine.Object.DestroyImmediate(DUMMY);
        }

		/// <summary>
		/// 管理者に通知を出す
		/// </summary>
		/// <remarks>管理しているウィジェットからの通知を処理したい場合はこの関数をオーバーライドすること</remarks>
		/// <param name="sender">通知元のウィジェット</param>
		/// <param name="message">通知のハンドル番号</param>
		/// <returns>通知が処理されたとき => true | 通知が処理されなかったとき => false</returns>
		public override bool Notify(IWidget sender, string message)
        {
            if (sender.Name == "AddComponent")
            {
				// リフレクションを使ってコンポーネントを追加
                Type deviceType = Type.GetType(message);
                if (deviceType == null)
                {
                    Debug.LogWarning(message + " class is not exist");
                    return true;
                }

				// ダミーオブジェクトにコンポーネントをアタッチ
                if (DUMMY.AddComponent(deviceType) is not InputDevice<string> device)
                    throw new InvalidProgramException();

                COLLECTION.Add(device);
                return true;
            }

            return base.Notify(sender, message);
        }
	} // DevicePropertyView
} // FEditor.Inputs
