using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cartography
{                              //2a1. can I have these set to ref, or need to be each in own method/4 methods? 
    public class Stats
    {
        //2a.

        public static void arrayMinMax(double[] inputArray, out double greatest, out double small)
        {
            
           

            greatest = inputArray.Max();

            small = inputArray.Min();
           
            
        }



        //2a1. 

       
          
           

       


        //2b. 

        public static double Median(double[] inputArray)
        {
            
           
           
            double[] sortArray = new double[inputArray.Length];

            for( int i = 0; i <inputArray.Length; i++)
            {
                sortArray[i] = inputArray[i];
            }

            
            Array.Sort(sortArray);

            double median;
             
            int medianrecord;

            int medianrecord2;

            int records = sortArray.Length;

            bool even = ((records % 2) == 0);

            if (even == false)
            {
                medianrecord = records / 2;

                median = sortArray[medianrecord];
                return median;
            }

            else 
            {
                medianrecord = records / 2 - 1;
                medianrecord2 = records / 2;

                median = ((sortArray[medianrecord] + sortArray[medianrecord2]) / 2);
                return median;
            }

           
         
        }





       //2b1.

        public static Structure.XY MedianPDOC(Structure.UTMCoord[] utm)
        {

            Structure.XY xy = new Structure.XY();

            double[] easting = new double[utm.Length];
            double[] northing = new double[utm.Length];

            for (int i = 0; i < utm.Length; i++)
            {
                easting[i] = utm[i].easting;
            }

            for (int i = 0; i < utm.Length; i++)
            {
                northing[i] = utm[i].northing;
            }



            Array.Sort(easting);
            Array.Sort(northing);

            int medianrecorde = 0;
            int medianrecordn = 0;

            int medianrecord2e = 0;
            int medianrecord2n = 0;

            int recordse = easting.Length;
            int recordsn = northing.Length;

            bool even = ((recordse % 2) == 0);

            if (even == false)
            {
                medianrecorde = recordse / 2;

                xy.y = northing[medianrecordn];
                xy.x = easting[medianrecorde];
                return xy;
            }

            else
            {
                medianrecorde = recordse / 2 - 1;
                medianrecordn = recordsn / 2 - 1;

                medianrecord2e = recordse / 2;
                medianrecord2n = recordsn / 2;

                xy.x = ((easting[medianrecorde] + easting[medianrecord2e]) / 2);
                xy.y = ((northing[medianrecordn] + northing[medianrecord2n]) / 2);
                return xy;
            }



        }


        
        
        //2c. 

        public static double Average(double[] inputArray)
        {
           

            double total = 0;

            double average;

            for (int i = 0; i < inputArray.Length; i++)
            {
                total += inputArray[i];

            }
            average = total / inputArray.Length;

            return average;
        }


        //2c1.

        public static Structure.XY AveragePDOC(Structure.UTMCoord[] utm)
        {
            Structure.XY xy = new Structure.XY();
            xy.x = 0;
            xy.y = 0;
            double totallat = 0;
            double totallong = 0;
           
            for (int i = 0; i < utm.Length; i++)
            {
                totallat += utm[i].northing;
                totallong = utm[i].easting;

            }
            xy.y = totallat / utm.Length;
            xy.x = totallong / utm.Length;


            return xy;
        }




        //2d.

        public static double StandardDev(double[] inputArray)
        {
            

            double average = Cartography.Stats.Average(inputArray);

            double sum = 0;

            for (int aj = 0; aj < inputArray.Length; aj++)
            {
                inputArray[aj] -= average;
            }
            for (int lk = 0; lk < inputArray.Length; lk++)
            {
                inputArray[lk] = Math.Pow(inputArray[lk], 2);
            }

            for (int al = 0; al < inputArray.Length; al++)
            {
                sum += inputArray[al];
            }




            double variance = sum / (inputArray.Length - 1);

            double standarddev = Math.Pow(variance, 0.5);

            return standarddev;
        }

        
        
        
        //2d1.
        
        
        public static Structure.XY StandardDevPDOC(Structure.UTMCoord[] utm)
        {
            Structure.XY xy = new Structure.XY();
            Structure.XY average = new Structure.XY();

            Structure.UTMCoord[] utm2 = new Structure.UTMCoord[utm.Length];
            for (int i = 0; i < utm.Length; i++)
            {
                utm2[i] = new Structure.UTMCoord();
                
                utm2[i] = utm[i];
            }
            average = Stats.AveragePDOC(utm2);
            

            double sumlat = 0;
            double sumlong = 0;
            for (int aj = 0; aj < utm.Length; aj++)
            {
                utm2[aj].northing -= average.y;
                utm2[aj].easting -= average.x;
            }
            for (int lk = 0; lk < utm.Length; lk++)
            {
                utm2[lk].northing = Math.Pow(utm[lk].northing, 2);
                utm2[lk].easting = Math.Pow(utm[lk].easting, 2);
            }

            for (int al = 0; al < utm.Length; al++)
            {
                sumlat += utm2[al].northing;
                sumlong += utm2[al].easting;
            }




            double variancelat = sumlat / (utm.Length - 1);
            double variancelong = sumlong / (utm.Length - 1);

            xy.y = Math.Pow(variancelat, 0.5);
            xy.x = Math.Pow(variancelong, 0.5);

            return xy;
           
        }



        //2e.

        public static void closestpointindex(Structure.XY[] xy, Structure.XY targetPt, bool ZeroDistanceLegal, out double distance, out int index)
        {
            distance = 5;
            index = 45;
            double[] distances = new double[xy.Length];
            for (int i = 0; i < xy.Length; i++)
            {
                distances[i] = Math.Pow(xy[i].x - targetPt.x, 2) + Math.Pow(xy[i].y - targetPt.y, 2); 
            }

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = Math.Pow(distances[i], 0.5);
            }

            double bogusdistance = distances.Min();

            int bogusindex = Array.IndexOf(distances, distances.Min());
            
            ZeroDistanceLegal = true;

            if (ZeroDistanceLegal == false)
            {
                if (bogusdistance == 0)
                {
                    distances[bogusindex] += distances.Max();

                    distance = distances.Min();

                    index = Array.IndexOf(distances, distances.Min());

                   
                }
            }

            else if (ZeroDistanceLegal == true)
            {
                distance = bogusdistance;
                index = bogusindex;
            }

           

            



        }




        //2e1.

        public static double[] closestpointdistancePDOC(Structure.UTMCoord[] xy, Structure.UTMCoord[] targetPt)
        {

            double[] distances = new double[xy.Length];
            for (int i = 0; i < xy.Length; i++)
            {
                distances[i] = Math.Pow(xy[i].easting - targetPt[i].easting, 2) + Math.Pow(xy[i].northing - targetPt[i].northing, 2);
            }

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = Math.Pow(distances[i], 0.5);

                distances[i] = distances.Min();
            }


            return distances;
        }



    }
}
