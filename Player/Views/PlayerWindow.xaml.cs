using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Player.View_Models;

namespace Player.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer TimelineTimer { get; private set; }

        public Storyboard CaptionStoryboard { get; private set; }

        public TimeSpan PauseTime { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            var playerViewModel = new PlayerViewModel(Player);
            DataContext = playerViewModel;

            CaptionStoryboard = Resources["CaptionStoryboard"] as Storyboard;

            PauseTime = new TimeSpan(0,0,0,0,0);

            //Set up ViewModel Event handlers
            playerViewModel.PlayRequested += (sender, args) =>
            {
                //If the video is right at the beginning
                if (PauseTime.TotalMilliseconds == 0)
                    CaptionStoryboard.Begin();
                else
                {
                    CaptionStoryboard.Resume();
                    PauseTime = new TimeSpan(0,0,0,0,0);
                }
            };
            playerViewModel.PauseRequested += (sender, args) =>
            {
                CaptionStoryboard.Pause();
                PauseTime = CaptionStoryboard.GetCurrentTime();
            };
            playerViewModel.StopRequested  += (sender, args) => CaptionStoryboard.Stop();
            playerViewModel.LoadRequested += (sender, args) =>
            {
                Player.Play();
                Player.Stop();
            };

            //Set up timer
            TimelineTimer = new DispatcherTimer();
            TimelineTimer.Tick += TimelineTimer_Tick;
            TimelineTimer.Interval = new TimeSpan(0,0,0,0,50);
        }

        void TimelineTimer_Tick(object sender, EventArgs e)
        {
            //Update the Timeline position to the player's position
            Timeline.Value = Player.Position.TotalMilliseconds;
        }

        private void Player_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            //Update Max value to the length of the video
            Timeline.Maximum = Player.NaturalDuration.TimeSpan.TotalMilliseconds;

            TimelineTimer.Start();
        }

        private void Timeline_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Stop Timer while clicking on timeline so that slider does not skip.
            TimelineTimer.Stop();
        }

        private void Timeline_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //Set Player position and restart timer
            int pos = (int)Timeline.Value;
            //Player.Position = new TimeSpan(0, 0, 0, 0, pos);
            CaptionStoryboard.Seek(new TimeSpan(0, 0, 0, 0, pos));
            TimelineTimer.Start();
        }
    }
}
