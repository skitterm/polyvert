using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polyvert.Objects;
using System.Drawing;
using System.Windows.Forms;

namespace Polyvert
{
    public class Draw
    {
        public static void DrawFile(PictureBox chainBox, FileGeo file)
        {
            VectorViewport view = new VectorViewport(chainBox);

            view.East = file.maxEast;
            view.West = file.maxWest;
            view.North = file.maxNorth;
            view.South = file.maxSouth;

            //draw each chain.
            foreach(Chain chain in file.Chains)
            {
                //draw these points, both as a start point and as an endpoint. 
                for (int k = 1; k < chain.Nodes.Count; k++)
                {
                    view.DrawLine(chain.Nodes[k - 1].Latitude, chain.Nodes[k - 1].Longitude, chain.Nodes[k].Latitude, chain.Nodes[k].Longitude); 
                }
            }
        }

        public static void DrawChain(PictureBox chainBox, Chain chain, FileGeo file, bool refresh = true)
        {

            VectorViewport viewport = new VectorViewport(chainBox);

            viewport.North = file.maxNorth;
            viewport.South = file.maxSouth;
            viewport.East = file.maxEast;
            viewport.West = file.maxWest;

          
            for (int i = 1; i < chain.Nodes.Count; i++)
            {
                viewport.DrawLine(chain.Nodes[i - 1].Latitude, chain.Nodes[i - 1].Longitude, chain.Nodes[i].Latitude, chain.Nodes[i].Longitude, refresh);
            }
        }

        public static void DrawPolygons(PictureBox polygonBox, Polygon polygon, FileGeo file)
        {
            VectorViewport viewport = new VectorViewport(polygonBox);

            viewport.North = file.maxNorth;
            viewport.South = file.maxSouth;
            viewport.East = file.maxEast;
            viewport.West = file.maxWest;

            foreach (Chain chain in polygon.Chains)
            {
                for (int i = 1; i < chain.Nodes.Count; i++)
                {
                    viewport.DrawLine(chain.Nodes[i - 1].Latitude, chain.Nodes[i - 1].Longitude, chain.Nodes[i].Latitude, chain.Nodes[i].Longitude);
                }
            }
        }

        public static void LinkNodes(ListBox NodeBoxID, ListBox ChainBoxID, ListBox PolyBoxID, FileGeo file)
        {
            //1.	All the nodes in the polyvert data set must be listed.
            //2.	The location of each node must be given on demand.
            //3.	Each chain that’s connected to a node must be enumerated on demand.
            //4.	All the adjacent polygons to a node must be enumerated on demand.
            
            for (int i = 0; i < file.NodeCount; i++)
            {
                NodeBoxID.Items.Add(file.Nodes[i].ID);
            }



            foreach (Chain chain in file.Chains)
            {
                ChainBoxID.Items.Add(chain.ID);
            }


            foreach (Polygon polygon in file.Polygons)
            {
                PolyBoxID.Items.Add(polygon.ID);
            }
           //get max for all four directions from node.
            //try max and see what it gives you. 
            

            //get max for all directions for midpoint.
            //take largest and that is the bounding box.

        }
    }
}
