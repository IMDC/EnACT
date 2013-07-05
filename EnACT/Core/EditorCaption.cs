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
    public class EditorCaption : Caption, INotifyPropertyChanged
    {
        #region Constants
        /// <summary>
        /// Contains names of the properties of this class as Strings.
        /// </summary>
        public static class PropertyNames
        {
            public const string Alignment="Alignment";
            public const string Begin    ="Begin";
            public const string End      ="End";
            public const string Location ="Location";
            public const string Duration ="Duration";
            public const string Speaker  ="Speaker";
            public const string Text     ="Text";
            public const string Words    ="Words";
        }
        #endregion

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
                NotifyPropertyChanged(PropertyNames.Speaker);
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
                NotifyPropertyChanged(PropertyNames.Location);
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
                NotifyPropertyChanged(PropertyNames.Alignment);
            }
        }

        /// <summary>
        /// Backing field for the Words property.
        /// </summary>
        private List<EditorCaptionWord> wordlist;
        /// <summary>
        /// The list of words in the caption
        /// </summary>
        public new List<EditorCaptionWord> Words
        {
            set
            {
                wordlist = value;
                NotifyPropertyChanged(PropertyNames.Words);
            }
            get { return wordlist; }
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
                NotifyPropertyChanged(PropertyNames.Words);
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
                NotifyPropertyChanged(PropertyNames.Words);
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
                NotifyPropertyChanged(PropertyNames.Duration);
            }
        }

        /// <summary>
        /// Wrapper property for CaptionView. Gets and Sets the Words of this EditorCaption.
        /// </summary>
        public String Text
        {
            set
            { 
                this.Feed(value);
                NotifyPropertyChanged(PropertyNames.Text);
            }
            get{ return this.GetAsString(); }
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
        public EditorCaption(String line, Speaker speaker, String Begin, String End)
        {
            //Set Timestamps. Duration is implicity set.
            this.Begin = new Timestamp(Begin);
            this.End = new Timestamp(End);

            //Set other Caption properties
            this.Speaker = speaker;
            this.Location = ScreenLocation.BottomCentre;
            this.Alignment = Alignment.Center;

            //Set up word list and feed it words
            this.Words = new List<EditorCaptionWord>();
            this.Feed(line);
        }
        #endregion

        #region MoveTo
        /// <summary>
        /// Returns the text sentence that this caption represents.
        /// Calls the WordListText method, and returns its value.
        /// </summary>
        /// <returns>The text of Words's words</returns>
        public override void MoveTo(double beginTime)
        {
            base.MoveTo(beginTime);
            NotifyPropertyChanged(PropertyNames.Begin);
        }
        #endregion

        #region AsString Setter and Getter
        /// <summary>
        /// Clears the list, then feeds a string into the list and turns it into CaptionWords.
        /// This method is hidden from the parent class Caption as it modifies a different property
        /// with the same name (the Words property).
        /// </summary>
        /// <param name="line">The string to turn into a list of CaptionWords.</param>
        public new void Feed(String line)
        {
            //Remove the previous line from the Words
            this.Words.Clear();

            //Split line up and add each word to the wordlist.
            String[] words = line.Split(); //Separate by spaces

            int cumulativePosition = 0;
            EditorCaptionWord cw;

            foreach (String word in words)
            {
                if (word != "")
                {
                    cw = new EditorCaptionWord(word, cumulativePosition);
                    this.Words.Add(cw);
                    cumulativePosition += cw.Length + SPACE_WIDTH;
                }
            }
            NotifyPropertyChanged(PropertyNames.Words);
        }

        /// <summary>
        /// Turns the list into a single String. This method is overriden from the Parent class 
        /// Caption as it modifies a different property with the same name (The Words property).
        /// </summary>
        /// <returns>A string containing all the CaptionWords in the list.</returns>
        public override String GetAsString()
        {
            //Stringbuilder is faster than String when it comes to appending text.
            StringBuilder s = new StringBuilder();
            //For every element but the last
            for (int i = 0; i < Words.Count - 1; i++)
            {
                s.Append(Words[i].ToString());
                s.Append(" ");
            }
            //Append the last element without adding a space after it
            if (0 < Words.Count)
                s.Append(Words[Words.Count - 1].ToString());

            return s.ToString();
        }
        #endregion
    }
    #endregion
}