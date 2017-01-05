using System;
using System.Diagnostics;
using System.Globalization;

namespace WindowsFormsApplication1
{
    class RehashMethod
    {
        private int _comparisons;
        private string _elapsedTime;

        private const int NOT_EMPTY = 1;
        private const int EMPTY = -1;

        public int Сomparisons => _comparisons;
        public string ElapsedTime => _elapsedTime;

        public void Entry(string identifier, TableIdentifier[] tableIdentifiers, int[] hashTable)
        {            
            int address = HashFunction.Hash(identifier);

            if(hashTable[address] == EMPTY)
            {
                hashTable[address] = NOT_EMPTY;
                tableIdentifiers[address] = new TableIdentifier { Item = identifier, Address = address };
            }
            else
            {
                var initialIndex = address;
                int offset = 1;

                while (true)
                {
                    var index = address + offset;
                    index = (index < tableIdentifiers.Length) ?
                        index : index % tableIdentifiers.Length;

                    if (index == initialIndex)
                        throw new Exception("Hash table is full");

                    if (hashTable[index] == EMPTY)
                    {
                        hashTable[index] = NOT_EMPTY;
                        tableIdentifiers[index] = new TableIdentifier { Item = identifier, Address = index };
                        return;
                    }

                    if (tableIdentifiers[index].Item == identifier)
                    {
                        return;
                    }

                    offset++;
                }
            }
        }

        public string Search(string identifier, TableIdentifier[] tableIdentifiers, int[] hashTable)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            int address = HashFunction.Hash(identifier);
            var mes = "";
            _comparisons = 0;

            if (hashTable[address] == EMPTY)
            {
                 mes = "Элемент не найден";
            }
            else
            {
                var initialIndex = address;
                int offset = 1;

                while (true)
                {
                    _comparisons++;

                    var index = address + offset;
                    index = (index < tableIdentifiers.Length) ?
                        index : index % tableIdentifiers.Length;

                    if (index == initialIndex)
                    {
                        mes = "Элемент не найден";
                        break;
                    }

                    if (hashTable[index] == NOT_EMPTY)
                    {
                        if (tableIdentifiers[index].Item == identifier)
                        {
                            mes = "Элемент найден";
                            break;
                        }
                        offset++;
                    }
                    else
                    {
                        mes = "Элемент не найден";
                        break;
                    }
                }
            }

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            _elapsedTime = string.Format(ts.TotalMilliseconds.ToString(CultureInfo.CurrentCulture));
            return mes;
        }
    }
}
