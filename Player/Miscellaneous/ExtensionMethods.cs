using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Miscellaneous
{
    //Namespace aliases to increase readability
    using MediaColor = System.Windows.Media.Color;
    using DrawingColor = System.Drawing.Color;

    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts a System.Drawing.Color struct into a System.Windows.Media.Color object.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static MediaColor ToMediaColor(this DrawingColor color)
        {
           return MediaColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
