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
        #region Constants and Members
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
        /// How many seconds of time the Timeline will show by default
        /// </summary>
        private const double DEFAULT_TIME_WIDTH = 10;
        /// <summary>
        /// The number TimeWidth is multiplied/divided by when it is changed
        /// </summary>
        private const int ZOOM_MULTIPLIER = 2;
        /// <summary>
        /// How wide the label boxes are
        /// </summary>
        private const int LOCATION_LABEL_WIDTH = 95;
        /// <summary>
        /// Half the width of the playhead's triangle in pixels
        /// </summary>
        private const float PLAYHEAD_HALF_WIDTH = 10;

        private const float PLAYHEAD_BAR_HEIGHT = PLAYHEAD_HALF_WIDTH * 2;

        private Timestamp[] playheadBarTimes;

        /// <summary>
        /// Backing variable for rightBoundTime
        /// </summary>
        private double rbTime;
        /// <summary>
        /// The time value of the Timeline at the right end of the control
        /// </summary>
        private double RightBoundTime
        {
            set { rbTime = value; }
            get { return rbTime; }
        }

        /// <summary>
        /// Backing variable for LeftBoundTime
        /// </summary>
        private double lbTime;
        /// <summary>
        /// The time value of the Timeline at the left end of the control
        /// </summary>
        private double LeftBoundTime
        {
            set 
            {
                //Set the left bound, but don't set it to less than 0
                lbTime = Math.Max(0,value);
                cbTime = value + TimeWidth / 2;
                //Set the right bound
                rbTime = value + TimeWidth;
                
            }
            get { return lbTime; }
        }

        /// <summary>
        /// Backing variable for CenterBoundTime
        /// </summary>
        private double cbTime;
        /// <summary>
        /// The time value of the Timeline that is represented by the Scrollbar.Value
        /// property. It is in-between rightBoundTime and LeftBoundTime
        /// </summary>
        private double CenterBoundTime
        {
            set
            {
                cbTime = value;
                //Set the left bound, but don't set it to less than 0
                lbTime = Math.Max(0, cbTime - TimeWidth / 2);
                //Set the right bound, but don't set it to more than the videoLength
                rbTime = Math.Min(cbTime + TimeWidth / 2, VideoLength);
            }
            get
            {
                return cbTime;
            }
        }

        /// <summary>
        /// How many seconds of time the Timeline will show
        /// </summary>
        public double TimeWidth { set; get; }
       
        /// <summary>
        /// Backing variable for the VideoLength property. Use the VideoLength property to 
        /// set this variable
        /// </summary>
        private double vidLen;
        /// <summary>
        /// Represents the length of the flash video, in seconds. Also sets the CaptionDrawingWidth
        /// </summary>
        public double VideoLength
        {
            set 
            {
                vidLen = value;
            }
            get { return vidLen; }
        }
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
        #endregion

        #region Constructor
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

            //Set timewidth to default
            TimeWidth = DEFAULT_TIME_WIDTH;

            //Set scrollbar to the beginning
            ScrollBar.Value = 0;

            DrawLocationLabels = true;

            //Set leftboundTime
            LeftBoundTime = 0;

            //Create an array of 3 timestamps
            playheadBarTimes = new Timestamp[] 
            {
                new Timestamp(LeftBoundTime), 
                new Timestamp(CenterBoundTime), 
                new Timestamp(RightBoundTime)
            };
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

            //The amount of height in the component available to draw on
            float availableHeight = Height - PLAYHEAD_BAR_HEIGHT; 

            //Subtract the height of the scrollbar if it is visible
            if (ScrollBar.Visible)
                availableHeight -= SystemInformation.HorizontalScrollBarHeight;

            float availableWidth; //The amount of width in the component available for captions
            //Set value based on whether or not labels are visible
            if (DrawLocationLabels)
                availableWidth = (float)(Width - LOCATION_LABEL_WIDTH - 3);
            else
                availableWidth = Width - 2;

            //How many pixels are drawn for each second of time.
            float pixelsPerSecond = (float)(availableWidth / TimeWidth);

            //Draw black outline around control
            g.DrawRectangle(outlinePen, 0, 0, Width-1, availableHeight + PLAYHEAD_BAR_HEIGHT-1);
            #endregion

            g.TranslateTransform(0, PLAYHEAD_BAR_HEIGHT);

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

                    //Draw separator line
                    if(i == 0) //First line wil be solid, not dashed.
                        g.DrawLine(outlinePen, x + w, y, Width, y);
                    else
                        g.DrawLine(dashLinePen, x + w, y, Width, y);
                }
                else
                    //Draw line separator line
                    g.DrawLine(dashLinePen, x, y, Width, y);
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
                foreach (Caption c in CaptionList)
                {
                    if (((LeftBoundTime <= c.Begin && c.Begin <= RightBoundTime)//Begin is in drawing area
                    || (LeftBoundTime <= c.End && c.End <= RightBoundTime)      //End is in drawing area
                    || (c.Begin <= LeftBoundTime && RightBoundTime <= c.End) )  //Caption spans more than 
                                                                                //the whole drawing area
                    && 0.1 <= c.Duration)                           //Duration of caption is less than 0.1
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
                        x = (float)(c.Begin - LeftBoundTime) * pixelsPerSecond;
                        //w = (float)(c.Duration - LeftBoundTime) * pixelsPerSecond;
                        w = (float)(c.End - LeftBoundTime) * pixelsPerSecond - x;

                        //Create a small space between the line dividers and the caption rectangles
                        //y+= 2; h -=4; //Gives one extra pixel of whitespace on top and bottom
                        y += 1; h -= 2;

                        g.FillOutlinedRoundedRectangle(new SolidBrush(Color.Green), outlinePen, x, y, w, h);
                    }
                }
            }
            #endregion

            #region PlayheadBar Times
            foreach (Timestamp t in playheadBarTimes)
            {
                if(t.AsDouble < LeftBoundTime)
                    t.AsDouble += TimeWidth;    //Shift it 1 unit down the line
                if (RightBoundTime < t.AsDouble)
                    t.AsDouble -= TimeWidth;
                x = (float)(t - LeftBoundTime) * pixelsPerSecond;
                y = -PLAYHEAD_BAR_HEIGHT;
                g.DrawString(t.AsString, f, textBrush, x, y);
            }
            #endregion

            #region Draw Playhead
            Brush playHeadBrush = new SolidBrush(Color.Black);
            Pen playHeadPen = new Pen(playHeadBrush, 2);

            //Get playhead position
            x =(float)(PlayHeadTime - LeftBoundTime) * pixelsPerSecond;
            
            //Make triangle head
            GraphicsPath phPath = new GraphicsPath();
            phPath.AddLine(x - PLAYHEAD_HALF_WIDTH, 0, x, PLAYHEAD_HALF_WIDTH);
            phPath.AddLine(x, PLAYHEAD_HALF_WIDTH, x + PLAYHEAD_HALF_WIDTH, 0);
            phPath.AddLine(x + PLAYHEAD_HALF_WIDTH, 0, x - PLAYHEAD_HALF_WIDTH, 0);
            
            Region phRegion = new Region(phPath);
            phRegion.Translate(0, -PLAYHEAD_BAR_HEIGHT);
            //Draw
            g.FillRegion(playHeadBrush, phRegion);
            g.DrawLine(playHeadPen, x, -PLAYHEAD_BAR_HEIGHT, x, availableHeight);
            #endregion
        }
        #endregion

        #region Redraw Methods
        /// <summary>
        /// Invalidates the area where captions are drawn, leaving the rest alone.
        /// </summary>
        public void RedrawCaptionsRegion()
        {
            if (DrawLocationLabels)
                Invalidate(new Rectangle(LOCATION_LABEL_WIDTH+1,1,
                    Width-LOCATION_LABEL_WIDTH-2, Height-2));
            else
                RedrawInnerRegion();
        }

        /// <summary>
        /// Invalidates the area inside the outline, leaving the outline alone.
        /// </summary>
        public void RedrawInnerRegion()
        {
            Invalidate(new Rectangle(1, 1, Width - 2, Height - 2));
        }
        #endregion

        #region Mouse Events
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

        #region ScrollBar Events
        /// <summary>
        /// Occurs when the scroll box has been moved by either a mouse or 
        /// keyboard action.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //Get the percent progress of value
            double valuePercent = ((double)ScrollBar.Value) / ScrollBar.Maximum;
            LeftBoundTime = VideoLength * valuePercent;
            Console.WriteLine("LeftBoundTime: {0}", LeftBoundTime);

            //Redraw area with captions
            RedrawCaptionsRegion();
        }

        /// <summary>
        /// Occurs when the Value property is changed, either by a Scroll event or 
        /// programmatically.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Zoom methods
        /// <summary>
        /// Zooms the Timeline inwards, decreasing the Timewidth
        /// </summary>
        public void ZoomIn()
        {
            TimeWidth /= ZOOM_MULTIPLIER;
            LeftBoundTime = LeftBoundTime;
            RedrawCaptionsRegion();
        }

        /// <summary>
        /// Zooms the Timeline outwards, increasing the Timewidth
        /// </summary>
        public void ZoomOut()
        {
            TimeWidth *= ZOOM_MULTIPLIER;
            LeftBoundTime = LeftBoundTime;
            RedrawCaptionsRegion();
        }

        /// <summary>
        /// Resets the zoom level back to DEFAULT_TIME_WIDTH
        /// </summary>
        public void ZoomReset()
        {
            TimeWidth = DEFAULT_TIME_WIDTH;
            LeftBoundTime = LeftBoundTime;
            RedrawCaptionsRegion();
        }
        #endregion

        #region UpdateTimeLinePosition
        /// <summary>
        /// Updates the Timeline's position, setting the playhead and centering it in the
        /// middle of the Timeline. Will also update the scrollbar.
        /// </summary>
        /// <param name="currentTime">The position the playhead is to be set at</param>
        public void UpdateTimeLinePosition(double currentTime)
        {
            ScrollBar.Value = Math.Min((int)(ScrollBar.Maximum * (currentTime / VideoLength)),
                ScrollBar.Maximum);
            PlayHeadTime = currentTime;
            if (LeftBoundTime < currentTime - TimeWidth / 2 || currentTime + TimeWidth / 2 < RightBoundTime)
                LeftBoundTime = currentTime - TimeWidth / 2;
        }
        #endregion
    }//Class
}//Namespace
