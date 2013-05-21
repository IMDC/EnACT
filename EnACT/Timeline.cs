using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace EnACT
{
    public partial class Timeline : UserControl
    {
        #region Constants, Members and Constructors
        /// <summary>
        /// A readonly string array of Labels for Marking locations for captions.
        /// </summary>
        public readonly string [] LocationLabels = 
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

        /// <summary>
        /// How many pixels in width are drawn for a second of time by default.
        /// </summary>
        private const int DEFAULT_PIXELS_PER_SECOND = 50;
        /// <summary>
        /// How much the pixels per second is changed by with each increase or decrease
        /// </summary>
        private const int ZOOM_MULTIPLIER = 5;
        /// <summary>
        /// How wide the label boxes are
        /// </summary>
        private const int LOCATION_LABEL_WIDTH = 95;
        /// <summary>
        /// Half the width of the playhead's triangle
        /// </summary>
        private const float PLAYHEAD_HALF_WIDTH = 10;

        /// <summary>
        /// The time value of the Timeline at the right end of the control
        /// </summary>
        private double rightBoundTime;
        /// <summary>
        /// The time value of the Timeline at the left end of the control
        /// </summary>
        private double leftBoundTime;
        /// <summary>
        /// How many pixels in width are drawn for a second of time.
        /// </summary>
        private int pixelsPerSecond;

        /// <summary>
        /// Represents the length of the flash video, in seconds
        /// </summary>
        public double VideoLength { set; get; }
        /// <summary>
        /// The position of the Video's playhead in seconds.
        /// </summary>
        public double PlayHeadTime { set; get; }
        /// <summary>
        /// Boolean that represents whether to draw location labels or not
        /// </summary>
        public Boolean DrawLocationLabels { set; get; }

        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<String, Speaker> SpeakerSet { set; get; }
        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<Caption> CaptionList { set; get; }

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
            DrawLocationLabels = true;
        }
        #endregion

        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            #region Var declaration and Control outline
            base.OnPaint(e); //Call parent method
            //Get graphics object
            Graphics g = e.Graphics;

            //Vars used throughout method
            float x, y, w, h;  //Vars for xs,ys,witdths and heights of drawables
            Pen outlinePen = new Pen(Color.Black, 1); //Black outline with width of 1 pixel
            Brush textBrush = new SolidBrush(Color.Black);  //Black brush

            float availableHeight; //The amount of height in the component available to draw on
            //Set value based one whether or not the scrollbar is visible
            if (HorizontalScroll.Visible)
            {
                availableHeight = Height - SystemInformation.HorizontalScrollBarHeight;
            }
            else
            {
                availableHeight = Height;
            }

            //Draw black outline around control
            g.DrawRectangle(outlinePen, 0, 0, Width-1, availableHeight-1);
            #endregion

            #region Draw labels and dash lines
            //Draw CaptionPosition Labels
            Font f = new Font(this.Font.FontFamily, 10); //CaptionPositions font

            Pen dashLinePen = new Pen(Color.Black); //Pen for drawing dotted lines
            dashLinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            dashLinePen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            x = 0;
            h = availableHeight / LocationLabels.Length;
            w = LOCATION_LABEL_WIDTH;
            for(float i =0; i<LocationLabels.Length; i++)
            {
                y = i*h;
                //Only draw alignment names if true;
                if (DrawLocationLabels)
                {
                    RectangleF r = new RectangleF(x, y, w, h);
                    g.DrawString(LocationLabels[(int)i], f, textBrush, r);
                    g.DrawRectangle(outlinePen, r.X, r.Y, r.Width, r.Height);
                }

                //Draw line separator line
                g.DrawLine(dashLinePen,x+w,y,Width,y);
            }
            #endregion

            #region Move drawing origin
            if (DrawLocationLabels)
                //Set drawing origin to the point where Location labels end.
                //Anything drawn after this will have a location relative to
                //(LOCATION_LABEL_WIDTH + 1, 0)
                g.TranslateTransform(LOCATION_LABEL_WIDTH + 1, 0);
            #endregion

            #region Draw Captions
            if (CaptionList != null)
            {
                leftBoundTime = 0;
                rightBoundTime = VideoLength;

                //float x, y, w, h;  //Vars for xs,ys,witdths and heights o
                foreach (Caption c in CaptionList)
                {
                    if (((leftBoundTime <= c.Begin && c.Begin <= rightBoundTime) //Begin is in drawing area
                    || (leftBoundTime <= c.End && c.End <= rightBoundTime))      //End is in drawing area
                    && 0.1 <= c.Duration)     //Duration of caption is less than 0.1
                    {
                        //Console.WriteLine("Caption: #{0} is within bounds", r[CaptionData.NPOS]);
                        y = 0;
                        h = availableHeight / LocationLabels.Length;
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

                        //Create a small space between the line dividers and the caption rectangles
                        //y+= 2; h -=4; //Gives one extra pixel of whitespace on top and bottom
                        y += 1; h -= 2;

                        //g.FillRectangle(new SolidBrush(Color.Green), x, y, w, h);
                        g.FillOutlinedRoundedRectangle(new SolidBrush(Color.Green), outlinePen, x, y, w, h);
                    }
                }
            }
            #endregion

            #region Draw Playhead
            Brush playHeadBrush = new SolidBrush(Color.Black);
            Pen playHeadPen = new Pen(playHeadBrush, 2);

            //Get playhead position
            x = (float)PlayHeadTime * pixelsPerSecond;
            
            //Make triangle head
            GraphicsPath phPath = new GraphicsPath();
            phPath.AddLine(x - PLAYHEAD_HALF_WIDTH, 0, x, PLAYHEAD_HALF_WIDTH);
            phPath.AddLine(x, PLAYHEAD_HALF_WIDTH, x + PLAYHEAD_HALF_WIDTH, 0);
            phPath.AddLine(x + PLAYHEAD_HALF_WIDTH, 0, x - PLAYHEAD_HALF_WIDTH, 0);
            Region phRegion = new Region(phPath);

            //Draw
            g.FillRegion(playHeadBrush, phRegion);
            g.DrawLine(playHeadPen, x, 0, x, availableHeight);
            #endregion
        }
        #endregion

        #region Events
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
                return;

            Console.WriteLine("Mouse Down!"); 
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Left)
                return;
            Console.WriteLine("Mouse Up!"); 
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button != MouseButtons.Left)
                return;
            Console.WriteLine("Mouse Moved!"); 
        }
        #endregion
    }//Class
}//Namespace
