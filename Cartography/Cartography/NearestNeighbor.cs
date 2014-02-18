using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
                      
namespace Cartography
{
    public class NearestNeighbor
    {
        //1
        public static Structure.XY MeanCentroid(Structure.XY zz)
        {
            Structure.XY xy = new Structure.XY();
            const int howlong = 5;
            Structure.UTMCoord[] utm = new Structure.UTMCoord[howlong];

            xy = Stats.AveragePDOC(utm);

            
            return xy;
        }


        //2
        public static Structure.XY MedianCentroid(Structure.XY zz)
        {
            Structure.XY xy = new Structure.XY();
            const int howlong = 5;
            Structure.UTMCoord[] utm = new Structure.UTMCoord[howlong];

            xy = Stats.MedianPDOC(utm);
           


            return xy;
        }


        //3
        public static Structure.XY StandardDeviation(Structure.XY zz)
        {
            Structure.XY xy = new Structure.XY();
            const int howlong = 5; 
            Structure.UTMCoord[] utm = new Structure.UTMCoord[howlong];

            xy = Stats.StandardDevPDOC(utm);


            return xy;
        }


        //4

        public static Structure.XY BoundBox1(Structure.UTMCoord[] utm)
        {
            int index1;
            Structure.XY latmax = new Structure.XY();
            double[] northing = new double[utm.Length];
            for (int i = 0; i < utm.Length; i++)
            {
                northing[i] = utm[i].northing;
            }

            index1 = Array.IndexOf(northing, northing.Max());
            latmax.y = utm[index1].northing;
            latmax.x = utm[index1].easting;

            return latmax;
        }

        public static Structure.XY BoundBox2(Structure.UTMCoord[] utm)
        {
            Structure.XY longmax = new Structure.XY();
            double[] easting = new double[utm.Length];
            for (int i = 0; i < utm.Length; i++)
            {
                easting[i] = utm[i].easting;
            }

            int index2;
            index2 = Array.IndexOf(easting, easting.Max());
            longmax.x = utm[index2].easting;
            longmax.y = utm[index2].northing;

            return longmax;
        }

        public static Structure.XY BoundBox3(Structure.UTMCoord[] utm)
        {
            Structure.XY latmin = new Structure.XY();
            double[] northing = new double[utm.Length];
            for (int i = 0; i < utm.Length; i++)
            {
                northing[i] = utm[i].northing;
            }

            int index3;
            index3 = Array.IndexOf(northing, northing.Min());
            latmin.y = utm[index3].northing;
            latmin.x = utm[index3].easting;
            return latmin;
        }

        public static Structure.XY BoundBox4(Structure.UTMCoord[] utm)
        {
            Structure.XY longmin = new Structure.XY();
            double[] easting = new double[utm.Length];
            for (int i = 0; i < utm.Length; i++)
            {
                easting[i] = utm[i].easting;
            }
            int index4;
            index4 = Array.IndexOf(easting, easting.Min());
            longmin.x = utm[index4].easting;
            longmin.y = utm[index4].northing;

            return longmin;
        }
       


        //5
        public static double BoundingBoxArea(Structure.UTMCoord[] utm)
        {
            //call the 4 methods of maxmin points and you're set, pal!
            Structure.XY latmax = new Structure.XY(); Structure.XY longmax = new Structure.XY(); 
            Structure.XY latmin = new Structure.XY(); Structure.XY longmin = new Structure.XY();

            latmax = BoundBox1(utm); longmax = BoundBox2(utm); latmin = BoundBox3(utm); longmin = BoundBox4(utm);


            //now...subtract the values from the previous exercise and that's good.  
            double area = 0;
            double xdiff;
            double ydiff;

            xdiff = longmax.x - longmin.x;
            ydiff = latmax.y - latmin.y;

            area = xdiff * ydiff;



            return area;
        }


        //6
        public static double AvgNearNeighDistance(Structure.UTMCoord[] utm)
        {


            double[] distances = new double[utm.Length];

            distances = Stats.closestpointdistancePDOC(utm, utm);
            
            
            double avgdistance = Stats.Average(distances);


            

            return avgdistance;
        }



        //7before you start

       


        //7
        public static double ExpectMeanNearNeighDistRand(double p)
        {
            
            double distance = 1 / (2 * Math.Sqrt(p));



            return distance;
        }



        //8
        public static double ExpectMeanNearNeighDistDispersion(double p)
        {
            double distance = Math.Pow(2, 0.5) / (Math.Pow(3, 0.25) * Math.Sqrt(p));


            return distance;
        }




        //9
        //public static double NearNeighIndex(double p)
        //{
        //    double index = AvgNearNeighDistance() / ExpectMeanNearNeighDistRand(p);

        //    return index;
        //}



        ////10
        //public static double NearNeighTestStatC(double p, double n)
        //{

        //    double c = (AvgNearNeighDistance() / ExpectMeanNearNeighDistRand(p)) / StandardErrorMeanNearNeighDist(n, p);


        //    return c;
        //}



        //11before

      

        //11
        public static double StandardErrorMeanNearNeighDist(double n, double p)
        {
            double error = 0.26136 / Math.Sqrt(n * p);


            return error;
        }
    

   






    }
}
