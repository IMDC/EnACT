using System.Collections.Generic;
using System.Text;

namespace LibEnACT
{
    /// <summary>
    /// Represents a Caption used by EnACT.
    /// </summary>
    public class Caption
    {
        #region Properties and Fields

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
        public virtual CaptionWordCollection Words { private set; get; }

        /// <summary>
        /// The text of a Caption. Setting this value will also populate or re-populate the Words
        /// list with the words derived from the caption text.
        /// </summary>
        public virtual string Text
        {
            set { Words.Feed(value);}
            get { return Words.ToString(); }
        }
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
            this.bkBegin = beginTime;
            this.bkEnd = bkBegin + bkDuration;
        }
        #endregion

        public void SetWordList(List<CaptionWord> cwList)
        {
            
        }

        #region ToString
        /// <summary>
        /// Returns the text sentence that this caption represents.
        /// Calls the WordListText method, and returns its value.
        /// </summary>
        /// <returns>The text of Words's words</returns>
        public override string ToString() { return Words.ToString(); }
        #endregion
    }
}
