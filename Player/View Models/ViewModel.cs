using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;

namespace Player.View_Models
{
    class ViewModel
    {
        #region Fields and Properties
        //RequestEvents
        public event EventHandler PlayRequested;

        public ICommand PlayCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand MediaOpenedCommand { get; set; }
        public ICommand SetVideoSourceCommand { get; set; }
        #endregion

        public ViewModel()
        {
            PlayCommand = new RelayCommand(Play, CanPlay);
            PauseCommand = new RelayCommand(Pause, (object parameter) => true);
            StopCommand = new RelayCommand(Stop, (object parameter) => true);
            MediaOpenedCommand = new RelayCommand(MediaOpened, (object parameter) => true);
            SetVideoSourceCommand = new RelayCommand(SetVideoSource, (object parameter) => true);
        }

        private bool CanPlay(object parameter)
        {
            return true;
        }

        private void Play(object parameter)
        {
            OnPlayRequested();
        }

        private void Pause(object parameter) { }
        private void Stop(object parameter) { }
        private void MediaOpened(object parameter) { }
        private void SetVideoSource(object parameter) { }

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
    }
}
