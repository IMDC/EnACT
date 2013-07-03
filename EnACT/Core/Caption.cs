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
    public class BaseCaption
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
        public virtual CaptionWordList Words { set; get; }
        #endregion //#region Properties and Fields

        #region Timestamp Properties
        /// <summary>
        /// Backing field for the Begin property.
        /// </summary>
        private Timestamp begin;
        /// <summary>
        /// Backing field for the End property.
        /// </summary>
        private Timestamp end;
        /// <summary>
        /// Backing field for the Duration property.
        /// </summary>
        private Timestamp duration;

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public virtual Timestamp Begin
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
            }
            get { return begin; }
        }

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public virtual Timestamp End
        {
            set
            {
                end = value;
                if (begin != null)
                    duration = end - begin;
            }
            get { return end; }
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
                duration = value;
                if (begin != null)
                    end = begin + duration;
                //Assume a null value would be 0.0 seconds
                else
                    //Create a new object instead of copying refrences.
                    end = new Timestamp(duration.AsDouble);
            }
            get { return duration; }
        }
        #endregion //#region Timestamp Properties

        #region Constructor
        /// <summary>
        /// Constructs a Caption object with a blank line and the DefaultSpeaker
        /// </summary>
        public BaseCaption() : this("", Speaker.Default) { }

        /// <summary>
        /// Constructs a Caption object with a line, and uses a reference to the
        /// line's speaker.
        /// </summary>
        /// <param name="line">Text to be displayed as a caption</param>
        /// <param name="speaker">The speaker of the caption</param>
        public BaseCaption(String line, Speaker speaker) : this(line, speaker, "00:00:00.0", "00:00:00.0") { }

        /// <summary>
        /// Constructs a Caption object with a given line, speaker, and begining 
        /// and ending timestamps
        /// </summary>
        /// <param name="line">Text to be displayed as a caption</param>
        /// <param name="speaker">The speaker of the caption</param>
        /// <param name="Begin">The timestamp representing the beginning of the caption</param>
        /// <param name="End">The timestamp representing the ending of the caption</param>
        public BaseCaption(String line, Speaker speaker, String Begin, String End)
        {
            this.Begin = new Timestamp(Begin);
            this.End = new Timestamp(End);

            this.Speaker = speaker;
            this.Location = ScreenLocation.BottomCentre;
            this.Alignment = Alignment.Center;

            this.Words = new CaptionWordList(line);
        }
        #endregion

        #region MoveTo
        /// <summary>
        /// Moves the caption to a new starting and ending point, maintaining the same Duration
        /// </summary>
        /// <param name="beginTime">The time this Caption will begin at</param>
        public virtual void MoveTo(double beginTime)
        {
            this.begin = beginTime;
            this.end = begin + duration;
        }
        #endregion

        #region ToString
        /// <summary>
        /// Returns the text sentence that this caption represents.
        /// Calls the WordListText method, and returns its value.
        /// </summary>
        /// <returns>The text of Words's words</returns>
        public override string ToString() { return this.Words.GetAsString(); }
        #endregion
    }
}
