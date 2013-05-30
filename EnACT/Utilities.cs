﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EnACT
{
    /// <summary>
    /// A utilities class meant for extending classes
    /// </summary>
    public static class Utilities
    {
        #region FillOutlinedRoundedRectangle
        /// <summary>
        /// Draws a rectangle shape with rounded edges. The rectangle will be drawn
        /// using a brush to fill the inside, and a pen for the outline, allowing
        /// for two different colours to be used.
        /// </summary>
        /// <param name="g">This graphics object</param>
        /// <param name="fillBrush">The brush used for filling the rectangle</param>
        /// <param name="outLinePen">The pen used to draw the rectangle's outline</param>
        /// <param name="x">X position of the rectangle</param>
        /// <param name="y">Y position of the rectangle</param>
        /// <param name="w">Width of the rectangle</param>
        /// <param name="h">Height of the rectangle</param>
        public static void FillOutlinedRoundedRectangle(this Graphics g, Brush fillBrush,
            Pen outLinePen, float x, float y, float w, float h)
        {
            float arcWidth = Math.Min(w,h)/2;
            float halfArcWidth = arcWidth / 2;

            GraphicsPath gp = new GraphicsPath();

            //Top left arc
            gp.AddArc(x, y, arcWidth, arcWidth, 180, 90);

            //Top side
            gp.AddLine(x + halfArcWidth, y, x + w - halfArcWidth, y);

            //Top right arc
            gp.AddArc(x + w - arcWidth, y, arcWidth, arcWidth, 270, 90);

            //Right side
            gp.AddLine(x + w, y + halfArcWidth, x + w, y + h - halfArcWidth);
            
            //Bottom right arc
            gp.AddArc(x + w - arcWidth, y + h - arcWidth, arcWidth, arcWidth, 0, 90);

            //Bottom side
            gp.AddLine(x + halfArcWidth, y + h, x + w - halfArcWidth, y + h);

            //Bottom left arc
            gp.AddArc(x, y + h - arcWidth, arcWidth, arcWidth, 90, 90);
            
            //Left side
            gp.AddLine(x, y + halfArcWidth, x, y + h - halfArcWidth);
            
            //Fill area
            Region r = new Region(gp);
            g.FillRegion(fillBrush, r);

            //Draw outline
            g.DrawPath(outLinePen, gp);
        }
        #endregion
    }
}
