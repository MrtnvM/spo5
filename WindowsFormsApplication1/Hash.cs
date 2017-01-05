using System.Linq;

namespace WindowsFormsApplication1
{
    static class HashFunction
    {
        public const int HASH_TABLE_SIZE = 200;

        public static int Hash(string key)
            => key.Take(5).Sum(c => (int)c) % HASH_TABLE_SIZE;
    }
}
