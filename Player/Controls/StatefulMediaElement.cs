using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Player.Controls
{
    public class StatefulMediaElement : MediaElement, IMediaPlayer
    {
        #region Fields and Properties
        /// <summary>
        /// Represents the current state of the media player.
        /// </summary>
        public PlayerState CurrentState { get; private set; }

        /// <summary>
        /// Represents the length of the video contained by the Media Player.
        /// </summary>
        public Duration VideoLength { get { return NaturalDuration; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes an instance of the StatefulMediaElement class.
        /// </summary>
        public StatefulMediaElement()
        {
            CurrentState = PlayerState.Closed;
        }
        #endregion

        public new void Play()
        {
                base.Play();
                CurrentState = PlayerState.Playing;
        }

        public new void Pause()
        {
            base.Pause();
            CurrentState = PlayerState.Paused;
        }

        public new void Stop()
        {
            base.Stop();
            CurrentState = PlayerState.Stopped;
        }
    }
}
