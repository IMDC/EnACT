using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EnACT
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Direction used by MoveRow buttons to determine whether to move the row up or down
        /// </summary>
        private enum MoveDirection { Up, Down };

        /// <summary>
        /// Default speaker, used when no speaker is currently specified
        /// </summary>
        public static Speaker DefaultSpeaker = new Speaker();

        /// <summary>
        /// Description speaker, used when a caption is a description such as [laughter] or [music]
        /// </summary>
        public static Speaker DescriptionSpeaker = new Speaker(Speaker.DESCRIPTIONNAME);

        /// <summary>
        /// The Caption Table shown by CaptionView
        /// </summary>
        public CaptionData CData { set; get; }

        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<String, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<Caption> CaptionList { set; get; }

        /// <summary>
        /// The object that represents the EnACT engine xml settings file
        /// </summary>
        public SettingsXML Settings { set; get; }

        /// <summary>
        /// Constructs a Mainform object. Initializes all components
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            SpeakerSet = new Dictionary<String, Speaker>();

            //Add the default speaker to the set of speakers
            SpeakerSet[DefaultSpeaker.Name] = DefaultSpeaker;
            //Add the Description Speaker to the set of speakers
            SpeakerSet[DescriptionSpeaker.Name] = DescriptionSpeaker;

            CaptionList = new List<Caption>();
            Settings = new SettingsXML();

            //Set up the CaptionView
            InitCaptionView();
            //Set up VideoPlayer
            InitVideoPlayer();
        }

        /// <summary>
        /// Creates the table used by CaptionView and then sets as CaptionView's DataSource
        /// </summary>
        private void InitCaptionView()
        {
            CData = new CaptionData(SpeakerSet);
            CaptionView.DataSource = CData;

            //Set the view to select a whole row when you click on a column
            CaptionView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Set the first column to readonly
            CaptionView.Columns[CaptionData.NPOS].ReadOnly = true;

            //Disable visibility of the Caption Object column
            CaptionView.Columns[CaptionData.CPOS].Visible = false;

            //Set the last column to take up the remaining horizontal space in the view
            CaptionView.Columns[CaptionData.TPOS].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// Initializes the Video Player
        /// </summary>
        private void InitVideoPlayer()
        {
            //This method can not be called in the EngineView constructor, so we have to call it here.
            FlashVideoPlayer.LoadMovie(0, @"C:\Users\imdc\Documents\enact\EnACT AS3 Engine\EditorEngine.swf");
        }

        /// <summary>
        /// Populates the Caption Table with captions read in from a script file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateButton_Click(object sender, EventArgs e)
        {
            CData.PopulateTable(CaptionList);
        }

        private void ParseText(object sender, EventArgs e)
        {
            TextParser t = new TextParser(SpeakerSet, CaptionList);
            t.ParseScriptFile(@"C:\Users\imdc\Documents\enact\Testing\testScript.txt");
        }

        /// <summary>
        /// Writes the three EnACT xml files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteXML(object sender, EventArgs e)
        {
            EnactXMLWriter w = new EnactXMLWriter(SpeakerSet,CaptionList,Settings);
            w.WriteAll();
        }

        /// <summary>
        /// Inserts a new row underneath where the user has selected in the 
        /// caption View.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event Arguments</param>
        private void InsertRowBut_Click(object sender, EventArgs e)
        {
            //If the user has a row selected
            if(CaptionView.CurrentRow!=null)
                CData.AddRowAt(CaptionView.CurrentRow.Index);
            //Else there is no current selection or the table is empty
            else
                CData.AddRowAt(0);
        }

        /// <summary>
        /// Deletes all rows in the table that contain a cell that is
        /// currently selected.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The Event Arguments</param>
        private void DeleteRowBut_Click(object sender, EventArgs e)
        {
            /* TODO: .NET 4.5 has a SortedSet<T> object. If we upgrade to 4.5, 
             * use that instead of a list. Unfortunately it is not available
             * in .NET 3.5 or older.
             */
            List<int> rowList = new List<int>();

            //Add the index of each selected row to a list and remove them
            foreach (DataGridViewRow r in CaptionView.SelectedRows)
            {
                rowList.Add(r.Index);
            }
            CData.RemoveRows(rowList);

            //Deselect everything
            //CaptionView.ClearSelection();
        }

        /// <summary>
        /// Event that is triggered when a cell is changed in CaptionView. Calls the
        /// ModifyCaptionData method of CData with the specified cell co-ordinates.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void CaptionView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CData.ModifyCaptionData(e.RowIndex, e.ColumnIndex);
        }

        /// <summary>
        /// Moves the currently selected row up one position in the Caption Table
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void MoveRowUpBut_Click(object sender, EventArgs e)
        {
            MoveRow(MoveDirection.Up);
        }

        /// <summary>
        /// Moves the currently selected row down one position in the Caption Table
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void MoveRowDownBut_Click(object sender, EventArgs e)
        {
            MoveRow(MoveDirection.Down);
        }

        /// <summary>
        /// Moves the currently selected row in CaptionView based on its index number 
        /// and the direction to move it.
        /// </summary>
        /// <param name="direction">Either Movedirection.Up or Movedirection.Down 
        /// depending on which direction to move the row.</param>
        private void MoveRow(MoveDirection direction)
        {
            if (CaptionView.CurrentRow == null)
                return;

            int captionViewIndex = CaptionView.CurrentRow.Index;    //The row index of the Caption View

            //If there is a sorted column that is not the number column
            if (CaptionView.SortedColumn != null && CaptionView.SortedColumn.Index != CaptionData.NPOS)
                return;     //Don't swap

            //The row index of the Caption Data table
            int captionDataIndex = (int)CaptionView[CaptionData.NPOS, captionViewIndex].Value - 1;

            //If the row is being moved up and is within bounds
            if (direction == MoveDirection.Up && 0 < captionViewIndex)
            {
                if (CaptionView.SortOrder.Equals(SortOrder.Descending))
                    CData.SwapRows(captionDataIndex, captionDataIndex + 1);
                else
                    CData.SwapRows(captionDataIndex, captionDataIndex - 1);

                //Change selected row the new position
                CaptionView.CurrentCell = CaptionView[0, captionViewIndex - 1];

            }
            //If the row is being moved down and the row is within bounds
            else if (direction == MoveDirection.Down && captionViewIndex < CaptionView.Rows.Count - 1)
            {
                if (CaptionView.SortOrder.Equals(SortOrder.Descending))
                    CData.SwapRows(captionDataIndex, captionDataIndex - 1);
                else
                    CData.SwapRows(captionDataIndex, captionDataIndex + 1);

                //Change selected row the new position
                CaptionView.CurrentCell = CaptionView[0, captionViewIndex + 1];
            }
        }

        private void TogglePlay(object sender, EventArgs e)
        {
            FlashVideoPlayer.TogglePlay();
        }

        private void JorgeButton_Click(object sender, EventArgs e)
        {
            JorgeForm TheJorgeForm = new JorgeForm(SpeakerSet,CaptionList,Settings, this);
            TheJorgeForm.Show();
        }

        public void JorgeMethod(String SRTPath, String OutFolderPath)
        {
            TextParser t = new TextParser(SpeakerSet, CaptionList);
            t.ParseSRTFile(@SRTPath);
            PopulateButton_Click(null, null);

            EnactXMLWriter w = new EnactXMLWriter(
                OutFolderPath + @"\speakers.xml", SpeakerSet,
                OutFolderPath + @"\dialogues.xml", CaptionList,
                OutFolderPath + @"\Settings.xml", Settings);
            w.WriteAll();
        }
    }//Class
}//Namespace
