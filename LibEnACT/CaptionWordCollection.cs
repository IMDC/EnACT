using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibEnACT
{
    public class CaptionWordCollection : ReadOnlyCollectionBase
    {
        #region Constants
        /// <summary>
        /// The default value for the SpaceWidth Property.
        /// </summary>
        public const int DefaultSpaceWidth = 1;
        #endregion

        /// <summary>
        /// The List of words that this collection wraps.
        /// </summary>
        private List<CaptionWord> _wordList;

        /// <summary>
        /// The width of spacing between words.
        /// </summary>
        public int SpaceWidth { set; get; }

        /// <summary>
        /// Gets the size of this collection.
        /// </summary>
        public override int Count
        {
            get { return _wordList.Count; }
        }

        /// <summary>
        /// Retrieves the CaptionWord at the specified index in this Collection.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>The element at the given index.</returns>
        public CaptionWord this[int index]
        {
            get { return _wordList[index]; }
        }

        /// <summary>
        /// Creates a CaptionWordCollection with a given line and spaceWidth.
        /// </summary>
        /// <param name="line">The line to split into CaptionWords.</param>
        /// <param name="spaceWidth">The number of space characters between each word.</param>
        public CaptionWordCollection(string line, int spaceWidth = DefaultSpaceWidth)
        {
            this.SpaceWidth = spaceWidth;
            this._wordList = new List<CaptionWord>();
            Feed(line);
        }

        /// <summary>
        /// Creates a CaptionWordCollection with a given CaptionWord List. The collection will re-
        /// calculate the indecies of each CaptionWord.
        /// </summary>
        /// <param name="cwList">An already constructed list of CaptionWords.</param>
        /// <param name="spaceWidth">The number of space characters between each word.</param>
        public CaptionWordCollection(List<CaptionWord> cwList, int spaceWidth = DefaultSpaceWidth)
        {
            this.SpaceWidth = spaceWidth;
            SetWordList(cwList);
        }

        /// <summary>
        /// Sets this collection's internal list to the given list, and recalculates the indecies of
        /// each CaptionWord.
        /// </summary>
        /// <param name="cwList">An already constructed list of CaptionWords.</param>
        internal void SetWordList(List<CaptionWord> cwList)
        {
            this._wordList = cwList;
            CalculateWordIndecies();
        }

        /// <summary>
        /// Gets an enumerator for this collection.
        /// </summary>
        /// <returns>An enumerator for this collection.</returns>
        public override IEnumerator GetEnumerator()
        {
            return _wordList.GetEnumerator();
        }

        /// <summary>
        /// Calculates the begin and end indecies for each caption word in this collection.
        /// </summary>
        private void CalculateWordIndecies()
        {
            int cumulativeIndex = 0;

            foreach (CaptionWord cw in _wordList)
            {
                cw.BeginIndex = cumulativeIndex;
                cumulativeIndex += cw.Length + SpaceWidth;
            }
        }

        /// <summary>
        /// Splits a string into a list of CaptionWords for this collection.
        /// </summary>
        /// <param name="line">The line to split into CaptionWords.</param>
        internal void Feed(String line)
        {
            //Remove the previous line from the Words
            _wordList.Clear();

            if (String.IsNullOrWhiteSpace(line))
            {
                _wordList.Add(new CaptionWord(""));
                return;
            }

            //Split line up and add each word to the wordlist.
            string[] words = line.Split(); //Separate by spaces
            int cumulativeIndex = 0;

            foreach (string word in words)
            {
                if (word != "")
                {
                    _wordList.Add(new CaptionWord(word, cumulativeIndex));
                    cumulativeIndex += word.Length + SpaceWidth;
                }
                else
                {
                    //Add a space to represent the empty word
                    cumulativeIndex += 1;
                }
            }
        }

        /// <summary>
        /// Gets a string representation of this CaptionWordCollection. The collection is returned
        /// as a list of all the CaptionWords with spaces of size SpaceWidth padding them.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //Stringbuilder is faster than string when it comes to appending text.
            StringBuilder s = new StringBuilder();
            //For every element but the last
            for (int i = 0; i < _wordList.Count - 1; i++)
            {
                s.Append(_wordList[i].ToString());
                s.Append(new string(' ', SpaceWidth));
            }
            //Append the last element without adding a space after it
            if (0 < _wordList.Count)
                s.Append(_wordList[_wordList.Count - 1].ToString());

            return s.ToString();
        }
    }
}
