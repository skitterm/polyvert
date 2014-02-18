using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cartography
{
    class RanOps
    {
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
        public static Structure.GeoPoint[,] RandArGeopnt(int numOfDoubles, double lowerLimit, double upperLimit)
        {
            Random rand = new Random();
            Structure.GeoPoint here = new Structure.GeoPoint();
            Structure.GeoPoint[,] geoPoint = new Structure.GeoPoint[numOfDoubles, 2];



            double range = upperLimit - lowerLimit;

            for (int i = 0; i < numOfDoubles; i++)
            {

                geoPoint[0, i] = rand.NextDouble() * range + lowerLimit;


            }


        }



        //3 c
        public static double[,] xy(int numOfDoubles, double lowerLimit, double upperLimit)
        {
            Random rand = new Random();

            double[,] reallegit = new double[numOfDoubles, 2];

            double range = upperLimit - lowerLimit;

            for (int row = 0; row < numOfDoubles; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    reallegit[row, col] = rand.NextDouble() * range + lowerLimit;
                }
            }

            return reallegit;
        }



    }
}
