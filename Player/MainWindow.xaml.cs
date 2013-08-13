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
        #endregion
    }
}
