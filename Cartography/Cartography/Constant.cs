using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cartography
{
    public class Constant
    {
        public const double DegToRad = 0.01745329251994329576923690768489; 
        public const double RadToDeg = 57.295779513082320876798154814105;

        public const double PI = Math.PI;
        public const double PIDev2 = PI / 2;
        public const double PIDev4 = PI / 4;
        public const double ThreePIDevFour = 3 * PI / 4;

        public const double PolRadius84 = 6356752.3;
        public const double EquatRadius84 = 6378137;
        public const double Flat84 = 0.00335281317789691440603238146967;
        public const double InvFlat84 = 298.257;

        public const double PolRadius27 = 6356583.8;
        public const double EquatRadius27 = 6378206.4;
        public const double Flat27 = 0.00339006034307410671909959997288;
        public const double InvFlat27 = 294.98;

        public const double PolRadiusINL = 6356911.9;
        public const double EquatRadiusINL = 6378388;
        public const double FlatINL = 0.003367003367003367003367003367;
        public const double InvFlatINL = 297;

        public const double CircuEarthKSph = 40008;
        public const double CircuEarthMSph = 24860;

        public const double RadiusEarthKSph = 6367.4709632205485934415016050076;
        public const double RadiusEarthMSph = 3956.5918852645180472144503574407;

        
        //Jake's code.
        #region Prefixes and trigonometry
        //Constants for prefixes
       public const double TERA = 1.0E+12;
       public const double GIGA = 1.0E+9;
       public const double MEGA = 1.0E+6;
       public const double KILO = 1.0E+3;
       public const double HECTO = 1.0E+2;
       public const double DEKA = 1.0E+1;
       public const double DECI = 1.0E-1;
       public const double CENTI = 1.0E-2;
       public const double MILLI = 1.0E-3;
       public const double MICRO = 1.0E-6;
       public const double NANO = 1.0E-9;
       public const double PICO = 1.0E-12;

        //Constants for trig
       public const double DEGREE2RADIAN = Math.PI / 180.0;
       public const double RADIAN2DEGREE = 1.0 / DEGREE2RADIAN;

        #endregion Prefixes and trigonometry

        #region Multipliers for length conversion

        //Constants for length conversion
        public const double ANGSTROM2NANOMETER = 0.1;
        public const double NANOMETER2ANGSTROM = 1.0 / ANGSTROM2NANOMETER;

        public const double ANGSTROM2METER = 1.0E-10;
        public const double METER2ANGSTROM = 1.0 / ANGSTROM2METER;

        public const double FATHOM2METER = 1.828804;
        public const double METER2FATHOM = 1.0 / FATHOM2METER;

        public const double FOOT2METER = 0.3048;
        public const double METER2FOOT = 1.0 / FOOT2METER;

        public const double SURVEYFOOT2METER = 0.3048006;
        public const double METER2SURVEYFOOT = 1.0 / SURVEYFOOT2METER;

        public const double INCH2CENTIMETER = 2.54;
        public const double CENTIMETER2INCH = 1.0 / INCH2CENTIMETER;

        public const double INCH2MILLIMETER = 25.4;
        public const double MILLIMETER2INCH = 1.0 / INCH2MILLIMETER;

        public const double MICROINCH2MICROMETER = 0.0254;
        public const double MICROMETER2MICROINCH = 1.0 / MICROINCH2MICROMETER;

        public const double MIL2MILLIMETER = 0.0254;
        public const double MILLIMETER2MIL = 1.0 / MIL2MILLIMETER;

        public const double MIL2MICROMETER = 25.4;
        public const double MICROMETER2MIL = 1.0 / MIL2MICROMETER;

        public const double YARD2METER = 0.9144;
        public const double METER2YARD = 1.0 / YARD2METER;

        public const double MILE2KILOMETER = 1.609344;
        public const double KILOMETER2MILE = 1.0 / MILE2KILOMETER;

        public const double NAUTICALMILE2KILOMETER = 1.852;
        public const double KILOMETER2NAUTICALMILE = 1.0 / NAUTICALMILE2KILOMETER;

        public const double NAUTICALMILE2MILE = 1.1507794;
        public const double MILE2NAUTICALMILE = 1.0 / NAUTICALMILE2MILE;

        public const double POINT2MILLIMETER = 0.35146;
        public const double MILLIMETER2POINT = 1.0 / POINT2MILLIMETER;

        public const double PICA2MILLIMETER = 4.2175;
        public const double MILLIMETER2PICA = 1.0 / PICA2MILLIMETER;

        #endregion Multipliers for length conversion

        #region Multipliers for area conversion
        //Constants for area conversion

        public const double ACRE2SQUAREMETER = 4046.873;
        public const double SQUAREMETER2ACRE = 1.0 / ACRE2SQUAREMETER;

        public const double ACRE2HECTARE = 0.4046873;
        public const double HECTARE2ACRE = 1.0 / ACRE2HECTARE;

        public const double CIRCULARMIL2SQUAREMILLIMETER = 0.000506708;
        public const double SQUAREMILLIMETER2CIRCULARMIL = 1.0 / CIRCULARMIL2SQUAREMILLIMETER;

        public const double SQUAREINCH2SQUARECENTIMETER = 6.4516;
        public const double SQUARECENTIMETER2SQUAREINCH = 1.0 / SQUAREINCH2SQUARECENTIMETER;

        public const double SQUAREINCH2SQUAREMILLIMETER = 645.16;
        public const double SQUAREMILLIMETER2SQUAREINCH = 1.0 / SQUAREINCH2SQUAREMILLIMETER;

        public const double SQUAREFOOT2SQUAREMETER = 0.09290304;
        public const double SQUAREMETER2SQUAREFOOT = 1.0 / SQUAREFOOT2SQUAREMETER;

        public const double SQUAREYARD2SQUAREMETER = 0.83612736;
        public const double SQUAREMETER2SQUAREYARD = 1.0 / SQUAREYARD2SQUAREMETER;

        public const double SQUAREMILE2SQUAREKILOMETER = 2.589988;
        public const double SQUAREKILOMETER2SQUAREMILE = 1.0 / SQUAREMILE2SQUAREKILOMETER;

        public const double SQUAREINCH2SQUAREMILE = 2.49097669E-10;

        public const double SQUAREINCH2SQUAREFOOT = .0069444;
        public const double SQUAREFOOT2SQUAREINCH = 1.0 / SQUAREINCH2SQUAREFOOT;

        public const double ACRE2SQUAREFOOT = 43560;
        public const double SQUAREFOOT2ACRE = 1.0 / ACRE2SQUAREFOOT;

        public const double ACRE2SQUAREINCH = 6272640;
        public const double SQUAREINCH2ACRE = 1.0 / ACRE2SQUAREINCH;

        public const double SQUAREMILE2ACRE = 640;
        public const double ACRE2SQUAREMILE = 1.0 / SQUAREMILE2ACRE;

        public const double SQUAREMETER2HECTARE = 0.0001;
        public const double SQUAREMETER2SQUAREKILOMETER = 0.000001;
        public const double SQUAREMETER2SQUAREMILE = .000000386102159;



        #endregion Multipliers for area conversion

        #region Multipliers for volume conversion
        //Constants for volume conversion

        public const double ACREFOOT2CUBICMETER = 1233.489;
        public const double CUBICMETER2ACREFOOT = 1.0 / ACREFOOT2CUBICMETER;

        public const double BARREL2CUBICMETER = 0.1589873;
        public const double CUBICMETER2BARREL = 1.0 / BARREL2CUBICMETER;

        public const double BARREL2LITER = 158.9873;
        public const double LITER2BARREL = 1.0 / BARREL2LITER;

        public const double BARREL2GALLON = 42.0;
        public const double GALLON2BARREL = 1.0 / BARREL2GALLON;

        public const double CUBICYARD2CUBICMETER = 0.764555;
        public const double CUBICMETER2CUBICYARD = 1.0 / CUBICYARD2CUBICMETER;

        public const double CUBICFOOT2CUBICMETER = 0.02831685;
        public const double CUBICMETER2CUBICFOOT = 1.0 / CUBICFOOT2CUBICMETER;

        public const double CUBICFOOT2LITER = 28.31685;
        public const double LITER2CUBICFOOT = 1.0 / CUBICFOOT2LITER;

        public const double BOARDFOOT2CUBICMETER = 0.002359737;
        public const double CUBICMETER2BOARDFOOT = 1.0 / BOARDFOOT2CUBICMETER;

        public const double REGISTERTON2CUBICMETER = 2.831685;
        public const double CUBICMETER2REGISTERTON = 1.0 / REGISTERTON2CUBICMETER;

        public const double BUSHEL2CUBICMETER = 0.03523907;
        public const double CUBICMETER2BUSHEL = 1.0 / BUSHEL2CUBICMETER;

        public const double GALLON2LITER = 3.785412;
        public const double LITER2GALLON = 1.0 / GALLON2LITER;

        public const double QUART2LITER = 0.9463529;
        public const double LITER2QUART = 1.0 / QUART2LITER;

        public const double PINT2LITER = 0.4731765;
        public const double LITER2PINT = 1.0 / PINT2LITER;

        public const double FLUIDOUNCE2MILLILETER = 29.57353;
        public const double MILLILETER2FLUIDOUNCE = 1.0 / FLUIDOUNCE2MILLILETER;

        public const double CUBICINCH2CUBICCENTIMETER = 16.387064;
        public const double CUBICCENTIMETER2CUBICINCH = 1.0 / CUBICINCH2CUBICCENTIMETER;

        #endregion Multipliers for volume conversion

        #region Multipliers for velocity conversion
        //Constants for velocity conversions

        public const double FOOTPERSECOND2METERPERSECOND = 0.3048;
        public const double METERPERSECOND2FOOTPERSECOND = 1.0 / FOOTPERSECOND2METERPERSECOND;

        public const double MILEPERHOUR2KILOMETERPERHOUR = 1.609344;
        public const double KILOMETERPERHOUR2MILEPERHOUR = 1.0 / MILEPERHOUR2KILOMETERPERHOUR;

        public const double KNOT2KILOMETERPERHOUR = 1.852;
        public const double KILOMETERPERHOUR2KNOT = 1.0 / KNOT2KILOMETERPERHOUR;

        public const double KNOT2MILEPERHOUR = 1.507794;
        public const double MILEPERHOUR2KNOT = 1.0 / KNOT2MILEPERHOUR;

        #endregion Multipliers for velocity conversion

    }
}
