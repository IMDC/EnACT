using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnACT
{
    /// <summary>
    /// Represents an emotive word used by the Editor. Contains indexes and a selection flag for
    /// use in marking it up.
    /// </summary>
    public class EditorCaptionWord : CaptionWord
    {
        #region Fields and Properties
        /// <summary>
        /// Backing field for BeginIndex
        /// </summary>
        private int beginIndex;
        /// <summary>
        /// The index of a caption that this word starts at.
        /// </summary>
        public int BeginIndex
        {
            set
            {
                beginIndex = value;
                EndIndex = value + Length;
            }
            get { return beginIndex; }
        }

        /// <summary>
        /// The index of a caption that this word ends at.
        /// </summary>
        public int EndIndex { private set; get; }

        /// <summary>
        /// Whether or not this value is currently selected.
        /// </summary>
        public bool IsSelected { set; get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs an EditorCaptionWord with a line of text and a begin index of 0. 
        /// NOTE: BeginIndex should be set after construction.
        /// </summary>
        /// <param name="text">The word string to make this EditorCaptionWord represent.</param>
        public EditorCaptionWord(string text) : this(text, beginIndex: 0) { }

        /// <summary>
        /// Constructs an EditorCaptionWord with the specified parameters, as well as default 
        /// emotion and intensity.
        /// </summary>
        /// <param name="text">The word string to make this EditorCaptionWord represent.</param>
        /// <param name="beginIndex">The index of this EditorCaptionWord in a string of 
        /// words.</param>
        public EditorCaptionWord(string text, int beginIndex) : 
            this(DefaultEmotion, DefaultIntensity, text, beginIndex) { }

        /// <summary>
        /// Constructs an EditorCaptionWord with the specified parameters.
        /// </summary>
        /// <param name="e">The emotion of this word.</param>
        /// <param name="i">The intensity of the emotion of this word.</param>
        /// <param name="text">The word string to make this EditorCaptionWord represent.</param>
        /// <param name="beginIndex">The index of this EditorCaptionWord in a string of 
        /// words.</param>
        public EditorCaptionWord(Emotion e, Intensity i, string text, int beginIndex)
            : base(e, i, text)
        {
            //Set index positions
            this.BeginIndex = beginIndex;

            //Set word to unselected
            this.IsSelected = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks to see if a charIndex is contained in this EditorCaptionWord
        /// </summary>
        /// <param name="charIndex">The index of the character to check</param>
        /// <returns>True if index is contained in the word, false if otherwise</returns>
        public bool Contains(int charIndex)
        {
            return (BeginIndex <= charIndex && charIndex <= EndIndex )
                ? true : false;
        }

        /// <summary>
        /// Checks to see if this EditorCaptionWord is contained in the given selection.
        /// </summary>
        /// <param name="selectionStart">The start position of the selection.</param>
        /// <param name="selectionLength">The length of the selection.</param>
        /// <returns>True if the selection</returns>
        public bool ContainedInSelection(int selectionStart, int selectionLength)
        {
            int selectionEnd = selectionStart + selectionLength;
            return ((BeginIndex <= selectionStart && selectionStart <= EndIndex)
                ||  (BeginIndex <= selectionEnd && selectionEnd <= EndIndex)
                ||  (selectionStart <= BeginIndex && EndIndex <= selectionEnd))
                ? true : false;
        }
        #endregion
    } //Class
} //Namespace
