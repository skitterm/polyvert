using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polyvert.Objects
{
    public class Node
    {
        public int ID { get; set; }
        private double latitude;
        private double longitude;
        public int Index { get; set; }
        


        public double Latitude
        {
            get
            {
                return latitude;
            }

            set
            {
                if (value <= -90 || value >= 90)
                {
                    throw new Exception(string.Format("The point {0} is out of range.", ID));
                }
                else
                {
                    latitude = value;
                }
            }
        }

        public double Longitude
        {
            get
            {
                return longitude;
            }

            set
            {
                if (value <= -180 || value >= 180)
                {
                    throw new Exception(string.Format("Node {0} is out of range", ID));
                }
                else
                {
                    longitude = value;
                }
            }
        }

       

        public Node()
        {
            ID = 0;
            Latitude = 0;
            Longitude = 0;
            
        }
    }
}
