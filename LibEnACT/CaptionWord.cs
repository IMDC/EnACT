namespace LibEnACT
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
        public const Emotion DefaultEmotion = Emotion.None;

        /// <summary>
        /// The default intensity used when constructing a new CaptionWord.
        /// </summary>
        public const Intensity DefaultIntensity = Intensity.Low;

        private int _beginIndex;

        public int BeginIndex
        {
            internal set
            {
                _beginIndex = value;
                EndIndex = value + Length;
            }
            get { return _beginIndex; }
        }

        public int EndIndex { private set; get; }

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
        public string Text { private set; get; }    //The part wrapped in <emotion> tag

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
        public CaptionWord() : this(string.Empty) { }

        /// <summary>
        /// Constructs a CaptionWord with the specified text string and default Emotion and 
        /// Intensity values.
        /// </summary>
        /// <param name="text">The word string to make this EditorCaptionWord represent.</param>
        public CaptionWord(string text) : this(DefaultEmotion, DefaultIntensity, text, 0) { }

        public CaptionWord(string text, int beginIndex)
            : this(DefaultEmotion, DefaultIntensity, text, beginIndex)
        {}

        /// <summary>
        /// Constructs a CaptionWord with the specified parameters.
        /// </summary>
        /// <param name="e">The emotion of this CaptionWord.</param>
        /// <param name="i">The intensity of the emotion of this word.</param>
        /// <param name="text">The word string to make this CaptionWord represent.</param>
        public CaptionWord(Emotion e, Intensity i, string text, int beginIndex)
        {
            this.Emotion = e;
            this.Intensity = i;
            this.Text = text;
            this.BeginIndex = beginIndex;
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
            return (BeginIndex <= charIndex && charIndex <= EndIndex );
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
                    ||  (selectionStart <= BeginIndex && EndIndex <= selectionEnd));
        }

        /// <summary>
        /// Returns the text that this word represents
        /// </summary>
        /// <returns>The text value</returns>
        public override string ToString() { return Text; }
        #endregion
    } //Class
} //Namespace
