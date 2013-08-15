using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;
using Player.Models;
using Microsoft.Win32;

namespace Player.View_Models
{
    public class PlayerViewModel : ViewModelBase
    {
        #region Constants
        public const double DefaultSpeedRatio = 1;

        public const double SpeedRatioChangeFactor = 2;
        #endregion

        #region Fields and Properties
        public PlayerModel PlayerModel { get; set; }

        /// <summary>
        /// A command that plays the video.
        /// </summary>
        public ICommand PlayCommand { get; private set; }
        /// <summary>
        /// A command that pauses the video.
        /// </summary>
        public ICommand PauseCommand { get; private set; }
        /// <summary>
        /// A command that stops the video.
        /// </summary>
        public ICommand StopCommand { get; private set; }
        /// <summary>
        /// A command that rewinds the video.
        /// </summary>
        public ICommand RewindCommand { get; private set; }
        /// <summary>
        /// A command that fast forwards the video.
        /// </summary>
        public ICommand FastForwardCommand { get; private set; }

        /// <summary>
        /// A command that opens up a file browser.
        /// </summary>
        public ICommand OpenVideoCommand { get; private set; }
        

        /// <summary>
        /// Backing Field for VideoURI
        /// </summary>
        private Uri bkVideoURI;
        /// <summary>
        /// The source of the video as a Uri.
        /// </summary>
        public Uri VideoUri
        {
            get { return bkVideoURI; }
            set
            {
                bkVideoURI = value;
                RaisePropertyChanged("VideoUri");
            }
        }

        /// <summary>
        /// Backing field for SpeedRatio.
        /// </summary>
        private double bkSpeedRatio;
        /// <summary>
        /// The speed that the media plays at. 1 is regular speed, greater than 1 plays faster, and
        /// less than 1 plays slower.
        /// </summary>
        public double SpeedRatio
        {
            get { return bkSpeedRatio; }
            set
            {
                bkSpeedRatio = value; 
                RaisePropertyChanged("SpeedRatio");
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// An event that is fired when a play command is executed.
        /// </summary>
        public event EventHandler PlayRequested;

        /// <summary>
        /// An event that is fired when a pause command is executed.
        /// </summary>
        public event EventHandler PauseRequested;

        /// <summary>
        /// An event that is fired when a stop command is executed.
        /// </summary>
        public event EventHandler StopRequested;

        public event EventHandler<EventArgs<double>> SpeedRatioChangeRequested;
        #endregion

        #region Constructor
        public PlayerViewModel()
        {
            //Construct PlayerModel
            PlayerModel = new PlayerModel();

            //Media Construct Commands
            PlayCommand = new RelayCommand(Play, CanPlay);
            PauseCommand = new RelayCommand(Pause, CanPause);
            StopCommand = new RelayCommand(Stop, CanStop);
            RewindCommand = new RelayCommand(Rewind, CanRewind);
            FastForwardCommand = new RelayCommand(FastForward, CanFastForward);

            //Construct File Menu Commands
            OpenVideoCommand = new RelayCommand(OpenVideo);

            SpeedRatio = DefaultSpeedRatio;
        }
        #endregion

        #region Media Command Methods
        /// <summary>
        /// Determines whether or not the ViewModel can play the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Whether or not the video can be played.</returns>
        private bool CanPlay(object parameter)
        {
            return PlayerModel.CurrentState == PlayerState.Paused 
                || PlayerModel.CurrentState == PlayerState.Stopped;
        }

        /// <summary>
        /// Plays the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        private void Play(object parameter)
        {
            //Invoke request so that the UI can play the video.
            OnPlayRequested();
            PlayerModel.CurrentState = PlayerState.Playing;
        }

        /// <summary>
        /// Determines whether or not the ViewModel can pause the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Whether or not the video can be paused.</returns>
        private bool CanPause(object parameter)
        {
            return PlayerModel.CurrentState == PlayerState.Playing;
        }

        /// <summary>
        /// Pauses the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        private void Pause(object parameter)
        {
            //Invoke request so that the UI can pause the video.
            OnPauseRequested();
            PlayerModel.CurrentState = PlayerState.Stopped;
        }

        /// <summary>
        /// Determines whether or not the ViewModel can stopped the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Whether or not the video can be stopped.</returns>
        private bool CanStop(object parameter)
        {
            return PlayerModel.CurrentState == PlayerState.Paused
                || PlayerModel.CurrentState == PlayerState.Playing;
        }

        /// <summary>
        /// Stops the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        private void Stop(object parameter)
        {
            //Invoke request so that the UI can stop the video.
            OnStopRequested();
            PlayerModel.CurrentState = PlayerState.Stopped;
        }

        private bool CanRewind(object parameter)
        {
            return PlayerModel.CurrentState == PlayerState.Paused
                   || PlayerModel.CurrentState == PlayerState.Playing;
        }

        private void Rewind(object parameter)
        {
            SpeedRatio /= SpeedRatioChangeFactor;
            OnSpeedRatioChangeRequested(SpeedRatio);
            PlayerModel.CurrentState = PlayerState.Playing;
        }

        private bool CanFastForward(object parameter)
        {
            return PlayerModel.CurrentState == PlayerState.Paused
                || PlayerModel.CurrentState == PlayerState.Playing
                || PlayerModel.CurrentState == PlayerState.Stopped;
        }

        private void FastForward(object parameter)
        {
            SpeedRatio *= SpeedRatioChangeFactor;
            OnSpeedRatioChangeRequested(SpeedRatio);
            PlayerModel.CurrentState = PlayerState.Playing;
        }
        #endregion

        #region File Menu Command Methods
        /// <summary>
        /// Opens a video file.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public void OpenVideo(object parameter)
        {
            var fileBrowserDialog = new OpenFileDialog
            {
                Filter = "Video Files|*.avi;*.mpg;*.mov;*.wmv|All Files|*.*"
            };

            var result = fileBrowserDialog.ShowDialog();

            if (result == true)
            {
                PlayerModel.VideoPath = fileBrowserDialog.FileName;
                VideoUri = new Uri(PlayerModel.VideoPath);
                PlayerModel.CurrentState = PlayerState.Stopped;
            }
        }
        #endregion

        #region Event Invocation Methods
        /// <summary>
        /// Raises the PlayRequested Event.
        /// </summary>
        private void OnPlayRequested()
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = PlayRequested;
            if (handler != null) { handler(this, EventArgs.Empty); }
        }

        /// <summary>
        /// Raises the PauseRequested Event.
        /// </summary>
        private void OnPauseRequested()
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = PauseRequested;
            if (handler != null) { handler(this, EventArgs.Empty); }
        }

        /// <summary>
        /// Raises the StopRequested Event.
        /// </summary>
        private void OnStopRequested()
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = StopRequested;
            if (handler != null) { handler(this, EventArgs.Empty); }
        }

        private void OnSpeedRatioChangeRequested(double speedRatio)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler<EventArgs<double>> handler = SpeedRatioChangeRequested;
            if (handler != null) { handler(this, new EventArgs<double>(speedRatio)); }
        }
        #endregion
    }
}
