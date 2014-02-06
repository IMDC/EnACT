using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibEnACT;
using Player.Miscellaneous;

namespace Player.Controls
{
    public class CaptionTextBlock : TextBlock
    {
        /// <summary>
        /// A reference to the Caption this CaptionTextBlock represents.
        /// </summary>
        public Caption Caption { get; set; }

        /// <summary>
        /// Constructs a CaptionTextBlock object representing the given Caption.
        /// </summary>
        /// <param name="c">The caption to represent.</param>
        /// <param name="name">The name of the CaptionTextBlock.</param>
        public CaptionTextBlock(Caption c, string name)
        {
            //Set reference
            this.Caption = c;

            //Set Textblock properties
            this.Name = name;
            this.Text = c.Text;

            //Confugure Speaker settings
            this.FontSize = c.Speaker.Font.Size;
            this.FontFamily = new FontFamily(c.Speaker.Font.Family);

            //TODO: Make a collection of speaker brushes to reduce object instantiations
            /* Set Background and Foreground Colours. Note, these attributes may be altered by the
             * animation of a textblock, particularily the colour animations.
             */
            this.Background = new SolidColorBrush(c.Speaker.Font.BackgroundColour.ToMediaColor());
            this.Foreground = new SolidColorBrush(c.Speaker.Font.ForegroundColour.ToMediaColor());

            //TODO Set alignment based on Caption Alignment
            //Set Alignments
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;

            //By default make t hidden
            this.Visibility = Visibility.Hidden;
        }
    }
}
