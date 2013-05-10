using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnACT
{
    public partial class Timeline : UserControl
    {
        #region Constants, Members and Constructors
        public readonly string [] LocationNames = 
        {
            "Top Left",
            "Top Center",
            "Top Right",
            "Middle Left",
            "Middle Center",
            "Middle Right",
            "Bottom Left",
            "Bottom Center",
            "BottomRight"
        };

        private const int DEFAULT_PIXELS_PER_SECOND = 50;
        private const int ZOOM_MULTIPLIER = 5;
        private const int LOCATION_LABEL_WIDTH = 95;

        /// <summary>
        /// Represents the length of the flash video, in seconds
        /// </summary>
        public double VideoLength { set; get; }

        private double TimelineLength 
        {
            get { return VideoLength * 10; }
        }


        private int pixelsPerSecond;

        public Boolean DrawLocationNames { set; get; }

        public double LeftEndTime { set; get; }
        public double RightEndTime { set; get; }

        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<String, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// The Caption Table shown by CaptionView
        /// </summary>
        public CaptionData CData { set; get; }

        public Timeline()
        {
            InitializeComponent();

            //Make the component use a doublebuffer, which will reduce flicker made by 
            //redrawing the control
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            ResizeRedraw = true; //Redraw the component everytime the form gets resized

            pixelsPerSecond = DEFAULT_PIXELS_PER_SECOND;
            DrawLocationNames = true;
        }
        #endregion

        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Get graphics object
            Graphics g = e.Graphics;

            Pen p = new Pen(Color.Black, 1);
            Brush b = new SolidBrush(Color.Black);
            float x, y, w, h;  //Vars for xs,ys,witdths and heights of drawables

            //Draw black outline around control
            g.DrawRectangle(p, 0, 0, Width-1, Height-1);


            if (DrawLocationNames)
                //Set drawing origin to the point where Location labels end.
                //Anything drawn after this will have a location relative to
                //(LOCATION_LABEL_WIDTH, 0)
                g.TranslateTransform(LOCATION_LABEL_WIDTH, 0);

            //Draw CaptionPosition Labels
            Font f = new Font(this.Font.FontFamily, 10); //CaptionPositions font
            Pen linePen = new Pen(Color.Black); //Pen for drawing dotted lines

            linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            linePen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            x = -LOCATION_LABEL_WIDTH;//0
            h = Height / LocationNames.Length;
            w = LOCATION_LABEL_WIDTH;
            for(float i =0; i<LocationNames.Length; i++)
            {
                y = i*h;
                //Only draw alignment names if true;
                if (DrawLocationNames)
                {
                    RectangleF r = new RectangleF(x, y, w, h);
                    g.DrawString(LocationNames[(int)i], f, b, r);
                    g.DrawRectangle(p, r.X, r.Y, r.Width, r.Height);
                }

                //Draw line separator line
                g.DrawLine(linePen,x+w,y,Width,y);
            }

            //Draw captions on screen if they exist
            if (CData != null)
            {
                LeftEndTime = 0;
                RightEndTime = VideoLength;

                //float x, y, w, h;  //Vars for xs,ys,witdths and heights o
                foreach (DataRow r in CData.Rows)
                {
                    Caption c = (Caption) r[CaptionData.CPOS];
                    if ((LeftEndTime <= c.Begin && c.Begin <= RightEndTime)
                    || (LeftEndTime <= c.End && c.End <= RightEndTime))
                    {
                        Console.WriteLine("Caption: #{0} is within bounds", r[CaptionData.NPOS]);
                        y = 0;
                        h = Height / LocationNames.Length;
                        switch (c.Location)
                        {
                            case ScreenLocation.TopLeft: y = 0 * h; break;
                            case ScreenLocation.TopCentre: y = 1 * h; break;
                            case ScreenLocation.TopRight: y = 2 * h; break;
                            case ScreenLocation.MiddleLeft: y = 3 * h; break;
                            case ScreenLocation.MiddleCenter: y = 4 * h; break;
                            case ScreenLocation.MiddleRight: y = 5 * h; break;
                            case ScreenLocation.BottomLeft: y = 6 * h; break;
                            case ScreenLocation.BottomCentre: y = 7 * h; break;
                            case ScreenLocation.BottomRight: y = 8 * h; break;
                            default: y = 0; break;
                        }
                        x = (float)c.Begin*pixelsPerSecond;
                        w = (float)c.End*pixelsPerSecond - x;

                        y+= 2; h -=3;

                        g.FillRectangle(new SolidBrush(Color.Green), x, y, w, h);
                    }
                }
            }
            //Console.WriteLine("Clip Rect X: {0}, Y: {1}, W: {2}, H: {3}", e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height);
        }
        #endregion
    }//Class
}//Namespace
