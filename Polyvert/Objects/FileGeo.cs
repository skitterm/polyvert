using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polyvert.Objects
{
    public class FileGeo
    {
        public string FileName { get; set; }
        public List<Polygon> Polygons { get; set; }
        public int PolygonCount { get; set; }
        public List<Chain> Chains { get; set; }
        public int ChainCount { get; set; }
        public List<Node> Nodes { get; set; }
        public int NodeCount { get; set; }
        public List<AttributeTable> Tables { get; set; }
        public int TableCount { get; set; }
        public double maxNorth { get; set; }
        public double maxSouth { get; set; }
        public double maxEast { get; set; }
        public double maxWest { get; set; }

    }
}
