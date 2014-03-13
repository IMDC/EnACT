using LibEnACT;

namespace EnACT.Core
{
    /// <summary>
    /// Represents an emotive word used by the Editor. Contains indexes and a selection flag for
    /// use in marking it up.
    /// </summary>
    public class EditorCaptionWord : CaptionWord
    {
        #region Fields and Properties
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
            : base(e, i, text, beginIndex)
        {
            //Set word to unselected
            this.IsSelected = false;
        }
        #endregion
    } //Class
} //Namespace
