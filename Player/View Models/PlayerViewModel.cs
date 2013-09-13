using System;
using System.IO;
using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;
using Player.Models;
using Microsoft.Win32;
using LibEnACT;

namespace Player.View_Models
{
    public class PlayerViewModel : ViewModelBase
    {
        #region Fields and Properties
        public PlayerModel PlayerModel { get; set; }

        public IMediaPlayer MediaPlayer { get; private set; }

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

        public event EventHandler LoadRequested;

        public event EventHandler<EventArgs<PlayerModel>> LoadCaptionsRequested;
        #endregion

        #region Constructor
        public PlayerViewModel(IMediaPlayer mediaPlayer)
        {
            //Set MediaPlayer
            MediaPlayer = mediaPlayer;

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
            return true;
//            return MediaPlayer.CurrentState == PlayerState.Paused 
//                || MediaPlayer.CurrentState == PlayerState.Stopped;
        }

        /// <summary>
        /// Plays the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        private void Play(object parameter)
        {
            //Invoke request so that the UI can play the video.
            OnPlayRequested();
        }

        /// <summary>
        /// Determines whether or not the ViewModel can pause the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Whether or not the video can be paused.</returns>
        private bool CanPause(object parameter)
        {
            return true;
//            return MediaPlayer.CurrentState == PlayerState.Playing;
        }

        /// <summary>
        /// Pauses the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        private void Pause(object parameter)
        {
            //Invoke request so that the UI can pause the video.
            OnPauseRequested();
        }

        /// <summary>
        /// Determines whether or not the ViewModel can stopped the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Whether or not the video can be stopped.</returns>
        private bool CanStop(object parameter)
        {
            return true;
//            return MediaPlayer.CurrentState == PlayerState.Paused
//                || MediaPlayer.CurrentState == PlayerState.Playing;
        }

        /// <summary>
        /// Stops the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        private void Stop(object parameter)
        {
            //Invoke request so that the UI can stop the video.
            OnStopRequested();
        }

        /// <summary>
        /// Determines whether or not the can be rewound.
        /// </summary>
        /// <param name="parameter">Paramater</param>
        /// <returns>Whether or not the video can be rewound</returns>
        private bool CanRewind(object parameter)
        {
            return MediaPlayer.CurrentState == PlayerState.Paused
                   || MediaPlayer.CurrentState == PlayerState.Playing;
        }

        /// <summary>
        /// Rewinds the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        private void Rewind(object parameter)
        {
            //TODO Implement this
        }

        /// <summary>
        /// Determines whether or not the can be fast forwarded.
        /// </summary>
        /// <param name="parameter">Paramater</param>
        /// <returns>Whether or not the video can be fast forwarded.</returns>
        private bool CanFastForward(object parameter)
        {
            return MediaPlayer.CurrentState == PlayerState.Paused
                || MediaPlayer.CurrentState == PlayerState.Playing
                || MediaPlayer.CurrentState == PlayerState.Stopped;
        }

        /// <summary>
        /// Fast Forwards the video.
        /// </summary>
        /// <param name="parameter"></param>
        private void FastForward(object parameter)
        {
            //TODO Implement this
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

            if(!result.Value)
                return;     //Return if no file opened.

            PlayerModel.VideoPath = fileBrowserDialog.FileName;
            VideoUri = new Uri(PlayerModel.VideoPath);
            OnLoadRequested();

            PlayerModel.CaptionsFileFilePath = Path.ChangeExtension(PlayerModel.VideoPath, ".enact");

            try
            {
                var tuple = XMLReader.ParseEngineXml(PlayerModel.CaptionsFileFilePath);

                PlayerModel.CaptionList = tuple.Item1;
                PlayerModel.SpeakerSet = tuple.Item2;
                PlayerModel.Settings = tuple.Item3;

                OnLoadCaptionsRequested(new EventArgs<PlayerModel>(PlayerModel));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No captions found.");
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

        private void OnLoadRequested()
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = LoadRequested;
            if (handler != null) { handler(this, EventArgs.Empty); }
        }

        private void OnLoadCaptionsRequested(EventArgs<PlayerModel> e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler<EventArgs<PlayerModel>> handler = LoadCaptionsRequested;

            if (handler != null) { handler(this, e); }
        }
        #endregion
    }
}
