using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace EnACT
{
    #region Enum
    /// <summary>
    /// What is currently selected by the CaptionTextBox.
    /// </summary>
    public enum CaptionTextBoxSelectionMode
    {
        NoSelection,
        SingleWordSelection,
        MultiWordSelection,
    }
    #endregion

    #region CaptionTextBox Class
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
        /// The text contained by this CaptionTextBox before the previous selection change.
        /// </summary>
        private string previousTextString = null;

        public CaptionTextBoxSelectionMode SelectionMode { set; get; }

        /// <summary>
        /// Backing field for Caption Property.
        /// </summary>
        private EditorCaption caption;
        /// <summary>
        /// The Caption currently displayed in the Text Box
        /// </summary>
        public EditorCaption Caption 
        {
            set
            {
                //If null clear the text and Caption
                if (value == null)
                    caption = null;
                else
                {
                    caption = value;
                    Text = caption.ToString();
                    foreach (EditorCaptionWord cw in Caption.Words)
                    {
                        //Return it to the original Caption colour
                        SetTextBackgroundColour(cw, CaptionStyle.GetColourOf(cw));
                    }
                }
            }
            get { return caption; }
        }
        #endregion

        #region Events
        /// <summary>
        /// An event that is fired when no captionword is currently selected by the user.
        /// </summary>
        public event EventHandler NothingSelected;

        /// <summary>
        /// An event that is fired when a single EditorCaptionWord is selected by the user.
        /// </summary>
        public event EventHandler<CaptionWordSelectedEventArgs> CaptionWordSelected;
        
        /// <summary>
        /// An event that is fired when more than 1 EditorCaptionWord is selected by the user.
        /// </summary>
        public event EventHandler MultipleCaptionWordsSelected;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a CaptionTextBox object.
        /// </summary>
        public CaptionTextBox() : base() 
        {
            this.SelectionMode = CaptionTextBoxSelectionMode.NoSelection;
            this.HideSelection = false;
        }
        #endregion

        #region OnSelectionChanged
        /// <summary>
        /// Method that is called when the caret or selection of the textbox is changed.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            //Console.WriteLine("Index: {0}, Length: {1}, Text: \"{2}\"", SelectionStart, 
            //    SelectionLength, SelectedText);

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

            /* The program checks to see if the text has changed in here and not in TextChanged
             * because the TextChanged event gets called everytime the text gets highlighted. At
             * this point in the code, the if statement will be checked only if the selection event
             * is not simple, so it should only happen when the selection is changed by the user.
             */
            if (!Text.Equals(previousTextString))
            {
                UpdateCaptionWords();
                previousTextString = Text;
            }

            //Avoid null exceptions by returning if null
            if (Caption == null)
                return;

            //Only call the method when just the caret is being displayed.
            if (SelectionLength == 0)
                HighlightCurrentWord();
            else
            {
                int numSelections = 0;
                EditorCaptionWord cw;
                //foreach (EditorCaptionWord cw in Caption.Words)
                for(int i=0; i< Caption.Words.Count; i++)
                {
                    cw = Caption.Words[i];
                    if (cw.ContainedInSelection(SelectionStart, SelectionLength))
                    {
                        cw.IsSelected = true;
                        numSelections++;
                    }
                }

                switch (numSelections)
                {
                    case 0: 
                        SelectionMode = CaptionTextBoxSelectionMode.NoSelection;
                        OnNothingSelected(EventArgs.Empty);
                        break;
                    case 1: HighlightCurrentWord(); break;
                    default:
                        SelectionMode = CaptionTextBoxSelectionMode.MultiWordSelection;
                        OnMultipleCaptionWordsSelected(EventArgs.Empty);
                        break;
                }
            }
        }
        #endregion

        #region UpdateCaptionWords
        /// <summary>
        /// Updates the Caption.Words collection to match the CaptionTextBox's text.
        /// </summary>
        private void UpdateCaptionWords()
        {
            //Unhighlight everything
            SetTextBackgroundColour(0, Text.Length - 1, CaptionStyle.None_Style.TextColour, 
                CaptionStyle.None_Style.BackColour);

            //Re-feed the words into the Caption
            Caption.Feed(this.Text);
        }
        #endregion

        #region HighlightCurrentWord
        /// <summary>
        /// Highlights the word that the caret is in.
        /// </summary>
        private void HighlightCurrentWord()
        {
            int caret = SelectionStart;
            bool wordSelected = false;

            foreach (EditorCaptionWord cw in Caption.Words)
            {
                //If the word contains the caret but hasn't been selected yet, highlight and then select it.
                if (cw.Contains(caret) && !cw.IsSelected)
                {
                    SetTextBackgroundColour(cw, CaptionStyle.Highlighted);
                    cw.IsSelected = true;

                }
                //If the word doesn't contain the caret but is selected, then unselect it.
                else if (!cw.Contains(caret) && cw.IsSelected)
                {
                    //Return it to the original Caption colour
                    SetTextBackgroundColour(cw, CaptionStyle.GetColourOf(cw));
                    cw.IsSelected = false;
                }

                //If cw is still selected after check
                if (cw.IsSelected)
                {
                    SelectionMode = CaptionTextBoxSelectionMode.SingleWordSelection;
                    OnCaptionWordSelected(new CaptionWordSelectedEventArgs(cw));
                    wordSelected = true;
                }
            }

            if (!wordSelected)
            {
                SelectionMode = CaptionTextBoxSelectionMode.NoSelection;
                OnNothingSelected(EventArgs.Empty);
            }
        }
        #endregion

        #region Clear
        /// <summary>
        /// Clears all text from the CaptionTextBox control. Deselects all CaptionWords and removes
        /// the current Caption reference.
        /// </summary>
        public new void Clear()
        {
            base.Clear();

            if (Caption != null)
            {
                //Deselect each caption
                foreach (EditorCaptionWord cw in Caption.Words) { cw.IsSelected = false; }
            }

            //Clear Caption
            Caption = null;
        }
        #endregion

        #region SetTextBackgroundColour
        /// <summary>
        /// Sets the background colour of the text specified by the arguments. Preserves the previous
        /// selection, restoring it after highlighting is done.
        /// </summary>
        /// <param name="word">The EditorCaptionWord to highlight.</param>
        /// <param name="style">The CaptionStyle to use.</param>
        public void SetTextBackgroundColour(EditorCaptionWord word, CaptionStyle style)
        { this.SetTextBackgroundColour(word.BeginIndex, word.Length, style.TextColour, style.BackColour); }

        /// <summary>
        /// Sets the background colour of the text specified by the arguments. Preserves the previous
        /// selection, restoring it after highlighting is done.
        /// </summary>
        /// <param name="startPos">Start position of text to be set.</param>
        /// <param name="length">Length of text to be set.</param>
        /// <param name="textColour">The colour to change the text colour to.</param>
        /// <param name="highlightColour">The colour to change the background colour to.</param>
        public void SetTextBackgroundColour(int startPos, int length, Color textColour, Color highlightColour)
        {
            //Save old values
            int oldStart  = SelectionStart;
            int oldLength = SelectionLength;

            //Colour given values
            SimpleSelect(startPos, length);
            SelectionColor = textColour;
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
        /// Invokes the CaptionWordSelected event, which happens when a single Caption CaptionWord
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

        /// <summary>
        /// Invokes the MultipleCaptionWordsSelected event, which happens when multiple 
        /// CaptionWords are selected by the user.
        /// </summary>
        /// <param name="e">Event Args</param>
        private void OnMultipleCaptionWordsSelected(EventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = MultipleCaptionWordsSelected;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Invokes the NothingSelected event, which happens when no CaptionWords are selected by
        /// the user.
        /// </summary>
        /// <param name="e">Event Args</param>
        private void OnNothingSelected(EventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = NothingSelected;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
    }//Class
    #endregion
}//Namespace