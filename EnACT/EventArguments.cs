using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This file is for Custom EventArgs classes
namespace EnACT
{
    /// <summary>
    /// Provides data for the Timeline.PlayheadChanged event
    /// </summary>
    public class TimelinePlayheadChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The time the playhead was set to when the event was fired.
        /// </summary>
        public double PlayheadTime { set; get; }

        /// <summary>
        /// Constructs a PlayheadChangedEventArgs object
        /// </summary>
        /// <param name="playheadTime">The time the playhead was set to when the event was fired.</param>
        public TimelinePlayheadChangedEventArgs(double playheadTime)
        {
            PlayheadTime = playheadTime;
        }
    }//Class
}
