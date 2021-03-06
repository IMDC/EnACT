﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using EnACT.Core;
using EnACT.Miscellaneous;
using EnACT.Properties;
using LibEnACT;
using XMLReader = EnACT.Core.XMLReader;

namespace EnACT.Forms
{
    public partial class MainForm : Form
    {
        #region Fields and Properties

        private bool videoReloadRequested;

        /// <summary>
        /// Backing field for ProjectInfo.
        /// </summary>
        private ProjectInfo bkProjectInfo;
        /// <summary>
        /// Contains Information about the current EnACT Project
        /// </summary>
        private ProjectInfo ProjectInfo 
        {
            get { return bkProjectInfo; }
            set
            {
                this.bkProjectInfo = value;
                this.SpeakerSet = value.SpeakerSet;
                this.CaptionList = value.CaptionList;
                this.Settings = value.Settings;
            }
        }

        /// <summary>
        /// Backing field for SpeakerSet.
        /// </summary>
        private Dictionary<string, Speaker> bkSpeakerSet;
        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<string, Speaker> SpeakerSet
        {
            get { return bkSpeakerSet; }
            set
            {
                this.bkSpeakerSet = value;
                Timeline.SpeakerSet = value;
                CaptionView.SpeakerSet = value;
            }
        }

        /// <summary>
        /// Backing field for CaptionList.
        /// </summary>
        private List<Caption> bkCaptionList;
        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<Caption> CaptionList
        {
            get { return bkCaptionList; }
            set
            {
                this.bkCaptionList = value;
                Timeline.CaptionList = value;
                CaptionView.CaptionSource = value;
                BindingCaptionList = new BindingList<Caption>(value);
                BindingCaptionList.ListChanged += BindingCaptionList_ListChanged;
            }
        }

        public BindingList<Caption> BindingCaptionList { get; private set; } 

        /// <summary>
        /// The object that represents the EnACT engine xml settings file
        /// </summary>
        public SettingsXml Settings { set; get; }
        #endregion Fields and Properties

        #region Constructor and Init Methods
        /// <summary>
        /// Constructs a Mainform object. Initializes all components
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            //Copy All .swfs into .exe directory
            InitSwfs();

            ProjectInfo = ProjectInfo.NoProject;

            //Hook Up Controller Events
            VideoPlayed += Controller_VideoPlayed;
            VideoPaused += Controller_VideoPaused;

            //Hook up event handlers to methods in this controller.
            SubscribeToEngineEvents();

            //Set up the CaptionView
            InitCaptionView();
            //Set up VideoPlayer
            InitVideoPlayer();
            //Set up Timeline
            InitTimeline();

            //Set the timer interval to 10 miliseconds
            PlayheadTimer.Interval = 10;

            //Hook up events
            SubscribeToMarkupEvents();

            //Set the controls to a disabled state with no caption
            ClearCaption();

            //Make CaptionTextBox read only
            this.CaptionTextBox.ReadOnly = true;

            videoReloadRequested = false;
        }

        /// <summary>
        /// Copies the base swfs into the application's folder.
        /// </summary>
        private void InitSwfs()
        {
            //Copy engine to application folder
            File.WriteAllBytes(Paths.BlankSwf, Resources.blank);
            File.WriteAllBytes(Paths.EditorEngine, Resources.EditorEngine);
        }

        /// <summary>
        /// Initializes the CaptionView. Everything related to CaptionView initialization should
        /// go in this method.
        /// </summary>
        private void InitCaptionView()
        {
            CaptionView.InitColumns();  //Set up columns
            CaptionView.SpeakerSet = SpeakerSet;
            CaptionView.CaptionSource = CaptionList;
        }

        /// <summary>
        /// Initializes the VideoPlayer. Everything related to VideoPlayer initialization should
        /// go in this method.
        /// </summary>
        private void InitVideoPlayer()
        {
            //This method can not be called in the EngineView constructor, so we will call it here.
            EngineView.LoadMovie(0, Paths.BlankSwf);
        }

        /// <summary>
        /// Initializes the Timeline. Everything related to Timeline initialization should
        /// go in this method.
        /// </summary>
        private void InitTimeline()
        {
            Timeline.SpeakerSet = SpeakerSet;
            Timeline.CaptionList = CaptionList;
        }
        #endregion Constructor and Init Methods

        #region CaptionView Buttons
        /// <summary>
        /// Inserts a new row below the selected caption in CaptionView.
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
        #endregion CaptionView Buttons

        #region Controller Buttons
        /// <summary>
        /// Toggles the video playing state between playing and paused
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void TogglePlay(object sender, EventArgs e)
        {
            TogglePlayer();
        }
        #endregion Controller Buttons

        #region Jorge
        public void JorgeMethod(string srtPath, string outFolderPath)
        {
            var tuple = ScriptParser.ParseSrtFile(srtPath);

            ProjectInfo.CaptionList = tuple.Item1;
            CaptionList = tuple.Item1;

            ProjectInfo.SpeakerSet = tuple.Item2;
            SpeakerSet = tuple.Item2;

            EnactXMLWriter.WriteCaptions(CaptionList, outFolderPath + @"\dialogues.xml");
            EnactXMLWriter.WriteSpeakers(SpeakerSet, outFolderPath + @"\speakers.xml");
            EnactXMLWriter.WriteSettings(Settings, outFolderPath + @"\Settings.xml");
        }
        #endregion Jorge

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
        #endregion Timeline Buttons

        #region EngineController Event Handler Methods
        /// <summary>
        /// Handles the VideoPlayed Event. Updates the text of ButtonPlayAndPause
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Controller_VideoPlayed(object sender, EventArgs e)
        {
            Button_PlayAndPause.Text = "Pause";
        }

        /// <summary>
        /// Handles the VideoPaused Event. Updates the text of ButtonPlayAndPause
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Controller_VideoPaused(object sender, EventArgs e)
        {
            Button_PlayAndPause.Text = "Play";
        }
        #endregion EngineController Event Handler Methods

        #region Debug Menu Items
        private void parseScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tuple = ScriptParser.ParseScriptFile(Paths.TestScript);

            ProjectInfo.CaptionList = tuple.Item1;
            CaptionList = tuple.Item1;

            ProjectInfo.SpeakerSet = tuple.Item2;
            SpeakerSet = tuple.Item2;
        }

        private void parseesrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tuple = ScriptParser.ParseEsrFile(Paths.TestEsr);

            ProjectInfo.CaptionList = tuple.Item1;
            CaptionList = tuple.Item1;

            ProjectInfo.SpeakerSet = tuple.Item2;
            SpeakerSet = tuple.Item2;
        }

        private void writeXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnactXMLWriter.WriteCaptions(CaptionList, Path.Combine(ProjectInfo.DirectoryPath, "dialogues.xml"));
            EnactXMLWriter.WriteSpeakers(SpeakerSet, Path.Combine(ProjectInfo.DirectoryPath, "speakers.xml"));
            EnactXMLWriter.WriteSettings(Settings, Path.Combine(ProjectInfo.DirectoryPath, "Settings.xml"));
        }

        private void jorgeButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JorgeForm theJorgeForm = new JorgeForm(SpeakerSet, CaptionList, Settings, this);
            theJorgeForm.Show();
        }

        private void debugMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //EngineView.LoadMovie(0, Path.Combine(ProjectInfo.DirectoryPath, ProjectInfo.EditorEngineFileName));
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
        }
        #endregion Debug Menu Items

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
                LoadCaption(CaptionList[i]);
            }
            //Otherwise clear the textbox
            else
            {
                //CaptionTextBox.Caption = null;
                ClearCaption();
            }
        }
        #endregion CaptionView SelectionChanged

        #region Emotion RadioButton Click Handlers
        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_None_Click(object sender, EventArgs e) { ChangeEmotion(Emotion.None); }

        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_Happy_Click(object sender, EventArgs e) { ChangeEmotion(Emotion.Happy); }

        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_Sad_Click(object sender, EventArgs e) { ChangeEmotion(Emotion.Sad); }

        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_Fear_Click(object sender, EventArgs e) { ChangeEmotion(Emotion.Fear); }

        /// <summary>
        /// Changes the Caption's iemotion to the emotion related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_Anger_Click(object sender, EventArgs e) { ChangeEmotion(Emotion.Anger); }
        #endregion Emotion RadioButton Click Handlers

        #region Intensity RadioButton Click Handlers
        /// <summary>
        /// Changes the Caption's intensity to the intensity related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_LowIntensity_Click(object sender, EventArgs e)
        { ChangeIntensity(Intensity.Low); }

        /// <summary>
        /// Changes the Caption's intensity to the intensity related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_MediumIntensity_Click(object sender, EventArgs e)
        { ChangeIntensity(Intensity.Medium); }

        /// <summary>
        /// Changes the Caption's intensity to the intensity related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_HighIntensity_Click(object sender, EventArgs e)
        { ChangeIntensity(Intensity.High); }
        #endregion Intensity RadioButton Click Handlers

        #region Location RadioButton Click Handlers
        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_TopLeft_Click(object sender, EventArgs e)
        { ChangeLocation(ScreenLocation.TopLeft); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_TopCenter_Click(object sender, EventArgs e)
        { ChangeLocation(ScreenLocation.TopCentre); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_TopRight_Click(object sender, EventArgs e)
        { ChangeLocation(ScreenLocation.TopRight); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_MiddleLeft_Click(object sender, EventArgs e)
        { ChangeLocation(ScreenLocation.MiddleLeft); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_MiddleCenter_Click(object sender, EventArgs e)
        { ChangeLocation(ScreenLocation.MiddleCentre); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_MiddleRight_Click(object sender, EventArgs e)
        { ChangeLocation(ScreenLocation.MiddleRight); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_BottomLeft_Click(object sender, EventArgs e)
        { ChangeLocation(ScreenLocation.BottomLeft); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_BottomCenter_Click(object sender, EventArgs e)
        { ChangeLocation(ScreenLocation.BottomCentre); }

        /// <summary>
        /// Changes the Caption's Location to the Location related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void RB_BottomRight_Click(object sender, EventArgs e)
        { ChangeLocation(ScreenLocation.BottomRight); }
        #endregion Location RadioButton Click Handlers

        #region Caption Alignment Button Click Handlers
        /// <summary>
        /// Changes the Caption's Alignment to the Alignment related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_LeftAlign_Click(object sender, EventArgs e)
        { ChangeAlignment(Alignment.Left); }

        /// <summary>
        /// Changes the Caption's Alignment to the Alignment related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_CenterAlign_Click(object sender, EventArgs e)
        { ChangeAlignment(Alignment.Center); }

        /// <summary>
        /// Changes the Caption's Alignment to the Alignment related to this button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_RightAlign_Click(object sender, EventArgs e)
        { ChangeAlignment(Alignment.Right); }
        #endregion Caption Alignment Button Click Handlers

        #region File Menu Item Click Handlers
        /// <summary>
        /// Opens up the NewProjectForm for creating a new project.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open Form
            NewProjectForm newProjectForm = new NewProjectForm();
            newProjectForm.ProjectCreated += this.NewProjectForm_ProjectCreated;
            newProjectForm.ShowDialog();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenProjectDialog.ShowDialog();

            //Only read in file of OK button was pressed
            if (result == DialogResult.OK)
                ProjectInfo = XMLReader.ParseProject(OpenProjectDialog.FileName);

            //Load the video
            EngineView.LoadMovie(0, Path.Combine(ProjectInfo.DirectoryPath, ProjectInfo.EditorEngineFileName));
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Write Project and Engine files
            EnactXMLWriter.WriteProject(ProjectInfo);
            EnactXMLWriter.WriteEngineXml(ProjectInfo, ProjectInfo.UnifiedXmlFile.AbsolutePath);

            //Write three engine xml files
            EnactXMLWriter.WriteCaptions(CaptionList, ProjectInfo.CaptionsFile.AbsolutePath);
            EnactXMLWriter.WriteSettings(Settings, ProjectInfo.SettingsFile.AbsolutePath);
            EnactXMLWriter.WriteSpeakers(SpeakerSet, ProjectInfo.SpeakersFile.AbsolutePath);

            //Copy engine to project folder
            File.WriteAllBytes(ProjectInfo.EngineFile.AbsolutePath,Properties.Resources.Engine);
            File.WriteAllBytes(ProjectInfo.EngineSkinFile.AbsolutePath,Properties.Resources.SkinOverPlayFullscreen);
            File.WriteAllBytes(ProjectInfo.EditorEngineFile.AbsolutePath,Properties.Resources.EditorEngine);

            //Copy video to project folder
            if (!File.Exists(ProjectInfo.VideoFile.AbsolutePath))
            {
                try { File.Copy(ProjectInfo.ExternalVideoPath, ProjectInfo.VideoFile.AbsolutePath); }
                catch (Exception ex) { Console.WriteLine(ex.StackTrace); } //Nothing
            }
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectInfo = ProjectInfo.NoProject;
        }

        /// <summary>
        /// Exits EnACT and closes all applications.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }
        #endregion

        #region Help Menu Item Click Handlers
        /// <summary>
        /// Opens the about box.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
        #endregion

        #region Project Menu Item Click Handlers
        /// <summary>
        /// Adds a speaker to the project.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void addSpeakerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Adds a row to the CaptionView.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void addCaptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptionView.AddRow();
        }
        #endregion

        #region NewProjectForm Event Handlers
        /// <summary>
        /// Handles the loading of the new project for the different controlls on the form.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void NewProjectForm_ProjectCreated(object sender, ProjectCreatedEventArgs e)
        {
            //Set ProjectInfo and core data references to the new project's data.
            ProjectInfo = e.ProjectInfo;

            //Save project by calling the SaveProject menu item click handler.
            saveProjectToolStripMenuItem.PerformClick();

            EngineView.LoadMovie(0,Path.Combine(ProjectInfo.DirectoryPath,ProjectInfo.EditorEngineFileName));
        }
        #endregion

        #region Reload_Video
        /// <summary>
        /// This function reloads the video with updated state so that the video reflects changes
        /// made in the editor.
        /// </summary>
        private void ReloadVideo()
        {
            /* What is happening here is that the video is paused, then all of the captions are 
             * saved to file. The EngineView is then loaded with a new .swf file, called 
             * "blank.swf". This is so that all of the captions dissapear and are reset on reload.
             * It seems that just calling the loadmovie function once does not seem to work when 
             * loading the same swf file successively, so here is a hackish workaround.
             */
            Pause();
            saveProjectToolStripMenuItem_Click(this, EventArgs.Empty);
            EngineView.ReloadMovie(ProjectInfo.EditorEngineFile.AbsolutePath);
        }

        private void Button_ReloadVideo_Click(object sender, EventArgs e)
        {
            ReloadVideo();
        }
        #endregion

        #region BindingList events
        void BindingCaptionList_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                case ListChangedType.ItemMoved:
                case ListChangedType.Reset:
                    videoReloadRequested = true;
                    break;
                case ListChangedType.PropertyDescriptorAdded:
                case ListChangedType.PropertyDescriptorChanged:
                case ListChangedType.PropertyDescriptorDeleted:
                    throw new NotImplementedException("Not implemented yet");
                default: throw new InvalidEnumArgumentException("e.ListChangedType", e.ListChangedType.GetHashCode(), 
                    typeof(ListChangedType));
            }
        }
        #endregion

        private void Button_Preview_Click(object sender, EventArgs e)
        {
            saveProjectToolStripMenuItem_Click(this, EventArgs.Empty);
            var window = new PreviewForm(ProjectInfo);
            window.Show();
        }

    }//Class
}//Namespace