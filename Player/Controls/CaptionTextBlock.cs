using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibEnACT;

namespace Player.Controls
{
    public class CaptionTextBlock : TextBlock
    {
        public CaptionTextBlock(Caption c, string name)
        {
            Name = name;
            Text = c.ToString();

            //Confugure Speaker settings
            FontSize = c.Speaker.Font.Size;
            FontFamily = new FontFamily(c.Speaker.Font.Family);

            //Set Background and Foreground Colours
            Background = Brushes.Black;
            Foreground = Brushes.White;

            //TODO Set alignment based on Caption Alignment
            //Set Alignments
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;

            //By default make t hidden
            Visibility = Visibility.Hidden;
        }
    }
}
