using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polyvert.Objects
{
    public class Chain
    {
        public int ID { get; set; }
        public int startNodeID { get; set; }
        public int endNodeID { get; set; }
        public List<Node> Midpoints { get; set; } //just midpoints as they are read in.
        public List<Node> Nodes { get; set; } //start node, midpoints, and endnode.
        public string LPolygon { get; set; }
        public string RPolygon { get; set; }
        public double Distance { get; set; }
    }
}
