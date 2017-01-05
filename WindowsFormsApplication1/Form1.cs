using System;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace L5
{
    public partial class Form1 : Form
    {
        private readonly Random _random = new Random();

        private readonly RehashMethod _rehashMethod = new RehashMethod();
        private readonly CombinedMethod _combinedMethod = new CombinedMethod();
        
        private readonly TableIdentifier[] _tableIdentifiers1 = 
            new TableIdentifier[HashFunction.HASH_TABLE_SIZE]; 

        private readonly int[] _hashTable = new int[HashFunction.HASH_TABLE_SIZE];

        private readonly char[] _chars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
            'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        private char GetRandomChar()
            => _chars[_random.Next(0, _chars.Length)];

        public Form1()
        {
            InitializeComponent();
            InitializeHashTables();

            for (int i = 0; i < 100; i++)
            {
                var identifier = "";

                for (int j = 0; j < _random.Next(1, 32); j++)
                    identifier = identifier + GetRandomChar();

                var hash = HashFunction.Hash(identifier);

                panel3.Visible = false;

                _rehashMethod.Entry(identifier, _tableIdentifiers1, _hashTable);
                _combinedMethod.Entry(identifier);

                dataGridView1.Rows.Add(identifier, hash);
            }
        }

        private void InitializeHashTables()
        {
            for (int i = 0; i < HashFunction.HASH_TABLE_SIZE; i++)
            {
                _hashTable[i] = -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;

            var newIdentifier = textBox1.Text;

            _rehashMethod.Entry(newIdentifier, _tableIdentifiers1, _hashTable);
            _combinedMethod.Entry(newIdentifier);

            dataGridView1.Rows.Add(newIdentifier, HashFunction.Hash(newIdentifier));

            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            label5.Text = _rehashMethod.Search(textBox2.Text, _tableIdentifiers1, _hashTable);
            label6.Text = _combinedMethod.Search(textBox2.Text);
            label8.Text = _rehashMethod.Сomparisons.ToString();
            label9.Text = _combinedMethod.Сomparisons.ToString();
            label12.Text = _rehashMethod.ElapsedTime;
            label11.Text = _combinedMethod.ElapsedTime;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _combinedMethod.Entry(textBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
