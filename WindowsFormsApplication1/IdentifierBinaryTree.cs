namespace WindowsFormsApplication1
{
    class IdentifierBinaryTree
    {
        private IdentifierNode Root;

        public int ComparisonCount { get; private set; }

        public IdentifierBinaryTree(string rootIdentifier)
        {
            Root = new IdentifierNode(rootIdentifier);
        }

        public void Add(string identifier)
        {
            Add(identifier, Root);
        }

        public bool HasInTree(string identifier)
        {
            ComparisonCount = 0;

            return HasInTree(identifier, Root);
        }

        private void Add(string identifier, IdentifierNode node)
        {
            if (node.Identifier == identifier)
                return;

            if (node.Identifier.CompareTo(identifier) < 0)
            {
                if (node.Left != null)
                {
                    Add(identifier, node.Left);
                    return;
                }
                node.Left = new IdentifierNode(identifier);
                return;
            }

            if (node.Identifier.CompareTo(identifier) > 0)
            {
                if (node.Right != null)
                {
                    Add(identifier, node.Right);
                    return;
                }
                node.Left = new IdentifierNode(identifier);
            }
        }

        private bool HasInTree(string identifier, IdentifierNode node)
        {
            if (node.Identifier == identifier)
            {
                ComparisonCount++;
                return true;
            }

            if (node.Identifier.CompareTo(identifier) < 0)
            {
                ComparisonCount++;

                if (node.Left == null)
                {
                    return false;
                }
                return HasInTree(identifier, node.Left);
            }

            if (node.Identifier.CompareTo(identifier) > 0)
            {
                ComparisonCount++;

                if (node.Right == null)
                {
                    return false;
                }
                return HasInTree(identifier, node.Right);
            }

            return true;
        }
    }

    class IdentifierNode
    {
        public string Identifier { get; }

        public IdentifierNode Left { get; set; }
        public IdentifierNode Right { get; set; }

        public IdentifierNode(string id)
        {
            Identifier = id;
        }
    }
}
