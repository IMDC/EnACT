using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;
using LibEnACT;

namespace Player.Models
{
    /// <summary>
    /// Model for the Player window.
    /// </summary>
    public class PlayerModel : INotifyPropertyChanged
    {
        #region Fields and Properties
        /// <summary>
        /// Backing field for VideoPath.
        /// </summary>
        private string bkVideoPath;
        /// <summary>
        /// A string that holds the Path to the video
        /// </summary>
        public string VideoPath 
        {
            get { return bkVideoPath; } 
            set
            {
                bkVideoPath = value;
                NotifyPropertyChanged("VideoPath");
            }
        }

        /// <summary>
        /// Backing field for CaptionsFileFilePath
        /// </summary>
        private string bkCaptionsFilePath;
        /// <summary>
        /// A string that holds the path to the captions file.
        /// </summary>
        public string CaptionsFileFilePath
        {
            get { return bkCaptionsFilePath; }
            set
            {
                bkCaptionsFilePath = value;
                NotifyPropertyChanged("CaptionsFileFilePath");
            }
        }

        public List<Caption> CaptionList { get; set; }

        public Dictionary<string,Speaker>  SpeakerSet { get; set; }

        public SettingsXml Settings { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes an instance of the PlayerModel Class.
        /// </summary>
        public PlayerModel()
        {
        }
        #endregion

        #region PropertyChanged
        /// <summary>
        /// An event that notifies a subscriber that a property in this class has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property changed.</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
