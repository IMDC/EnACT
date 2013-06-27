using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace EnACT
{
    #region Caption Enums
    /// <summary>
    /// The Emotion enum represents which type of emotion the caption will
    /// be displayed in.
    /// </summary>
    public enum Emotion 
    {
        Unknown = -1,
        None    = 0,
        Happy   = 1,
        Sad     = 2,
        Fear    = 3,
        Anger   = 4,
    };

    /// <summary>
    /// The Intensity enum represents how intense the emotion in a caption will be.
    /// </summary>
    public enum Intensity 
    {
        None   = 0,
        Low    = 1,
        Medium = 2,
        High   = 3
    };

    /// <summary>
    /// The Location Enum represents in which of the 9 possible areas a caption
    /// will be displayed in.
    /// </summary>
    public enum ScreenLocation 
    {
        BottomLeft   = 1,
        BottomCentre = 2,
        BottomRight  = 3,
        MiddleLeft   = 4,
        MiddleCenter = 5,
        MiddleRight  = 6,
        TopLeft      = 7,
        TopCentre    = 8,
        TopRight     = 9
    };

    /// <summary>
    /// The Alignment enum represents the text alignment of a caption.
    /// </summary>
    public enum Alignment
    {
        Left   = 0,
        Center = 1,
        Right  = 2
    }
    #endregion

    #region Caption Class
    /// <summary>
    /// The caption Class represents a Captioned line of text.
    /// </summary>
    public class Caption : INotifyPropertyChanged
    {
        #region Private fields
        private Timestamp begin;
        private Timestamp end;
        private Timestamp duration;
        #endregion

        #region PropertyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        #region Timestamp Properties
        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public Timestamp Begin 
        { 
            set
            {
                begin = value;
                if (end != null)
                {
                    if (begin < end)
                        duration = end - begin;
                    else
                    {
                        //Make end time the same as begin time
                        end = new Timestamp(begin.AsDouble);
                        duration = 0;
                    }
                }
                NotifyPropertyChanged("Begin");
            }
            get { return begin; } 
        }

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public Timestamp End
        {
            set
            {
                end = value;
                if(begin != null)
                    duration = end - begin;
                NotifyPropertyChanged("End");
            }
            get { return end; }
        }

        /// <summary>
        /// A timestamp representing how long the duration of this caption is.
        /// Setting the duration will also alter the End timestamp by setting
        /// it as End = Begin + Duration, or a copy of Duration if begin is null.
        /// </summary>
        public Timestamp Duration
        {
            set
            {
                duration = value;
                if (begin != null)
                    end = begin + duration;
                //Assume a null value would be 0.0 seconds
                else
                    //Create a new object instead of copying refrences.
                    end = new Timestamp(duration.AsDouble);
                NotifyPropertyChanged("Duration");
            }
            get { return duration; }
        }
        #endregion

        #region Public Properties
        private Speaker speaker;
        /// <summary>
        /// A reference to a speaker in the program's speaker list.
        /// </summary>
        public Speaker Speaker 
        {
            set
            {
                speaker = value;
                NotifyPropertyChanged("Duration");
            }
            get { return speaker; }
        }

        /// <summary>
        /// The location of a caption on the screen (Eg top left, centre right, etc)
        /// </summary>
        public ScreenLocation Location { set; get;}

        /// <summary>
        /// The textual alignment of a caption (eg Left, Centre, Right)
        /// </summary>
        public Alignment Alignment { set; get; }

        /// <summary>
        /// The list of words in the caption
        /// </summary>
        public CaptionWordList WordList { set; get; }

        public String Text
        {
            set
            { 
                WordList.Feed(value);
                NotifyPropertyChanged("Text");
            }
            get{ return WordList.GetAsString(); }
        }
        #endregion

        #region MoveTo
        /// <summary>
        /// Moves the caption to a new starting and ending point, maintaining the same Duration
        /// </summary>
        /// <param name="beginTime">The time this Caption will begin at</param>
        public void MoveTo(double beginTime)
        {
            this.begin = beginTime;
            this.end = begin + duration;

            NotifyPropertyChanged("Begin");
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a Caption object with a blank line and the DefaultSpeaker
        /// </summary>
        public Caption() : this("", Speaker.Default) { }
                             
        /// <summary>
        /// Constructs a Caption object with a line, and uses a reference to the
        /// line's speaker.
        /// </summary>
        /// <param name="line">Text to be displayed as a caption</param>
        /// <param name="speaker">The speaker of the caption</param>
        public Caption(String line, Speaker speaker) : this(line, speaker, "00:00:00.0", "00:00:00.0") { }

        /// <summary>
        /// Constructs a Caption object with a given line, speaker, and begining 
        /// and ending timestamps
        /// </summary>
        /// <param name="line">Text to be displayed as a caption</param>
        /// <param name="speaker">The speaker of the caption</param>
        /// <param name="Begin">The timestamp representing the beginning of the caption</param>
        /// <param name="End">The timestamp representing the ending of the caption</param>
        public Caption(String line, Speaker speaker, String Begin, String End)
        {
            this.Begin     = new Timestamp(Begin);
            this.End       = new Timestamp(End);

            this.Speaker   = speaker;
            this.Location  = ScreenLocation.BottomCentre;
            this.Alignment = Alignment.Center;

            this.WordList = new CaptionWordList(line);
        }
        #endregion

        #region ToString
        /// <summary>
        /// Returns the text sentence that this caption represents.
        /// Calls the WordListText method, and returns its value.
        /// </summary>
        /// <returns>The text of WordList's words</returns>
        public override string ToString()
        {
            return this.WordList.GetAsString();
        } 
        #endregion
    }
    #endregion
}