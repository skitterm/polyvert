using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cartography
{
    public class Scale
    {
        
        
        //so....how in the world do we figure out what the "target units" are? And does all this go outside of the brackets?  

        public static double calcGD(double mapDistance, double scaleFactor)
        {
            
            double gd = 0.0;
            gd = mapDistance * scaleFactor;
            
            return gd;
        }

        public static double calcMD(double groundDistance, double scaleFactor)
        {
            double md = 0.0;
            md = groundDistance / scaleFactor;

            return md;
        }

        public static double calcSF(double groundDistance, double mapDistance)
        {
            double sf = 0.0;
            sf = groundDistance / mapDistance;

            return sf;
        }

        public static string groundDistance(double scaleFactor, double mapDistance, string sUnits, string tUnits)
        {
            double grounddistance = -.96;

            string gdistance;

            if ((sUnits == "inches")&&(tUnits == "feet"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance  = gD / 12;
            
            }

            else if ((sUnits == "inches") && (tUnits == "yards"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD / 36;
            }

            else if ((sUnits == "inches") && (tUnits == "meters"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD * 0.0254;
            }

            else if ((sUnits == "inches") && (tUnits == "miles"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD / 63360;
            }
            
            else if ((sUnits == "inches") && (tUnits == "kilometers"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD * 0.0000254;
            }


            else if ((sUnits == "centimeters") && (tUnits == "feet"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD * 0.032808399;

            }

            else if ((sUnits == "centimeters") && (tUnits == "yards"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD * 0.010936133;
            }

            else if ((sUnits == "centimeters") && (tUnits == "meters"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD / 100;

            }

            else if ((sUnits == "centimeters") && (tUnits == "miles"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD * .00000621371192;
            }

            else if ((sUnits == "centimeters") && (tUnits == "kilometers"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD / 100000;
            }

            else if ((sUnits == "millimeters") && (tUnits == "feet"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD * 0.0032808399;
            }

            else if ((sUnits == "millimeters") && (tUnits == "yards"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD * 0.0010936133;
            }

            else if ((sUnits == "millimeters") && (tUnits == "meters"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD / 1000;
            }

            else if ((sUnits == "millimeters") && (tUnits == "miles"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD * .000000621371192;
            }

            else if ((sUnits == "millimeters") && (tUnits == "kilometers"))
            {
                double gD = calcGD(mapDistance, scaleFactor);

                grounddistance = gD / 1000000;
            }

            else
            {
                throw new Exception("Incorrect Unit.");
                
            }

            gdistance = grounddistance.ToString() + " " + tUnits;

           
            return gdistance;

        }

    }
}
