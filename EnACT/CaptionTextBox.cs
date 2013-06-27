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
    public class CaptionTextBox : RichTextBox
    {
        #region Fields and Properties
        /// <summary>
        /// Set this bool to true to bypass the OnSelectionChanged method.
        /// </summary>
        private bool simpleSelectFlag = false;

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

        #region Events
        public event EventHandler<CaptionWordSelectedEventArgs> CaptionWordSelected;
        #endregion

        #region Constructor
        public CaptionTextBox() : base() { }
        #endregion

        #region Methods
        /// <summary>
        /// Method that is called when the caret or selection of the textbox is changed.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            Console.WriteLine("Index: {0}, Length: {1}, Text: \"{2}\"", SelectionStart, SelectionLength, SelectedText);

            /* In order to avoid a StackOverflow exception, this method will return before calling
             * HighlightCurrentWord. This is due to the fact that in order to highlight a word, it
             * needs to be selected, and when it is selected this event gets fired. It is not the
             * best design, but it currently seems to be the only way to work around this control.
             */
            if (simpleSelectFlag)
            {
                simpleSelectFlag = false;
                return;
            }

            //Only call the method when just the caret is being displayed.
            if(SelectionLength == 0)
                HighlightCurrentWord();
        }

        /// <summary>
        /// Highlights the word that the caret is in.
        /// </summary>
        private void HighlightCurrentWord()
        {
            int caret = SelectionStart;

            foreach (CaptionWord cw in Caption.WordList)
            {
                //If the word contains the caret but hasn't been selected yet, highlight and then
                //select it.
                if (cw.Contains(caret) && !cw.IsSelected)
                {
                    SetTextBackgroundColour(cw, SystemColors.Highlight);
                    cw.IsSelected = true;
                    OnCaptionWordSelected(new CaptionWordSelectedEventArgs(cw));
                }
                //If the word doesn't contain the caret but is selected, then unselect it.
                else if(!cw.Contains(caret) && cw.IsSelected)
                {
                    SetTextBackgroundColour(cw, Color.White);
                    cw.IsSelected = false;
                }
            }
        }
        #endregion

        #region SetTextBackgroundColour
        /// <summary>
        /// Sets the background colour of the text specified by the arguments. Preserves the previous
        /// selection, restoring it after highlighting is done.
        /// </summary>
        /// <param name="word">The CaptionWord to highlight</param>
        /// <param name="highlightColour">The colour to change the background colour to.</param>
        public void SetTextBackgroundColour(CaptionWord word, Color highlightColour)
        { this.SetTextBackgroundColour(word.BeginIndex, word.Length, highlightColour); }

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
            SimpleSelect(startPos, length);
            SelectionBackColor = highlightColour;

            //Reselect old values
            SimpleSelect(oldStart, oldLength);
        }
        #endregion

        #region SimpleSelect
        /// <summary>
        /// Calls the Select method as well as setting the simpleSelectFlag variable to true,
        /// making the onSelectionChanged method return immediately.
        /// </summary>
        /// <param name="start">The position of the first character in the current text selection 
        /// within the text box. </param>
        /// <param name="length">The number of characters to select. </param>
        private void SimpleSelect(int start, int length)
        {
            simpleSelectFlag = true;
            Select(start, length);
        }
        #endregion

        #region Event Invocation Methods
        /// <summary>
        /// Invokes the CaptionWordSelected event, which happens when a single Caption Word
        /// is selected.
        /// </summary>
        /// <param name="e">Event Args</param>
        private void OnCaptionWordSelected(CaptionWordSelectedEventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler<CaptionWordSelectedEventArgs> handler = CaptionWordSelected;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
    }//Class
}//Namespace
