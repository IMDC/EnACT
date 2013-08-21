using System.Windows.Controls;
using LibEnACT;

namespace Player.Controls
{
    /// <summary>
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayer : UserControl
    {
        public MediaPlayer()
        {
            InitializeComponent();
        }

        public void AddCaption(string s)
        {
            TextBlock t = new TextBlock
            {
                Text = s
            };
            CaptionGrid.Children.Add(t);
        }
    }
}
