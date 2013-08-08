namespace EnACT.Forms
{
    partial class NewProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CheckBox_GenerateScript = new System.Windows.Forms.CheckBox();
            this.TextBox_ScriptPath = new System.Windows.Forms.TextBox();
            this.TextBox_VideoPath = new System.Windows.Forms.TextBox();
            this.Textbox_ProjectName = new System.Windows.Forms.TextBox();
            this.Textbox_ProjectPath = new System.Windows.Forms.TextBox();
            this.Label_ScriptPath = new System.Windows.Forms.Label();
            this.Label_VideoPath = new System.Windows.Forms.Label();
            this.Label_ProjectName = new System.Windows.Forms.Label();
            this.Label_ProjectPath = new System.Windows.Forms.Label();
            this.Button_ScriptPath = new System.Windows.Forms.Button();
            this.Button_ProjectPath = new System.Windows.Forms.Button();
            this.Button_VideoPath = new System.Windows.Forms.Button();
            this.Button_CreateProject = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.ScriptFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ProjectBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.VideoFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // CheckBox_GenerateScript
            // 
            this.CheckBox_GenerateScript.AutoSize = true;
            this.CheckBox_GenerateScript.Location = new System.Drawing.Point(508, 15);
            this.CheckBox_GenerateScript.Name = "CheckBox_GenerateScript";
            this.CheckBox_GenerateScript.Size = new System.Drawing.Size(152, 17);
            this.CheckBox_GenerateScript.TabIndex = 0;
            this.CheckBox_GenerateScript.Text = "Don\'t use an existing script";
            this.ToolTip.SetToolTip(this.CheckBox_GenerateScript, "Check this option if you do not have a transcript to start with, and instead are " +
        "creating subtitles from scratch.");
            this.CheckBox_GenerateScript.UseVisualStyleBackColor = true;
            this.CheckBox_GenerateScript.CheckedChanged += new System.EventHandler(this.CheckBox_GenerateScript_CheckedChanged);
            // 
            // TextBox_ScriptPath
            // 
            this.TextBox_ScriptPath.Location = new System.Drawing.Point(103, 12);
            this.TextBox_ScriptPath.Name = "TextBox_ScriptPath";
            this.TextBox_ScriptPath.Size = new System.Drawing.Size(368, 20);
            this.TextBox_ScriptPath.TabIndex = 1;
            // 
            // TextBox_VideoPath
            // 
            this.TextBox_VideoPath.Location = new System.Drawing.Point(103, 38);
            this.TextBox_VideoPath.Name = "TextBox_VideoPath";
            this.TextBox_VideoPath.Size = new System.Drawing.Size(368, 20);
            this.TextBox_VideoPath.TabIndex = 2;
            // 
            // Textbox_ProjectName
            // 
            this.Textbox_ProjectName.Location = new System.Drawing.Point(103, 64);
            this.Textbox_ProjectName.Name = "Textbox_ProjectName";
            this.Textbox_ProjectName.Size = new System.Drawing.Size(368, 20);
            this.Textbox_ProjectName.TabIndex = 3;
            // 
            // Textbox_ProjectPath
            // 
            this.Textbox_ProjectPath.Location = new System.Drawing.Point(103, 90);
            this.Textbox_ProjectPath.Name = "Textbox_ProjectPath";
            this.Textbox_ProjectPath.Size = new System.Drawing.Size(368, 20);
            this.Textbox_ProjectPath.TabIndex = 4;
            // 
            // Label_ScriptPath
            // 
            this.Label_ScriptPath.AutoSize = true;
            this.Label_ScriptPath.Location = new System.Drawing.Point(12, 15);
            this.Label_ScriptPath.Name = "Label_ScriptPath";
            this.Label_ScriptPath.Size = new System.Drawing.Size(37, 13);
            this.Label_ScriptPath.TabIndex = 5;
            this.Label_ScriptPath.Text = "Script:";
            // 
            // Label_VideoPath
            // 
            this.Label_VideoPath.AutoSize = true;
            this.Label_VideoPath.Location = new System.Drawing.Point(12, 41);
            this.Label_VideoPath.Name = "Label_VideoPath";
            this.Label_VideoPath.Size = new System.Drawing.Size(37, 13);
            this.Label_VideoPath.TabIndex = 6;
            this.Label_VideoPath.Text = "Video:";
            // 
            // Label_ProjectName
            // 
            this.Label_ProjectName.AutoSize = true;
            this.Label_ProjectName.Location = new System.Drawing.Point(12, 67);
            this.Label_ProjectName.Name = "Label_ProjectName";
            this.Label_ProjectName.Size = new System.Drawing.Size(74, 13);
            this.Label_ProjectName.TabIndex = 7;
            this.Label_ProjectName.Text = "Project Name:";
            // 
            // Label_ProjectPath
            // 
            this.Label_ProjectPath.AutoSize = true;
            this.Label_ProjectPath.Location = new System.Drawing.Point(12, 93);
            this.Label_ProjectPath.Name = "Label_ProjectPath";
            this.Label_ProjectPath.Size = new System.Drawing.Size(68, 13);
            this.Label_ProjectPath.TabIndex = 8;
            this.Label_ProjectPath.Text = "Project Path:";
            // 
            // Button_ScriptPath
            // 
            this.Button_ScriptPath.Location = new System.Drawing.Point(477, 12);
            this.Button_ScriptPath.Name = "Button_ScriptPath";
            this.Button_ScriptPath.Size = new System.Drawing.Size(25, 20);
            this.Button_ScriptPath.TabIndex = 9;
            this.Button_ScriptPath.Text = "...";
            this.ToolTip.SetToolTip(this.Button_ScriptPath, "Choose the script file.");
            this.Button_ScriptPath.UseVisualStyleBackColor = true;
            this.Button_ScriptPath.Click += new System.EventHandler(this.Button_ScriptPath_Click);
            // 
            // Button_ProjectPath
            // 
            this.Button_ProjectPath.Location = new System.Drawing.Point(477, 90);
            this.Button_ProjectPath.Name = "Button_ProjectPath";
            this.Button_ProjectPath.Size = new System.Drawing.Size(25, 20);
            this.Button_ProjectPath.TabIndex = 11;
            this.Button_ProjectPath.Text = "...";
            this.ToolTip.SetToolTip(this.Button_ProjectPath, "Choose the destination folder for the project.");
            this.Button_ProjectPath.UseVisualStyleBackColor = true;
            this.Button_ProjectPath.Click += new System.EventHandler(this.Button_ProjectPath_Click);
            // 
            // Button_VideoPath
            // 
            this.Button_VideoPath.Location = new System.Drawing.Point(477, 38);
            this.Button_VideoPath.Name = "Button_VideoPath";
            this.Button_VideoPath.Size = new System.Drawing.Size(25, 20);
            this.Button_VideoPath.TabIndex = 12;
            this.Button_VideoPath.Text = "...";
            this.ToolTip.SetToolTip(this.Button_VideoPath, "Choose the video file.");
            this.Button_VideoPath.UseVisualStyleBackColor = true;
            this.Button_VideoPath.Click += new System.EventHandler(this.Button_VideoPath_Click);
            // 
            // Button_CreateProject
            // 
            this.Button_CreateProject.Location = new System.Drawing.Point(579, 65);
            this.Button_CreateProject.Name = "Button_CreateProject";
            this.Button_CreateProject.Size = new System.Drawing.Size(74, 23);
            this.Button_CreateProject.TabIndex = 13;
            this.Button_CreateProject.Text = "Create";
            this.ToolTip.SetToolTip(this.Button_CreateProject, "Create the project.");
            this.Button_CreateProject.UseVisualStyleBackColor = true;
            this.Button_CreateProject.Click += new System.EventHandler(this.Button_CreateProject_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(579, 90);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(74, 23);
            this.Button_Cancel.TabIndex = 14;
            this.Button_Cancel.Text = "Cancel";
            this.ToolTip.SetToolTip(this.Button_Cancel, "Cancel the new project creation.");
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // ScriptFileDialog
            // 
            this.ScriptFileDialog.Filter = "Script Files| *.esr;*.srt;*.txt; |All Files|*.*";
            // 
            // VideoFileDialog
            // 
            this.VideoFileDialog.Filter = "Video Files|*.avi;*.mpg;*.flv|All Files|*.*";
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 121);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_CreateProject);
            this.Controls.Add(this.Button_VideoPath);
            this.Controls.Add(this.Button_ProjectPath);
            this.Controls.Add(this.Button_ScriptPath);
            this.Controls.Add(this.Label_ProjectPath);
            this.Controls.Add(this.Label_ProjectName);
            this.Controls.Add(this.Label_VideoPath);
            this.Controls.Add(this.Label_ScriptPath);
            this.Controls.Add(this.Textbox_ProjectPath);
            this.Controls.Add(this.Textbox_ProjectName);
            this.Controls.Add(this.TextBox_VideoPath);
            this.Controls.Add(this.TextBox_ScriptPath);
            this.Controls.Add(this.CheckBox_GenerateScript);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectForm";
            this.Text = "Create a New Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CheckBox_GenerateScript;
        private System.Windows.Forms.TextBox TextBox_ScriptPath;
        private System.Windows.Forms.TextBox TextBox_VideoPath;
        private System.Windows.Forms.TextBox Textbox_ProjectName;
        private System.Windows.Forms.TextBox Textbox_ProjectPath;
        private System.Windows.Forms.Label Label_ScriptPath;
        private System.Windows.Forms.Label Label_VideoPath;
        private System.Windows.Forms.Label Label_ProjectName;
        private System.Windows.Forms.Label Label_ProjectPath;
        private System.Windows.Forms.Button Button_ScriptPath;
        private System.Windows.Forms.Button Button_ProjectPath;
        private System.Windows.Forms.Button Button_VideoPath;
        private System.Windows.Forms.Button Button_CreateProject;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.OpenFileDialog ScriptFileDialog;
        private System.Windows.Forms.FolderBrowserDialog ProjectBrowserDialog;
        private System.Windows.Forms.OpenFileDialog VideoFileDialog;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}