using System;
using System.Collections.Generic;



namespace FSystem
{
    /// <summary>
    /// 符号なし実数(float)
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    [Serializable]
    public struct ufloat : IEquatable<ufloat>, IEqualityComparer<ufloat>, IComparable<ufloat>, IComparer<ufloat>
    {
        public readonly static ufloat zero = new();

#if UNITY_64
        [UnityEngine.SerializeField, UnityEngine.Min(0.0f)]
#endif
        private float _value;
        

        public ufloat(float value) => _value = (value < 0.0f) ? 0.0f : value;

        public static explicit operator int(in ufloat value)    => (value._value < 0.0f) ? 0 : (int)value._value;
        public static explicit operator float(in ufloat value)  => (value._value < 0.0f) ? 0.0f : value._value;
        public static explicit operator ufloat(int value)   => new (value);
        public static explicit operator ufloat(float value) => new (value);

        public static ufloat operator +(in ufloat lhs, in ufloat rhs) => new (lhs._value + rhs._value);
        public static ufloat operator -(in ufloat lhs, in ufloat rhs) => new(lhs._value - rhs._value);
        public static ufloat operator *(in ufloat lhs, in ufloat rhs) => new(lhs._value * rhs._value);
        public static ufloat operator /(in ufloat lhs, in ufloat rhs) => new(lhs._value / rhs._value);

        public static ufloat operator +(in ufloat lhs, in float rhs) => new(lhs._value + rhs);
        public static ufloat operator -(in ufloat lhs, in float rhs) => new(lhs._value - rhs);
        public static ufloat operator *(in ufloat lhs, in float rhs) => new(lhs._value * rhs);
        public static ufloat operator /(in ufloat lhs, in float rhs) => new(lhs._value / rhs);

        public static ufloat operator +(in float lhs, in ufloat rhs) => new(lhs + rhs._value);
        public static ufloat operator -(in float lhs, in ufloat rhs) => new(lhs - rhs._value);
        public static ufloat operator *(in float lhs, in ufloat rhs) => new(lhs * rhs._value);
        public static ufloat operator /(in float lhs, in ufloat rhs) => new(lhs / rhs._value);

        public static bool operator <(in ufloat lhs, in ufloat rhs) => (lhs._value < rhs._value);
        public static bool operator >(in ufloat lhs, in ufloat rhs) => (lhs._value > rhs._value);
        public static bool operator <=(in ufloat lhs, in ufloat rhs) => (lhs._value <= rhs._value);
        public static bool operator >=(in ufloat lhs, in ufloat rhs) => (lhs._value >= rhs._value);
        public static bool operator ==(in ufloat lhs, in ufloat rhs) => (lhs._value == rhs._value);
        public static bool operator !=(in ufloat lhs, in ufloat rhs) => (lhs._value != rhs._value);

        public static bool operator <(in ufloat lhs, in float rhs) => (lhs._value < rhs);
        public static bool operator >(in ufloat lhs, in float rhs) => (lhs._value > rhs);
        public static bool operator <=(in ufloat lhs, in float rhs) => (lhs._value <= rhs);
        public static bool operator >=(in ufloat lhs, in float rhs) => (lhs._value >= rhs);
        public static bool operator ==(in ufloat lhs, in float rhs) => (lhs._value == rhs);
        public static bool operator !=(in ufloat lhs, in float rhs) => (lhs._value != rhs);

        public static bool operator <(in float lhs, in ufloat rhs) => (lhs < rhs._value);
        public static bool operator >(in float lhs, in ufloat rhs) => (lhs > rhs._value);
        public static bool operator <=(in float lhs, in ufloat rhs) => (lhs <= rhs._value);
        public static bool operator >=(in float lhs, in ufloat rhs) => (lhs >= rhs._value);
        public static bool operator ==(in float lhs, in ufloat rhs) => (lhs == rhs._value);
        public static bool operator !=(in float lhs, in ufloat rhs) => (lhs != rhs._value);

        public readonly override bool Equals(object obj) => obj is ufloat fLOAT && _value == fLOAT._value;
        public readonly override int GetHashCode() => HashCode.Combine(_value);

        readonly bool IEquatable<ufloat>.Equals(ufloat other) => (_value == other._value);
        readonly int IEqualityComparer<ufloat>.GetHashCode(ufloat obj) => HashCode.Combine(_value);

        readonly bool IEqualityComparer<ufloat>.Equals(ufloat x, ufloat y) => (x._value == y._value);

        readonly int IComparable<ufloat>.CompareTo(ufloat other) => (_value < other._value) ? -1 : ((_value > other._value) ? 1 : 0);
        readonly int IComparer<ufloat>.Compare(ufloat x, ufloat y) => (x._value < y._value) ? -1 : ((x._value > y._value) ? 1 : 0);
    }
}