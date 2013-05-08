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
        public readonly string [] CaptionPositions = 
        {
            "Top Left",
            "Top Middle",
            "Top Right",
            "Center Left",
            "Center Middle",
            "Center Right",
            "Bottom Left",
            "Bottom Middle",
            "BottomRight"
        };

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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Get graphics object
            Graphics g = e.Graphics;

            Pen p = new Pen(Color.Black, 1);
            Brush b = new SolidBrush(Color.Black);

            //Draw black outline around control
            g.DrawRectangle(p, 0, 0, Width-1, Height-1);

            //Draw CaptionPosition Labels
            Font f = new Font(this.Font.FontFamily, 10); //CaptionPositions font
            Pen linePen = new Pen(Color.Black); //Pen for drawing dotted lines

            linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            linePen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
            float x, y, w, h;  //Vars for xs,ys,witdths and heights o
            for(float i =0; i<CaptionPositions.Length; i++)
            {
                x = 0;
                h = Height/CaptionPositions.Length;
                y = i*h;
                w = 95;
                RectangleF r = new RectangleF(x,y,w,h);
                g.DrawString(CaptionPositions[(int)i], f, b, r);
                g.DrawRectangle(p,r.X,r.Y,r.Width,r.Height);

                
                g.DrawLine(linePen,x+w,y,Width,y);

            }
            Console.WriteLine("Clip Rect X: {0}, Y: {1}, W: {2}, H: {3}", e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height);
        }
    }
}
