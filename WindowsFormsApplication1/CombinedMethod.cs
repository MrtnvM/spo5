using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using WindowsFormsApplication1;

namespace L5
{
    class CombinedMethod
    {
        private readonly List<IdentifierBinaryTree> _identifiers = new List<IdentifierBinaryTree>(); 
        private readonly int[] _hashTable = new int[HashFunction.HASH_TABLE_SIZE];

        private int _comparisons;
        private string _elapsedTime;

        private const int EMPTY = -1;

        public int Сomparisons => _comparisons;
        public string ElapsedTime => _elapsedTime;

        private int _pointer = 0;


        public CombinedMethod()
        {
            for (int i = 0; i < _hashTable.Length; i++)
                _hashTable[i] = EMPTY;
        }

        public void Entry(string identifier)
        {
            int address = HashFunction.Hash(identifier);

            if (_hashTable[address] == EMPTY)
            {
                var identifierTree = new IdentifierBinaryTree(identifier);
                _identifiers.Add(identifierTree);
                _hashTable[address] = _pointer;
                _pointer++;
            }
            else
            {
                var pointerOnTree = _hashTable[address];
                _identifiers[pointerOnTree].Add(identifier);
            }
        }

        public bool IsIdentifierExist(string identifier)
        {
            int address = HashFunction.Hash(identifier);

            if (_hashTable[address] == EMPTY)
            {
                return false;
            }

            var pointerOnTree = _hashTable[address];
            var tree = _identifiers[pointerOnTree];
            return tree.HasInTree(identifier);
        }

        public string Search(string identifier)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _comparisons = 0;
            int address = HashFunction.Hash(identifier);
            string mes = "";

            if (_hashTable[address] == EMPTY)
            {
                mes = "Элемент не найден";
            }
            else
            {
                var pointerOnTree = _hashTable[address];
                var tree = _identifiers[pointerOnTree];
                var isExist = tree.HasInTree(identifier);
                mes = isExist ? "Элемент найден" : "Элемент не найден";
                _comparisons = tree.ComparisonCount;
            }

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            _elapsedTime = string.Format(ts.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return mes;
        }
    }
}
