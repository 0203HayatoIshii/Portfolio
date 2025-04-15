using System;
using System.Runtime.InteropServices;

namespace FSystem
{
    /// <summary>
    /// 基本データ全てに変換可能な共用体
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct union
    {
        [Flags]
        public enum ValueType : int
        {
            None       = 0,
            SBYTE      = (1 << 0),
            SHORT      = (1 << 1),
            INT        = (1 << 2),
            LONG       = (1 << 3),
            BYTE       = (1 << 4),
            USHORT     = (1 << 5),
            UINT       = (1 << 6),
            ULONG      = (1 << 7),
            FLOAT      = (1 << 8),
            DOUBLE     = (1 << 9),
            BOOL       = (1 << 10),
            CHAR       = (1 << 11),
            VECTOR2    = (1 << 12),
            VECTOR2INT = (1 << 13),
            VECTOR3    = (1 << 14),
            VECTOR3INT = (1 << 15),
            QUATERNION = (1 << 16),
        }

        public readonly static union Zero = new();


        [FieldOffset(0)]
        private ValueType _type;

        [FieldOffset(4)]
        private sbyte _sbyte;
        [FieldOffset(4)]
        private short _short;
        [FieldOffset(4)]
        private int _int;
        [FieldOffset(4)]
        private long _long;
        [FieldOffset(4)]
        private byte _byte;
        [FieldOffset(4)]
        private ushort _ushort;
        [FieldOffset(4)]
        private uint _uint;
        [FieldOffset(4)]
        private ulong _ulong;

        [FieldOffset(4)]
        private float _float;
        [FieldOffset(4)]
        private double _double;

        [FieldOffset(4)]
        private bool _bool;
        [FieldOffset(4)]
        private char _char;

#if UNITY_64
        [FieldOffset(4)]
        private UnityEngine.Vector2 _vector2;
        [FieldOffset(4)]
        private UnityEngine.Vector2Int _vector2Int;
        [FieldOffset(4)]
        private UnityEngine.Vector3 _vector3;
        [FieldOffset(4)]
        private UnityEngine.Vector3Int _vector3Int;
        [FieldOffset(4)]
        private UnityEngine.Quaternion _quaternion;
#endif


        public readonly ValueType Type { get => _type; }

        public readonly void GetVal(out sbyte result)
        {
            if ((_type & ~ValueType.SBYTE) == 0)
                result = _sbyte;

            throw new InvalidCastException("the uion data type is" + _type + "(requested sbyte)");
        }
        public readonly void GetVal(out short result)
        {
            if ((_type & ~ValueType.SHORT) == 0)
                result = _short;

            throw new InvalidCastException("the uion data type is" + _type + "(requested short)");
        }
        public readonly void GetVal(out int result)
        {
            if ((_type & ~ValueType.INT) == 0)
            result = _int;

            throw new InvalidCastException("the uion data type is" + _type + "(requested int)");
        }
        public readonly void GetVal(out long result)
        {
            if ((_type & ~ValueType.LONG) == 0)
                result = _long;

            throw new InvalidCastException("the uion data type is" + _type + "(requested long)");
        }
        public readonly void GetVal(out byte result)
        {
            if ((_type & ~ValueType.BYTE) == 0)
                result = _byte;

            throw new InvalidCastException("the uion data type is" + _type + "(requested byte)");
        }
        public readonly void GetVal(out ushort result)
        {
            if ((_type & ~ValueType.USHORT) == 0)
                result = _ushort;

            throw new InvalidCastException("the uion data type is" + _type + "(requested ushort)");
        }
        public readonly void GetVal(out uint result)
        {
            if ((_type & ~ValueType.UINT) == 0)
                result = _uint;

            throw new InvalidCastException("the uion data type is" + _type + "(requested uint)");
        }
        public readonly void GetVal(out ulong result)
        {
            if ((_type & ~ValueType.ULONG) == 0)
                result = _ulong;

            throw new InvalidCastException("the uion data type is" + _type + "(requested ulong)");
        }

        public readonly void GetVal(out float result)
        {
            if ((_type & ~ValueType.FLOAT) == 0)
                result = _float;

            throw new InvalidCastException("the uion data type is" + _type + "(requested float)");
        }
        public readonly void GetVal(out double result)
        {
            if ((_type & ~ValueType.DOUBLE) == 0)
                result = _double;

            throw new InvalidCastException("the uion data type is" + _type + "(requested double)");
        }

        public readonly void GetVal(out bool result)
        {
            if ((_type & ~ValueType.BOOL) == 0)
                result = _bool;

            throw new InvalidCastException("the uion data type is" + _type + "(requested bool)");
        }
        public readonly void GetVal(out char result)
        {
            if ((_type & ~ValueType.CHAR) == 0)
                result = _char;

            throw new InvalidCastException("the uion data type is" + _type + "(requested char)");
        }

#if UNITY_64
        public readonly void GetVal(out UnityEngine.Vector2 result)
        {
            if ((_type & ~ValueType.VECTOR2) == 0)
                result = _vector2;

            throw new InvalidCastException("the uion data type is" + _type + "(requested Vector2)");
        }
        public readonly void GetVal(out UnityEngine.Vector2Int result)
        {
            if ((_type & ~ValueType.VECTOR2INT) == 0)
                result = _vector2Int;

            throw new InvalidCastException("the uion data type is" + _type + "(requested Vector2Int)");
        }

        public readonly void GetVal(out UnityEngine.Vector3 result)
        {
            if ((_type & ~ValueType.VECTOR3) == 0)
                result = _vector3;

            throw new InvalidCastException("the uion data type is" + _type + "(requested Vector3)");
        }
        public readonly void GetVal(out UnityEngine.Vector3Int result)
        {
            if ((_type & ~ValueType.VECTOR3INT) == 0)
                result = _vector3Int;

            throw new InvalidCastException("the uion data type is" + _type + "(requested Vector3Int)");
        }

        public readonly void GetVal(out UnityEngine.Quaternion result)
        {
            if ((_type & ~ValueType.QUATERNION) == 0)
                result = _quaternion;

            throw new InvalidCastException("the uion data type is" + _type + "(requested Quaternion)");
        }
#endif

        public static explicit operator sbyte (union val)
        {
            if ((val._type & ~ValueType.SBYTE) == 0)
                return val._sbyte;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested sbyte)");
        }
        public static explicit operator short (union val)
        {
            if ((val._type & ~ValueType.SHORT) == 0)
                return val._short;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested short)");
        }
        public static explicit operator int (union val)
        {
            if ((val._type & ~ValueType.INT) == 0)
                return val._int;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested int)");
        }
        public static explicit operator long (union val)
        {
            if ((val._type & ~ValueType.LONG) == 0)
                return val._long;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested long)");
        }
        public static explicit operator byte (union val)
        {
            if ((val._type & ~ValueType.BYTE) == 0)
                return val._byte;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested byte)");
        }
        public static explicit operator ushort (union val)
        {
            if ((val._type & ~ValueType.USHORT) == 0)
                return val._ushort;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested ushort)");
        }
        public static explicit operator uint (union val)
        {
            if ((val._type & ~ValueType.UINT) == 0)
                return val._uint;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested uint)");
        }
        public static explicit operator ulong (union val)
        {
            if ((val._type & ~ValueType.ULONG) == 0)
                return val._ulong;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested ulong)");
        }

        public static explicit operator float (union val)
        {
            if ((val._type & ~ValueType.FLOAT) == 0)
                return val._float;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested float)");
        }
        public static explicit operator double (union val)
        {
            if ((val._type & ~ValueType.DOUBLE) == 0)
                return val._double;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested double)");
        }

        public static explicit operator bool (union val)
        {
            if ((val._type & ~ValueType.BOOL) == 0)
                return val._bool;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested bool)");
        }
        public static explicit operator char (union val)
        {
            if ((val._type & ~ValueType.CHAR) == 0)
                return val._char;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested char)");
        }

#if UNITY_64
        public static explicit operator UnityEngine.Vector2 (union val)
        {
            if ((val._type & ~ValueType.VECTOR2) == 0)
                return val._vector2;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested Vector2)");
        }
        public static explicit operator UnityEngine.Vector2Int(union val)
        {
            if ((val._type & ~ValueType.VECTOR2INT) == 0)
                return val._vector2Int;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested Vector2Int)");
        }

        public static explicit operator UnityEngine.Vector3 (union val)
        {
            if ((val._type & ~ValueType.VECTOR3) == 0)
                return val._vector3;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested Vector3)");
        }
        public static explicit operator UnityEngine.Vector3Int(union val)
        {
            if ((val._type & ~ValueType.VECTOR3INT) == 0)
                return val._vector3Int;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested Vector3Int)");
        }

        public static explicit operator UnityEngine.Quaternion (union val)
        {
            if ((val._type & ~ValueType.QUATERNION) == 0)
                return val._quaternion;

            throw new InvalidCastException("the uion data type is" + val._type + "(requested Quaternion)");
        }
#endif

        public static explicit operator union(sbyte val)
        {
            var ret = new union();
            ret._sbyte = val;
            ret._type = ValueType.SBYTE;
            return ret;
        }
        public static explicit operator union(short val)
        {
            var ret = new union();
            ret._short = val;
            ret._type = ValueType.SHORT;
            return ret;
        }
        public static explicit operator union(int val)
        {
            var ret = new union();
            ret._int = val;
            ret._type = ValueType.INT;
            return ret;
        }
        public static explicit operator union(long val)
        {
            var ret = new union();
            ret._long = val;
            ret._type = ValueType.LONG;
            return ret;
        }
        public static explicit operator union(byte val)
        {
            var ret = new union();
            ret._byte = val;
            ret._type = ValueType.BYTE;
            return ret;
        }
        public static explicit operator union(ushort val)
        {
            var ret = new union();
            ret._ushort = val;
            ret._type = ValueType.SHORT;
            return ret;
        }
        public static explicit operator union(uint val)
        {
            var ret = new union();
            ret._uint = val;
            ret._type = ValueType.UINT;
            return ret;
        }
        public static explicit operator union(ulong val)
        {
            var ret = new union();
            ret._ulong = val;
            ret._type = ValueType.ULONG;
            return ret;
        }

        public static explicit operator union(float val)
        {
            var ret = new union();
            ret._float = val;
            ret._type = ValueType.FLOAT;
            return ret;
        }
        public static explicit operator union(double val)
        {
            var ret = new union();
            ret._double = val;
            ret._type = ValueType.DOUBLE;
            return ret;
        }

        public static explicit operator union(bool val)
        {
            var ret = new union();
            ret._bool = val;
            ret._type = ValueType.BOOL;
            return ret;
        }
        public static explicit operator union(char val)
        {
            var ret = new union();
            ret._char = val;
            ret._type = ValueType.CHAR;
            return ret;
        }

#if UNITY_64
        public static explicit operator union(UnityEngine.Vector2 val)
        {
            var ret = new union();
            ret._vector2 = val;
            ret._type = ValueType.VECTOR2;
            return ret;
        }
        public static explicit operator union(UnityEngine.Vector2Int val)
        {
            var ret = new union();
            ret._vector2Int = val;
            ret._type = ValueType.VECTOR2INT;
            return ret;
        }
        public static explicit operator union(UnityEngine.Vector3 val)
        {
            var ret = new union();
            ret._vector3 = val;
            ret._type = ValueType.VECTOR3;
            return ret;
        }
        public static explicit operator union(UnityEngine.Vector3Int val)
        {
            var ret = new union();
            ret._vector3Int = val;
            ret._type = ValueType.VECTOR3INT;
            return ret;
        }
        public static explicit operator union(UnityEngine.Quaternion val)
        {
            var ret = new union();
            ret._quaternion = val;
            ret._type = ValueType.QUATERNION;
            return ret;
        }
#endif

        public readonly bool IsZero()
        {
#if UNITY_64
            bool isZero = (_quaternion == UnityEngine.Quaternion.identity);
#else
            bool isZero = (_long == 0L);
#endif
            return isZero;
        }
    }
}