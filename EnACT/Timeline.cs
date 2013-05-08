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

            //Draw labels
            Font f = new Font(this.Font.FontFamily, 10);
            for(float i =0; i<CaptionPositions.Length; i++)
            {
                float x = 0;
                float h = Height/CaptionPositions.Length;
                float y = e.ClipRectangle.Y + i*h;
                float w = 95;
                RectangleF r = new RectangleF(x,y,w,h);
                g.DrawString(CaptionPositions[(int)i], f, b, r);
                g.DrawRectangle(p,r.X,r.Y,r.Width,r.Height);

            } 
        }
    }
}
