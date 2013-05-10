using System;
using System.Collections.Generic;
using System.Data;

namespace EnACT
{
    /// <summary>
    /// Represents the table of captions displayed in the CaptionView component
    /// in class MainForm
    /// </summary>
    public class CaptionData : DataTable
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
        /// Speaker name column position (3)
        /// </summary>
        public const int SPOS = 3;
        /// <summary>
        /// Caption text column position (4)
        /// </summary>
        public const int TPOS = 4;
        /// <summary>
        /// Caption object column position (5)
        /// </summary>
        public const int CPOS = 5;

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
        /// Speaker-name column name
        /// </summary>
        public const String SNAME = "Speaker";
        /// <summary>
        /// Caption text column name
        /// </summary>
        public const String TNAME = "Text";
        /// <summary>
        /// Caption object column name
        /// </summary>
        public const String CNAME = "Caption";
        #endregion

        #region Members
        /// <summary>
        /// Column that holds the Caption Number
        /// </summary>
        private DataColumn NumberColumn;
        /// <summary>
        /// Column that holds BeginTimestamp
        /// </summary>
        private DataColumn BeginColum;
        /// <summary>
        /// Column that holds EndTimestamp
        /// </summary>
        private DataColumn EndColumn;
        /// <summary>
        /// Column that holds Speaker Name
        /// </summary>
        private DataColumn SpeakerColumn;
        /// <summary>
        /// Column that holds Caption Text
        /// </summary>
        private DataColumn TextColumn;
        /// <summary>
        /// Column that holds the caption object
        /// </summary>
        private DataColumn CaptionColumn;

        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<String, Speaker> SpeakerSet { set; get; }
        #endregion

        #region Constuctors
        /// <summary>
        /// Constructs a CaptionData object with a given SpeakerSet and a title of
        /// "CaptionTable"
        /// </summary>
        /// <param name="SpeakerSet">The set of speakers to be referred to</param>
        public CaptionData(Dictionary<String, Speaker> SpeakerSet) : 
            this("CaptionTable", SpeakerSet) { }
        /// <summary>
        /// Constructs a CaptionData object with a given SpeakerSet and title
        /// </summary>
        /// <param name="name">Title of the table</param>
        /// <param name="SpeakerSet">The set of speakers to be referred to</param>
        public CaptionData(String name, Dictionary<String, Speaker> SpeakerSet) : base(name)
        {
            InitializeComponent();
            this.SpeakerSet = SpeakerSet;
        }

        /// <summary>
        /// Initializes component
        /// </summary>
        private void InitializeComponent()
        {
            this.NumberColumn = new System.Data.DataColumn();
            this.BeginColum = new System.Data.DataColumn();
            this.EndColumn = new System.Data.DataColumn();
            this.SpeakerColumn = new System.Data.DataColumn();
            this.TextColumn = new System.Data.DataColumn();
            this.CaptionColumn = new System.Data.DataColumn();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // NumberColumn
            // 
            this.NumberColumn.ColumnName = "Number";
            this.NumberColumn.DataType = typeof(int);
            // 
            // BeginColum
            // 
            this.BeginColum.ColumnName = "Begin";
            // 
            // EndColumn
            // 
            this.EndColumn.ColumnName = "End";
            // 
            // SpeakerColumn
            // 
            this.SpeakerColumn.ColumnName = "Speaker";
            // 
            // TextColumn
            // 
            this.TextColumn.ColumnName = "Text";
            // 
            // CaptionColumn
            // 
            this.CaptionColumn.ColumnName = "Caption";
            this.CaptionColumn.DataType = typeof(EnACT.Caption);
            // 
            // CaptionData
            // 
            this.Columns.AddRange(new System.Data.DataColumn[] {
            this.NumberColumn,
            this.BeginColum,
            this.EndColumn,
            this.SpeakerColumn,
            this.TextColumn,
            this.CaptionColumn});
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        /// <summary>
        /// Populates the CaptionData's Table with the data contained in CaptionList
        /// </summary>
        /// <param name="CaptionList">The list to populate the Table with</param>
        public void PopulateTable(List<Caption> CaptionList)
        {
            foreach (Caption c in CaptionList)
            {
                //Add rows with the following values:
                //Number,Begin,End,Speaker,Text
                Rows.Add(Rows.Count + 1, c.Begin, c.End, c.Speaker.Name, c.WordListText(),c);
            }
        }

        /// <summary>
        /// Adds a new Row at the specified index. The row previously located at that index will be
        /// pushed down to the index below it.
        /// </summary>
        /// <param name="RowNumber">The row index number to insert the new row at</param>
        public void AddRowAt(int RowNumber)
        {
            DataRow r = CreateCaptionDataRow();
            Rows.InsertAt(r, RowNumber);

            //Renumber the Rows
            RenumerateBottomToTop(RowNumber);
        }

        /// <summary>
        /// Creates a new caption row with default caption values bound to each
        /// cell of the row.
        /// </summary>
        /// <returns>The newly created row</returns>
        public DataRow CreateCaptionDataRow()
        {
            Caption c = new Caption();
            DataRow r = NewRow();
            r[NPOS] = -1;
            r[BPOS] = c.Begin;
            r[EPOS] = c.End;
            r[SPOS] = c.Speaker.Name;
            r[TPOS] = c.WordListText();
            r[CPOS] = c;

            return r;
        }
        
        /// <summary>
        /// Removes a list of given rows from the table.
        /// </summary>
        /// <param name="RowList">The list of rows to remove</param>
        public void RemoveRows(List<int> RowList)
        {
            /* TODO: .NET 4.5 has a SortedSet<T> object. If we upgrade to 4.5, 
             * use that instead of a list. Unfortunately it is not available
             * in .NET 3.5 or older.
             */

            //Sort and arrange in reverse order
            RowList.Sort();
            RowList.Reverse();

            //Remove rows from the bottom up
            foreach (int i in RowList)
            {
                Rows.RemoveAt(i);
            }

            //Re-number the rows
            Renumerate();
        }

        /// <summary>
        /// Re-numbers each row from top to bottom in the table by setting 
        /// its number column equal to the row's current index.
        /// </summary>
        private void Renumerate()
        {
            //Renumerate from the start of the table
            RenumerateTopToBottom(0);
        }

        /// <summary>
        /// Re-numbers the rows in the table from the specified BeginIndex to the end of the table
        /// </summary>
        /// <param name="BeginIndex">Row index to start renumerating from</param>
        private void RenumerateTopToBottom(int BeginIndex)
        {
            for (int i = BeginIndex; i < Rows.Count; i++ )
            {
                Rows[i][NPOS] = i+1;
            }
        }

        /// <summary>
        /// Re-numbers the rows in the table from the end of the table to the specified end index
        /// </summary>
        /// <param name="EndIndex">To index to stop renumerating on</param>
        private void RenumerateBottomToTop(int EndIndex)
        {
            for (int i = Rows.Count-1; EndIndex <= i; i--)
            {
                   Rows[i][NPOS] = i+1;
            }
        }

        /// <summary>
        /// Swaps the position of two rows with each other.
        /// </summary>
        /// <param name="index1">Index of the first row to swap</param>
        /// <param name="index2">Index of the second row to swap</param>
        public void SwapRows(int index1, int index2)
        {
            DataRow r1 = NewRow();
            DataRow r2 = NewRow();

            r1.ItemArray = Rows[index1].ItemArray;
            r2.ItemArray = Rows[index2].ItemArray;


            Rows.RemoveAt(index1);
            Rows.InsertAt(r2, index1);
            Rows.RemoveAt(index2);
            Rows.InsertAt(r1, index2);

            RenumerateTopToBottom(Math.Min(index1, index2));
        }

        /// <summary>
        /// Handles the modification of a CaptionData cell by updating its
        /// related Caption object
        /// </summary>
        /// <param name="Row">The row of the modified cell</param>
        /// <param name="Column">The Column of the modified cell</param>
        public void ModifyCaptionData(int Row, int Column)
        {
            Caption c = (Caption) Rows[Row][CPOS];
            switch (Column)
            {
                //Nothing should be done for Number
                case NPOS: break;
                //Set Begin value
                case BPOS:
                    try { c.Begin = (String)Rows[Row][Column]; } //Attempt to set it
                    catch (InvalidTimestampStringException)
                    {
                        Rows[Row][Column] = c.Begin; //Reset if invalid
                    }
                    break;
                //Set End value
                case EPOS:
                    try { c.End = (String)Rows[Row][Column]; } //Attempt to set it
                    catch (InvalidTimestampStringException)
                    {
                        Rows[Row][Column] = c.End; //Reset if invalid
                    }
                    break;
                //Change speakers
                case SPOS:
                    ModifySpeaker(Row);
                    break;
                //Create a new WordList
                case TPOS:
                    c.FeedWordList((String)Rows[Row][Column]);
                    break;
                default:
                    Console.WriteLine("No case found: {0}", Column);
                    break;
            }
        }

        /// <summary>
        /// Modifies the speaker associated with a caption.
        /// </summary>
        /// <param name="Row">The row which the caption is located at</param>
        public void ModifySpeaker(int Row)
        {
            String SpeakerName = (String)Rows[Row][SPOS];
            //Convert the name to uppercase
            SpeakerName = SpeakerName.ToUpper();
            Caption c = (Caption)Rows[Row][CPOS];

            //If the speaker already exists, then change to that speaker
            if (SpeakerSet.ContainsKey(SpeakerName))
            {
                c.Speaker = SpeakerSet[SpeakerName];
                Rows[Row][SPOS] = SpeakerName;  //Update speaker to uppercase version
            }
            //Otherwise create a new speaker
            else
            {
                Speaker s = new Speaker(SpeakerName);
                SpeakerSet[s.Name] = s;
                c.Speaker = s;
                Rows[Row][SPOS] = s.Name;  //Update speaker to uppercase version
            }
        }
    }
}
