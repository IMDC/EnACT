using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a NewProjectForm.
        /// </summary>
        public NewProjectForm()
        {
            InitializeComponent();
            ProjectInfo = null;
        }
        #endregion

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
                MessageBox.Show("You must enter a valid path to a Video file.", "Error!");
                return;
            }

            if (String.IsNullOrWhiteSpace(Textbox_ProjectName.Text))
            {
                MessageBox.Show("You must enter a project name.", "Error!");
                return;
            }

            if (String.IsNullOrWhiteSpace(Textbox_ProjectPath.Text))
            {
                MessageBox.Show("You must enter a valid path for the project folder.", "Error!");
                return;
            }

            //Generate P based on checkbox state
            if (CheckBox_GenerateScript.Checked)
                ProjectInfo = new ProjectInfo(Textbox_ProjectName.Text,TextBox_VideoPath.Text, 
                    Textbox_ProjectPath.Text);
            else
                ProjectInfo = new ProjectInfo(Textbox_ProjectName.Text, TextBox_ScriptPath.Text, 
                    TextBox_VideoPath.Text,Textbox_ProjectPath.Text);

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
        #endregion
    }
}
