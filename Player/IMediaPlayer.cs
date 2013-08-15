using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Player
{
    /// <summary>
    /// An Interface for media players that exposes certain properties to the ViewModel used by
    /// the View that implements this media player.
    /// </summary>
    public interface IMediaPlayer
    {
        /// <summary>
        /// Represents the current position of the video in the Media Player.
        /// </summary>
        TimeSpan Position { get; set; }

        /// <summary>
        /// Represents the current state of the media player.
        /// </summary>
        PlayerState CurrentState { get; }

        /// <summary>
        /// Represents the length of the video in the media player if one is loaded.
        /// </summary>
        Duration VideoLength { get; }
    }
}
