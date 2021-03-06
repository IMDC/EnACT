﻿using System;

//This file is for Custom EventArgs classes
namespace EnACT.Controls
{
    #region TimelinePlayheadChangedEventArgs
    /// <summary>
    /// Provides data for the Timeline.PlayheadChanged event
    /// </summary>
    public class TimelinePlayheadChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The time the playhead was set to when the event was fired.
        /// </summary>
        public double PlayheadTime { private set; get; }

        /// <summary>
        /// Constructs a PlayheadChangedEventArgs object
        /// </summary>
        /// <param name="playheadTime">The time the playhead was set to when the event was fired.</param>
        public TimelinePlayheadChangedEventArgs(double playheadTime)
        {
            PlayheadTime = playheadTime;
        }
    }//Class
    #endregion

    #region TimelineCaptionTimestampChangedEventArgs
    /// <summary>
    /// Provides data for the Timeline.CaptionTimestampChanged event
    /// </summary>
    public class TimelineCaptionTimestampChangedEventArgs : EventArgs
    {
        public TimelineCaptionTimestampChangedEventArgs()
        {
        }
    }
    #endregion
}//Namespace
