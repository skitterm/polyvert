using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
                   //CHANGED: centMer (line 10-ish) is calculated specially for these points, accepting longMax and longMin as parameters. 
namespace Cartography
{
    public class UTM
    {
        public static Structure.UTMCoord GeopointToUTM (Structure.GeoPoint latlong, Structure.Ellipsoidcoord ellip)//double longMax, double longMin
        {
            Structure.UTMCoord utm = new Structure.UTMCoord();
           

            utm.zoneNumber = ((int)latlong.longitudeDeg + 180) / 6 + 1;

            double centMer = ((utm.zoneNumber * 6) - 180) - 3; //defailt centMer calculator
            //double centMer = (longMax + longMin) / 2; 
           

            if (latlong.latitudeDeg >= 56.0 && latlong.latitudeDeg < 64.0)
            {
                if (latlong.longitudeDeg >= 3.0 && latlong.longitudeDeg < 12.0)
                    utm.zoneNumber = 32;
            }

            
            if (latlong.latitudeDeg >= 72.0 && latlong.latitudeDeg < 84.0)
            {
                if (latlong.longitudeDeg >= 0.0 && latlong.longitudeDeg < 9.0)

                    utm.zoneNumber = 31;

                else if (latlong.longitudeDeg >= 9.0 && latlong.longitudeDeg < 21.0)

                    utm.zoneNumber = 33;

                else if (latlong.longitudeDeg >= 21.0 && latlong.longitudeDeg < 33.0)

                    utm.zoneNumber = 35;

                else if (latlong.longitudeDeg >= 33.0 && latlong.longitudeDeg < 42.0)

                    utm.zoneNumber = 37;
            }

            try
            {
                if (latlong.latitudeDeg >= 0 && latlong.latitudeDeg < 8.0) utm.zoneLetter = 'N';
                else if (latlong.latitudeDeg >= 8.0 && latlong.latitudeDeg < 16.0) utm.zoneLetter = 'P';
                else if (latlong.latitudeDeg >= 16.0 && latlong.latitudeDeg < 24.0) utm.zoneLetter = 'Q';
                else if (latlong.latitudeDeg >= 24.0 && latlong.latitudeDeg < 32.0) utm.zoneLetter = 'R';
                else if (latlong.latitudeDeg >= 32.0 && latlong.latitudeDeg < 40.0) utm.zoneLetter = 'S';
                else if (latlong.latitudeDeg >= 40.0 && latlong.latitudeDeg < 48.0) utm.zoneLetter = 'T';
                else if (latlong.latitudeDeg >= 48.0 && latlong.latitudeDeg < 56.0) utm.zoneLetter = 'U';
                else if (latlong.latitudeDeg >= 56.0 && latlong.latitudeDeg < 64.0) utm.zoneLetter = 'V';
                else if (latlong.latitudeDeg >= 64.0 && latlong.latitudeDeg < 72.0) utm.zoneLetter = 'W';
                else if (latlong.latitudeDeg >= 72.0 && latlong.latitudeDeg < 84.0) utm.zoneLetter = 'X';
                else if (latlong.latitudeDeg >= -8.0 && latlong.latitudeDeg < 0.0) utm.zoneLetter = 'M';
                else if (latlong.latitudeDeg >= -16.0 && latlong.latitudeDeg < -8.0) utm.zoneLetter = 'L';
                else if (latlong.latitudeDeg >= -24.0 && latlong.latitudeDeg < -16.0) utm.zoneLetter = 'K';
                else if (latlong.latitudeDeg >= -32.0 && latlong.latitudeDeg < -24.0) utm.zoneLetter = 'J';
                else if (latlong.latitudeDeg >= -40.0 && latlong.latitudeDeg < -32.0) utm.zoneLetter = 'H';
                else if (latlong.latitudeDeg >= -48.0 && latlong.latitudeDeg < -40.0) utm.zoneLetter = 'G';
                else if (latlong.latitudeDeg >= -56.0 && latlong.latitudeDeg < -48.0) utm.zoneLetter = 'F';
                else if (latlong.latitudeDeg >= -64.0 && latlong.latitudeDeg < -56.0) utm.zoneLetter = 'E';
                else if (latlong.latitudeDeg >= -72.0 && latlong.latitudeDeg < -64.0) utm.zoneLetter = 'D';
                else if (latlong.latitudeDeg >= -80.0 && latlong.latitudeDeg < -72.0) utm.zoneLetter = 'C';
                else if ((latlong.latitudeDeg >= -90.0 && latlong.latitudeDeg < -80.0) || (latlong.latitudeDeg >= 84.0 && latlong.latitudeDeg < 90.0)) utm.zoneLetter = 'I';
            }
            
            catch (Exception)
            {
                throw new Exception("UTM Zoneletter error.");
            }

            double eccentricity = 2 * ellip.flattening - Math.Pow(ellip.flattening, 2);

            double eccprimeSq = eccentricity / (1 - eccentricity);

            latlong.latitudeDeg = latlong.latitudeDeg * Constant.DegToRad;

            latlong.longitudeDeg = latlong.longitudeDeg * Constant.DegToRad;

            double M = ellip.eqRadius * ((1 - eccentricity / 4 - 3 * Math.Pow(eccentricity, 2) / 64 - 5 * Math.Pow(eccentricity, 3) / 256) *
            latlong.latitudeDeg - (3 * eccentricity / 8 + 3 * Math.Pow(eccentricity, 2) / 32 + 45 * Math.Pow(eccentricity, 3) / 1024) *
            Math.Sin(2 * latlong.latitudeDeg) + (15 * Math.Pow(eccentricity, 2) / 256 + 45 * Math.Pow(eccentricity, 3) / 1024) *
            Math.Sin(4 * latlong.latitudeDeg) - (35 * Math.Pow(eccentricity, 3) / 3072) *
            Math.Sin(6 * latlong.latitudeDeg));

            double M0 = 0;

            double N = ellip.eqRadius / Math.Pow((1 - eccentricity * Math.Pow(Math.Sin(latlong.latitudeDeg), 2)), .5);
           
            double C = eccprimeSq * Math.Pow(Math.Cos(latlong.latitudeDeg), 2);
            
            double T = Math.Pow(Math.Tan(latlong.latitudeDeg), 2);

            double centMerRA = centMer * Constant.DegToRad;


            double A = Math.Cos(latlong.latitudeDeg) * (latlong.longitudeDeg - centMerRA);

            const double K0 = .9996;


            utm.easting = K0 * N * (A + (1 - T + C) * Math.Pow(A, 3) / 6 + (5 - 18 * T + Math.Pow(T, 2) + 72 * C - 58 * eccprimeSq) *
            Math.Pow(A, 5) / 120);

            utm.northing = K0 * (M - M0 + N * Math.Tan(latlong.latitudeDeg) * (A * A / 2 + (5 - T + 9 * C + 4 * (C * C)) * Math.Pow(A, 4) /
            24 + (61 - 58 * T + T * T + 600 * C - 330 * eccprimeSq) * Math.Pow(A, 6) / 720));

            
            if (latlong.latitudeDeg < 0)
            {
                utm.northing += 10000000;
            }

            utm.easting += 500000;


            return utm;
            }

        public static Structure.GeoPoint UTMToGeopoint (Structure.UTMCoord utm, Structure.Ellipsoidcoord ellip)
        {
            Structure.GeoPoint latlong = new Structure.GeoPoint();

            const double k0 = .9996;

            double eccentricity = 2 * ellip.flattening - Math.Pow(ellip.flattening, 2);
           
            double eccprsq = eccentricity / (1 - eccentricity);

            double centMer = (utm.zoneNumber - 1) * 6 - 180 + 3;

            double centMerRa = centMer * Constant.DegToRad;

            double e1 = (1 - Math.Pow((1 - eccentricity), 0.5)) / (1 + Math.Pow((1 - eccentricity), 0.5));

            const double m0 = 0;

            double m = m0 + utm.northing / k0;

            double u = m / (ellip.eqRadius * (1 - eccentricity / 4 - 3 * Math.Pow(eccentricity, 2) / 64 - 5 *
               Math.Pow(eccentricity, 3) / 256));

            double lat1 = u + (3 * e1 / 2 - 27 * Math.Pow(e1, 3) / 32) * Math.Sin(2 * u) + (21 * Math.Pow(e1, 2)
              / 16 - 55 * Math.Pow(e1, 4) * 32) * Math.Sin(4 * u) + (151 * Math.Pow(e1, 3) / 96)
              * Math.Sin(6 * u);

            double c1 = eccprsq * (Math.Cos(lat1) * Math.Cos(lat1));

            double t1 = Math.Tan(lat1) * Math.Tan(lat1);

            double n1 = ellip.eqRadius / Math.Pow((1 - eccentricity * (Math.Sin(lat1) * Math.Sin(lat1))), 0.5);

            double r1 = ellip.eqRadius * (1 - eccentricity) / Math.Pow((1 - eccentricity * (Math.Sin(lat1) * 
                Math.Sin(lat1))), 
                1.5);

            double d = utm.easting / (n1 * k0);

            latlong.latitudeDeg = lat1 - (n1 * Math.Tan(lat1) / r1) *
                (Math.Pow(d, 2) / 2 - (5 + 3 * t1 + 10 * c1 - 4 * Math.Pow(c1, 2) - 9 * eccprsq) * 
                    Math.Pow(d, 4) / 24 + (61 + 90 * t1 + 298 * c1 + 45 * Math.Pow(t1, 2) - 
                252 * eccprsq - 3 * Math.Pow(c1, 2)) * Math.Pow(d, 6) / 720);


            latlong.longitudeDeg = centMerRa + (d - (1 + 2 * t1 + c1) * Math.Pow(d, 3)
                / 6 + (5 - 2 * c1 + 28 * t1 - 3 * Math.Pow(c1, 2) + 8 * eccprsq + 24 * Math.Pow(t1, 2))
                * Math.Pow(d, 5) / 120) / Math.Cos(lat1);

            latlong.latitudeDeg *= Constant.RadToDeg;
            latlong.longitudeDeg *= Constant.RadToDeg;


            return latlong;
        }


    }
}
