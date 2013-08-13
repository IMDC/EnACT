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
            DataContext = new PlayerViewModel();
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
                MediaPlayer.Media.Source = new Uri(fileBrowserDialog.FileName);
            }
        }
        #endregion
    }
}
