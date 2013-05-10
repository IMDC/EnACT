using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EnACT
{
    #region Caption Enums
    /// <summary>
    /// The Emotion enum represents which type of emotion the caption will
    /// be displayed in.
    /// </summary>
    public enum Emotion 
    {
		Unknown = -1,
		None = 0,
		Happy = 1,
		Sad = 2,
		Fear = 3,
		Anger = 4,
	};

    /// <summary>
    /// The Intensity enum represents how intense the emotion in a caption will be.
    /// </summary>
	public enum Intensity 
    {
		None = 0,
		Low = 1,
		Medium = 2,
		High = 3
	};

    /// <summary>
    /// The Location Enum represents in which of the 9 possible areas a caption
    /// will be displayed in.
    /// </summary>
	public enum ScreenLocation 
    {
		BottomLeft = 1,
		BottomCentre = 2,
		BottomRight = 3,
		MiddleLeft = 4,
		MiddleCenter = 5,
		MiddleRight = 6,
		TopLeft = 7,
		TopCentre = 8,
		TopRight = 9
	};

    /// <summary>
    /// The Alignment enum represents the text alignment of a caption.
    /// </summary>
	public enum Alignment
    {
		Left = 0,
		Center = 1,
		Right = 2
	}
    #endregion

    #region Caption Class
    /// <summary>
    /// The caption Class represents a Captioned line of text.
    /// </summary>
    public class Caption
    {
        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public Timestamp Begin { set; get; }

        /// <summary>
        /// A timestamp representing the begin time of a caption. Set in the 
        /// form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public Timestamp End { set; get; }

        /// <summary>
        /// A reference to a speaker in the program's speaker list.
        /// </summary>
        public Speaker Speaker { set; get; }

        /// <summary>
        /// The location of a caption on the screen (Eg top left, centre right, etc)
        /// </summary>
        public ScreenLocation Location { set; get;}

        /// <summary>
        /// The textual alignment of a caption (eg Left, Centre, Right)
        /// </summary>
        public Alignment Alignment { set; get; }

        /// <summary>
        /// The list of words in the caption
        /// </summary>
        public List<CaptionWord> WordList { set; get; }

        /// <summary>
        /// Constructs a Caption object with a blank line and the DefaultSpeaker
        /// </summary>
        public Caption() : this("", MainForm.DefaultSpeaker) { }
                             
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
            this.Begin = new Timestamp(Begin);
            this.End = new Timestamp(End);

            this.Speaker = speaker;
            this.Location = ScreenLocation.BottomCentre;
            this.Alignment = Alignment.Center;

            this.WordList = new List<CaptionWord>();

            this.FeedWordList(line);
        }

        /// <summary>
        /// Takes in a string and feeds it into the wordlist, splitting it up
        /// and turning each word in the string into a captionWord
        /// </summary>
        /// <param name="line">The String to enter</param>
        public void FeedWordList(String line)
        {
            //Remove the previous line from the WordList
            WordList.Clear();

            //Split line up and add each word to the wordlist.
            String[] words = line.Split(); //Separate by spaces
            foreach (String word in words)
            {
                if (word != "")
                    this.WordList.Add(new CaptionWord(word));
            }
        }

        /// <summary>
        /// Returns the captions in WordList as a string, with each CaptionWord being
        /// separated by spaces (' ').
        /// </summary>
        /// <returns>A String representation of wordlist</returns>
        public String WordListText()
        {
            //Stringbuilder is faster than String when it comes to appending text.
            StringBuilder s = new StringBuilder();
            //For every element but the last
            for (int i = 0; i < WordList.Count - 1; i++)
            {
                s.Append(WordList[i].ToString());
                s.Append(" ");
            }
            //Append the last element without adding a space after it
            if(0 < WordList.Count)
                s.Append(WordList[WordList.Count - 1].ToString());
            return s.ToString();
        }

        /// <summary>
        /// Returns the text sentence that this caption represents.
        /// Calls the WordListText method, and returns its value.
        /// </summary>
        /// <returns>The text of WordList's words</returns>
        public override string ToString()
        {
            return this.WordListText();
        }
    }
    #endregion
}