using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
{
    /// <summary>
    /// Represents an emotive word used by EnACT.
    /// </summary>
    public class CaptionWord
    {
        #region Constants, Fields and Properties
        /// <summary>
        /// The default emotion used when constructing a new CaptionWord.
        /// </summary>
        public const Emotion DEFAULT_EMOTION = Emotion.None;

        /// <summary>
        /// The default intensity used when constructing a new CaptionWord.
        /// </summary>
        public const Intensity DEFAULT_INTENSITY = Intensity.Low;

        /// <summary>
        /// Represents the type of emotion (Happy, Sad, etc) of this CaptionWord
        /// </summary>
        public Emotion Emotion { set; get; }

        /// <summary>
        /// The level of intensity of the emotion
        /// </summary>
        public Intensity Intensity { set; get; }

        /// <summary>
        /// The text of the CaptionWord.
        /// </summary>
        public String Text { set; get; }    //The part wrapped in <emotion> tag

        /// <summary>
        /// Returns the length of the Text string. It is the same value as Text.Length
        /// </summary>
        public int Length { get { return Text.Length; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a Caption word with an empty text string, and default Emotion and Intensity
        /// values.
        /// </summary>
        public CaptionWord() : this(String.Empty) { }

        /// <summary>
        /// Constructs a CaptionWord with the specified text string and default Emotion and 
        /// Intensity values.
        /// </summary>
        /// <param name="text">The word string to make this EditorCaptionWord represent.</param>
        public CaptionWord(String text) : this(DEFAULT_EMOTION, DEFAULT_INTENSITY, text) { }

        /// <summary>
        /// Constructs a CaptionWord with the specified parameters.
        /// </summary>
        /// <param name="e">The emotion of this CaptionWord.</param>
        /// <param name="i">The intensity of the emotion of this word.</param>
        /// <param name="text">The word string to make this CaptionWord represent.</param>
        public CaptionWord(Emotion e, Intensity i, String text)
        {
            this.Emotion = e;
            this.Intensity = i;
            this.Text = text;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the text that this word represents
        /// </summary>
        /// <returns>The text value</returns>
        public override string ToString() { return Text; }
        #endregion
    } //Class
} //Namespace
