using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace EnACT
{
    class CaptionView : DataGridView
    {
        #region Constants
        /// <summary>
        /// Number column position (0)
        /// </summary>
        public const int NPOS = 0;
        /// <summary>
        /// Begin time column position (1)
        /// </summary>
        public const int BPOS = 1;
        /// <summary>
        /// End time column position (2)
        /// </summary>
        public const int EPOS = 2;
        /// <summary>
        /// Duration column position (3)
        /// </summary>
        public const int DPOS = 3;
        /// <summary>
        /// Speaker name column position (4)
        /// </summary>
        public const int SPOS = 4;
        /// <summary>
        /// Caption text column position (5)
        /// </summary>
        public const int TPOS = 5;

        /// <summary>
        /// Number column name
        /// </summary>
        public const String NNAME = "Number";
        /// <summary>
        /// Begin time column name
        /// </summary>
        public const String BNAME = "Begin";
        /// <summary>
        /// End time column name
        /// </summary>
        public const String ENAME = "End";
        /// <summary>
        /// Duration column name
        /// </summary>
        public const String DNAME = "Duration";
        /// <summary>
        /// Speaker-name column name
        /// </summary>
        public const String SNAME = "Speaker";
        /// <summary>
        /// Caption text column name
        /// </summary>
        public const String TNAME = "Text";
        #endregion

        #region Members and Properties
        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<String, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<Caption> CaptionList { private set; get; }

        /// <summary>
        /// A caption list that can automatically update the CaptionView. Use this object
        /// instead of CaptionList when coding in CaptionView
        /// </summary>
        private BindingList<Caption> BindingList { set; get; }

        DataGridViewColumn NumberColumn;
        DataGridViewColumn BeginColumn;
        DataGridViewColumn EndColumn;
        DataGridViewColumn DurationColumn;
        DataGridViewColumn SpeakerColumn;
        DataGridViewColumn TextColumn;

        /// <summary>
        /// Sets the CaptionView's CaptionList and initializes it for Caption View.
        /// Returns the Captionlist.
        /// </summary>
        public List<Caption> CaptionSource
        {
            set
            {
                //Make sure value isn't null
                if(value == null)
                    return;

                CaptionList = value;
                BindingList = new BindingList<Caption>(value);

                DataSource = BindingList;   //Bind list to view
                HookUpEvents();
            }
            get { return CaptionList; }
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

            //Init Columns
            NumberColumn = new DataGridViewTextBoxColumn();
            NumberColumn.Name = NNAME;
            NumberColumn.HeaderText = NNAME;
            NumberColumn.ReadOnly = true;   //Set Number column to read only

            BeginColumn = new DataGridViewTextBoxColumn();
            BeginColumn.Name = BNAME;
            BeginColumn.HeaderText = BNAME;
            BeginColumn.ValueType = typeof(Timestamp);
            BeginColumn.DataPropertyName = BNAME;

            EndColumn = new DataGridViewTextBoxColumn();
            EndColumn.Name = ENAME;
            EndColumn.HeaderText = ENAME;
            EndColumn.ValueType = typeof(Timestamp);
            EndColumn.DataPropertyName = ENAME;

            DurationColumn = new DataGridViewTextBoxColumn();
            DurationColumn.Name = DNAME;
            DurationColumn.HeaderText = DNAME;
            DurationColumn.ValueType = typeof(Timestamp);
            DurationColumn.DataPropertyName = DNAME;

            SpeakerColumn = new DataGridViewTextBoxColumn();
            SpeakerColumn.Name = SNAME;
            SpeakerColumn.HeaderText = SNAME;
            SpeakerColumn.DataPropertyName = SNAME;

            TextColumn = new DataGridViewTextBoxColumn();
            TextColumn.Name = TNAME;
            TextColumn.HeaderText = TNAME;
            TextColumn.DataPropertyName = TNAME;

            //Add Columns to View
            Columns.Add(NumberColumn);
            Columns.Add(BeginColumn);
            Columns.Add(EndColumn);
            Columns.Add(DurationColumn);
            Columns.Add(SpeakerColumn);
            Columns.Add(TextColumn);

            //Set the last column to fill up remaining space
            Columns[Columns.Count-1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                BindingList.Insert(CurrentRow.Index, new Caption());
            }
            //Else there is no current selection or the table is empty
            else
            {
                BindingList.Insert(0, new Caption());
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
            List<Caption> cList = new List<Caption>();

            //Add the index of each selected row to a list and remove them
            foreach (DataGridViewRow r in SelectedRows)
            {
                cList.Add(BindingList[r.Index]);
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
            this.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.CaptionView_RowPrePaint);
            this.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.CaptionView_CellParsing);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region events
        /// <summary>
        /// Re-adds all events to this instance of CaptionView. Call this method after setting the DataSource
        /// </summary>
        private void HookUpEvents()
        {
            /* For some reason events get reset when the datasource changes, for reasons currently 
             * unkown. Make sure to add all event handlers to this method as it is called right 
             * after the DataSource is set.
             */
            this.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.CaptionView_RowPrePaint);
            this.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.CaptionView_CellParsing);
        }

        /// <summary>
        /// Parses a cell once the user has exited from it. Will convert the cell values into
        /// Data usable by captions.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void CaptionView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            //Caption c = BindingList[r];
            switch (e.ColumnIndex)
            {
                //Set Begin or End value
                case BPOS:
                case EPOS:
                case DPOS:
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
                case SPOS:
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
                default: break;
            }
        }

        /// <summary>
        /// Event that gets called before the paint method. Sets the Value of the current row's
        /// number cell to the index of that row +1
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void CaptionView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Rows[e.RowIndex].Cells[NPOS].Value = e.RowIndex + 1;
        }
        #endregion
    }
}
