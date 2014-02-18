using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cartography;
using Polyvert.Objects;

namespace Polyvert
{
    public class GeometricCalc
    {
        //call the Cartography spherical distance method. 
        /// <summary>
        /// combined distance of all the segments of one chain.
        /// </summary>
        /// <param name="chain">A chain</param>
        /// <param name="file">The polyvert file, with polygons, chains, and nodes.</param>
        /// <returns>Chain distance in Kilometers.</returns>
        public double ChainDistance(Chain chain, FileGeo file)
        {
            double distance = 0;

            for (int i = 1; i < chain.Nodes.Count; i++)
            {
                //calculate the distance between each point and the one preceding it. 
                distance += Trigonometry.SphereDistanceTwoPoints(chain.Nodes[i - 1].Latitude,
                    chain.Nodes[i - 1].Longitude, chain.Nodes[i].Latitude, chain.Nodes[i].Longitude);
            }

            return distance;
        }

        /// <summary>
        /// Calculates perimeter from the chain lengths
        /// </summary>
        /// <param name="polygon">Polygon for perimeter</param>
        /// <param name="file"></param>
        /// <returns>Perimeter distance in Kilometers.</returns>
        public double PerimeterDistance(Polygon polygon, FileGeo file)
        {
            double distance = 0;

            foreach (int id in polygon.ChainIDs)
            {
                int index = id - file.Chains[0].ID;

                distance += file.Chains[index].Distance;
            }

            return distance;
        }

        public double Area(Polygon polygon)
        {
            double area = 0;


            List<Node> allNodes = OrderPoints(polygon); // orders the points in the polygon in a circular fashion. 

            List<Structure.UTMCoord> utmCoords = NodeToUTM(allNodes); // converts all nodes and midpoints to UTM so can calculate area. 

            double numerator = 0.0; // it's the cross of the zones that throws everything off!

            for (int i = 1; i < utmCoords.Count; i++)
            {
                /*
                if (utmCoords[i].zoneNumber != utmCoords[i - 1].zoneNumber)
                {
                    throw new Exception("UTM Zone Numbers must be the same.");
                }*/
                numerator += (utmCoords[i - 1].easting * utmCoords[i].northing - utmCoords[i - 1].northing * utmCoords[i].easting);
            }

            numerator += (utmCoords[utmCoords.Count - 1].easting * utmCoords[0].northing - utmCoords[utmCoords.Count - 1].northing * utmCoords[0].easting);

            //area = ((x1y2 - y1x2) + (x2y3 - y2x3)...) / 2

            area = Math.Abs(numerator / 2);

            area = area / 1000000;//translate to square kilometers. 

            return area;
        }

       
        /// <summary>
        /// Converts all nodes and midpoints to UTM coordinates. Useful in calculating distance and area.
        /// </summary>
        /// <param name="allNodes">The nodes and midpoints, in order, in a polygon.</param>
        /// <returns>An ordered list of all nodes and midpoints in a polygon, in UTM format.</returns>
        private List<Structure.UTMCoord> NodeToUTM(List<Node> allNodes)
        {
            List<Structure.UTMCoord> utmCoords = new List<Structure.UTMCoord>(); // NEW METHOD

            foreach (Node node in allNodes)
            {
                Structure.GeoPoint point = new Structure.GeoPoint();

                point.latitudeDeg = node.Latitude;
                point.longitudeDeg = node.Longitude;

                Structure.Ellipsoidcoord ellipsoid = new Structure.Ellipsoidcoord();
                Structure.UTMCoord utmCoord = new Structure.UTMCoord();

                ellipsoid.eqRadius = Constant.EquatRadius84;
                ellipsoid.flattening = Constant.Flat84;
                ellipsoid.invFlatting = Constant.InvFlat84;
                ellipsoid.polarRadius = Constant.PolRadius84;

                utmCoord = UTM.GeopointToUTM(point, ellipsoid);
                utmCoords.Add(utmCoord);
            }

            return utmCoords;
        }

        /// <summary>
        /// Orders polygon nodes and midpoints in order.
        /// </summary>
        /// <param name="polygon">A polygon whose points need ordering</param>
        /// <returns>An ordered list of the points in the polygon.</returns>
        private List<Node> OrderPoints(Polygon polygon)
        {
            List<Node> allNodes = new List<Node>();

            for (int i = 0; i < polygon.Chains.Count; i++)
            {
                Chain chain = polygon.Chains[i];
                //add for first chain. It will determine order. BUT it needs to be in correct order itself. 


                if (polygon.Chains.Count == 1)//if the polygon only has one chain...
                {
                    for (int j = 0; j < chain.Nodes.Count; j++) 
                    {
                        allNodes.Add(chain.Nodes[j]);
                    }
                    continue;
                }
                if (i == 0)
                {
                    Chain secondChain = polygon.Chains[1];
                    // if its endnode equals next chain's start or endnode, then good. 
                    if (chain.endNodeID == secondChain.startNodeID || chain.endNodeID == secondChain.endNodeID)
                    {
                        for (int j = 1; j < chain.Nodes.Count; j++)//leave out startnode; avoids duplication of start/end nodes of chain.
                        {
                            allNodes.Add(chain.Nodes[j]);
                        }
                        continue;
                    }
                    //else reverse its order. 
                    else
                    {
                        chain.Nodes.Reverse();
                        int endTemp = chain.endNodeID;
                        chain.endNodeID = chain.startNodeID;
                        chain.startNodeID = endTemp;
                        for (int j = 1; j < chain.Nodes.Count; j++)
                        {
                            allNodes.Add(chain.Nodes[j]);
                        }
                        continue;
                    }
                }

                Chain previousChain = polygon.Chains[i - 1];

                //if in correct order, add. 
                if (chain.startNodeID == previousChain.endNodeID || chain.endNodeID == previousChain.startNodeID)
                {
                    for (int j = 1; j < chain.Nodes.Count; j++)
                    {
                        allNodes.Add(chain.Nodes[j]);
                    }
                }
                //if not, reverse node order and swith start and endnode. 
                else if (chain.endNodeID == previousChain.endNodeID || chain.startNodeID == previousChain.startNodeID)
                {
                    chain.Nodes.Reverse();
                    int endTemp = chain.endNodeID;
                    chain.endNodeID = chain.startNodeID;
                    chain.startNodeID = endTemp;
                    for (int j = 1; j < chain.Nodes.Count; j++)
                    {
                        allNodes.Add(chain.Nodes[j]);
                    }
                }
                else
                {
                    throw new Exception(string.Format("There are disconnected chains in polygon {0}: chains {1} and {2}", polygon.ID, previousChain.ID, chain.ID));
                }

            }

            return allNodes;
        }
    }
}
