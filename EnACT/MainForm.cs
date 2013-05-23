using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EnACT
{
    public partial class MainForm : Form
    {
        #region Members
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
        #endregion

        #region Constructor and Init Methods
        /// <summary>
        /// Constructs a Mainform object. Initializes all components
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            //Construct the speakerset with a comparator that ignores case;
            SpeakerSet = new Dictionary<String, Speaker>(StringComparer.OrdinalIgnoreCase);

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
            //Set up Timeline
            InitTimeline();

            PlayheadTimer.Interval = 10;
        }

        /// <summary>
        /// Creates the table used by CaptionView and then sets as CaptionView's DataSource
        /// </summary>
        private void InitCaptionView()
        {
            CaptionView.InitColumns();  //Set up columns
            CaptionView.SpeakerSet = SpeakerSet;
            CaptionView.CaptionSource = CaptionList;
        }

        /// <summary>
        /// Initializes the Video Player
        /// </summary>
        private void InitVideoPlayer()
        {
            //This method can not be called in the EngineView constructor, so we have to call it here.
            EngineView.LoadMovie(0, @"C:\Users\imdc\Documents\enact\EnACT AS3 Engine\EditorEngine.swf");
        }
        
        /// <summary>
        /// Initialization for the Timeline
        /// </summary>
        private void InitTimeline()
        {
            Timeline.SpeakerSet = SpeakerSet;
            Timeline.CaptionList = CaptionList;
        }
        #endregion

        /// <summary>
        /// Populates the Caption Table with captions read in from a script file.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void PopulateButton_Click(object sender, EventArgs e)
        {
            CaptionView.UpdateView();
        }

        private void ParseText(object sender, EventArgs e)
        {
            TextParser t = new TextParser(SpeakerSet, CaptionList);
            t.ParseScriptFile(@"C:\Users\imdc\Documents\enact\Testing\testScript.txt");
            CaptionView.UpdateView();
        }

        /// <summary>
        /// Writes the three EnACT xml files.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void WriteXML(object sender, EventArgs e)
        {
            EnactXMLWriter w = new EnactXMLWriter(SpeakerSet,CaptionList,Settings);
            w.WriteAll();
        }

        /// <summary>
        /// Inserts a new row where the user has selected in the 
        /// caption View.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event Arguments</param>
        private void InsertRowBut_Click(object sender, EventArgs e)
        {
            CaptionView.AddRow();
        }

        /// <summary>
        /// Deletes all rows in the table that contain a cell that is
        /// currently selected.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The Event Arguments</param>
        private void DeleteRowBut_Click(object sender, EventArgs e)
        {
            CaptionView.DeleteSelectedRows();
        }

        /// <summary>
        /// Event that is triggered when a cell is changed in CaptionView. Calls the
        /// ModifyCaptionData method of CData with the specified cell co-ordinates.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void CaptionView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Timeline.RedrawCaptionsRegion();
        }

        /// <summary>
        /// Moves the currently selected row up one position in the Caption Table
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void MoveRowUpBut_Click(object sender, EventArgs e)
        {
            CaptionView.MoveRowUp();
        }

        /// <summary>
        /// Moves the currently selected row down one position in the Caption Table
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void MoveRowDownBut_Click(object sender, EventArgs e)
        {
            CaptionView.MoveRowDown();
        }

        private Boolean isPlaying = false;
        /// <summary>
        /// Toggles the video playing state between playing and paused
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void TogglePlay(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                EngineView.Pause();
                PlayheadTimer.Stop();
                Button_PlayAndPause.Text = "Play";
                isPlaying = false;
            }
            else
            {
                EngineView.Play();
                PlayheadTimer.Start();
                Button_PlayAndPause.Text = "Pause";
                isPlaying = true;
            }
        }

        #region Jorge
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
        #endregion

        /// <summary>
        /// Handles the event fired when FlashVideoPlayer is done loading
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void FlashVideoPlayer_VideoLoaded(object sender, EventArgs e)
        {
            Double vidLength = EngineView.VideoLength();
            TrackBar_Timeline.Maximum = (int) vidLength * 10;
            Timeline.VideoLength = vidLength;
        }

        private void PlayheadTimer_Tick(object sender, EventArgs e)
        {
            double playHeadTime = EngineView.GetPlayheadTime();
            int vidPos = (int)playHeadTime * 10;
            if (TrackBar_Timeline.Minimum <= vidPos && vidPos <= TrackBar_Timeline.Maximum)
                TrackBar_Timeline.Value = vidPos;

            //Timeline.PlayHeadTime = playHeadTime;
            Timeline.UpdateTimeLinePosition(playHeadTime);
            
            //Redraw Timeline
            Timeline.RedrawCaptionsRegion();

            TrackBar_Timeline.Update();
        }

        private void TimeLine_ValueChanged(object sender, EventArgs e)
        {
            //FlashVideoPlayer.SetPlayHeadTime((double)TimeLine.Value/10.0);
        }

        private void Button_ParseESR_Click(object sender, EventArgs e)
        {
            TextParser t = new TextParser(SpeakerSet, CaptionList);
            t.ParseESRFile(@"C:\Users\imdc\Documents\enact\Testing\testScript_2.esr");
            CaptionView.UpdateView();
        }

        private void DebugButton_Click(object sender, EventArgs e)
        {
            //Timeline.AutoScrollMinSize = new System.Drawing.Size(1200,0);
            Timeline.DrawLocationLabels = false;
            Timeline.RedrawCaptionsRegion();
        }

        /// <summary>
        /// Toggles drawing of caption location labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ShowLabels_Click(object sender, EventArgs e)
        {
            if (Timeline.DrawLocationLabels)
            {
                Timeline.DrawLocationLabels = false;
                Timeline.RedrawCaptionsRegion();
            }
            else
            {
                Timeline.DrawLocationLabels = true;
                //Redraw entire region, including labels
                Timeline.RedrawInnerRegion();
            }
        }

        private void Button_ZoomTimelineIn_Click(object sender, EventArgs e)
        {
            Timeline.ZoomIn();
        }

        private void Button_ZoomTimelineOut_Click(object sender, EventArgs e)
        {
            Timeline.ZoomOut();
        }
    }//Class
}//Namespace
