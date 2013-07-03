using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace EnACT
{
    #region EditorCaption Class
    /// <summary>
    /// A caption that is used by the Editor. Implements INotifyPropertyChanged in order to update
    /// CaptionView when properties are changed.
    /// </summary>
    public class EditorCaption : BaseCaption, INotifyPropertyChanged
    {
        #region Properties and Fields
        /// <summary>
        /// A reference to a speaker in the program's speaker list.
        /// </summary>
        public override Speaker Speaker
        {
            get { return base.Speaker; }
            set
            {
                base.Speaker = value;
                NotifyPropertyChanged("Speaker");
            }
        }

        /// <summary>
        /// The location of a caption on the screen (Eg top left, centre right, etc)
        /// </summary>
        public override ScreenLocation Location
        {
            get { return base.Location; }
            set
            {
                base.Location = value;
                NotifyPropertyChanged("Location");
            }
        }

        /// <summary>
        /// The textual alignment of a caption (eg Left, Centre, Right)
        /// </summary>
        public override Alignment Alignment
        {
            get { return base.Alignment; }
            set
            {
                base.Alignment = value;
                NotifyPropertyChanged("Alignment");
            }
        }

        /// <summary>
        /// The list of words in the caption
        /// </summary>
        public override CaptionWordList Words
        {
            get { return base.Words; }
            set
            {
                base.Words = value;
                NotifyPropertyChanged("Words");
            }
        }

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public override Timestamp Begin
        {
            get { return base.Begin; }
            set
            {
                base.Begin = value;
                NotifyPropertyChanged("Begin");
            }
        }

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public override Timestamp End
        {
            get { return base.End; }
            set
            {
                base.End = value;
                NotifyPropertyChanged("End");
            }
        }

        /// <summary>
        /// A timestamp representing how long the duration of this caption is.
        /// Setting the duration will also alter the End timestamp by setting
        /// it as End = Begin + Duration, or a copy of Duration if begin is null.
        /// </summary>
        public override Timestamp Duration
        {
            get { return base.Duration; }
            set
            {
                base.Duration = value;
                NotifyPropertyChanged("Duration");
            }
        }

        /// <summary>
        /// Wrapper property for CaptionView. Gets and Sets the Words of this EditorCaption.
        /// </summary>
        public String Text
        {
            set
            { 
                Words.Feed(value);
                NotifyPropertyChanged("Text");
            }
            get{ return Words.GetAsString(); }
        }
        #endregion

        #region PropertyChanged Event
        /// <summary>
        /// An event that notifies a subscriber that a property in this EditorCaption has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(String info)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null) { handler(this, new PropertyChangedEventArgs(info)); }
        }
        #endregion       

        #region Constructor
        /// <summary>
        /// Constructs a EditorCaption object with a blank line and the DefaultSpeaker
        /// </summary>
        public EditorCaption() : this("", Speaker.Default) { }
                             
        /// <summary>
        /// Constructs a EditorCaption object with a line, and uses a reference to the
        /// line's speaker.
        /// </summary>
        /// <param name="line">Text to be displayed as a caption</param>
        /// <param name="speaker">The speaker of the caption</param>
        public EditorCaption(String line, Speaker speaker) : this(line, speaker, "00:00:00.0", "00:00:00.0") { }

        /// <summary>
        /// Constructs a EditorCaption object with a given line, speaker, and begining 
        /// and ending timestamps
        /// </summary>
        /// <param name="line">Text to be displayed as a caption</param>
        /// <param name="speaker">The speaker of the caption</param>
        /// <param name="Begin">The timestamp representing the beginning of the caption</param>
        /// <param name="End">The timestamp representing the ending of the caption</param>
        public EditorCaption(String line, Speaker speaker, String Begin, String End) : base(line, speaker, Begin, End) { }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the text sentence that this caption represents.
        /// Calls the WordListText method, and returns its value.
        /// </summary>
        /// <returns>The text of Words's words</returns>
        public override void MoveTo(double beginTime)
        {
            base.MoveTo(beginTime);
            NotifyPropertyChanged("BeginTime");
        }
        #endregion
    }
    #endregion
}