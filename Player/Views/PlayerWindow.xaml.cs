using System;
using System.Windows;
using Microsoft.Win32;
using Player.View_Models;

namespace Player.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var playerViewModel = new PlayerViewModel();
            DataContext = playerViewModel;

            //Set up ViewModel Event handlers
            playerViewModel.PlayRequested += (sender, args) =>
            {
                Player.Media.Play();
            };

            playerViewModel.PauseRequested += (sender, args) =>
            {
                Player.Media.Pause();
            };

            playerViewModel.StopRequested += (sender, args) =>
            {
                Player.Media.Stop();
            };
        }

        #region File MenuItem Click Handlers
        /// <summary>
        /// Opens a dialog to select a video to play.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void MenuItemOpenVideo_OnClick(object sender, RoutedEventArgs e)
        {
            var fileBrowserDialog = new OpenFileDialog
            {
                Filter = "Video Files|*.avi;*.mpg;*.mov;*.wmv|All Files|*.*"
            };

            var result = fileBrowserDialog.ShowDialog();

            if (result == true)
            {
                Player.Media.Source = new Uri(fileBrowserDialog.FileName);
            }
        }
        #endregion
    }
}
