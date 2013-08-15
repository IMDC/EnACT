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
        TimeSpan Position { get; set; }

        PlayerState CurrentState { get; }
    }
}
