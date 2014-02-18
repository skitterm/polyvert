using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Cartography
{
    public class Randomoperations
    {
        //3 a
        public static double[] RandArray(int numOfDoubles, double lowerLimit, double upperLimit)
        {
            Random rand = new Random();
           
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
                geoPoint[i] = new Structure.GeoPoint();
                geoPoint[i].latitudeDeg = rand.NextDouble() * range + lowerLimit;
                geoPoint[i].longitudeDeg = rand.NextDouble() * range + lowerLimit;
            }


            return geoPoint;
        }



        //3 c
        public static Structure.XY[] XY(int numOfDoubles, double lowerLimit, double upperLimit)
        {
            Random rand = new Random();

            Structure.XY[] xy = new Structure.XY[numOfDoubles];

            double range = upperLimit - lowerLimit;

            for (int i = 0; i < numOfDoubles; i++)
            {
                xy[i] = new Structure.XY();
                xy[i].x = rand.NextDouble() * range + lowerLimit;
                xy[i].y = rand.NextDouble() * range + lowerLimit;
            }

            return xy;
        }


    }
}
