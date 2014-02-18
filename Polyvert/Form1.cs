using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Polyvert.Objects;

namespace Polyvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           
            InitializeComponent();

            FileGeo file = new FileGeo();
            
            file.FileName = @"Paper.pv";

            Read.ReadFile(file);

            Draw.DrawFile(pictureBoxFile, file);


            Draw.LinkNodes(NodeBoxNodeID, ChainBoxChainID, PolyBoxPolyID, file);


            NodeBoxNodeID.Click += (sender, e) => NodeBoxNodeID_SelectedIndexChanged(file);
            ChainBoxChainID.Click += (sender, e) => ChainBoxChainID_SelectedIndexChanged(file);
            PolyBoxPolyID.Click += (sender, e) => PolyBoxPolyID_SelectedIndexChanged(file);
        }

        private void NodeBoxNodeID_SelectedIndexChanged(FileGeo file)
        {
            NodeBoxChainID.Items.Clear();
            NodeBoxNodeCoord.Items.Clear();
            NodeBoxPolyID.Items.Clear();

            int ID = (int)NodeBoxNodeID.SelectedItem;

            int index = ID - file.Nodes[0].ID;

            NodeBoxNodeCoord.Items.Add(Math.Round(file.Nodes[index].Latitude, 3).ToString("#0.000") + "  " + Math.Round(file.Nodes[index].Longitude, 3).ToString("#0.000"));

            List<int> chainIDs = new List<int>();

            List<string> listOfLRPolys = new List<string>();
            List<string> listOfUniquePolys = new List<string>();

            //access all chains with this endnode.
            foreach (Chain chain in file.Chains)
            {
                if (chain.startNodeID == ID || chain.endNodeID == ID)
                {
                    chainIDs.Add(chain.ID);

                    listOfLRPolys.Add(chain.LPolygon); //add two lists the left and right polygons of the chains. 
                    listOfLRPolys.Add(chain.RPolygon);
                    listOfUniquePolys.Add(chain.LPolygon);
                    listOfUniquePolys.Add(chain.RPolygon);

                    NodeBoxChainID.Items.Add(chain.ID + "  ( " + chain.startNodeID + " -> " + chain.endNodeID + " )");
                }
            }

            //iterate through listofLRPolys

            foreach (string polyID in listOfLRPolys)
            {
                if (listOfUniquePolys.Contains(polyID))
                { 
                    listOfUniquePolys.RemoveAll(x => x == polyID);
                    listOfUniquePolys.Add(polyID);
                }
            }

            //iterate through uniquepolys to weed out same as index and also add to GUI

            foreach (string polyID in listOfUniquePolys)
            {
                if (polyID == ID.ToString())
                {
                    continue;
                }

                NodeBoxPolyID.Items.Add(polyID);
            }

        }


        private void ChainBoxChainID_SelectedIndexChanged(FileGeo file)
        {
            ChainLabelNodeIDs.Text = "";
            ChainLabelLPolygon.Text = "";
            ChainLabelDist.Text = "";
            ChainTitleNodes.Text = "Nodes";
            ChainTitlePolygons.Text = "Polygons";
            ChainTitleDist.Text = "Distance";

            int ID = (int)ChainBoxChainID.SelectedItem;

            int index = ID - file.Chains[0].ID;

            ChainLabelNodeIDs.Text = file.Chains[index].startNodeID + " -> " + file.Chains[index].endNodeID;

            ChainLabelLPolygon.Text = "Left:  " + file.Chains[index].LPolygon;
            ChainLabelRPolygon.Text = "Right:  " + file.Chains[index].RPolygon;

            ChainLabelDist.Text = file.Chains[index].Distance.ToString();

            Draw.DrawChain(pictureBoxChain, file.Chains[index], file);

        }

        private void PolyBoxPolyID_SelectedIndexChanged(FileGeo file)
        {
            PolyListAdjPoly.Items.Clear();
            PolyListChain.Items.Clear();
            PolyListAttributes.Items.Clear();
            PolyTxtPerDist.Text = "";
            PolyTxtArea.Text = "";

            int ID = (int)PolyBoxPolyID.SelectedItem;

            int index = ID - file.Polygons[0].ID;
            List<string> listOfLRPolys = new List<string>();//combined list of the l and r polygons in polygon's chains.

            List<string> listOfUniquePolys = new List<string>(); // unique values. 


            Polygon polygon = file.Polygons[index];

            //get list of chain IDs. 

            foreach (int chainID in polygon.ChainIDs) // this is off each time. must be set each time. 
            {
                int chainIndex = chainID - file.Chains[0].ID;

                //access the chain L R polygon by this ID.
                //add these to listOfLRPolys and listOfUniquePolys
                listOfLRPolys.Add(file.Chains[chainIndex].RPolygon); // Trying to pull out by their index. Need to pull out by their ID. 
                listOfLRPolys.Add(file.Chains[chainIndex].LPolygon);
                listOfUniquePolys.Add(file.Chains[chainIndex].RPolygon); // Trying to pull out by their index. Need to pull out by their ID. 
                listOfUniquePolys.Add(file.Chains[chainIndex].LPolygon);
                PolyListChain.Items.Add(chainID + "  ( " + file.Chains[chainIndex].startNodeID + " -> " + file.Chains[chainIndex].endNodeID + " )  ");
            }

            //iterate through one of the lists. 

            foreach (string polyID in listOfLRPolys)
            {
                //if the value exists in the other list, blow it away and then add that item. 

                if (listOfUniquePolys.Contains(polyID))
                {
                    listOfUniquePolys.RemoveAll(x => x == polyID);

                    listOfUniquePolys.Add(polyID);
                }
            }

            foreach (string polyID in listOfUniquePolys)
            {
                if (polyID == ID.ToString())
                {
                    continue;
                }

                PolyListAdjPoly.Items.Add(polyID);
            }

            foreach (AttributeTable table in file.Tables)
            {
                PolyListAttributes.Items.Add(table.TableName + ": " + table.entries[index].value);

            }

            GeometricCalc calc = new GeometricCalc();

            PolyTxtPerDist.Text = calc.PerimeterDistance(polygon, file).ToString();

            PolyTxtArea.Text = polygon.Area.ToString();

            Draw.DrawPolygons(pictureBoxPolygon, polygon, file);
        }
       
    }
}
