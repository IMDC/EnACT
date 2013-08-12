using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        /// <summary>
        /// Closes the video but does not close the player.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void MenuItemCloseVideo_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Closes the player.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void MenuItemExit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Media Button Click Handlers
        private void ButtonRewind_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonPause_OnClick(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Media.Pause();
        }

        private void ButtonPlay_OnClick(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Media.Play();
        }

        private void ButtonStop_OnClick(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Media.Stop();
        }

        private void ButtonForward_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
