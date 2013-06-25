using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace EnACT
{
    public partial class MainForm : Form
    {
        #region Fields and Properties
        /// <summary>
        /// The controller for enact.
        /// </summary>
        public EngineController Controller { set; get; }

        /// <summary>
        /// The controller for marking up Captions
        /// </summary>
        public MarkupController MarkupController { set; get; }

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

            //Set up the Controllers
            InitController();
            InitMarkupController();

            //Set references from controller.
            this.SpeakerSet = Controller.SpeakerSet;
            this.CaptionList = Controller.CaptionList;
            this.Settings = Controller.Settings;
        }

        /// <summary>
        /// Constructs and initializes the controller
        /// </summary>
        private void InitController()
        {
            //Construct Controller
            Controller = new EngineController();

            //Hook Up Controller Events
            Controller.VideoPlayed += new EventHandler(this.Controller_VideoPlayed);
            Controller.VideoPaused += new EventHandler(this.Controller_VideoPaused);

            //Set references
            Controller.CaptionView       = this.CaptionView;
            Controller.EngineView        = this.EngineView;
            Controller.PlayheadLabel     = this.PlayheadLabel;
            Controller.PlayheadTimer     = this.PlayheadTimer;
            Controller.Timeline          = this.Timeline;
            Controller.TrackBar_Timeline = this.TrackBar_Timeline;

            Controller.InitControls();
        }

        /// <summary>
        /// Constucts and initializes the MarkupController
        /// </summary>
        private void InitMarkupController()
        {
            //Construct Controller
            MarkupController = new MarkupController();

            //Location Controls
            MarkupController.GB_Location        = this.GB_Location;
            MarkupController.RB_BottomRight     = this.RB_BottomRight;
            MarkupController.RB_BottomCenter    = this.RB_BottomCenter;
            MarkupController.RB_BottomLeft      = this.RB_BottomLeft;
            MarkupController.RB_MiddleRight     = this.RB_MiddleRight;
            MarkupController.RB_MiddleCenter    = this.RB_MiddleCenter;
            MarkupController.RB_MiddleLeft      = this.RB_MiddleLeft;
            MarkupController.RB_TopRight        = this.RB_TopRight;
            MarkupController.RB_TopCenter       = this.RB_TopCenter;
            MarkupController.RB_TopLeft         = this.RB_TopLeft;

            //Emotion Type Controls
            MarkupController.GB_EmotionType     = this.GB_EmotionType;
            MarkupController.RB_Anger           = this.RB_Anger;
            MarkupController.RB_Fear            = this.RB_Fear;
            MarkupController.RB_Sad             = this.RB_Sad;
            MarkupController.RB_Happy           = this.RB_Happy;
            MarkupController.RB_None            = this.RB_None;

            //Emotion Intensity Controls
            MarkupController.GB_Intensity       = this.GB_Intensity;
            MarkupController.RB_HighIntensity   = this.RB_HighIntensity;
            MarkupController.RB_MediumIntensity = this.RB_MediumIntensity;
            MarkupController.RB_LowIntensity    = this.RB_LowIntensity;

            //Caption Text Alignment Controls
            MarkupController.Button_LeftAlign   = this.Button_LeftAlign;
            MarkupController.Button_CenterAlign = this.Button_CenterAlign;
            MarkupController.Button_RightAlign  = this.Button_RightAlign;

            //CaptionTextBox
            MarkupController.CaptionTextBox     = this.CaptionTextBox;
        }
        #endregion

        #region CaptionView Buttons
        /// <summary>
        /// Inserts a new row above the selected caption in CaptionView.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_InsertRow_Click(object sender, EventArgs e)
        {
            CaptionView.AddRow();
        }

        /// <summary>
        /// Deletes the currently selected rows.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_DeleteRow_Click(object sender, EventArgs e)
        {
            CaptionView.DeleteSelectedRows();
        }

        /// <summary>
        /// Moves the currently selected caption one row up in CaptionView
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_MoveRowUp_Click(object sender, EventArgs e)
        {
            CaptionView.MoveRowUp();
        }

        /// <summary>
        /// Moves the currently selected caption one row down in CaptionView
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_MoveRowDown_Click(object sender, EventArgs e)
        {
            CaptionView.MoveRowDown();
        }
        #endregion

        #region Controller Buttons
        /// <summary>
        /// Toggles the video playing state between playing and paused
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void TogglePlay(object sender, EventArgs e)
        {
            Controller.TogglePlay();
        }
        #endregion

        #region Jorge
        public void JorgeMethod(String SRTPath, String OutFolderPath)
        {
            TextParser t = new TextParser(SpeakerSet, CaptionList);
            t.ParseSRTFile(@SRTPath);
            CaptionView.UpdateView();

            EnactXMLWriter w = new EnactXMLWriter(
                OutFolderPath + @"\speakers.xml", SpeakerSet,
                OutFolderPath + @"\dialogues.xml", CaptionList,
                OutFolderPath + @"\Settings.xml", Settings);
            w.WriteAll();
        }
        #endregion

        #region Timeline Buttons
        private void Button_ShowLabels_Click(object sender, EventArgs e)
        {
            Timeline.ToggleDrawLocationLabels();
        }

        private void Button_ZoomTimelineIn_Click(object sender, EventArgs e)
        {
            Timeline.ZoomIn();
        }

        private void Button_ZoomTimelineOut_Click(object sender, EventArgs e)
        {
            Timeline.ZoomOut();
        }

        private void Button_ZoomReset_Click(object sender, EventArgs e)
        {
            Timeline.ZoomReset();
        }
        #endregion

        #region EngineController Event Handler Methods
        /// <summary>
        /// Handles the VideoPlayed Event. Updates the text of ButtonPlayAndPause
        /// </summary>
        /// <param name="Sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Controller_VideoPlayed(object Sender, EventArgs e)
        {
            Button_PlayAndPause.Text = "Pause";
        }

        /// <summary>
        /// Handles the VideoPaused Event. Updates the text of ButtonPlayAndPause
        /// </summary>
        /// <param name="Sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Controller_VideoPaused(object Sender, EventArgs e)
        {
            Button_PlayAndPause.Text = "Play";
        }
        #endregion

        #region Debug Menu Items
        private void parseScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextParser t = new TextParser(SpeakerSet, CaptionList);
            t.ParseScriptFile(Paths.TestScript);
            CaptionView.UpdateView();
        }

        private void parseesrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextParser t = new TextParser(SpeakerSet, CaptionList);
            t.ParseESRFile(Paths.TestESR);
            CaptionView.UpdateView();
        }

        private void writeXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnactXMLWriter w = new EnactXMLWriter(SpeakerSet, CaptionList, Settings);
            w.WriteAll();
        }

        private void jorgeButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JorgeForm TheJorgeForm = new JorgeForm(SpeakerSet, CaptionList, Settings, this);
            TheJorgeForm.Show();
        }

        private void debugMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (CaptionView.UserInputEnabled)
            //    CaptionView.UserInputEnabled = false;
            //else
            //    CaptionView.UserInputEnabled = true;
        }
        #endregion

        #region CaptionView SelectionChanged
        /// <summary>
        /// Event Handler for CaptionView.SelectionChanged. Will set the caption of CaptionTextBox
        /// when appropriate.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void CaptionView_SelectionChanged(object sender, EventArgs e)
        {
            //Only use a caption if its the only selected row
            if (CaptionView.SelectedRows.Count == 1)
            {
                //Get Index of selected row
                int i = CaptionView.SelectedRows[0].Index;

                //Assign Caption
                MarkupController.LoadCaption(CaptionList[i]);
            }
            //Otherwise clear the textbox
            else
            {
                //CaptionTextBox.Caption = null;
                MarkupController.ClearCaption();
            }
        }
        #endregion

        #region Emotion RadioButton Click Handlers
        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_None_Click(object sender, EventArgs e) { MarkupController.ChangeEmotion(Emotion.None); }

        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_Happy_Click(object sender, EventArgs e) { MarkupController.ChangeEmotion(Emotion.Happy); }

        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_Sad_Click(object sender, EventArgs e) { MarkupController.ChangeEmotion(Emotion.Sad); }

        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_Fear_Click(object sender, EventArgs e) { MarkupController.ChangeEmotion(Emotion.Fear); }

        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_Anger_Click(object sender, EventArgs e) { MarkupController.ChangeEmotion(Emotion.Anger); }
        #endregion

        #region Intensity RadioButton Click Handlers
        /// <summary>
        /// Changes the Caption's intensity to the intensity related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_LowIntensity_Click(object sender, EventArgs e)
        { MarkupController.ChangeIntensity(Intensity.Low); }

        /// <summary>
        /// Changes the Caption's intensity to the intensity related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_MediumIntensity_Click(object sender, EventArgs e)
        { MarkupController.ChangeIntensity(Intensity.Medium); }

        /// <summary>
        /// Changes the Caption's intensity to the intensity related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_HighIntensity_Click(object sender, EventArgs e)
        { MarkupController.ChangeIntensity(Intensity.High); }
        #endregion

        #region Location RadioButton Click Handlers
        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_TopLeft_Click(object sender, EventArgs e)
        { MarkupController.ChangeLocation(ScreenLocation.TopLeft); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_TopCenter_Click(object sender, EventArgs e)
        { MarkupController.ChangeLocation(ScreenLocation.TopCentre); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_TopRight_Click(object sender, EventArgs e)
        { MarkupController.ChangeLocation(ScreenLocation.TopRight); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_MiddleLeft_Click(object sender, EventArgs e)
        { MarkupController.ChangeLocation(ScreenLocation.MiddleLeft); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_MiddleCenter_Click(object sender, EventArgs e)
        { MarkupController.ChangeLocation(ScreenLocation.MiddleCenter); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_MiddleRight_Click(object sender, EventArgs e)
        { MarkupController.ChangeLocation(ScreenLocation.MiddleRight); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_BottomLeft_Click(object sender, EventArgs e)
        { MarkupController.ChangeLocation(ScreenLocation.BottomLeft); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_BottomCenter_Click(object sender, EventArgs e)
        { MarkupController.ChangeLocation(ScreenLocation.BottomCentre); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_BottomRight_Click(object sender, EventArgs e)
        { MarkupController.ChangeLocation(ScreenLocation.BottomRight); }
        #endregion
    }//Class
}//Namespace
