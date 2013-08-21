using System.Windows.Controls;

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
            //Make Media controllable by external commands like play, pause, etc.
            Media.LoadedBehavior = MediaState.Manual;
        }
    }
}
