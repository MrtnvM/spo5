using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    public class TableIdentifier
    {
        public string Item { get; set; }
        public int Address { get; set; }
    }

    public class IdentifierTableList
    {
        public List<TableIdentifier> List { get; }

        public IdentifierTableList()
        {
            List = new List<TableIdentifier>();
        }
    }
}
