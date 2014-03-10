using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LibEnACT
{
    /// <summary>
    /// Represents a Caption used by EnACT.
    /// </summary>
    public class Caption : INotifyPropertyChanged
    {
        #region Constants
        /// <summary>
        /// Contains names of the properties of this class as Strings.
        /// </summary>
        public static class PropertyNames
        {
            public const string Alignment = "Alignment";
            public const string Begin = "Begin";
            public const string End = "End";
            public const string Location = "Location";
            public const string Duration = "Duration";
            public const string Speaker = "Speaker";
            public const string Text = "Text";
            public const string Words = "Words";
        }
        #endregion

        #region Properties and Fields
        //Backing Fields
        private Speaker _speaker;
        private ScreenLocation _location;
        private Alignment _alignment;
        private CaptionWordCollection _words;
        private Timestamp _begin;
        private Timestamp _end;
        private Timestamp _duration;

        /// <summary>
        /// A reference to a speaker in the program's speaker list.
        /// </summary>
        public virtual Speaker Speaker
        {
            set
            {
                _speaker = value;
                NotifyPropertyChanged();
            }
            get { return _speaker; }
        }

        /// <summary>
        /// The location of a caption on the screen (Eg top left, centre right, etc)
        /// </summary>
        public virtual ScreenLocation Location
        {
            set
            {
                _location = value;
                NotifyPropertyChanged();
            }
            get { return _location; }
        }

        /// <summary>
        /// The textual alignment of a caption (eg Left, Centre, Right)
        /// </summary>
        public virtual Alignment Alignment
        {
            set
            {
                _alignment = value;
                NotifyPropertyChanged();
            }
            get { return _alignment; }
        }

        /// <summary>
        /// The list of words in the caption
        /// </summary>
        public virtual CaptionWordCollection Words
        {
            private set
            {
                _words = value;
                NotifyPropertyChanged();
            }
            get { return _words; }
        }

        /// <summary>
        /// The text of a Caption. Setting this value will also populate or re-populate the Words
        /// list with the words derived from the caption text.
        /// </summary>
        public virtual string Text
        {
            set
            {
                Words.Feed(value);
                NotifyPropertyChanged();
            }
            get { return Words.ToString(); }
        }
        #endregion //#region Properties and Fields

        #region Timestamp Properties

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public virtual Timestamp Begin
        {
            set
            {
                _begin = value;
                if (_end != null)
                {
                    if (_begin < _end)
                        _duration = _end - _begin;
                    else
                    {
                        //Make end time the same as begin time
                        _end = new Timestamp(_begin.AsDouble);
                        _duration = 0;
                    }
                    //TODO: Notify that multiple timestamp properties were changed?
                    NotifyPropertyChanged();
                }
            }
            get { return _begin; }
        }

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public virtual Timestamp End
        {
            set
            {
                _end = value;
                if (_begin != null)
                    _duration = _end - _begin;

                NotifyPropertyChanged();
            }
            get { return _end; }
        }

        /// <summary>
        /// A timestamp representing how long the duration of this caption is.
        /// Setting the duration will also alter the End timestamp by setting
        /// it as End = Begin + Duration, or a copy of Duration if begin is null.
        /// </summary>
        public virtual Timestamp Duration
        {
            set
            {
                _duration = value;
                if (_begin != null)
                    _end = _begin + _duration;
                //Assume a null value would be 0.0 seconds
                else
                    //Create a new object instead of copying refrences.
                    _end = new Timestamp(_duration.AsDouble);

                NotifyPropertyChanged();
            }
            get { return _duration; }
        }
        #endregion //#region Timestamp Properties

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
        public Caption(string line, Speaker speaker) : this(line, speaker, "00:00:00.0", "00:00:00.0") { }

        /// <summary>
        /// Constructs a Caption object with a given line, speaker, and begining 
        /// and ending timestamps
        /// </summary>
        /// <param name="line">Text to be displayed as a caption</param>
        /// <param name="speaker">The speaker of the caption</param>
        /// <param name="begin">The timestamp representing the beginning of the caption</param>
        /// <param name="end">The timestamp representing the ending of the caption</param>
        /// <param name="spaceWidth">The width in characters of the spacing between words.</param>
        public Caption(string line, Speaker speaker, string begin, string end, int spaceWidth=CaptionWordCollection.DefaultSpaceWidth)
        {
            //Set Timestamps. Duration is implicity set.
            this.Begin = new Timestamp(begin);
            this.End = new Timestamp(end);

            //Set other Caption properties
            this.Speaker = speaker;
            this.Location = ScreenLocation.BottomCentre;
            this.Alignment = Alignment.Center;

            //Set up word collection and feed it words
            this.Words = new CaptionWordCollection(line,spaceWidth);
        }
        #endregion

        #region MoveTo
        /// <summary>
        /// Moves the caption to a new starting and ending point, maintaining the same Duration
        /// </summary>
        /// <param name="beginTime">The time this Caption will begin at</param>
        public virtual void MoveTo(double beginTime)
        {
            this._begin = beginTime;
            this._end = _begin + _duration;
            NotifyPropertyChanged(PropertyNames.Begin);
        }
        #endregion

        public void SetWordList(List<CaptionWord> cwList)
        {
            Words.SetWordList(cwList);
        }

        #region ToString
        /// <summary>
        /// Returns the text sentence that this caption represents.
        /// Calls the WordListText method, and returns its value.
        /// </summary>
        /// <returns>The text of Words's words</returns>
        public override string ToString() { return Words.ToString(); }
        #endregion

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies event subscribers that a property in this class has been changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has been changed.</param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
