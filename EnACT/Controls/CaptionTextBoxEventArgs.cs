using System;
using EnACT.Core;

namespace EnACT.Controls
{
    #region CaptionWordSelectedEventArgs
    /// <summary>
    /// Event Arguments for the CaptionTextBox.CaptionWordSelected event.
    /// </summary>
    public class CaptionWordSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// The word selected by the user in CaptionTextBox.
        /// </summary>
        public EditorCaptionWord SelectedWord { private set; get; }

        /// <summary>
        /// Constructs a CaptionWordSelectedEventArgs with a specified EditorCaptionWord.
        /// </summary>
        /// <param name="selectedWord">The selected EditorCaptionWord.</param>
        public CaptionWordSelectedEventArgs(EditorCaptionWord selectedWord)
        {
            this.SelectedWord = selectedWord;
        }
    }
    #endregion
}
