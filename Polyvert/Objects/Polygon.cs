using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polyvert.Objects
{
    public class Polygon
    {
        public int ID { get; set; }
        public List<int> ChainIDs { get; set; }
        public List<Chain> Chains { get; set; }
        public double Perimeter { get; set; }
        public double Area { get; set; }

    }
}
