using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace EnACT
{
    class CaptionView : DataGridView
    {
        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<String, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<Caption> CaptionList { private set; get; }

        /// <summary>
        /// A caption list that can automatically update the CaptionView
        /// </summary>
        private BindingList<Caption> BindingList { set; get; }

        /// <summary>
        /// Sets the CaptionView's CaptionList and initializes it for Caption View.
        /// Returns the Captionlist.
        /// </summary>
        public List<Caption> CaptionSource
        {
            set
            {
                CaptionList = value;
                BindingList = new BindingList<Caption>(value);

                DataSource = BindingList;
            }
            get { return CaptionList; }
        }
        
        /// <summary>
        /// Constructs a new CaptionView
        /// </summary>
        public CaptionView()
        {
            //Set the view to select a whole row when you click on a column
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Adds a row to the CaptionsList at the index of the currently selected row
        /// </summary>
        public void AddRow()
        {
            //If the user has a row selected
            if(CurrentRow!=null)
                BindingList.Insert(CurrentRow.Index, new Caption());
            //Else there is no current selection or the table is empty
            else
                BindingList.Insert(0, new Caption());
        }

        /// <summary>
        /// Deletes the rows currently selected by the user
        /// </summary>
        public void DeleteSelectedRows()
        {
            /* TODO: .NET 4.5 has a SortedSet<T> object. If we upgrade to 4.5, 
            * use that instead of a list. Unfortunately it is not available
            * in .NET 3.5 or older.
            */
            List<Caption> cList = new List<Caption>();

            //Add the index of each selected row to a list and remove them
            foreach (DataGridViewRow r in SelectedRows)
            {
                //rowList.Add(r.Index);
                cList.Add(BindingList[r.Index]);
                //c = BindingList[r.Index];

            }

            //Start from the bottom of the list
            cList.Reverse();

            //Remove each caption
            foreach (Caption c in cList)
            {
                BindingList.Remove(c);
            }

            //Deselect everything
            //CaptionView.ClearSelection();
        }

        /// <summary>
        /// Moves a row up by 1 position, provided it can be moved up
        /// </summary>
        public void MoveRowUp()
        {
            if (CurrentRow == null)
                return;

            int index = CurrentRow.Index;
            if (0 < index)
            {
                SwapRows(index, index - 1);

                //Change selected row the new position
                CurrentCell = this[0, index - 1];
            }
        }

        /// <summary>
        /// Moves a row down by 1 position, provided it can be moved down
        /// </summary>
        public void MoveRowDown()
        {
            if (CurrentRow == null)
                return;

            int index = CurrentRow.Index;
            if (index < BindingList.Count - 1)
            {
                SwapRows(index, index + 1);

                //Change selected row the new position
                CurrentCell = this[0, index + 1];
            }
        }

        /// <summary>
        /// Swaps two rows with each other in BindingList. Note that this method does not check
        /// that the two indices are within the bounds of BindingList
        /// </summary>
        /// <param name="index1">The first index to swap</param>
        /// <param name="index2">The second index to swap</param>
        private void SwapRows(int index1, int index2)
        {
            Caption temp = BindingList[index1];
            BindingList[index1] = BindingList[index2];
            BindingList[index2] = temp;
        }

        /// <summary>
        /// Updates this CaptionView so that it displays the current list of items. This method
        /// should be called anytime CaptionList is updated outside of CaptionView.
        /// </summary>
        public void UpdateView()
        {
            BindingList.ResetBindings();
        }
    }
}
