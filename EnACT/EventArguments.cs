using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This file is for Custom EventArgs classes
namespace EnACT
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

    #region CaptionWordSelectedEventArgs
    /// <summary>
    /// Event Arguments for the CaptionTextBox.CaptionWordSelected event.
    /// </summary>
    public class CaptionWordSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// The word selected by the user in CaptionTextBox.
        /// </summary>
        public CaptionWord SelectedWord { private set; get; }

        /// <summary>
        /// Constructs a CaptionWordSelectedEventArgs with a specified CaptionWord.
        /// </summary>
        /// <param name="selectedWord">The selected CaptionWord.</param>
        public CaptionWordSelectedEventArgs(CaptionWord selectedWord)
        {
            this.SelectedWord = selectedWord;
        }
    }
    #endregion
}//Namespace
