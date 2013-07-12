using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Drawing;

namespace EnACT
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
            public const int TextColum       = 100;
        }

        /// <summary>
        /// Contains names of the Columns in CaptionView
        /// </summary>
        public static class ColumnNames
        {
            public const String Number    = "Number";
            public const String Begin     = "Begin";
            public const String End       = "End";
            public const String Duration  = "Duration";
            public const String Speaker   = "Speaker";
            public const String Alignment = "Alignment";
            public const String Location  = "Location";
            public const String Text      = "Text";
        }
        #endregion

        #region Fields and Properties
        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<String, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<EditorCaption> CaptionList { private set; get; }

        /// <summary>
        /// A caption list that can automatically update the CaptionView. Use this object
        /// instead of CaptionList when coding in CaptionView
        /// </summary>
        private BindingList<EditorCaption> BindingList { set; get; }

        private DataGridViewColumn NumberColumn;
        private DataGridViewColumn BeginColumn;
        private DataGridViewColumn EndColumn;
        private DataGridViewColumn DurationColumn;
        private DataGridViewColumn SpeakerColumn;
        private DataGridViewColumn AlignmentColumn;
        private DataGridViewColumn LocationColumn;
        private DataGridViewColumn TextColumn;

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
            NumberColumn = new DataGridViewTextBoxColumn();
            NumberColumn.Name = ColumnNames.Number;
            NumberColumn.HeaderText = ColumnNames.Number;
            NumberColumn.MinimumWidth = MinimumColumnWidths.NumberColumn;
            NumberColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            NumberColumn.ReadOnly = true;   //Set Number column to read only

            BeginColumn = new DataGridViewTextBoxColumn();
            BeginColumn.Name = ColumnNames.Begin;
            BeginColumn.HeaderText = ColumnNames.Begin;
            BeginColumn.ValueType = typeof(Timestamp);
            BeginColumn.DataPropertyName = EditorCaption.PropertyNames.Begin;
            BeginColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            BeginColumn.MinimumWidth = MinimumColumnWidths.TimestampColumn;

            EndColumn = new DataGridViewTextBoxColumn();
            EndColumn.Name = ColumnNames.End;
            EndColumn.HeaderText = ColumnNames.End;
            EndColumn.ValueType = typeof(Timestamp);
            EndColumn.DataPropertyName = EditorCaption.PropertyNames.End;
            EndColumn.MinimumWidth = MinimumColumnWidths.TimestampColumn;
            EndColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DurationColumn = new DataGridViewTextBoxColumn();
            DurationColumn.Name = ColumnNames.Duration;
            DurationColumn.HeaderText = ColumnNames.Duration;
            DurationColumn.ValueType = typeof(Timestamp);
            DurationColumn.DataPropertyName = EditorCaption.PropertyNames.Duration;
            DurationColumn.MinimumWidth = MinimumColumnWidths.TimestampColumn;
            DurationColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            SpeakerColumn = new DataGridViewTextBoxColumn();
            SpeakerColumn.Name = ColumnNames.Speaker;
            SpeakerColumn.HeaderText = ColumnNames.Speaker;
            SpeakerColumn.DataPropertyName = EditorCaption.PropertyNames.Speaker;
            SpeakerColumn.MinimumWidth = MinimumColumnWidths.SpeakerColumn;
            SpeakerColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            AlignmentColumn = new DataGridViewTextBoxColumn();
            AlignmentColumn.Name = ColumnNames.Alignment;
            AlignmentColumn.HeaderText = ColumnNames.Alignment;
            AlignmentColumn.DataPropertyName = EditorCaption.PropertyNames.Alignment;
            AlignmentColumn.MinimumWidth = MinimumColumnWidths.AlignmentColumn;
            AlignmentColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            LocationColumn = new DataGridViewTextBoxColumn();
            LocationColumn.Name = ColumnNames.Location;
            LocationColumn.HeaderText = ColumnNames.Location;
            LocationColumn.DataPropertyName = EditorCaption.PropertyNames.Location;
            LocationColumn.MinimumWidth = MinimumColumnWidths.LocationColumn;
            LocationColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            TextColumn = new DataGridViewTextBoxColumn();
            TextColumn.Name = ColumnNames.Text;
            TextColumn.HeaderText = ColumnNames.Text;
            TextColumn.DataPropertyName = ColumnNames.Text;

            //Add Columns to View
            Columns.Add(NumberColumn);
            Columns.Add(BeginColumn);
            Columns.Add(EndColumn);
            Columns.Add(DurationColumn);
            Columns.Add(SpeakerColumn);
            Columns.Add(AlignmentColumn);
            Columns.Add(LocationColumn);
            Columns.Add(TextColumn);

            //Set the last column to fill up remaining space
            Columns[Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        #endregion

        #region Row Manipulation
        /// <summary>
        /// Adds a row to the CaptionsList at the index of the currently selected row
        /// </summary>
        public void AddRow()
        {
            //If the user has a row selected
            if (CurrentRow != null)
            {
                BindingList.Insert(CurrentRow.Index, new EditorCaption());
            }
            //Else there is no current selection or the table is empty
            else
            {
                BindingList.Insert(0, new EditorCaption());
            }
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
            List<EditorCaption> cList = new List<EditorCaption>();

            //Add the index of each selected row to a list and remove them
            foreach (DataGridViewRow r in SelectedRows)
            {
                cList.Add(BindingList[r.Index]);
            }

            //Start from the bottom of the list
            cList.Reverse();

            //Remove each caption
            foreach (EditorCaption c in cList)
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
            EditorCaption temp = BindingList[index1];
            BindingList[index1] = BindingList[index2];
            BindingList[index2] = temp;
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
                    String s = (String) e.Value;
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
                    String a = (String) e.Value;
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
                default: throw new ArgumentException("Invalid Column " + e.ColumnIndex, "e.ColumnIndex");
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
            Console.WriteLine("Forcolor: {0}", ForeColor.ToString());
            ReadOnly = true;
            ForeColor = SystemColors.GrayText;
            ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
            EnableHeadersVisualStyles = false;
        }
        #endregion
    }
}
