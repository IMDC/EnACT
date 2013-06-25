using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace EnACT
{
    /// <summary>
    /// A class meant for marking up Captions with emotions.
    /// </summary>
    class CaptionTextBox : RichTextBox
    {
        #region Fields and Properties
        /// <summary>
        /// Backing field for Caption Property.
        /// </summary>
        private Caption caption;
        /// <summary>
        /// The Caption currently displayed in the Text Box
        /// </summary>
        public Caption Caption 
        {
            set
            {
                //If null clear the text and Caption
                if (value == null)
                {
                    caption = null;
                    Text = String.Empty;
                }
                else
                {
                    caption = value;
                    Text = caption.ToString();
                }
            }
            get { return caption; }
        }
        #endregion

        #region Constructor
        public CaptionTextBox() : base() { }
        #endregion

        #region Methods
        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            Console.WriteLine("Index: {0}, Length: {1}, Text: \"{2}\"",SelectionStart, SelectionLength, SelectedText);

            int start = SelectionStart;
            int length = 0;

            //Look to see if a word is to be selected
            for (int i = 0; i < Caption.WordList.Count; i++)
            {
                if (Caption.WordList[i].Contains(SelectionStart))
                {
                    start = Caption.WordList[i].BeginIndex;
                    length = Caption.WordList[i].Length + CaptionWordList.SPACE_WIDTH;
                    Select(start, length);
                    break;
                }
            }
        }

        /// <summary>
        /// Sets the background colour of the text specified by the arguments. Preserves the previous
        /// selection, restoring it after highlighting is done.
        /// </summary>
        /// <param name="startPos">Start position of text to be set.</param>
        /// <param name="length">Length of text to be set.</param>
        /// <param name="highlightColour">The colour to change the background colour to.</param>
        public void SetTextBackgroundColour(int startPos, int length, Color highlightColour)
        {
            //Save old values
            int oldStart = SelectionStart;
            int oldLength = SelectionLength;

            //Colour given values
            Select(startPos, length);
            SelectionBackColor = System.Drawing.Color.Green;

            //Reselect old values
            Select(oldStart, oldLength);
        }
        #endregion
    }
}
