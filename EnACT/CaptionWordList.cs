﻿using System;
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
        #region Constructors
        public CaptionWordList() : base() { }

        public CaptionWordList(String line) : base() { }
        #endregion

        #region Methods
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
            foreach (String word in words)
            {
                if (word != "")
                    Add(new CaptionWord(word));
            }
        }

        /// <summary>
        /// Turns the list into a single String.
        /// </summary>
        /// <returns>A string containing all the CaptionWords in the list.</returns>
        public String Text()
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

        /// <summary>
        /// Returns the CaptionWordList represented as a String.
        /// </summary>
        /// <returns>The CaptionWordList represented as a String.</returns>
        public override string ToString()
        {
            return Text();
        }
        #endregion
    }
}