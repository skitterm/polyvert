using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cartography
{
    public class Structure
    {
        public class HDMS
        {
            public char hemisphere;
            public int degrees;
            public int minutes;
            public double seconds;

        }

        public class GeoDMS
        {
            public char latHemisphere;
            public int latDegrees;
            public int latMinutes;
            public double latSeconds;

            public char longHemisphere;
            public int longDegrees;
            public int longMinutes;
            public double longSeconds;
        }

        public class GeoPoint
        {
            public double latitudeDeg;
            public double longitudeDeg;
        }

        public class UTMCoord
        {
            public int zoneNumber;
            public char zoneLetter;
            public double northing;
            public double easting;

        }

        public class Ellipsoidcoord
        {
            public double eqRadius;
            public double polarRadius;
            public double flattening;
            public double invFlatting;
        }


        public class XY
        {
            public double x;
            public double y;
        }


        //3 a
        public static double[] RandArray(int numOfDoubles, double lowerLimit, double upperLimit)
        {
            Random rand = new Random();
            numOfDoubles = 6;
            double[] randArray = new double[numOfDoubles];


            double difference = upperLimit - lowerLimit;

            for (int i = 0; i < numOfDoubles; i++)
            {
                randArray[i] = rand.NextDouble() * difference + lowerLimit;

            }

            return randArray;
        }



        //3 b
        public static Structure.GeoPoint[] RandArGeopnt(int numOfDoubles, double lowerLimit, double upperLimit)
        {
            Random rand = new Random();

            Structure.GeoPoint[] geoPoint = new Structure.GeoPoint[numOfDoubles];

            double range = upperLimit - lowerLimit;

            for (int i = 0; i < numOfDoubles; i++)
            {
                geoPoint[i].latitudeDeg = rand.NextDouble() * range + lowerLimit;
                geoPoint[i].longitudeDeg = rand.NextDouble() * range + lowerLimit;
            }

            
            return geoPoint;
        }



        //3 c
        public static Structure.XY[] xy(int numOfDoubles, double lowerLimit, double upperLimit)
        {
            Random rand = new Random();

            Structure.XY[] xy = new Structure.XY[numOfDoubles];

            double range = upperLimit - lowerLimit;

            for (int i = 0; i < numOfDoubles; i++)
            {
                xy[i].x = rand.NextDouble() * range + lowerLimit;
                xy[i].y = rand.NextDouble() * range + lowerLimit;
            }

            return xy;
        }

    }
}
