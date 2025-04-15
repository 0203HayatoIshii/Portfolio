using System;
using System.Collections.Generic;


namespace FSystem
{
    /// <summary>
    /// 固有ハンドル値
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public readonly struct HANDLE : IEquatable<HANDLE>, IEqualityComparer<HANDLE>, IComparable<HANDLE>, IComparer<HANDLE>
    {
        private readonly int _id;

        public HANDLE(int id) => _id = id;
        
        public static bool operator ==(HANDLE lhs, HANDLE rhs) => (lhs._id == rhs._id);
        public static bool operator !=(HANDLE lhs, HANDLE rhs) => (lhs._id != rhs._id);
        public static bool operator ==(HANDLE lhs, int rhs)    => (lhs._id == rhs    );
        public static bool operator !=(HANDLE lhs, int rhs)    => (lhs._id == rhs    );

        public override bool Equals(object obj) => (obj.GetHashCode() == _id);
        public override int GetHashCode() => _id;

        bool IEquatable<HANDLE>.Equals(HANDLE other) => (_id == other._id);

        bool IEqualityComparer<HANDLE>.Equals(HANDLE x, HANDLE y) => (x._id == y._id);
        int IEqualityComparer<HANDLE>.GetHashCode(HANDLE obj) => (obj._id);

        int IComparable<HANDLE>.CompareTo(HANDLE other) => (_id < other._id) ? -1 : ((_id == other._id) ? 0 : 1);
        int IComparer<HANDLE>.Compare(HANDLE x, HANDLE y) => (x._id < y._id) ? -1 : ((x._id == y._id) ? 0 : 1);
    }



    /// <summary>
    /// 固有ハンドル値
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public readonly struct HANDLE<T> : IEquatable<HANDLE<T>>, IEqualityComparer<HANDLE<T>>, IComparable<HANDLE<T>>, IComparer<HANDLE<T>>
    {
        private readonly int _id;

        public HANDLE(int id) => _id = id;

        public static bool operator ==(HANDLE<T> lhs, HANDLE<T> rhs) => (lhs._id == rhs._id);
        public static bool operator !=(HANDLE<T> lhs, HANDLE<T> rhs) => (lhs._id != rhs._id);
        public static bool operator ==(HANDLE<T> lhs, int rhs) => (lhs._id == rhs);
        public static bool operator !=(HANDLE<T> lhs, int rhs) => (lhs._id == rhs);

        public override bool Equals(object obj) => (obj.GetHashCode() == _id);
        public override int GetHashCode() => HashCode.Combine(_id);

        bool IEquatable<HANDLE<T>>.Equals(HANDLE<T> other) => (_id == other._id);

        bool IEqualityComparer<HANDLE<T>>.Equals(HANDLE<T> x, HANDLE<T> y) => (x._id == y._id);
        int IEqualityComparer<HANDLE<T>>.GetHashCode(HANDLE<T> obj) => (obj._id);

        int IComparable<HANDLE<T>>.CompareTo(HANDLE<T> other) => (_id < other._id) ? -1 : ((_id == other._id) ? 0 : 1);
        int IComparer<HANDLE<T>>.Compare(HANDLE<T> x, HANDLE<T> y) => (x._id < y._id) ? -1 : ((x._id == y._id) ? 0 : 1);
    }
}