using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnACT
{
    /// <summary>
    /// A label that displays the Video progress in the form of currentTime/TotalLength
    /// </summary>
    public class PlayheadLabel : Label
    {
        /// <summary>
        /// Backing field for PlayheadTime
        /// </summary>
        private Timestamp bkPlayheadTime;
        /// <summary>
        /// The Timestamp that displays the playhead's time in the label
        /// </summary>
        public Timestamp PlayheadTime
        {
            get { return bkPlayheadTime; }
            set
            {
                bkPlayheadTime = value;
                UpdateText();
            }
        }

        /// <summary>
        /// Backing field for VideoLength
        /// </summary>
        private Timestamp bkVideoLength;
        /// <summary>
        /// The Timestamp that displays the total length of the video
        /// </summary>
        public Timestamp VideoLength
        {
            get { return bkVideoLength; }
            set
            {
                bkVideoLength = value;
                UpdateText();
            }
        }

        /// <summary>
        /// Constructs a PlayheadLabel with default values
        /// </summary>
        public PlayheadLabel()
        {
            bkPlayheadTime = new Timestamp();
            bkVideoLength = new Timestamp();
            UpdateText();
        }

        /// <summary>
        /// Updates the text of the label to show the latest PlayheadTime and VideoLength
        /// </summary>
        public void UpdateText()
        {
            this.Text = String.Format("{0} / {1}", PlayheadTime, VideoLength);
        }
    }
}
