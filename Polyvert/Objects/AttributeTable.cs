using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polyvert.Objects
{
    public class AttributeTable
    {
        public string TableName { get; set; }
        public List<Entries> entries { get; set; }
    }

    public class Entries
    {
        public int ID { get; set; }
        public string value { get; set; }
    }
}
