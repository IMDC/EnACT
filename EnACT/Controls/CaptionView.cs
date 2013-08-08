using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EnACT.Core;

namespace EnACT.Controls
{
    public class CaptionView : DataGridView
    {
        #region Constants
        /// <summary>
        /// Contains the minimum width of CaptionView Columns in Pixels
        /// </summary>
        public static class MinimumColumnWidths
        {
            public const int NumberColumn    = 50;
            public const int TimestampColumn = 70;
            public const int SpeakerColumn   = 100;
            public const int AlignmentColumn = 70;
            public const int LocationColumn  = 80;
            public const int TextColumn      = 100;
        }

        /// <summary>
        /// Contains names of the Columns in CaptionView
        /// </summary>
        public static class ColumnNames
        {
            public const string Number    = "Number";
            public const string Begin     = "Begin";
            public const string End       = "End";
            public const string Duration  = "Duration";
            public const string Speaker   = "Speaker";
            public const string Alignment = "Alignment";
            public const string Location  = "Location";
            public const string Text      = "Text";
        }
        #endregion

        #region Fields and Properties
        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<string, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<EditorCaption> CaptionList { private set; get; }

        /// <summary>
        /// A caption list that can automatically update the CaptionView. Use this object
        /// instead of CaptionList when coding in CaptionView
        /// </summary>
        private BindingList<EditorCaption> BindingList { set; get; }

        private DataGridViewColumn numberColumn;
        private DataGridViewColumn beginColumn;
        private DataGridViewColumn endColumn;
        private DataGridViewColumn durationColumn;
        private DataGridViewColumn speakerColumn;
        private DataGridViewColumn alignmentColumn;
        private DataGridViewColumn locationColumn;
        private DataGridViewColumn textColumn;

        /// <summary>
        /// Sets the CaptionView's CaptionList and initializes it for Caption View.
        /// Gets the Captionlist.
        /// </summary>
        public List<EditorCaption> CaptionSource
        {
            set
            {
                //Make sure value isn't null
                if(value == null)
                    return;

                CaptionList = value;
                BindingList = new BindingList<EditorCaption>(value);

                DataSource = BindingList;   //Bind list to view
                BindingList.ListChanged += new ListChangedEventHandler(BindingList_ListChanged);
            }
            get { return CaptionList; }
        }

        /// <summary>
        /// Backing field for UserInputEnabled
        /// </summary>
        private bool bkUserInputEnabled;

        /// <summary>
        /// Property that represents whether user input is allowed or not. When set, it will
        /// enable or disable user input on the control
        /// </summary>
        public bool UserInputEnabled 
        {
            set
            {
                bkUserInputEnabled = value;
                if (bkUserInputEnabled)  //Value == true
                    EnableUserInput();
                else
                    DisableUserInput();
            }
            get { return bkUserInputEnabled; }  
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a new CaptionView
        /// </summary>
        public CaptionView()
        {
            //Make the component use a doublebuffer, which will reduce flicker made by 
            //redrawing the control
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            //Set the view to select a whole row when you click on a column
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Don't create columns for each property.
            AutoGenerateColumns = false;

            //Allow user input
            UserInputEnabled = true;
        }

        /// <summary>
        /// Initializes columns for the Caption View.
        /// </summary>
        public void InitColumns()
        {
            /* Do not call this method from the constructor of Caption View. It will
             * create a bug within the Visual Studio designer that will duplicate 
             * columns. Instead, it is better to call this method from the form or
             * component that implements Caption view.
             */
            numberColumn = new DataGridViewTextBoxColumn
            {
                Name = ColumnNames.Number,
                HeaderText = ColumnNames.Number,
                MinimumWidth = MinimumColumnWidths.NumberColumn,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader,
                ReadOnly = true
            };

            beginColumn = new DataGridViewTextBoxColumn
            {
                Name = ColumnNames.Begin,
                HeaderText = ColumnNames.Begin,
                ValueType = typeof (Timestamp),
                DataPropertyName = EditorCaption.PropertyNames.Begin,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = MinimumColumnWidths.TimestampColumn
            };

            endColumn = new DataGridViewTextBoxColumn
            {
                Name = ColumnNames.End,
                HeaderText = ColumnNames.End,
                ValueType = typeof (Timestamp),
                DataPropertyName = EditorCaption.PropertyNames.End,
                MinimumWidth = MinimumColumnWidths.TimestampColumn,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            durationColumn = new DataGridViewTextBoxColumn
            {
                Name = ColumnNames.Duration,
                HeaderText = ColumnNames.Duration,
                ValueType = typeof (Timestamp),
                DataPropertyName = EditorCaption.PropertyNames.Duration,
                MinimumWidth = MinimumColumnWidths.TimestampColumn,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            speakerColumn = new DataGridViewTextBoxColumn
            {
                Name = ColumnNames.Speaker,
                HeaderText = ColumnNames.Speaker,
                DataPropertyName = EditorCaption.PropertyNames.Speaker,
                MinimumWidth = MinimumColumnWidths.SpeakerColumn,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            alignmentColumn = new DataGridViewTextBoxColumn
            {
                Name = ColumnNames.Alignment,
                HeaderText = ColumnNames.Alignment,
                DataPropertyName = EditorCaption.PropertyNames.Alignment,
                MinimumWidth = MinimumColumnWidths.AlignmentColumn,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            locationColumn = new DataGridViewTextBoxColumn
            {
                Name = ColumnNames.Location,
                HeaderText = ColumnNames.Location,
                DataPropertyName = EditorCaption.PropertyNames.Location,
                MinimumWidth = MinimumColumnWidths.LocationColumn,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            textColumn = new DataGridViewTextBoxColumn
            {
                Name = ColumnNames.Text,
                HeaderText = ColumnNames.Text,
                DataPropertyName = ColumnNames.Text
            };

            //Add Columns to View
            Columns.Add(numberColumn);
            Columns.Add(beginColumn);
            Columns.Add(endColumn);
            Columns.Add(durationColumn);
            Columns.Add(speakerColumn);
            Columns.Add(alignmentColumn);
            Columns.Add(locationColumn);
            Columns.Add(textColumn);

            //Set the last column to fill up remaining space
            Columns[Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        #endregion

        #region Row Manipulation
        /// <summary>
        /// Adds a row to the CaptionsList below the index of the currently selected row
        /// </summary>
        public void AddRow()
        {
            //If there are no rows in the list, add a new row
            if (BindingList.Count == 0)
            {
                BindingList.Add(new EditorCaption());
                SetCurrentRow(0, ColumnNames.Text);
            }
            //Else insert new row under the currently selected row.
            else
            {
                BindingList.Insert(CurrentRow.Index + 1, new EditorCaption());
                SetCurrentRow(CurrentRow.Index + 1, ColumnNames.Text);
            }
        }

        /// <summary>
        /// Deletes the rows currently selected by the user
        /// </summary>
        public void DeleteSelectedRows()
        {
            //Create a sorted set that contains ints from largest to smallest
            var indexSet = new SortedSet<int>(Comparer<int>.Create((x,y) => y.CompareTo(x)));

            //Insert selected row indexes into set
            foreach (DataGridViewRow r in SelectedRows){ indexSet.Add(r.Index); }

            //Delete rows from largest index to smallest
            foreach (int i in indexSet) { BindingList.RemoveAt(i); }

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
                SetCurrentRow(index - 1);
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
                SetCurrentRow(index + 1);
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
            EditorCaption temp = BindingList[index1];
            BindingList[index1] = BindingList[index2];
            BindingList[index2] = temp;
        }

        /// <summary>
        /// Sets the current row to the specified row index. 
        /// </summary>
        /// <param name="rowIndex">The row index to change the CurrentRow to.</param>
        private void SetCurrentRow(int rowIndex)
        {
            CurrentCell = this[0, rowIndex];
        }

        /// <summary>
        /// Sets the current row to the specified row index.
        /// </summary>
        /// <param name="rowIndex">The row index to change the CurrentRow to.</param>
        /// <param name="columnName">The name of a column to select in the new CurrentRow as the 
        /// new currently active cell.</param>
        private void SetCurrentRow(int rowIndex, string columnName)
        {
            CurrentCell = this[columnName, rowIndex];
        }
        #endregion

        #region UpdateView
        /// <summary>
        /// Updates this CaptionView so that it displays the current list of items. This method
        /// should be called anytime CaptionList is updated outside of CaptionView.
        /// </summary>
        public void UpdateView()
        {
            BindingList.ResetBindings();
        }
        #endregion

        #region Initialize
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // CaptionView
            // 
            this.AutoGenerateColumns = false;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region Events
        /// <summary>
        /// Parses a cell once the user has entered data into it.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnCellParsing(DataGridViewCellParsingEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            switch (Columns[c].Name)
            {
                case ColumnNames.Number: break;
                //Set Begin, End, or Duration value
                case ColumnNames.Begin:
                case ColumnNames.End:
                case ColumnNames.Duration:
                    try
                    {
                        //Attempt to convert the value
                        e.Value = new Timestamp(e.Value.ToString());
                    }
                    catch (InvalidTimestampException)
                    {
                        //Leave the value as it is
                        e.Value = (Timestamp) Rows[r].Cells[c].Value;
                    }
                    finally
                    {
                        e.ParsingApplied = true;
                    }
                    break;
                case ColumnNames.Speaker:
                    string s = (string) e.Value;
                    if (SpeakerSet.ContainsKey(s))
                    {
                        e.Value = SpeakerSet[s];
                    }
                    else
                    {
                        SpeakerSet[s] = new Speaker(s);
                        e.Value = SpeakerSet[s];
                    }
                    e.ParsingApplied = true;
                    break;
                case ColumnNames.Alignment:
                    string a = (string) e.Value;
                    //Check to see if value is equal to the alignment text name
                    if (a.Equals("Left", StringComparison.OrdinalIgnoreCase))
                        e.Value = Alignment.Left;
                    else if (a.Equals("Center", StringComparison.OrdinalIgnoreCase))
                        e.Value = Alignment.Center;
                    else if (a.Equals("Right", StringComparison.OrdinalIgnoreCase))
                        e.Value = Alignment.Right;
                    else
                        e.Value = Rows[r].Cells[c].Value; //If no match reset value
                    e.ParsingApplied = true;
                    break;
                case ColumnNames.Location:
                    break;
                case ColumnNames.Text: break;
                default: throw new ArgumentException("Invalid Column " + e.ColumnIndex, "e");
            }
            base.OnCellParsing(e);
        }

        /// <summary>
        /// Method called before a row is Painted. Sets the value of the number column
        /// to the row number the column is in, indexed from 1.
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);
            Rows[e.RowIndex].Cells[ColumnNames.Number].Value = e.RowIndex + 1;
        }

        /// <summary>
        /// Event that is called when bindinglist is changed.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            //Console.WriteLine(e.ListChangedType.ToString());
            //Redraw the current row if it is in bounds
            if (0 <= e.NewIndex && e.NewIndex < Rows.Count)
            {
                InvalidateRow(e.NewIndex);
            }
        }
        #endregion

        #region Enable/Disable User Input
        /// <summary>
        /// Called from the setter of the UserInputEnabled property. Enables the
        /// CaptionView and changes the colors back to the original colors.
        /// </summary>
        private void EnableUserInput()
        {
            ReadOnly = false;
            ForeColor = SystemColors.ControlText;
            ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            EnableHeadersVisualStyles = true;

        }

        /// <summary>
        /// Called from the setter of the UserInputEnabled property. Makes the
        /// Captionview readonly and grays-out the text.
        /// </summary>
        private void DisableUserInput()
        {
            Console.WriteLine("Forecolor: {0}", ForeColor.ToString());
            ReadOnly = true;
            ForeColor = SystemColors.GrayText;
            ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
            EnableHeadersVisualStyles = false;
        }
        #endregion
    }
}
