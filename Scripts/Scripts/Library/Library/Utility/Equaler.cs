using System.Collections.Generic;


namespace FSystem
{
    public class ByteEqualer : IEqualityComparer<byte>
    {
        public bool Equals(byte x, byte y) => (x == y);
        public int GetHashCode(byte obj) => obj;
    }
    public class IntEqualer : IEqualityComparer<int>
    {
        public bool Equals(int x, int y) => (x == y);
        public int GetHashCode(int obj) => obj;
    }
    public class HandleEqualer : IEqualityComparer<HANDLE>
    {
        public bool Equals(HANDLE x, HANDLE y) => (x == y);
        public int GetHashCode(HANDLE obj) => obj.GetHashCode();
    }
    public class StringEqualer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) => (x == y);
        public int GetHashCode(string obj) => obj.GetHashCode();
    }
}