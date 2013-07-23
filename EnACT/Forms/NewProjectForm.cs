using System;
using System.IO;
using System.Windows.Forms;

namespace EnACT
{
    /// <summary>
    /// The form that is used for creating new projects.
    /// </summary>
    public partial class NewProjectForm : Form
    {
        #region Fields and Properties
        /// <summary>
        /// Contains Information about the current EnACT Project.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
        #endregion Fields and Properties

        #region Events
        /// <summary>
        /// An event that is fired when the project is created.
        /// </summary>
        public EventHandler<ProjectCreatedEventArgs> ProjectCreated;
        #endregion Events

        #region Constructor
        /// <summary>
        /// Constructs a NewProjectForm.
        /// </summary>
        public NewProjectForm()
        {
            InitializeComponent();
            ProjectInfo = null;
        }
        #endregion Constructor

        #region Click Handlers
        /// <summary>
        /// Gets the Path of the script.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_ScriptPath_Click(object sender, EventArgs e)
        {
            ScriptFileDialog.ShowDialog();
            TextBox_ScriptPath.Text = ScriptFileDialog.FileName;
        }

        /// <summary>
        /// Gets the path of the video.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_VideoPath_Click(object sender, EventArgs e)
        {
            VideoFileDialog.ShowDialog();
            TextBox_VideoPath.Text = VideoFileDialog.FileName;
        }

        /// <summary>
        /// Gets the path of the project.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_ProjectPath_Click(object sender, EventArgs e)
        {
            ProjectBrowserDialog.ShowDialog();
            Textbox_ProjectPath.Text = ProjectBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// When checked, disables the ScriptPath controls and signals the program not to import a
        /// script/transcript file.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void CheckBox_GenerateScript_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_GenerateScript.Checked)
            {
                TextBox_ScriptPath.Enabled = false;
                Button_ScriptPath.Enabled = false;
            }
            else
            {
                TextBox_ScriptPath.Enabled = true;
                Button_ScriptPath.Enabled = true;
            }
        }

        /// <summary>
        /// Creates a project and closes this window.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_CreateProject_Click(object sender, EventArgs e)
        {
            //Check to see if Text boxes are empty when they should not be.
            if (String.IsNullOrWhiteSpace(TextBox_ScriptPath.Text) && !CheckBox_GenerateScript.Checked)
            {
                MessageBox.Show("You must either enter a path to a Script, or check the Check Box next to it.",
                    "Error!");
                return;
            }

            if (String.IsNullOrWhiteSpace(TextBox_VideoPath.Text))
            {
                MessageBox.Show("You must enter a path to a Video file.", "Error!");
                return;
            }

            if (String.IsNullOrWhiteSpace(Textbox_ProjectName.Text))
            {
                MessageBox.Show("You must enter a project name.", "Error!");
                return;
            }

            if (String.IsNullOrWhiteSpace(Textbox_ProjectPath.Text))
            {
                MessageBox.Show("You must enter a path for the project folder.", "Error!");
                return;
            }

            //Generate P based on checkbox state
            if (CheckBox_GenerateScript.Checked)
                ProjectInfo = new ProjectInfo(Textbox_ProjectName.Text, TextBox_VideoPath.Text,
                    Textbox_ProjectPath.Text);
            else
                ProjectInfo = new ProjectInfo(Textbox_ProjectName.Text, TextBox_ScriptPath.Text,
                    TextBox_VideoPath.Text, Textbox_ProjectPath.Text);

            if (ProjectInfo.UseExistingScript)
            {
                try //Attempt to parse file.
                {
                    var tuple = TextParser.Parse(ProjectInfo.ScriptPath);
                    ProjectInfo.CaptionList = tuple.Item1;
                    ProjectInfo.SpeakerSet = tuple.Item2;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Error trying to read in script file. " + ProjectInfo.DirectoryPath +
                        " has an invalid file extension.", "Error: " + ProjectInfo.ScriptPath);
                    return;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error trying to read in script file. File is either corrupted or not named with" +
                        "the correct file extension.", "Error: " + ProjectInfo.ScriptPath);
                    return;
                }
            }

            //Attempt to create project directory
            try
            {
                Directory.CreateDirectory(ProjectInfo.DirectoryPath);
            }
            catch (IOException)
            {
                MessageBox.Show("Error: IOException encountered while attempting to create project directory.",
                    "Error Creating Directory");
                return;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Error: Program does not have required permissions to create project directory.",
                    "Error Creating Directory");
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error: Project path is not a valid path.", "Error Creating Directory");
                return;
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Error: Path contains a colon character (:) that is not part of a drive label "
                    + "('C:\\').", "Error Creating Directory");
                return;
            }

            //Fire event and close form
            OnProjectCreated(new ProjectCreatedEventArgs(ProjectInfo));
            this.Close();
        }

        /// <summary>
        /// Cancels a new project by closing the window.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Click Handlers

        #region Event Invokations
        /// <summary>
        /// Raises the ProjectCreated event.
        /// </summary>
        /// <param name="e">The event arguments needed for the event</param>
        private void OnProjectCreated(ProjectCreatedEventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler<ProjectCreatedEventArgs> handler = ProjectCreated;

            if (handler != null) { handler(this, e); }
        }
        #endregion Event Invokations
    }
}