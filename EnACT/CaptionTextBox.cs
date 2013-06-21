using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            int index = Caption.WordList.GetCaptionWordIndexAt(SelectionStart);

            int start = Caption.WordList.GetStringStartIndexAt(index);

            int length;

            if(Caption.WordList.AsString[start] == ' ')
                length = 0;
            else
                length = Caption.WordList[index].Length;

            Select(start, length);
        }

        #endregion
    }
}
