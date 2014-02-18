using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Polyvert
{
    public class VectorViewport:IDisposable
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private PictureBox Parent;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Bitmap Buffer;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Graphics G;

        /// <summary>
        /// The west edge of the picture box. Longitude.
        /// </summary>
        public double West
        {
            get
            {
                return mWest;
            }
            set
            {
                if (value > East) throw new Exception("West can't be greater than east");
                if (value > 180 || value < -180) throw new ArgumentOutOfRangeException("West");
                mWest = value;
            }
        }
        /// <summary>
        /// The East edge of the picture box. Longitude.
        /// </summary>
        public double East
        {
            get
            {
                return mEast;
            }
            set
            {
                if (value < West) throw new Exception("East can't be less than west");
                if (value > 180 || value < -180) throw new ArgumentOutOfRangeException("East");
                mEast = value;
            }
        }
        /// <summary>
        /// The north edge of the picture box. Latitude.
        /// </summary>
        public double North
        {
            get
            {
                return mNorth;
            }
            set
            {
                if (value < South) throw new Exception("North can't be less than South");
                if (value > 90 || value < -90) throw new ArgumentOutOfRangeException("North");
                mNorth = value;
            }
        }
        /// <summary>
        /// The south edge of the picture box. Latitude.
        /// </summary>
        public double South
        {
            get
            {
                return mSouth;
            }
            set
            {
                if (value > North) throw new Exception("South can't be greater than North");
                if (value > 90 || value < -90) throw new ArgumentOutOfRangeException("South");
                mSouth = value;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private double mWest = -180;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private double mEast = 180;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private double mNorth = 90;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private double mSouth = -90;


        /// <summary>
        /// Create a vector viewport by binding it to a picture box that has already been instantiated
        /// </summary>
        /// <param name="DrawIn"></param>
        public VectorViewport(PictureBox DrawIn)
        {
            this.Parent = DrawIn;
            Initialize();
        }
        /// <summary>
        /// Run Initialize the buffer and graphics object
        /// </summary>
        private void Initialize()
        {
            Buffer = new Bitmap(Parent.Width, Parent.Height);
            Parent.Image = Buffer;
            G = Graphics.FromImage(Buffer);
            G.FillRegion(Brushes.White, new Region(new Rectangle(new Point(0, 0), Buffer.Size)));
        }
        /// <summary>
        /// Draw a line between two latitude and longitude pairs
        /// </summary>
        /// <param name="StartLatitude"></param>
        /// <param name="StartLongitude"></param>
        /// <param name="EndLatitude"></param>
        /// <param name="EndLongitude"></param>
        /// <param name="ForceRefresh">Force the picture box to refresh. For faster drawing, draw all the lines, then call Refresh() on the picturebox being drawn in</param>
        public void DrawLine(double StartLatitude, double StartLongitude, double EndLatitude, double EndLongitude, bool ForceRefresh = true)
        {
            Point Start = World_to_Canvas(StartLatitude, StartLongitude);
            Point End = World_to_Canvas(EndLatitude, EndLongitude);
            G.DrawLine(new Pen(Color.Black), Start, End);
            Parent.Refresh();
        }
        /// <summary>
        /// Convert a world LatLon to a canvas XY
        /// </summary>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
        /// <returns></returns>
        private Point World_to_Canvas(double Latitude, double Longitude)
        {
            Point Ans = new Point();
            Ans.Y = Parent.Height - (int)(Parent.Height * (Latitude - South) / (North - South));
            Ans.X = (int)(Parent.Width * (Longitude - West) / (East - West));
            return Ans;
        }
        /// <summary>
        /// Clear the entire canvas
        /// </summary>
        public void Clear()
        {
            G.FillRegion(Brushes.White, new Region(new Rectangle(new Point(0, 0), Buffer.Size)));
        }
        /// <summary>
        /// Dispose of the object. Buffer cleared and grpahics object released
        /// </summary>
        public void Dispose()
        {
            G.Dispose();
            Buffer.Dispose();
        }
    }
}
