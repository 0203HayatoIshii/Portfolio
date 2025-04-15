using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace FSystem.GameComponents
{
	/// <summary>
	/// スプラインで使用する地点を記録するコンテナ
	/// </summary>
	/// <remarks>製作者 : 石井隼人</remarks>
	public class SplineContainer : MonoBehaviour, ICollection<Vector3>, IEnumerable<Vector3>
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		[SerializeField] private List<Vector3> _points;

		//*************************************************************************************************
		// パブリックプロパティ
		//*************************************************************************************************
		/// <summary> ポイントの個数 </summary>
		public int Count { get => _points?.Count ?? 0; }
		/// <summary> ポイントの配列 </summary>
		public Vector3[] Points { get => _points?.ToArray(); }
		/// <summary> 読み取り専用化を示すフラグ </summary>
		public bool IsReadOnly { get => false; }

		public Vector3 this[int index] 
        {
            get => ((index < 0) || (index >= Count)) ? Vector3.zero : _points[index]; 
            set => _points[index] = value; 
        }

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public void Add(Vector3 point)
        {
            _points ??= new List<Vector3>(1);
            _points.Add(point);
        }
        public bool Remove(Vector3 item) => _points.Remove(item);
        public void Remove(int index) => _points.RemoveAt(index);
        public void Clear() => _points?.Clear();

        public bool Contains(Vector3 item) => _points?.Contains(item) ?? false;
        public void CopyTo(Vector3[] array, int arrayIndex) => _points.CopyTo(array, arrayIndex);

        public IEnumerator<Vector3> GetEnumerator() => _points?.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _points?.GetEnumerator();
	} // SplineContainer
} // FSystem.GameComponents