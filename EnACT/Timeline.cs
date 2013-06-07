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
        #region Constants
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
        /// An enum representing what action is being performed by the mouse.
        /// </summary>
        private enum MouseMoveAction
        {
            movePlayhead,
            moveCaption,
            changeCaptionTime,
            noAction
        };

        /// <summary>
        /// An enum representing what caption timestamp to change when adjusting the time of a caption
        /// </summary>
        private enum TimestamptoChange
        {
            begin,
            end,
            none
        };

        /// <summary>
        /// How many seconds of time the Timeline will show by default
        /// </summary>
        private const double DEFAULT_TIME_WIDTH = 10;
        /// <summary>
        /// How many seconds of time are shown after the video end
        /// </summary>
        private const double ENDTIMEBUFFER = 5;
        /// <summary>
        /// The number TimeWidth is multiplied/divided by when it is changed
        /// </summary>
        private const int ZOOM_MULTIPLIER = 2;
        /// <summary>
        /// The Default zoom level
        /// </summary>
        private const int DEFAULT_ZOOM_LEVEL = 10;
        /// <summary>
        /// The highest level of zoom (zooming in) the timeline will allow
        /// </summary>
        private const int MAX_ZOOM_LEVEL = 13;
        /// <summary>
        /// The lowest level of zoom the Timeline will allow
        /// </summary>
        private const int MIN_ZOOM_LEVEL = 0;

        /// <summary>
        /// How wide the label boxes are
        /// </summary>
        private const int LOCATION_LABEL_WIDTH = 95;
        /// <summary>
        /// Half the width of the playhead's triangle in pixels
        /// </summary>
        private const int PLAYHEAD_HALF_WIDTH = 10;

        /// <summary>
        /// The draw height in pixels of the playhead bar
        /// </summary>
        private const int PLAYHEAD_BAR_HEIGHT = PLAYHEAD_HALF_WIDTH * 2;

        private const int CAPTION_SELECTION_WIDTH = 3;
        #endregion

        #region Private fields
        /// <summary>
        /// An array of Timestamps used to keep track of position in the Timeline
        /// </summary>
        private Timestamp[] playheadBarTimes;

        /// <summary>
        /// The level of Zoom the Timeline is at.
        /// </summary>
        private int zoomLevel;

        /// <summary>
        /// The amount of pixels drawn per second of caption time
        /// </summary>
        private float pixelsPerSecond;

        /// <summary>
        /// The width of the component that is available for drawing captions
        /// </summary>
        private float availableWidth;

        /// <summary>
        /// The current action being performed by the mouse.
        /// </summary>
        private MouseMoveAction mouseMoveAction;

        /// <summary>
        /// The caption selected to be modified by mouse movements
        /// </summary>
        private Caption selectedCaption;

        private double selectedCaptionDifference;

        private TimestamptoChange timestampToChange;

        /// <summary>
        /// Represents TimeWidth divided by 2
        /// </summary>
        private double halfTimeWidth;
        #endregion

        #region Private Properties
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
                cbTime = value + halfTimeWidth;
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
                lbTime = Math.Max(0, cbTime - halfTimeWidth);
                //Set the right bound, but don't set it to more than the videoLength
                rbTime = Math.Min(cbTime + halfTimeWidth, VideoLength);
            }
            get
            {
                return cbTime;
            }
        }

        /// <summary>
        /// Gets the x origin value based on whether or not labels are drawn
        /// </summary>
        private int XCaptionOrigin
        {
            get
            {
                if (DrawLocationLabels)
                    return LOCATION_LABEL_WIDTH;
                else
                    return 0;
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Backing field for TimeWidth property
        /// </summary>
        private double tw;
        /// <summary>
        /// How many seconds of time the Timeline will show
        /// </summary>
        public double TimeWidth 
        {
            set
            {
                tw = value;
                halfTimeWidth = tw / 2;
            }
            get { return tw; }
        }
       
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

        #region Events
        /// <summary>
        /// An event that is called when the user changes the Playhead on the Timeline
        /// </summary>
        public event EventHandler<TimelinePlayheadChangedEventArgs> PlayheadChanged;

        /// <summary>
        /// An event that is called when the user changes a Caption Timestamp, such as
        /// Caption.End or Caption.Duration
        /// </summary>
        public event EventHandler<TimelineCaptionTimestampChangedEventArgs> CaptionTimestampChanged;

        /// <summary>
        /// An event that is raised when the user selects and moves a caption on the timeline
        /// with the mouse
        /// </summary>
        public event EventHandler CaptionMoved;
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

            //Create an array of 5 timestamps
            playheadBarTimes = new Timestamp[5];
            //Set the timestamps
            SetPlayHeadBarTimes();

            //Set the zoom level
            zoomLevel = DEFAULT_ZOOM_LEVEL;

            //Set default action
            mouseMoveAction = MouseMoveAction.noAction;

            RedrawInnerRegion();
            SetScrollBarValues();
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

            //Set value based on whether or not labels are visible
            if (DrawLocationLabels)
                availableWidth = (float)(Width - LOCATION_LABEL_WIDTH - 3);
            else
                availableWidth = Width - 2;

            //How many pixels are drawn for each second of time.
            pixelsPerSecond = (float)(availableWidth / TimeWidth);

            //Draw black outline around control
            g.DrawRectangle(outlinePen, 0, 0, Width-1, availableHeight + PLAYHEAD_BAR_HEIGHT-1);

            g.TranslateTransform(0, PLAYHEAD_BAR_HEIGHT);
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
                else
                    w = 0;

                //Draw separator line
                if (i == 0) //First line wil be solid, not dashed.
                    g.DrawLine(outlinePen, x + w, y, Width, y);
                else
                    g.DrawLine(dashLinePen, x + w, y, Width, y);
            }
            #endregion

            #region Move drawing origin
            if (DrawLocationLabels)
                //Set drawing origin to the point where Location labels end.
                //Anything drawn after this will have a location relative to
                //(LOCATION_LABEL_WIDTH + 1, 0)
                g.TranslateTransform(LOCATION_LABEL_WIDTH + 1, 0);
            #endregion

            #region Draw End Marker
            if (LeftBoundTime <= VideoLength && VideoLength <= RightBoundTime)
            {
                x = (float)(VideoLength - LeftBoundTime) * pixelsPerSecond;
                g.DrawLine(new Pen(Color.Red, 2), x, -PLAYHEAD_BAR_HEIGHT, x, availableHeight);
            }
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
            //Set the times
            SetPlayHeadBarTimes();

            foreach (Timestamp t in playheadBarTimes)
            {
                //Skip the timestamp if null
                if (t == null)
                    continue;

                x = (float)(t - LeftBoundTime) * pixelsPerSecond;
                y = -PLAYHEAD_BAR_HEIGHT;
                w = 70;

                //Make a rectangle centered at x
                RectangleF timeRect = new RectangleF(x-w/2, y, w, PLAYHEAD_BAR_HEIGHT);
                //Draw string in centered rectangle
                g.DrawString(t.AsString, f, textBrush, timeRect);

                //Draw a little line marking the actual position of the timestamp
                g.DrawLine(outlinePen, x, -PLAYHEAD_HALF_WIDTH/2, x, 0);
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
            {
                Region r = new Region(new Rectangle(LOCATION_LABEL_WIDTH + 1, 1,
                    Width - LOCATION_LABEL_WIDTH - 2, Height - 2));
                r.Union(new Rectangle(1, 1, Width - 2, PLAYHEAD_BAR_HEIGHT));
                Invalidate(r);
            }
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

        #region OnMouseDown
        /// <summary>
        /// Raises the MouseDown event.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
                return;

            double mouseClickTime = XCoordinateToTime(e.X);

            RectangleF playheadBarRect = new RectangleF(XCaptionOrigin, 0, 
                Width - LOCATION_LABEL_WIDTH, PLAYHEAD_BAR_HEIGHT);

            if (playheadBarRect.Contains(e.Location))
            {
                //Set Action
                mouseMoveAction = MouseMoveAction.movePlayhead;

                //Set playhead time based on click location
                PlayHeadTime = mouseClickTime;
                RedrawCaptionsRegion(); //redraw the playhead
                //Invoke PlayheadChanged event
                OnPlayheadChanged(new TimelinePlayheadChangedEventArgs(PlayHeadTime));
            }
            else
            {
                double beginX;  //X-Coord of c.Begin
                double endX;    //X-Coord of c.End
                foreach (Caption c in CaptionList)
                {
                    beginX = TimeToXCoordinate(c.Begin);

                    //If selecting the beginning of a caption
                    if (e.X - CAPTION_SELECTION_WIDTH <= beginX && beginX <= e.X + CAPTION_SELECTION_WIDTH)
                    {
                        selectedCaption = c;
                        mouseMoveAction = MouseMoveAction.changeCaptionTime;
                        timestampToChange = TimestamptoChange.begin;
                        Console.WriteLine("C.Begin");
                        break;
                    }

                    endX = TimeToXCoordinate(c.End);

                    //If selecting the end of the Caption
                    if (e.X - CAPTION_SELECTION_WIDTH <= endX && endX <= e.X + CAPTION_SELECTION_WIDTH)
                    {
                        selectedCaption = c;
                        mouseMoveAction = MouseMoveAction.changeCaptionTime;
                        timestampToChange = TimestamptoChange.end;
                        Console.WriteLine("C.End");
                        break;
                    }

                    //If selecting the center of the caption
                    if (beginX <= e.X && e.X <= endX)
                    {
                        selectedCaption = c;
                        selectedCaptionDifference = mouseClickTime - c.Begin;
                        mouseMoveAction = MouseMoveAction.moveCaption;
                        Console.WriteLine("MoveCaption");
                        break;
                    }
                }//foreach
            }//else
            //Console.WriteLine("Mouse Down!"); 
        }//OnMouseDown
        #endregion

        #region OnMouseUp
        /// <summary>
        /// Raises the MouseUp event.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Left)
                return;

            //Clear selected caption
            selectedCaption = null;

            //Reset the action
            mouseMoveAction = MouseMoveAction.noAction;
            timestampToChange = TimestamptoChange.none;
            //Console.WriteLine("Mouse Up!"); 
        }
        #endregion

        #region OnMouseMove
        /// <summary>
        /// Raises the MouseMove event.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button != MouseButtons.Left)
                return;

            double mouseClickTime = XCoordinateToTime(e.X);

            if (mouseMoveAction == MouseMoveAction.movePlayhead)
            {
                //Set playhead time based on click location
                PlayHeadTime = mouseClickTime;
                RedrawCaptionsRegion(); //redraw the playhead
                //Invoke PlayheadChanged event
                OnPlayheadChanged(new TimelinePlayheadChangedEventArgs(PlayHeadTime));
            }
            else if (mouseMoveAction == MouseMoveAction.changeCaptionTime)
            {
                if (timestampToChange == TimestamptoChange.begin)
                {
                    selectedCaption.Begin = mouseClickTime;
                    OnCaptionTimestampChanged(new TimelineCaptionTimestampChangedEventArgs());
                }
                else if (timestampToChange == TimestamptoChange.end)
                {
                    selectedCaption.End = mouseClickTime;
                    OnCaptionTimestampChanged(new TimelineCaptionTimestampChangedEventArgs());
                }
                RedrawCaptionsRegion();
            }
            else if (mouseMoveAction == MouseMoveAction.moveCaption)
            {
                Console.WriteLine("Something!");
                 //(double)(xPos / pixelsPerSecond + LeftBoundTime);
                double oldDuration = selectedCaption.Duration;

                selectedCaption.Begin = mouseClickTime - selectedCaptionDifference;
                selectedCaption.Duration = oldDuration;

                OnCaptionMoved(EventArgs.Empty);

                RedrawCaptionsRegion();
            }
            //Console.WriteLine("Mouse Moved!"); 
        }
        #endregion

        #region OnMouseClick
        /// <summary>
        /// Raises the MouseClick event. If the click is inside the playhead bar, it will
        /// move the playhead to that location
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            int xPos = e.X;

            //Subtract the width of Location Labels if they're being shown
            if (DrawLocationLabels)
                xPos -= LOCATION_LABEL_WIDTH;

            RectangleF playheadBarRect = new RectangleF(xPos, 0,
                Width - LOCATION_LABEL_WIDTH, PLAYHEAD_BAR_HEIGHT);

            if (playheadBarRect.Contains(e.Location))
            {
                //Set playhead time based on click location
                PlayHeadTime = (double)(xPos / pixelsPerSecond + LeftBoundTime);
                //Invoke PlayheadChanged event
                OnPlayheadChanged(new TimelinePlayheadChangedEventArgs(PlayHeadTime));
                RedrawCaptionsRegion(); //redraw the playhead
            }
        }
        #endregion

        #region OnMouseWheel Event
        /// <summary>
        /// Raises the MouseWheel event.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (ScrollBar.Visible)
            {
                //Console.WriteLine("Mousewheel Delta: {0}", e.Delta);
                if (ScrollBar.Minimum > ScrollBar.Value - e.Delta)
                    ScrollBar.Value = 0;
                else if (ScrollBar.Value - e.Delta > ScrollBar.Maximum - ScrollBar.LargeChange)
                    ScrollBar.Value = ScrollBar.Maximum - ScrollBar.LargeChange;
                else
                    ScrollBar.Value -= e.Delta;
                ScrollBar_Scroll(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, ScrollBar.Value));
            }
        }
        #endregion

        #region Mouse Focus Events
        /// <summary>
        /// Raises the MouseEnter event. Gives focus to the control.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Focus();
        }

        /// <summary>
        /// Raises the MouseLeave event.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            InvokeLostFocus(this, new EventArgs());
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
            //If the value is 0, snap the leftBoundTime to 0 to prevent rounding errors.
            if (e.NewValue == 0)
                LeftBoundTime = 0;
            else
            {
                //Get the percent progress of value
                double valuePercent = ((double)ScrollBar.Value) / ScrollBar.Maximum;
                LeftBoundTime = VideoLength * valuePercent;
            }
            
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
            //Console.WriteLine("LeftBoundTime: {0}, Scrollbar Value: {1}", LeftBoundTime, ScrollBar.Value);
        }

        /// <summary>
        /// Adjusts the size of the scrollbar based on the length of the video and the TimeWidth
        /// </summary>
        public void SetScrollBarValues()
        {
            ScrollBar.Minimum = 0; //Min size should always be 0
            ScrollBar.SmallChange = (int)(availableWidth / 10);
            ScrollBar.LargeChange = (int)availableWidth;

            /* The ScrollBar.Maximum values is defined as the total amount of pixels to
             * draw out the entire video at the current timewidth plus 1 large change value.
             * This is due to a bug with the windows scrollbar.
             */
            ScrollBar.Maximum = (int)((VideoLength+ENDTIMEBUFFER) * pixelsPerSecond + ScrollBar.LargeChange);

            //Set scroll value to the value of LeftBoundTime
            if(VideoLength != 0)
                ScrollBar.Value = Math.Min((int)(ScrollBar.Maximum * (LeftBoundTime / VideoLength)),
                    ScrollBar.Maximum);

            //If the timewidth is greater than the video length, hide the scrollbar.
            if (VideoLength < TimeWidth)
            {
                ScrollBar.Visible = false;
                ScrollBar.Value = 0;
            }
            else
                ScrollBar.Visible = true;
        }
        #endregion

        #region Zoom methods
        /// <summary>
        /// Zooms the Timeline inwards, decreasing the Timewidth
        /// </summary>
        public void ZoomIn()
        {
            if(zoomLevel < MAX_ZOOM_LEVEL)
            {
                TimeWidth /= ZOOM_MULTIPLIER;
                LeftBoundTime = LeftBoundTime;
                SetPlayHeadBarTimes();
                RedrawCaptionsRegion();
                zoomLevel++; //Increase zoom level
                SetScrollBarValues();
            }
        }

        /// <summary>
        /// Zooms the Timeline outwards, increasing the Timewidth
        /// </summary>
        public void ZoomOut()
        {
            if(MIN_ZOOM_LEVEL < zoomLevel)
            {
                TimeWidth *= ZOOM_MULTIPLIER;
                LeftBoundTime = LeftBoundTime;
                SetPlayHeadBarTimes();
                RedrawCaptionsRegion();
                zoomLevel--; //Decrease zoom level
                SetScrollBarValues();
            }
        }

        /// <summary>
        /// Resets the zoom level back to DEFAULT_TIME_WIDTH
        /// </summary>
        public void ZoomReset()
        {
            zoomLevel = DEFAULT_ZOOM_LEVEL;
            TimeWidth = DEFAULT_TIME_WIDTH;
            LeftBoundTime = LeftBoundTime;
            SetPlayHeadBarTimes();
            RedrawCaptionsRegion();
            SetScrollBarValues();
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
            PlayHeadTime = currentTime;
            //If LeftBoundTime or RightBoundTime are not half of a Timewidth away from the current time and
            //the scrollbar hasn't reached its maximum scrollable position yet
            if ((LeftBoundTime < currentTime - halfTimeWidth || currentTime + halfTimeWidth < RightBoundTime)
                && ScrollBar.Value < ScrollBar.Maximum - ScrollBar.LargeChange )
                LeftBoundTime = currentTime - halfTimeWidth;
            //Set scroll value to the value of LeftBoundTime
            ScrollBar.Value = Math.Min((int)(ScrollBar.Maximum * (LeftBoundTime / VideoLength)),
                ScrollBar.Maximum);
        }
        #endregion

        #region Playhead Bar
        /// <summary>
        /// Sets the playHeadBarTimes so that they will be in multiples of halfTimeWidth
        /// </summary>
        public void SetPlayHeadBarTimes()
        {
            int multiplier = (int) Math.Floor(CenterBoundTime / TimeWidth);

            //Set the times in intervals of halfTimeWidth
            playheadBarTimes[0] = Math.Max(0,TimeWidth * multiplier - TimeWidth);
            playheadBarTimes[1] = Math.Max(0,TimeWidth * multiplier - halfTimeWidth);
            playheadBarTimes[2] = Math.Max(0,TimeWidth * multiplier);
            playheadBarTimes[3] = Math.Max(0,TimeWidth * multiplier + halfTimeWidth);
            playheadBarTimes[4] = Math.Max(0,TimeWidth * multiplier + TimeWidth);

            /* If the first two times are the same value (ie, both are 0), then set the first value to
             * null, as setting both to 0 will cause the timeline to draw both times, making the
             * 00:00:00.0 timestamp look thicker than the others. This will typically happen when the 
             * CenterBoundTime is less than 1 Timewidth unit.
             */
            if (playheadBarTimes[0].Equals(playheadBarTimes[1]))
                playheadBarTimes[0] = null;
            //Do the same for the second Time
            if (playheadBarTimes[1].Equals(playheadBarTimes[2]))
                playheadBarTimes[1] = null;
        }
        #endregion

        #region ToggleDrawLocationLabels
        public void ToggleDrawLocationLabels()
        {
            if (DrawLocationLabels)
            {
                DrawLocationLabels = false;
                RedrawCaptionsRegion();
            }
            else
            {
                DrawLocationLabels = true;
                //Redraw entire region, including labels
                RedrawInnerRegion();
            }
            SetScrollBarValues();
        }
        #endregion

        #region Event Invocation Methods
        /// <summary>
        /// Invokes the PlayheadChanged event, which should happen everytime the playhead
        /// is changed by the user.
        /// </summary>
        /// <param name="e">Event Args</param>
        private void OnPlayheadChanged(TimelinePlayheadChangedEventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler<TimelinePlayheadChangedEventArgs> handler = PlayheadChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Invokes the CaptionTimestampChanged event, which happens when any of Caption.Begin,
        /// Caption.End, or Caption.Duration are changed from the Timeline.
        /// </summary>
        /// <param name="e">Event Arg</param>
        private void OnCaptionTimestampChanged(TimelineCaptionTimestampChangedEventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler<TimelineCaptionTimestampChangedEventArgs> handler = CaptionTimestampChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Invokes the CaptionMoved event, which happens when a Caption is moved by the mouse
        /// in the Timeline
        /// </summary>
        /// <param name="e">Event Arg</param>
        private void OnCaptionMoved(EventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = CaptionMoved;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region Time and Ordinate Conversion
        /// <summary>
        /// Converts X Coordinate to a time based on Leftboundtime and whether Location
        /// Labels are being drawn.
        /// </summary>
        /// <param name="x">X Coordinate to convert</param>
        /// <returns>Time represented by the X Coordinate</returns>
        private double XCoordinateToTime(int x)
        {
            return (double)((x - XCaptionOrigin) / pixelsPerSecond + LeftBoundTime);
        }

        /// <summary>
        /// Converts a time into an X Coordinate on the timeline based on leftbound time and
        /// whether Location Labels are being drawn
        /// </summary>
        /// <param name="time">Time to convert</param>
        /// <returns>An X Coordinate represented by the time</returns>
        private float TimeToXCoordinate(double time)
        {
            return (float)((time - LeftBoundTime) * pixelsPerSecond + XCaptionOrigin);
        }
        #endregion
    }//Class
}//Namespace
