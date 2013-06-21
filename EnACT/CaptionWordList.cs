using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
{
    /// <summary>
    /// A List for Containing CaptionWords
    /// </summary>
    public class CaptionWordList : List<CaptionWord>
    {
        #region Fields and Properties
        private const int SPACE_WIDTH = 1;

        /// <summary>
        /// Gets or sets the CaptionWordList as a String.
        /// </summary>
        public String AsString
        {
            set { Feed(value); }
            get { return GetAsString(); }
        }
        #endregion

        #region Constructors
        public CaptionWordList() : base() {}

        public CaptionWordList(String line) : this()
        {
            Feed(line);
        }
        #endregion

        #region AsString Setter and Getter
        /// <summary>
        /// Clears the list, then feeds a string into the list and turns it into CaptionWords.
        /// </summary>
        /// <param name="line">The string to turn into a list of CaptionWords.</param>
        public void Feed(String line)
        {
            //Remove the previous line from the WordList
            Clear();

            //Split line up and add each word to the wordlist.
            String[] words = line.Split(); //Separate by spaces

            int cumulativePosition = 0;
            CaptionWord cw;

            foreach (String word in words)
            {
                if (word != "")
                {
                    cw = new CaptionWord(word, cumulativePosition);
                    Add(cw);
                    cumulativePosition += cw.Length + SPACE_WIDTH;
                }
            }
        }

        /// <summary>
        /// Turns the list into a single String.
        /// </summary>
        /// <returns>A string containing all the CaptionWords in the list.</returns>
        public String GetAsString()
        {
            //Stringbuilder is faster than String when it comes to appending text.
            StringBuilder s = new StringBuilder();
            //For every element but the last
            for (int i = 0; i < Count - 1; i++)
            {
                s.Append(this[i].ToString());
                s.Append(" ");
            }
            //Append the last element without adding a space after it
            if (0 < Count)
                s.Append(this[Count - 1].ToString());

            return s.ToString();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the index of the CaptionWord that would contain the specified character
        /// indexed at stringIndex.
        /// </summary>
        /// <param name="stringIndex">The index of the total string to look from.</param>
        /// <returns>The index of the CaptionWord that contains the stringIndex.</returns>
        public int GetCaptionWordIndexAt(int stringIndex)
        {
            //Ensure string index is not less than 0
            if (stringIndex < 0)
                throw new ArgumentOutOfRangeException("stringIndex",
                    "Argument stringIndex out of range: " + stringIndex);

            int cumulativeLength = 0;
            int i;

            for (i = 0; i < Count; i++)
            {
                if (stringIndex < cumulativeLength)
                    break;
                else if (i == Count - 1)
                    cumulativeLength += this[i].Length;
                else
                    cumulativeLength += this[i].Length + SPACE_WIDTH;
            }

            return i-1;
        }

        /// <summary>
        /// Gets the string index of the first character of the CaptionWord specified by captionWordIndex.
        /// </summary>
        /// <param name="captionWordIndex">The index of the CaptionWord to use.</param>
        /// <returns>The CaptionWord's first character's index in the string of words.</returns>
        public int GetStringStartIndexAt(int captionWordIndex)
        {
            //Ensure string index is not less than 0 or greater than word Count
            if (captionWordIndex < 0 || this.Count <= captionWordIndex)
                throw new ArgumentOutOfRangeException("captionWordIndex", 
                    "Argument captionWordIndex out of range: " + captionWordIndex);

            int stringIndexPos = 0;

            for (int i = 0; i < captionWordIndex; i++)
            {
                stringIndexPos += this[i].Length;

                //Add a space if not the last word.
                if (i < Count - 1)
                    stringIndexPos += SPACE_WIDTH;
            }

            return stringIndexPos;
        }

        /// <summary>
        /// Returns the CaptionWordList represented as a String.
        /// </summary>
        /// <returns>The CaptionWordList represented as a String.</returns>
        public override string ToString()
        {
            return GetAsString();
        }
        #endregion
    }
}
