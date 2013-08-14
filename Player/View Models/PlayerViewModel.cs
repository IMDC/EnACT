using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;
using Player.Models;

namespace Player.View_Models
{
    public class PlayerViewModel
    {
        #region Fields and Properties
        public PlayerModel PlayerModel { get; set; }

        //Commands
        public ICommand PlayCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand MediaOpenedCommand { get; set; }
        public ICommand SetVideoSourceCommand { get; set; }
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
        #endregion

        #region Constructor
        public PlayerViewModel()
        {
            //Construct PlayerModel
            PlayerModel = new PlayerModel();

            //Construct Commands
            PlayCommand = new RelayCommand(Play, CanPlay);
            PauseCommand = new RelayCommand(Pause, CanPause);
            StopCommand = new RelayCommand(Stop, CanStop);
            MediaOpenedCommand = new RelayCommand(MediaOpened, (object parameter) => true);
            SetVideoSourceCommand = new RelayCommand(SetVideoSource, (object parameter) => true);
        }
        #endregion

        #region Command Methods
        /// <summary>
        /// Determines whether or not the ViewModel can play the video.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Whether or not the video can be played.</returns>
        private bool CanPlay(object parameter)
        {
            return PlayerModel.PlayState == PlayerModel.PlayerPlayState.Paused 
                || PlayerModel.PlayState == PlayerModel.PlayerPlayState.Stopped;
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
            return PlayerModel.PlayState == PlayerModel.PlayerPlayState.Playing;
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
            return PlayerModel.PlayState == PlayerModel.PlayerPlayState.Paused
                || PlayerModel.PlayState == PlayerModel.PlayerPlayState.Playing;
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

        private void MediaOpened(object parameter) { }
        private void SetVideoSource(object parameter) { }
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
        #endregion
    }
}
