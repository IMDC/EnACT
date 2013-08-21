﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Player.Controls;
using Player.View_Models;

namespace Player.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPaused = false;

        public DispatcherTimer TimelineTimer { get; private set; }

        public Storyboard CaptionStoryboard { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            var playerViewModel = new PlayerViewModel(Player.Media);
            DataContext = playerViewModel;

            CaptionStoryboard = (Storyboard) Player.Resources["CaptionStoryboard"];

            //Set up ViewModel Event handlers
            playerViewModel.PlayRequested += (sender, args) =>
            {
                if(isPaused)
                {
                    CaptionStoryboard.Resume();
                    isPaused = false;
                }
                else //The video is right at the beginning
                    CaptionStoryboard.Begin();
            };

            //Pause the video and remember that it was paused.
            playerViewModel.PauseRequested += (sender, args) =>
            {
                CaptionStoryboard.Pause();
                isPaused = true;
            };

            playerViewModel.StopRequested  += (sender, args) => CaptionStoryboard.Stop();

            //Quickly Play and Stop the video so that it gets loaded into the media element.
            playerViewModel.LoadRequested += (sender, args) =>
            {
                Player.Media.Play();
                Player.Media.Stop();
            };

            Player.Media.MediaOpened += Player_OnMediaOpened;

            //Set up timer
            TimelineTimer = new DispatcherTimer();
            TimelineTimer.Tick += TimelineTimer_Tick;
            TimelineTimer.Interval = new TimeSpan(0,0,0,0,50);
        }

        void TimelineTimer_Tick(object sender, EventArgs e)
        {
            //Update the Timeline position to the player's position
            Timeline.Value = Player.Media.Position.TotalMilliseconds;
        }

        private void Player_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            //Update Max value to the length of the video
            Timeline.Maximum = Player.Media.NaturalDuration.TimeSpan.TotalMilliseconds;
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
            CaptionStoryboard.Seek(new TimeSpan(0, 0, 0, 0, pos));
            TimelineTimer.Start();
        }

        private void CheckBoxHideControls_Checked(object sender, RoutedEventArgs e)
        {   
            Player.AddCaption("Helloooo");
        }
    }
}
