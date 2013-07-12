using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
{
    /// <summary>
    /// Represents a Caption used by EnACT.
    /// </summary>
    public class Caption
    {
        #region Properties and Fields
        /// <summary>
        /// The width of spacing between words.
        /// </summary>
        public const int SpaceWidth = 1;

        /// <summary>
        /// A reference to a speaker in the program's speaker list.
        /// </summary>
        public virtual Speaker Speaker { set; get; }

        /// <summary>
        /// The location of a caption on the screen (Eg top left, centre right, etc)
        /// </summary>
        public virtual ScreenLocation Location { set; get; }

        /// <summary>
        /// The textual alignment of a caption (eg Left, Centre, Right)
        /// </summary>
        public virtual Alignment Alignment { set; get; }

        /// <summary>
        /// The list of words in the caption
        /// </summary>
        public virtual List<CaptionWord> Words { set; get; }
        #endregion //#region Properties and Fields

        #region Timestamp Properties
        /// <summary>
        /// Backing field for the Begin property.
        /// </summary>
        private Timestamp bkBegin;
        /// <summary>
        /// Backing field for the End property.
        /// </summary>
        private Timestamp bkEnd;
        /// <summary>
        /// Backing field for the Duration property.
        /// </summary>
        private Timestamp bkDuration;

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public virtual Timestamp Begin
        {
            set
            {
                bkBegin = value;
                if (bkEnd != null)
                {
                    if (bkBegin < bkEnd)
                        bkDuration = bkEnd - bkBegin;
                    else
                    {
                        //Make end time the same as begin time
                        bkEnd = new Timestamp(bkBegin.AsDouble);
                        bkDuration = 0;
                    }
                }
            }
            get { return bkBegin; }
        }

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public virtual Timestamp End
        {
            set
            {
                bkEnd = value;
                if (bkBegin != null)
                    bkDuration = bkEnd - bkBegin;
            }
            get { return bkEnd; }
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
                bkDuration = value;
                if (bkBegin != null)
                    bkEnd = bkBegin + bkDuration;
                //Assume a null value would be 0.0 seconds
                else
                    //Create a new object instead of copying refrences.
                    bkEnd = new Timestamp(bkDuration.AsDouble);
            }
            get { return bkDuration; }
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
            //Set Timestamps. Duration is implicity set.
            this.Begin = new Timestamp(Begin);
            this.End = new Timestamp(End);

            //Set other Caption properties
            this.Speaker = speaker;
            this.Location = ScreenLocation.BottomCentre;
            this.Alignment = Alignment.Center;

            //Set up word list and feed it words
            this.Words = new List<CaptionWord>();
            this.Feed(line);
        }
        #endregion

        #region MoveTo
        /// <summary>
        /// Moves the caption to a new starting and ending point, maintaining the same Duration
        /// </summary>
        /// <param name="beginTime">The time this Caption will begin at</param>
        public virtual void MoveTo(double beginTime)
        {
            this.bkBegin = beginTime;
            this.bkEnd = bkBegin + bkDuration;
        }
        #endregion

        #region AsString Setter and Getter
        /// <summary>
        /// Clears the list, then feeds a string into the list and turns it into CaptionWords.
        /// </summary>
        /// <param name="line">The string to turn into a list of CaptionWords.</param>
        public virtual void Feed(String line)
        {
            //Remove the previous line from the Words
            Words.Clear();

            //Split line up and add each word to the wordlist.
            String[] words = line.Split(); //Separate by spaces

            foreach (String word in words)
            {
                if (word != "") { Words.Add(new CaptionWord(word)); }
            }
        }

        /// <summary>
        /// Turns the list into a single String.
        /// </summary>
        /// <returns>A string containing all the CaptionWords in the list.</returns>
        public virtual String GetAsString()
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

        #region ToString
        /// <summary>
        /// Returns the text sentence that this caption represents.
        /// Calls the WordListText method, and returns its value.
        /// </summary>
        /// <returns>The text of Words's words</returns>
        public override string ToString() { return this.GetAsString(); }
        #endregion
    }
}
