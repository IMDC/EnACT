using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
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
        public CaptionWord SelectedWord { private set; get; }

        /// <summary>
        /// Constructs a CaptionWordSelectedEventArgs with a specified CaptionWord.
        /// </summary>
        /// <param name="selectedWord">The selected CaptionWord.</param>
        public CaptionWordSelectedEventArgs(CaptionWord selectedWord)
        {
            this.SelectedWord = selectedWord;
        }
    }
    #endregion
}
