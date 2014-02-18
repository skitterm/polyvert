using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
          //CHANGES: commented out warning for different UTM zones--not important here (with own central meridian)


namespace Cartography
{
    public class Trigonometry
    {
       
        public static double calcRadians(double degrees)
        {//A
            double Radians = degrees * 0.0174532925;

            return Radians; 
        }

        
        public static double calcDegrees(double radians)
        {//B
            double degrees = radians * 57.2957795;

            return degrees;
        }


        public static double calcDecimalDeg(int degrees, int minutes, double seconds)
        {//C
            int dEgrees = Math.Abs(degrees);


            int minutesNonLegit = minutes / 60;

            double minutesLegit = (double)minutes / 60;

            double minutesremainder = minutesLegit - minutesNonLegit;

            double decimalDegrees =  dEgrees + minutes / 60 + minutesremainder + seconds / 3600;

            //give the degrees its original sign. 

            if (degrees < 0)
            {
                decimalDegrees = decimalDegrees * -1;
            }


                return decimalDegrees;
        }


        public static void calcDMS(double decimalDegrees, out int degrees, out int minutes, out double seconds)
        {//D
            degrees = (int)(decimalDegrees);
            
            //new value to plug into seconds.
            double almost = Math.Abs((decimalDegrees - degrees) * 60);
            minutes = Math.Abs((int)(almost));
            seconds = Math.Abs((almost - minutes) * 60);

        }

         public static void gDMStgPnt(char laHemisphere, int laDegrees, int laMinutes, double laSeconds, 
             char loHemisphere, int loDegrees, int loMinutes, double loSeconds, out double geoPointLa, out double geoPointLo)
         {//E
             geoPointLa = calcDecimalDeg(laDegrees, laMinutes, laSeconds);

             geoPointLo = calcDecimalDeg(loDegrees, loMinutes, loSeconds);
            //add hemispheres. 

             if (laHemisphere == 'S')
             {
                 geoPointLa = geoPointLa * -1;
             }


             if (loHemisphere == 'W')
             {
                 geoPointLo = geoPointLo * -1;
             }



             
         }

         public static Structure.GeoPoint gDMStgPntPDOC(Structure.GeoDMS Input)
         {//F
           Structure.GeoPoint output = new Structure.GeoPoint();

            output.latitudeDeg = calcDecimalDeg(Input.latDegrees, Input.latMinutes, Input.latSeconds);

            output.longitudeDeg = calcDecimalDeg(Input.longDegrees, Input.longMinutes, Input.longSeconds);

            if (Input.latHemisphere == 'S')
            {
                output.latitudeDeg *= -1;
            }


            if (Input.longHemisphere == 'W')
            {
                output.longitudeDeg *= -1;
            }


            return output;

         }

    
        public static void PtToDMS(double geopointLa, double geopointLo, out char laHemisphere, out int laDegrees, out int laMinutes, out double laSeconds,
            out char loHemisphere, out int loDegrees, out int loMinutes, out double loSeconds)
        {//G
            calcDMS(geopointLa, out laDegrees, out laMinutes, out laSeconds);

            calcDMS(geopointLo, out loDegrees, out loMinutes, out loSeconds);

            if (geopointLa < 0)
            {
                laHemisphere = 'S';
            }

            else laHemisphere = 'N';

            if (geopointLo < 0)
            {
                loHemisphere = 'W';
            }

            else loHemisphere = 'E';
        }

        public static Structure.GeoDMS GeodmstPntPDOC(Structure.GeoPoint input)
        {//H
            Structure.GeoDMS output = new Structure.GeoDMS();

            calcDMS(input.latitudeDeg, out output.latDegrees, out output.latMinutes, out output.latSeconds);
            
            calcDMS(input.longitudeDeg, out output.longDegrees, out output.longMinutes, out output.longSeconds);

            if (input.latitudeDeg < 0)
            {
                output.latHemisphere = 'S';
            }
            else output.latHemisphere = 'N';

            if (input.longitudeDeg < 0)
            {
                output.longHemisphere = 'W';
            }
            else output.longHemisphere = 'E';

            return output;
        }

        public static double DistPythX1(double lat1, double long1, double lat2, double long2)
        {//I a. 

            double a = lat1 - lat2;
            double b = long1 - long2;
            double cSquared;
            
            cSquared = Math.Pow(a, 2) + Math.Pow(b, 2);

            double c = Math.Sqrt(cSquared);

           
            return c;
        }


        public static double DistPythXY(Structure.XY point1, Structure.XY point2)
        {//I a. 

            double a = point1.x - point2.x;
            double b = point1.y - point2.y;
            double cSquared;

            cSquared = Math.Pow(a, 2) + Math.Pow(b, 2);

            double c = Math.Sqrt(cSquared);


            return c;
        }


        public static double DistPythGeoPoint(Structure.GeoPoint point1, Structure.GeoPoint point2)
        {//I b. 
            
            double a = point1.latitudeDeg - point2.latitudeDeg;
            double b = point1.longitudeDeg - point2.longitudeDeg;
            double cSquared;

            cSquared = Math.Pow(a, 2) + Math.Pow(b, 2);

            double c = Math.Sqrt(cSquared);


            return c;
        }

        public static double DistPythUTM(Structure.UTMCoord utm1, Structure.UTMCoord utm2)
        {//I c.
            /*
            if (utm1.zoneNumber != utm2.zoneNumber)
            {
                throw new Exception("UTM coordinates must be in the same Zone Number.");
            }
            */
            double a = utm1.northing - utm2.northing;
            double b = utm1.easting - utm2.easting;
            double cSquared;

            cSquared = Math.Pow(a, 2) + Math.Pow(b, 2);

            double c = Math.Sqrt(cSquared);


            return c;
        }

        public static double SphereDistanceGeoHDMS(Structure.GeoDMS dms1, Structure.GeoDMS dms2)
        {//J a.

            Structure.GeoPoint Gms1 = new Structure.GeoPoint();
            Gms1 = gDMStgPntPDOC(dms1);
            
            
            Structure.GeoPoint Gms2 = new Structure.GeoPoint();
            Gms2 = gDMStgPntPDOC(dms2);


            Gms1.latitudeDeg = Gms1.latitudeDeg * Constant.DegToRad;
            Gms1.longitudeDeg = Gms1.longitudeDeg * Constant.DegToRad;
            Gms2.latitudeDeg = Gms2.latitudeDeg * Constant.DegToRad;
            Gms2.longitudeDeg = Gms2.longitudeDeg * Constant.DegToRad;

            double longdif =  Gms1.longitudeDeg - Gms2.longitudeDeg;

            double cosofD;

            cosofD = Math.Sin(Gms1.latitudeDeg) * Math.Sin(Gms2.latitudeDeg) + Math.Cos(Gms1.latitudeDeg) * Math.Cos(Gms2.latitudeDeg) * Math.Cos(longdif);
            double D = Math.Acos(cosofD);
            D = D * Constant.RadToDeg;
            double distance = D * 111.111;
            return distance;

        } 

        public static double SphereDistanceTwoPoints(double lat1, double long1, double lat2, double long2)
        {//J b. 

            

            lat1 = lat1 * Constant.DegToRad;
            lat2 = lat2 * Constant.DegToRad;
            long1 = long1 * Constant.DegToRad;
            long2 = long2 * Constant.DegToRad;
            double longDif = long1 - long2;
            double cosofD;

            cosofD = Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(longDif);
            double D = Math.Acos(cosofD);
            D = D * Constant.RadToDeg;
            double distance = D * 111.111;
            return distance;
        }

        public static double SphereDistanceGeoPoint(Structure.GeoPoint input1, Structure.GeoPoint input2)
        {//j c. 

            input1.latitudeDeg = input1.latitudeDeg * Constant.DegToRad;
            input2.latitudeDeg = input2.latitudeDeg * Constant.DegToRad;
            input1.longitudeDeg = input1.longitudeDeg * Constant.DegToRad;
            input2.longitudeDeg = input2.longitudeDeg * Constant.DegToRad;

            double longdif = input1.longitudeDeg - input2.longitudeDeg;

            double cosofD;

            cosofD = Math.Sin(input1.latitudeDeg) * Math.Sin(input2.latitudeDeg) + Math.Cos(input1.latitudeDeg) * Math.Cos(input2.latitudeDeg) * Math.Cos(longdif);
            
            double D = Math.Acos(cosofD);
            D = D * Constant.RadToDeg;
            double distance = D * 111.111;
            
            
            return distance;

        }

        public static double SphereDirectionGeoHDMS(Structure.GeoDMS dms1, Structure.GeoDMS dms2)
        {//K a. 
            Structure.GeoPoint Gms1 = new Structure.GeoPoint();
            Gms1 = gDMStgPntPDOC(dms1);

            Structure.GeoPoint Gms2 = new Structure.GeoPoint();
            Gms2 = gDMStgPntPDOC(dms2);


            Gms1.latitudeDeg = Gms1.latitudeDeg * Constant.DegToRad;
            Gms2.latitudeDeg = Gms2.latitudeDeg * Constant.DegToRad;
            Gms1.longitudeDeg = Gms1.longitudeDeg * Constant.DegToRad;
            Gms2.longitudeDeg = Gms2.longitudeDeg * Constant.DegToRad;

            double longdif =  Gms1.longitudeDeg - Gms2.longitudeDeg;

           
            
            double cosofD;
            cosofD = Math.Sin(Gms1.latitudeDeg) * Math.Sin(Gms2.latitudeDeg) + Math.Cos(Gms1.latitudeDeg) * Math.Cos(Gms2.latitudeDeg) * Math.Cos(longdif);
            double d = Math.Acos(cosofD);



            double cosofC;
            cosofC = (Math.Sin(Gms2.latitudeDeg) - Math.Sin(Gms1.latitudeDeg) * cosofD) / (Math.Cos(Gms1.latitudeDeg) * Math.Sin(d));
            double c = Math.Acos(cosofC);




            double lsin = Math.Sin(longdif);
            
            c = c * Constant.RadToDeg;
            
            if (lsin < 0)
            {
                c = 360 - c;
            }


           

            return c;
        }

        public static double SphereDirectionTwoPoints(double lat1, double long1, double lat2, double long2)
        {//K b.

            lat1 = lat1 * Constant.DegToRad;
            lat2 = lat2 * Constant.DegToRad;
            long1 = long1 * Constant.DegToRad;
            long2 = long2 * Constant.DegToRad;
            
            double longdif = long1 - long2;


            double cosofD;
            cosofD = Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(longdif);
            double d = Math.Acos(cosofD);



            double cosofC;
            cosofC = (Math.Sin(lat2) - Math.Sin(lat1) * cosofD) / (Math.Cos(lat1) * Math.Sin(d));
            double c = Math.Acos(cosofC);



            double lsin = Math.Sin(longdif);

            c = c * Constant.RadToDeg;

            if (lsin < 0)
            {
                c = 360 - c;
            }




            return c;

        }

        public static double SphereDirectionGeoPoint(Structure.GeoPoint input1, Structure.GeoPoint input2)
        {//K c.

            input1.latitudeDeg = input1.latitudeDeg * Constant.DegToRad;
            input1.longitudeDeg = input1.longitudeDeg * Constant.DegToRad;
            input2.latitudeDeg = input2.latitudeDeg * Constant.DegToRad;
            input2.longitudeDeg = input2.longitudeDeg * Constant.DegToRad;


            double longdif = input1.longitudeDeg - input2.longitudeDeg;


            double cosofD;
            cosofD = Math.Sin(input1.latitudeDeg) * Math.Sin(input2.latitudeDeg) + Math.Cos(input1.latitudeDeg) * Math.Cos(input2.latitudeDeg) * Math.Cos(longdif);
            double d = Math.Acos(cosofD);



            double cosofC;
            cosofC = (Math.Sin(input2.latitudeDeg) - Math.Sin(input1.latitudeDeg) * cosofD) / (Math.Cos(input1.latitudeDeg) * Math.Sin(d));
            double c = Math.Acos(cosofC);

           

            double lsin = Math.Sin(longdif);


            c = c * Constant.RadToDeg;

            if (lsin < 0)
            {
                c = 360 - c;
            }




            return c;



        }
     }
}
//54S 797978E 2323232N

