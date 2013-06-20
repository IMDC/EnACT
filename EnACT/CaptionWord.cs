using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnACT
{
    /// <summary>
    /// Represents an emotive word in a caption.
    /// </summary>
    public class CaptionWord
    {
        #region Fields and Properties
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

        public int Length
        {
            get { return Text.Length; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates an emotionless CaptionWord object with the inputted text
        /// </summary>
        /// <param name="text">The text to be emoted</param>
        public CaptionWord(String text)
        {
            this.Emotion = Emotion.None;
            this.Intensity = Intensity.None;
            this.Text = text;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the text that this word represents
        /// </summary>
        /// <returns>The text value</returns>
        public override string ToString()
        {
            return Text;
        }
        #endregion
    }
}
