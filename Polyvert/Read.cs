using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Polyvert.Objects;
using Cartography;

namespace Polyvert
{
    public class Read
    {
        
        public static void ReadFile(FileGeo file)
        {
            List<Polygon> allPolygons = new List<Polygon>();

            FileStream stream = File.Open(file.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
           
            StreamReader rdr = new StreamReader(stream);

            file.maxEast = -181; //these fields will of necessity have to be overwritten.
            file.maxWest = 180;
            file.maxNorth = -91;
            file.maxSouth = 90;   
            
            while (!rdr.EndOfStream)
            {
                //first line HAS TO BE "POLYVERT_1.0"

                string fileType = rdr.ReadLine();
                if (fileType != "POLYVERT_1.0")
                {
                    throw new Exception(string.Format("The file {0} is not compatible with the Polyvert 1.0 specifications.", file.FileName));
                }

                //read polygons
                file.PolygonCount = int.Parse(rdr.ReadLine());
                file.Polygons = new List<Polygon>();
                for (int i = 0; i < file.PolygonCount; i++)
                {
                   file.Polygons.Add(ReadPolygons(ref rdr));
                }
             
                //read chains
                
                file.ChainCount = int.Parse(rdr.ReadLine());
                file.Chains = new List<Chain>();
                for (int i = 0; i < file.ChainCount; i++)
                {
                    ReadChains(ref rdr, ref file);
                }

                //read nodes
                file.NodeCount = int.Parse(rdr.ReadLine());
                file.Nodes = new List<Node>();
                for (int i = 0; i < file.NodeCount; i++)
                {
                    ReadNodes(ref rdr, ref file);
                    file.Nodes[i].Index = file.Nodes[i].ID - file.Nodes[0].ID;
                }

                //read attributes
                file.Tables = ReadAttributes(ref rdr, file);

                foreach (Chain chain in file.Chains)
                {
                    NodesToChain(chain, file);
                }

                foreach (Polygon polygon in file.Polygons)
                {
                    ChainsToPolygon(polygon, file);
                }

                FinalCalc(file);
            }
        }

        /// <summary>
        /// reads polygons and assigns to its fields.
        /// </summary>
        /// <param name="rdr">the StreamReader object used to read the file.</param>
        private static Polygon ReadPolygons(ref StreamReader rdr)
        {
            //polygonID:Chains
            Polygon polygon = new Polygon();
            string rawRead = rdr.ReadLine();
            //1. split on the colon
            string[] colonSplit = rawRead.Split(':');
            //2. part 0 parsed to int is the id
            polygon.ID = int.Parse(colonSplit[0]);
            //3. part 1 split on the comma
            string[] commaSplit = colonSplit[1].Split(',');
            //4. parts each are a chainID
            polygon.ChainIDs = new List<int>();
            for (int i = 0; i < commaSplit.Length; i++)
            {
                polygon.ChainIDs.Add(int.Parse(commaSplit[i]));
            }

            return polygon;
        }

        /// <summary>
        /// reads chains and assigns values to its fields.
        /// </summary>
        /// <param name="rdr">the StreamReader object used to read the file.</param>
        private static void ReadChains(ref StreamReader rdr, ref FileGeo file)
        {

            Chain chain = new Chain();
            
            string rawLine = rdr.ReadLine();
            //1.split on the colon.
            string[] colonSplit = rawLine.Split(':');
            //2.part 0 parsed to int is the id
            chain.ID = int.Parse(colonSplit[0]);
            //3. part 1 split on the comma.
            string[] commaSplit = colonSplit[1].Split(',');
            //4. foreach pair, assign to midpoint x and ys. 
            chain.Midpoints = new List<Node>();
            for (int i = 0; i < commaSplit.Length; i+= 2)
            {
                if (commaSplit.Length == 1)
                {
                    //if ODD, THROW ERROR THAT FILE IS BAD. 
                    break;
                }
                Node midpoint = new Node();
                midpoint.Latitude = double.Parse(commaSplit[i]);
                midpoint.Longitude = double.Parse(commaSplit[i + 1]);
                chain.Midpoints.Add(midpoint);
                
                if (midpoint.Longitude > file.maxEast)  //none of these are getting hit. 
                {
                    file.maxEast = midpoint.Longitude;
                }
                if (midpoint.Longitude < file.maxWest)
	            {
                    file.maxWest = midpoint.Longitude;
	            }
                if (midpoint.Latitude > file.maxNorth)
	            {
                    file.maxNorth = midpoint.Latitude;
	            }
                if (midpoint.Latitude < file.maxSouth)
	            {
                    file.maxSouth = midpoint.Latitude;
	            }
            }
            //5. part 2 split on the - , assign to start and endnode.
            string[] dashSplit = colonSplit[2].Split('-');
            chain.startNodeID = int.Parse(dashSplit[0]);
            chain.endNodeID = int.Parse(dashSplit[1]);
       
            //6. part 3 split on dash, assign to left and right polygon. 
            string[] finalDashSplit = colonSplit[3].Split('-');
            chain.LPolygon = finalDashSplit[0];
            chain.RPolygon = finalDashSplit[1];

            if (chain.LPolygon == "0")
                chain.LPolygon = "World";
            if (chain.RPolygon == "0")
                chain.RPolygon = "World";

           
            file.Chains.Add(chain);
        }

        /// <summary>
        /// reads nodes and assigns values to its fields.
        /// </summary>
        /// <param name="rdr">the StreamReader object used to read the file.</param>
        private static void ReadNodes(ref StreamReader rdr, ref FileGeo file)
        {
            //node id: latitude, longitude
            Node node = new Node();
            string rawLine = rdr.ReadLine();
            //1. split on the colon.
            string[] colonSplit = rawLine.Split(':');
            //2. part 0 is id.
            node.ID = int.Parse(colonSplit[0]);
            //3. split on the comma. 
            string[] commaSplit = colonSplit[1].Split(',');
            //4. part 0 is y, part 1 is x.
            node.Latitude = double.Parse(commaSplit[0]);
            node.Longitude = double.Parse(commaSplit[1]);
            //this will fail for anything less than 0. Find way to set it to the first value--once. 
            if (node.Longitude > file.maxEast)
            {
                file.maxEast = node.Longitude;
            }
            if (node.Longitude < file.maxWest)
            {
                file.maxWest = node.Longitude;
            }
            if (node.Latitude > file.maxNorth)
            {
                file.maxNorth = node.Latitude;
            }
            if (node.Latitude < file.maxSouth)
            {
                file.maxSouth = node.Latitude;
            }

            file.Nodes.Add(node);

        }

        /// <summary>
        /// reads attributes, which may vary in number.
        /// </summary>
        private static List<AttributeTable> ReadAttributes(ref StreamReader rdr, FileGeo file)
        {
            List<AttributeTable> tables = new List<AttributeTable>();
            //attribute table name.
            //ID:value...
            file.TableCount = 0;
            while (!rdr.EndOfStream)
            {
                AttributeTable table = new AttributeTable();

                int countCheckPoly = int.Parse(rdr.ReadLine());
                if (countCheckPoly != file.PolygonCount)
                {
                    throw new Exception(string.Format("The file has more attributes than polygons. This cannot be."));
                }
                table.TableName = rdr.ReadLine();

                table.entries = new List<Entries>();

                for (int i = 0; i < file.PolygonCount; i++)
                {
                    Entries entry = new Entries();

                    string rawLine = rdr.ReadLine();

                    string[] pieces = rawLine.Split(':');

                    entry.ID = int.Parse(pieces[0]);

                    entry.value = pieces[1];

                    table.entries.Add(entry);
                }

                tables.Add(table);
                file.TableCount++;
            }

            return tables;
        }

        /// <summary>
        /// Calculates properties of the objects that require use of the completely-read file.
        /// </summary>
        /// <param name="file"></param>
        private static void FinalCalc(FileGeo file)
        {
            GeometricCalc calc = new GeometricCalc();

            foreach (Chain chain in file.Chains)
            {
                chain.Distance = calc.ChainDistance(chain, file);
            }

            foreach (Polygon polygon in file.Polygons)
            {
                polygon.Area = calc.Area(polygon);
            }
        }

       /// <summary>
       /// Populates the Chain.Nodes property--makes a list of all the nodes on the chain.
       /// </summary>
       /// <param name="chain">the chain whose nodes are read.</param>
       /// <param name="file">the file containing all nodes, chains, and polygons.</param>
        private static void NodesToChain(Chain chain, FileGeo file)
        {
            chain.Nodes = new List<Node>();

            Node startNode = file.Nodes[chain.startNodeID - file.Nodes[0].ID];
            chain.Nodes.Add(startNode);

            foreach (Node midpoint in chain.Midpoints)
            {
                chain.Nodes.Add(midpoint);
            }

            Node endNode = file.Nodes[chain.endNodeID - file.Nodes[0].ID];
            chain.Nodes.Add(endNode);
        }


        private static void ChainsToPolygon(Polygon polygon, FileGeo file)
        {
            polygon.Chains = new List<Chain>();

            foreach (int chainID in polygon.ChainIDs)
            {
                Chain chain = file.Chains[chainID - file.Chains[0].ID];
                polygon.Chains.Add(chain);
            }
        }
    }
}
