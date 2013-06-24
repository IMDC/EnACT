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
        public int BeginIndex { set; get; }
        public int EndIndex { set; get; }

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
        public CaptionWord(String text, int beginIndex)
        {
            this.Emotion = Emotion.None;
            this.Intensity = Intensity.None;
            this.Text = text;
            this.BeginIndex = beginIndex;
            this.EndIndex = beginIndex + Length;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks to see if a charIndex is contained in this CaptionWord
        /// </summary>
        /// <param name="charIndex">The index of the character to check</param>
        /// <returns>True if index is contained in the word, false if otherwise</returns>
        public bool Contains(int charIndex)
        {
            return (BeginIndex < charIndex && charIndex < EndIndex 
                || (Length == 1 && BeginIndex == charIndex))
                ? true : false;
        }

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
