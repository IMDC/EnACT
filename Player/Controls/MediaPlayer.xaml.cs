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

        private int i = -1;
        public void AddCaption(string s)
        {
            if (0 <= i && i < CaptionGrid.Children.Count)
            {
                CaptionGrid.Children.RemoveAt(i);
                i = -1;
            }
            else
            {
                TextBlock t = new TextBlock
                {
                    Text = s,
                    FontSize = 20,
                };
                i = CaptionGrid.Children.Add(t);
                
                Grid.SetZIndex(t,-1); //Should go before Children.Add?
            }
        }
    }
}
