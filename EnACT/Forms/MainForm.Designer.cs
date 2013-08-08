using EnACT.Controls;

namespace EnACT
{
    partial class MainForm
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
            EnACT.Timestamp timestamp1 = new EnACT.Timestamp();
            EnACT.Timestamp timestamp2 = new EnACT.Timestamp();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MenuStrip_MainForm = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpeakerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCaptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseesrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jorgeButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Button_InsertRow = new System.Windows.Forms.Button();
            this.Button_DeleteRow = new System.Windows.Forms.Button();
            this.Button_MoveRowUp = new System.Windows.Forms.Button();
            this.Button_MoveRowDown = new System.Windows.Forms.Button();
            this.Button_PlayAndPause = new System.Windows.Forms.Button();
            this.TrackBar_Timeline = new System.Windows.Forms.TrackBar();
            this.PlayheadTimer = new System.Windows.Forms.Timer(this.components);
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Button_ShowLabels = new System.Windows.Forms.Button();
            this.Button_ZoomTimelineIn = new System.Windows.Forms.Button();
            this.Button_ZoomTimelineOut = new System.Windows.Forms.Button();
            this.Button_ZoomReset = new System.Windows.Forms.Button();
            this.GB_Location = new System.Windows.Forms.GroupBox();
            this.RB_BottomRight = new System.Windows.Forms.RadioButton();
            this.RB_BottomCenter = new System.Windows.Forms.RadioButton();
            this.RB_BottomLeft = new System.Windows.Forms.RadioButton();
            this.RB_MiddleRight = new System.Windows.Forms.RadioButton();
            this.RB_MiddleCenter = new System.Windows.Forms.RadioButton();
            this.RB_MiddleLeft = new System.Windows.Forms.RadioButton();
            this.RB_TopCenter = new System.Windows.Forms.RadioButton();
            this.RB_TopLeft = new System.Windows.Forms.RadioButton();
            this.RB_TopRight = new System.Windows.Forms.RadioButton();
            this.GB_EmotionType = new System.Windows.Forms.GroupBox();
            this.RB_Anger = new System.Windows.Forms.RadioButton();
            this.RB_Fear = new System.Windows.Forms.RadioButton();
            this.RB_Sad = new System.Windows.Forms.RadioButton();
            this.RB_Happy = new System.Windows.Forms.RadioButton();
            this.RB_None = new System.Windows.Forms.RadioButton();
            this.GB_Intensity = new System.Windows.Forms.GroupBox();
            this.RB_HighIntensity = new System.Windows.Forms.RadioButton();
            this.RB_MediumIntensity = new System.Windows.Forms.RadioButton();
            this.RB_LowIntensity = new System.Windows.Forms.RadioButton();
            this.Button_LeftAlign = new System.Windows.Forms.Button();
            this.Button_CenterAlign = new System.Windows.Forms.Button();
            this.Button_RightAlign = new System.Windows.Forms.Button();
            this.OpenProjectDialog = new System.Windows.Forms.OpenFileDialog();
            this.CaptionTextBox = new CaptionTextBox();
            this.PlayheadLabel = new PlayheadLabel();
            this.Timeline = new EnACT.Timeline();
            this.EngineView = new EngineView();
            this.CaptionView = new CaptionView();
            this.MenuStrip_MainForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_Timeline)).BeginInit();
            this.GB_Location.SuspendLayout();
            this.GB_EmotionType.SuspendLayout();
            this.GB_Intensity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EngineView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptionView)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStrip_MainForm
            // 
            this.MenuStrip_MainForm.BackColor = System.Drawing.SystemColors.Control;
            this.MenuStrip_MainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.MenuStrip_MainForm.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip_MainForm.Name = "MenuStrip_MainForm";
            this.MenuStrip_MainForm.Size = new System.Drawing.Size(944, 24);
            this.MenuStrip_MainForm.TabIndex = 0;
            this.MenuStrip_MainForm.Text = "MainMenuForm";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.closeProjectToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator3,
            this.exportToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.closeProjectToolStripMenuItem.Text = "Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // saveProjectAsToolStripMenuItem
            // 
            this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveProjectAsToolStripMenuItem.Text = "Save Project As";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.printToolStripMenuItem.Text = "Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Preview";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(151, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(151, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator5,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator6,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator7,
            this.optionsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(119, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(119, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSpeakerToolStripMenuItem,
            this.addCaptionToolStripMenuItem,
            this.projectSettingsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(56, 20);
            this.toolStripMenuItem1.Text = "Project";
            // 
            // addSpeakerToolStripMenuItem
            // 
            this.addSpeakerToolStripMenuItem.Name = "addSpeakerToolStripMenuItem";
            this.addSpeakerToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addSpeakerToolStripMenuItem.Text = "Add Speaker";
            this.addSpeakerToolStripMenuItem.Click += new System.EventHandler(this.addSpeakerToolStripMenuItem_Click);
            // 
            // addCaptionToolStripMenuItem
            // 
            this.addCaptionToolStripMenuItem.Name = "addCaptionToolStripMenuItem";
            this.addCaptionToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addCaptionToolStripMenuItem.Text = "Add Caption";
            this.addCaptionToolStripMenuItem.Click += new System.EventHandler(this.addCaptionToolStripMenuItem_Click);
            // 
            // projectSettingsToolStripMenuItem
            // 
            this.projectSettingsToolStripMenuItem.Name = "projectSettingsToolStripMenuItem";
            this.projectSettingsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.projectSettingsToolStripMenuItem.Text = "Project Settings";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parseScriptToolStripMenuItem,
            this.parseesrToolStripMenuItem,
            this.writeXMLToolStripMenuItem,
            this.jorgeButtonToolStripMenuItem,
            this.debugMethodToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // parseScriptToolStripMenuItem
            // 
            this.parseScriptToolStripMenuItem.Name = "parseScriptToolStripMenuItem";
            this.parseScriptToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.parseScriptToolStripMenuItem.Text = "Parse Script";
            this.parseScriptToolStripMenuItem.Click += new System.EventHandler(this.parseScriptToolStripMenuItem_Click);
            // 
            // parseesrToolStripMenuItem
            // 
            this.parseesrToolStripMenuItem.Name = "parseesrToolStripMenuItem";
            this.parseesrToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.parseesrToolStripMenuItem.Text = "Parse .esr";
            this.parseesrToolStripMenuItem.Click += new System.EventHandler(this.parseesrToolStripMenuItem_Click);
            // 
            // writeXMLToolStripMenuItem
            // 
            this.writeXMLToolStripMenuItem.Name = "writeXMLToolStripMenuItem";
            this.writeXMLToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.writeXMLToolStripMenuItem.Text = "Write XML";
            this.writeXMLToolStripMenuItem.Click += new System.EventHandler(this.writeXMLToolStripMenuItem_Click);
            // 
            // jorgeButtonToolStripMenuItem
            // 
            this.jorgeButtonToolStripMenuItem.Name = "jorgeButtonToolStripMenuItem";
            this.jorgeButtonToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.jorgeButtonToolStripMenuItem.Text = "Jorge Button";
            this.jorgeButtonToolStripMenuItem.Click += new System.EventHandler(this.jorgeButtonToolStripMenuItem_Click);
            // 
            // debugMethodToolStripMenuItem
            // 
            this.debugMethodToolStripMenuItem.Name = "debugMethodToolStripMenuItem";
            this.debugMethodToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.debugMethodToolStripMenuItem.Text = "Debug Method";
            this.debugMethodToolStripMenuItem.Click += new System.EventHandler(this.debugMethodToolStripMenuItem_Click);
            // 
            // Button_InsertRow
            // 
            this.Button_InsertRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_InsertRow.Location = new System.Drawing.Point(882, 584);
            this.Button_InsertRow.Name = "Button_InsertRow";
            this.Button_InsertRow.Size = new System.Drawing.Size(50, 40);
            this.Button_InsertRow.TabIndex = 7;
            this.Button_InsertRow.Text = "Insert";
            this.ToolTip.SetToolTip(this.Button_InsertRow, "Insert a new row above the currently selected row");
            this.Button_InsertRow.UseVisualStyleBackColor = true;
            this.Button_InsertRow.Click += new System.EventHandler(this.Button_InsertRow_Click);
            // 
            // Button_DeleteRow
            // 
            this.Button_DeleteRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_DeleteRow.Location = new System.Drawing.Point(882, 630);
            this.Button_DeleteRow.Name = "Button_DeleteRow";
            this.Button_DeleteRow.Size = new System.Drawing.Size(50, 40);
            this.Button_DeleteRow.TabIndex = 8;
            this.Button_DeleteRow.Text = "Delete";
            this.ToolTip.SetToolTip(this.Button_DeleteRow, "Delete the currently selected row");
            this.Button_DeleteRow.UseVisualStyleBackColor = true;
            this.Button_DeleteRow.Click += new System.EventHandler(this.Button_DeleteRow_Click);
            // 
            // Button_MoveRowUp
            // 
            this.Button_MoveRowUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_MoveRowUp.Location = new System.Drawing.Point(882, 478);
            this.Button_MoveRowUp.Name = "Button_MoveRowUp";
            this.Button_MoveRowUp.Size = new System.Drawing.Size(50, 40);
            this.Button_MoveRowUp.TabIndex = 9;
            this.Button_MoveRowUp.Text = "Up";
            this.ToolTip.SetToolTip(this.Button_MoveRowUp, "Move the currently selected row up");
            this.Button_MoveRowUp.UseVisualStyleBackColor = true;
            this.Button_MoveRowUp.Click += new System.EventHandler(this.Button_MoveRowUp_Click);
            // 
            // Button_MoveRowDown
            // 
            this.Button_MoveRowDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_MoveRowDown.Location = new System.Drawing.Point(882, 524);
            this.Button_MoveRowDown.Name = "Button_MoveRowDown";
            this.Button_MoveRowDown.Size = new System.Drawing.Size(50, 40);
            this.Button_MoveRowDown.TabIndex = 10;
            this.Button_MoveRowDown.Text = "Down";
            this.ToolTip.SetToolTip(this.Button_MoveRowDown, "Move the currently selected row down");
            this.Button_MoveRowDown.UseVisualStyleBackColor = true;
            this.Button_MoveRowDown.Click += new System.EventHandler(this.Button_MoveRowDown_Click);
            // 
            // Button_PlayAndPause
            // 
            this.Button_PlayAndPause.Location = new System.Drawing.Point(807, 193);
            this.Button_PlayAndPause.Name = "Button_PlayAndPause";
            this.Button_PlayAndPause.Size = new System.Drawing.Size(125, 23);
            this.Button_PlayAndPause.TabIndex = 12;
            this.Button_PlayAndPause.Text = "Play";
            this.ToolTip.SetToolTip(this.Button_PlayAndPause, "Play or Pause the Video");
            this.Button_PlayAndPause.UseVisualStyleBackColor = true;
            this.Button_PlayAndPause.Click += new System.EventHandler(this.TogglePlay);
            // 
            // TrackBar_Timeline
            // 
            this.TrackBar_Timeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackBar_Timeline.Location = new System.Drawing.Point(341, 222);
            this.TrackBar_Timeline.Name = "TrackBar_Timeline";
            this.TrackBar_Timeline.Size = new System.Drawing.Size(591, 45);
            this.TrackBar_Timeline.TabIndex = 14;
            // 
            // Button_ShowLabels
            // 
            this.Button_ShowLabels.Location = new System.Drawing.Point(341, 193);
            this.Button_ShowLabels.Name = "Button_ShowLabels";
            this.Button_ShowLabels.Size = new System.Drawing.Size(109, 23);
            this.Button_ShowLabels.TabIndex = 18;
            this.Button_ShowLabels.Text = "Show/Hide Labels";
            this.ToolTip.SetToolTip(this.Button_ShowLabels, "Show or hide labels on Timeline.");
            this.Button_ShowLabels.UseVisualStyleBackColor = true;
            this.Button_ShowLabels.Click += new System.EventHandler(this.Button_ShowLabels_Click);
            // 
            // Button_ZoomTimelineIn
            // 
            this.Button_ZoomTimelineIn.Location = new System.Drawing.Point(456, 193);
            this.Button_ZoomTimelineIn.Name = "Button_ZoomTimelineIn";
            this.Button_ZoomTimelineIn.Size = new System.Drawing.Size(75, 23);
            this.Button_ZoomTimelineIn.TabIndex = 19;
            this.Button_ZoomTimelineIn.Text = "Zoom In";
            this.ToolTip.SetToolTip(this.Button_ZoomTimelineIn, "Zoom the timeline inwards, showing less");
            this.Button_ZoomTimelineIn.UseVisualStyleBackColor = true;
            this.Button_ZoomTimelineIn.Click += new System.EventHandler(this.Button_ZoomTimelineIn_Click);
            // 
            // Button_ZoomTimelineOut
            // 
            this.Button_ZoomTimelineOut.Location = new System.Drawing.Point(537, 193);
            this.Button_ZoomTimelineOut.Name = "Button_ZoomTimelineOut";
            this.Button_ZoomTimelineOut.Size = new System.Drawing.Size(75, 23);
            this.Button_ZoomTimelineOut.TabIndex = 20;
            this.Button_ZoomTimelineOut.Text = "Zoom Out";
            this.ToolTip.SetToolTip(this.Button_ZoomTimelineOut, "Zoom the timeline outwards, showing more");
            this.Button_ZoomTimelineOut.UseVisualStyleBackColor = true;
            this.Button_ZoomTimelineOut.Click += new System.EventHandler(this.Button_ZoomTimelineOut_Click);
            // 
            // Button_ZoomReset
            // 
            this.Button_ZoomReset.Location = new System.Drawing.Point(618, 193);
            this.Button_ZoomReset.Name = "Button_ZoomReset";
            this.Button_ZoomReset.Size = new System.Drawing.Size(82, 23);
            this.Button_ZoomReset.TabIndex = 21;
            this.Button_ZoomReset.Text = "Reset Zoom";
            this.ToolTip.SetToolTip(this.Button_ZoomReset, "Reset the zoom level back to the default zoom level");
            this.Button_ZoomReset.UseVisualStyleBackColor = true;
            this.Button_ZoomReset.Click += new System.EventHandler(this.Button_ZoomReset_Click);
            // 
            // GB_Location
            // 
            this.GB_Location.Controls.Add(this.RB_BottomRight);
            this.GB_Location.Controls.Add(this.RB_BottomCenter);
            this.GB_Location.Controls.Add(this.RB_BottomLeft);
            this.GB_Location.Controls.Add(this.RB_MiddleRight);
            this.GB_Location.Controls.Add(this.RB_MiddleCenter);
            this.GB_Location.Controls.Add(this.RB_MiddleLeft);
            this.GB_Location.Controls.Add(this.RB_TopCenter);
            this.GB_Location.Controls.Add(this.RB_TopLeft);
            this.GB_Location.Controls.Add(this.RB_TopRight);
            this.GB_Location.Location = new System.Drawing.Point(507, 46);
            this.GB_Location.Name = "GB_Location";
            this.GB_Location.Size = new System.Drawing.Size(89, 90);
            this.GB_Location.TabIndex = 25;
            this.GB_Location.TabStop = false;
            this.GB_Location.Text = "Location";
            // 
            // RB_BottomRight
            // 
            this.RB_BottomRight.AutoSize = true;
            this.RB_BottomRight.Location = new System.Drawing.Point(70, 63);
            this.RB_BottomRight.Name = "RB_BottomRight";
            this.RB_BottomRight.Size = new System.Drawing.Size(14, 13);
            this.RB_BottomRight.TabIndex = 8;
            this.RB_BottomRight.TabStop = true;
            this.RB_BottomRight.UseVisualStyleBackColor = true;
            this.RB_BottomRight.Click += new System.EventHandler(this.RB_BottomRight_Click);
            // 
            // RB_BottomCenter
            // 
            this.RB_BottomCenter.AutoSize = true;
            this.RB_BottomCenter.Location = new System.Drawing.Point(38, 63);
            this.RB_BottomCenter.Name = "RB_BottomCenter";
            this.RB_BottomCenter.Size = new System.Drawing.Size(14, 13);
            this.RB_BottomCenter.TabIndex = 7;
            this.RB_BottomCenter.TabStop = true;
            this.RB_BottomCenter.UseVisualStyleBackColor = true;
            this.RB_BottomCenter.Click += new System.EventHandler(this.RB_BottomCenter_Click);
            // 
            // RB_BottomLeft
            // 
            this.RB_BottomLeft.AutoSize = true;
            this.RB_BottomLeft.Location = new System.Drawing.Point(6, 63);
            this.RB_BottomLeft.Name = "RB_BottomLeft";
            this.RB_BottomLeft.Size = new System.Drawing.Size(14, 13);
            this.RB_BottomLeft.TabIndex = 6;
            this.RB_BottomLeft.TabStop = true;
            this.RB_BottomLeft.UseVisualStyleBackColor = true;
            this.RB_BottomLeft.Click += new System.EventHandler(this.RB_BottomLeft_Click);
            // 
            // RB_MiddleRight
            // 
            this.RB_MiddleRight.AutoSize = true;
            this.RB_MiddleRight.Location = new System.Drawing.Point(70, 42);
            this.RB_MiddleRight.Name = "RB_MiddleRight";
            this.RB_MiddleRight.Size = new System.Drawing.Size(14, 13);
            this.RB_MiddleRight.TabIndex = 5;
            this.RB_MiddleRight.TabStop = true;
            this.RB_MiddleRight.UseVisualStyleBackColor = true;
            this.RB_MiddleRight.Click += new System.EventHandler(this.RB_MiddleRight_Click);
            // 
            // RB_MiddleCenter
            // 
            this.RB_MiddleCenter.AutoSize = true;
            this.RB_MiddleCenter.Location = new System.Drawing.Point(38, 40);
            this.RB_MiddleCenter.Name = "RB_MiddleCenter";
            this.RB_MiddleCenter.Size = new System.Drawing.Size(14, 13);
            this.RB_MiddleCenter.TabIndex = 4;
            this.RB_MiddleCenter.TabStop = true;
            this.RB_MiddleCenter.UseVisualStyleBackColor = true;
            this.RB_MiddleCenter.Click += new System.EventHandler(this.RB_MiddleCenter_Click);
            // 
            // RB_MiddleLeft
            // 
            this.RB_MiddleLeft.AutoSize = true;
            this.RB_MiddleLeft.Location = new System.Drawing.Point(6, 40);
            this.RB_MiddleLeft.Name = "RB_MiddleLeft";
            this.RB_MiddleLeft.Size = new System.Drawing.Size(14, 13);
            this.RB_MiddleLeft.TabIndex = 3;
            this.RB_MiddleLeft.TabStop = true;
            this.RB_MiddleLeft.UseVisualStyleBackColor = true;
            this.RB_MiddleLeft.Click += new System.EventHandler(this.RB_MiddleLeft_Click);
            // 
            // RB_TopCenter
            // 
            this.RB_TopCenter.AutoSize = true;
            this.RB_TopCenter.Location = new System.Drawing.Point(38, 17);
            this.RB_TopCenter.Name = "RB_TopCenter";
            this.RB_TopCenter.Size = new System.Drawing.Size(14, 13);
            this.RB_TopCenter.TabIndex = 1;
            this.RB_TopCenter.TabStop = true;
            this.RB_TopCenter.UseVisualStyleBackColor = true;
            this.RB_TopCenter.Click += new System.EventHandler(this.RB_TopCenter_Click);
            // 
            // RB_TopLeft
            // 
            this.RB_TopLeft.AutoSize = true;
            this.RB_TopLeft.Location = new System.Drawing.Point(6, 17);
            this.RB_TopLeft.Name = "RB_TopLeft";
            this.RB_TopLeft.Size = new System.Drawing.Size(14, 13);
            this.RB_TopLeft.TabIndex = 0;
            this.RB_TopLeft.TabStop = true;
            this.RB_TopLeft.UseVisualStyleBackColor = true;
            this.RB_TopLeft.Click += new System.EventHandler(this.RB_TopLeft_Click);
            // 
            // RB_TopRight
            // 
            this.RB_TopRight.AutoSize = true;
            this.RB_TopRight.Location = new System.Drawing.Point(70, 17);
            this.RB_TopRight.Name = "RB_TopRight";
            this.RB_TopRight.Size = new System.Drawing.Size(14, 13);
            this.RB_TopRight.TabIndex = 2;
            this.RB_TopRight.TabStop = true;
            this.RB_TopRight.UseVisualStyleBackColor = true;
            this.RB_TopRight.Click += new System.EventHandler(this.RB_TopRight_Click);
            // 
            // GB_EmotionType
            // 
            this.GB_EmotionType.Controls.Add(this.RB_Anger);
            this.GB_EmotionType.Controls.Add(this.RB_Fear);
            this.GB_EmotionType.Controls.Add(this.RB_Sad);
            this.GB_EmotionType.Controls.Add(this.RB_Happy);
            this.GB_EmotionType.Controls.Add(this.RB_None);
            this.GB_EmotionType.Location = new System.Drawing.Point(341, 46);
            this.GB_EmotionType.Name = "GB_EmotionType";
            this.GB_EmotionType.Size = new System.Drawing.Size(78, 139);
            this.GB_EmotionType.TabIndex = 26;
            this.GB_EmotionType.TabStop = false;
            this.GB_EmotionType.Text = "Emotion";
            // 
            // RB_Anger
            // 
            this.RB_Anger.AutoSize = true;
            this.RB_Anger.Location = new System.Drawing.Point(9, 109);
            this.RB_Anger.Name = "RB_Anger";
            this.RB_Anger.Size = new System.Drawing.Size(53, 17);
            this.RB_Anger.TabIndex = 4;
            this.RB_Anger.TabStop = true;
            this.RB_Anger.Text = "Anger";
            this.RB_Anger.UseVisualStyleBackColor = true;
            this.RB_Anger.Click += new System.EventHandler(this.RB_Anger_Click);
            // 
            // RB_Fear
            // 
            this.RB_Fear.AutoSize = true;
            this.RB_Fear.Location = new System.Drawing.Point(9, 86);
            this.RB_Fear.Name = "RB_Fear";
            this.RB_Fear.Size = new System.Drawing.Size(46, 17);
            this.RB_Fear.TabIndex = 3;
            this.RB_Fear.TabStop = true;
            this.RB_Fear.Text = "Fear";
            this.RB_Fear.UseVisualStyleBackColor = true;
            this.RB_Fear.Click += new System.EventHandler(this.RB_Fear_Click);
            // 
            // RB_Sad
            // 
            this.RB_Sad.AutoSize = true;
            this.RB_Sad.Location = new System.Drawing.Point(9, 63);
            this.RB_Sad.Name = "RB_Sad";
            this.RB_Sad.Size = new System.Drawing.Size(44, 17);
            this.RB_Sad.TabIndex = 2;
            this.RB_Sad.TabStop = true;
            this.RB_Sad.Text = "Sad";
            this.RB_Sad.UseVisualStyleBackColor = true;
            this.RB_Sad.Click += new System.EventHandler(this.RB_Sad_Click);
            // 
            // RB_Happy
            // 
            this.RB_Happy.AutoSize = true;
            this.RB_Happy.Location = new System.Drawing.Point(9, 40);
            this.RB_Happy.Name = "RB_Happy";
            this.RB_Happy.Size = new System.Drawing.Size(56, 17);
            this.RB_Happy.TabIndex = 1;
            this.RB_Happy.TabStop = true;
            this.RB_Happy.Text = "Happy";
            this.RB_Happy.UseVisualStyleBackColor = true;
            this.RB_Happy.Click += new System.EventHandler(this.RB_Happy_Click);
            // 
            // RB_None
            // 
            this.RB_None.AutoSize = true;
            this.RB_None.Location = new System.Drawing.Point(9, 17);
            this.RB_None.Name = "RB_None";
            this.RB_None.Size = new System.Drawing.Size(51, 17);
            this.RB_None.TabIndex = 0;
            this.RB_None.TabStop = true;
            this.RB_None.Text = "None";
            this.RB_None.UseVisualStyleBackColor = true;
            this.RB_None.Click += new System.EventHandler(this.RB_None_Click);
            // 
            // GB_Intensity
            // 
            this.GB_Intensity.Controls.Add(this.RB_HighIntensity);
            this.GB_Intensity.Controls.Add(this.RB_MediumIntensity);
            this.GB_Intensity.Controls.Add(this.RB_LowIntensity);
            this.GB_Intensity.Location = new System.Drawing.Point(425, 46);
            this.GB_Intensity.Name = "GB_Intensity";
            this.GB_Intensity.Size = new System.Drawing.Size(76, 90);
            this.GB_Intensity.TabIndex = 27;
            this.GB_Intensity.TabStop = false;
            this.GB_Intensity.Text = "Intensity";
            // 
            // RB_HighIntensity
            // 
            this.RB_HighIntensity.AutoSize = true;
            this.RB_HighIntensity.Location = new System.Drawing.Point(6, 61);
            this.RB_HighIntensity.Name = "RB_HighIntensity";
            this.RB_HighIntensity.Size = new System.Drawing.Size(47, 17);
            this.RB_HighIntensity.TabIndex = 2;
            this.RB_HighIntensity.TabStop = true;
            this.RB_HighIntensity.Text = "High";
            this.RB_HighIntensity.UseVisualStyleBackColor = true;
            this.RB_HighIntensity.Click += new System.EventHandler(this.RB_HighIntensity_Click);
            // 
            // RB_MediumIntensity
            // 
            this.RB_MediumIntensity.AutoSize = true;
            this.RB_MediumIntensity.Location = new System.Drawing.Point(6, 38);
            this.RB_MediumIntensity.Name = "RB_MediumIntensity";
            this.RB_MediumIntensity.Size = new System.Drawing.Size(62, 17);
            this.RB_MediumIntensity.TabIndex = 1;
            this.RB_MediumIntensity.TabStop = true;
            this.RB_MediumIntensity.Text = "Medium";
            this.RB_MediumIntensity.UseVisualStyleBackColor = true;
            this.RB_MediumIntensity.Click += new System.EventHandler(this.RB_MediumIntensity_Click);
            // 
            // RB_LowIntensity
            // 
            this.RB_LowIntensity.AutoSize = true;
            this.RB_LowIntensity.Location = new System.Drawing.Point(6, 15);
            this.RB_LowIntensity.Name = "RB_LowIntensity";
            this.RB_LowIntensity.Size = new System.Drawing.Size(45, 17);
            this.RB_LowIntensity.TabIndex = 0;
            this.RB_LowIntensity.TabStop = true;
            this.RB_LowIntensity.Text = "Low";
            this.RB_LowIntensity.UseVisualStyleBackColor = true;
            this.RB_LowIntensity.Click += new System.EventHandler(this.RB_LowIntensity_Click);
            // 
            // Button_LeftAlign
            // 
            this.Button_LeftAlign.Location = new System.Drawing.Point(425, 142);
            this.Button_LeftAlign.Name = "Button_LeftAlign";
            this.Button_LeftAlign.Size = new System.Drawing.Size(30, 30);
            this.Button_LeftAlign.TabIndex = 28;
            this.Button_LeftAlign.Text = "L";
            this.Button_LeftAlign.UseVisualStyleBackColor = true;
            this.Button_LeftAlign.Click += new System.EventHandler(this.Button_LeftAlign_Click);
            // 
            // Button_CenterAlign
            // 
            this.Button_CenterAlign.Location = new System.Drawing.Point(461, 142);
            this.Button_CenterAlign.Name = "Button_CenterAlign";
            this.Button_CenterAlign.Size = new System.Drawing.Size(30, 30);
            this.Button_CenterAlign.TabIndex = 29;
            this.Button_CenterAlign.Text = "C";
            this.Button_CenterAlign.UseVisualStyleBackColor = true;
            this.Button_CenterAlign.Click += new System.EventHandler(this.Button_CenterAlign_Click);
            // 
            // Button_RightAlign
            // 
            this.Button_RightAlign.Location = new System.Drawing.Point(497, 142);
            this.Button_RightAlign.Name = "Button_RightAlign";
            this.Button_RightAlign.Size = new System.Drawing.Size(30, 30);
            this.Button_RightAlign.TabIndex = 30;
            this.Button_RightAlign.Text = "R";
            this.Button_RightAlign.UseVisualStyleBackColor = true;
            this.Button_RightAlign.Click += new System.EventHandler(this.Button_RightAlign_Click);
            // 
            // OpenProjectDialog
            // 
            this.OpenProjectDialog.Filter = "EnACT Project Files| *.enproj; |All Files|*.*";
            // 
            // CaptionTextBox
            // 
            this.CaptionTextBox.Caption = null;
            this.CaptionTextBox.HideSelection = false;
            this.CaptionTextBox.Location = new System.Drawing.Point(602, 50);
            this.CaptionTextBox.Name = "CaptionTextBox";
            this.CaptionTextBox.SelectionMode = CaptionTextBoxSelectionMode.NoSelection;
            this.CaptionTextBox.Size = new System.Drawing.Size(274, 86);
            this.CaptionTextBox.TabIndex = 24;
            this.CaptionTextBox.Text = "";
            // 
            // PlayheadLabel
            // 
            this.PlayheadLabel.AutoSize = true;
            this.PlayheadLabel.Location = new System.Drawing.Point(338, 32);
            this.PlayheadLabel.Name = "PlayheadLabel";
            timestamp1.AsDouble = 0D;
            timestamp1.AsString = "00:00:00.0";
            this.PlayheadLabel.PlayheadTime = timestamp1;
            this.PlayheadLabel.Size = new System.Drawing.Size(120, 13);
            this.PlayheadLabel.TabIndex = 23;
            this.PlayheadLabel.Text = "00:00:00.0 / 00:00:00.0";
            timestamp2.AsDouble = 0D;
            timestamp2.AsString = "00:00:00.0";
            this.PlayheadLabel.VideoLength = timestamp2;
            // 
            // Timeline
            // 
            this.Timeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Timeline.AutoScroll = true;
            this.Timeline.AutoScrollMinSize = new System.Drawing.Size(920, 0);
            this.Timeline.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Timeline.CaptionList = null;
            this.Timeline.DrawLocationLabels = true;
            this.Timeline.Location = new System.Drawing.Point(12, 273);
            this.Timeline.MinimumSize = new System.Drawing.Size(0, 199);
            this.Timeline.Name = "Timeline";
            this.Timeline.PlayHeadTime = 0D;
            this.Timeline.Size = new System.Drawing.Size(920, 199);
            this.Timeline.SpeakerSet = null;
            this.Timeline.TabIndex = 15;
            this.Timeline.TimeWidth = 10D;
            this.Timeline.VideoLength = 0D;
            // 
            // EngineView
            // 
            this.EngineView.Enabled = true;
            this.EngineView.Location = new System.Drawing.Point(12, 27);
            this.EngineView.Name = "EngineView";
            this.EngineView.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("EngineView.OcxState")));
            this.EngineView.Size = new System.Drawing.Size(320, 240);
            this.EngineView.TabIndex = 11;
            // 
            // CaptionView
            // 
            this.CaptionView.AllowUserToAddRows = false;
            this.CaptionView.AllowUserToDeleteRows = false;
            this.CaptionView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CaptionView.CaptionSource = null;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CaptionView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CaptionView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CaptionView.Location = new System.Drawing.Point(12, 478);
            this.CaptionView.Name = "CaptionView";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CaptionView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.CaptionView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CaptionView.Size = new System.Drawing.Size(864, 192);
            this.CaptionView.SpeakerSet = null;
            this.CaptionView.TabIndex = 1;
            this.CaptionView.UserInputEnabled = true;
            this.CaptionView.SelectionChanged += new System.EventHandler(this.CaptionView_SelectionChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 682);
            this.Controls.Add(this.Button_RightAlign);
            this.Controls.Add(this.Button_CenterAlign);
            this.Controls.Add(this.Button_LeftAlign);
            this.Controls.Add(this.GB_Intensity);
            this.Controls.Add(this.GB_EmotionType);
            this.Controls.Add(this.GB_Location);
            this.Controls.Add(this.CaptionTextBox);
            this.Controls.Add(this.PlayheadLabel);
            this.Controls.Add(this.Button_ZoomReset);
            this.Controls.Add(this.Button_ZoomTimelineOut);
            this.Controls.Add(this.Button_ZoomTimelineIn);
            this.Controls.Add(this.Button_ShowLabels);
            this.Controls.Add(this.Timeline);
            this.Controls.Add(this.TrackBar_Timeline);
            this.Controls.Add(this.Button_PlayAndPause);
            this.Controls.Add(this.EngineView);
            this.Controls.Add(this.Button_MoveRowDown);
            this.Controls.Add(this.Button_MoveRowUp);
            this.Controls.Add(this.Button_DeleteRow);
            this.Controls.Add(this.Button_InsertRow);
            this.Controls.Add(this.CaptionView);
            this.Controls.Add(this.MenuStrip_MainForm);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainMenuStrip = this.MenuStrip_MainForm;
            this.MinimumSize = new System.Drawing.Size(960, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EnACT";
            this.MenuStrip_MainForm.ResumeLayout(false);
            this.MenuStrip_MainForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_Timeline)).EndInit();
            this.GB_Location.ResumeLayout(false);
            this.GB_Location.PerformLayout();
            this.GB_EmotionType.ResumeLayout(false);
            this.GB_EmotionType.PerformLayout();
            this.GB_Intensity.ResumeLayout(false);
            this.GB_Intensity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EngineView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptionView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip_MainForm;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private CaptionView CaptionView;
        private System.Windows.Forms.Button Button_InsertRow;
        private System.Windows.Forms.Button Button_DeleteRow;
        private System.Windows.Forms.Button Button_MoveRowUp;
        private System.Windows.Forms.Button Button_MoveRowDown;
        private EngineView EngineView;
        private System.Windows.Forms.Button Button_PlayAndPause;
        private System.Windows.Forms.TrackBar TrackBar_Timeline;
        private System.Windows.Forms.Timer PlayheadTimer;
        private Timeline Timeline;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Button Button_ShowLabels;
        private System.Windows.Forms.Button Button_ZoomTimelineIn;
        private System.Windows.Forms.Button Button_ZoomTimelineOut;
        private System.Windows.Forms.Button Button_ZoomReset;
        private PlayheadLabel PlayheadLabel;
        private System.Windows.Forms.ToolStripMenuItem parseScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parseesrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jorgeButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugMethodToolStripMenuItem;
        private CaptionTextBox CaptionTextBox;
        private System.Windows.Forms.GroupBox GB_Location;
        private System.Windows.Forms.RadioButton RB_BottomRight;
        private System.Windows.Forms.RadioButton RB_BottomCenter;
        private System.Windows.Forms.RadioButton RB_BottomLeft;
        private System.Windows.Forms.RadioButton RB_MiddleRight;
        private System.Windows.Forms.RadioButton RB_MiddleCenter;
        private System.Windows.Forms.RadioButton RB_MiddleLeft;
        private System.Windows.Forms.RadioButton RB_TopRight;
        private System.Windows.Forms.RadioButton RB_TopCenter;
        private System.Windows.Forms.RadioButton RB_TopLeft;
        private System.Windows.Forms.GroupBox GB_EmotionType;
        private System.Windows.Forms.RadioButton RB_Anger;
        private System.Windows.Forms.RadioButton RB_Fear;
        private System.Windows.Forms.RadioButton RB_Sad;
        private System.Windows.Forms.RadioButton RB_Happy;
        private System.Windows.Forms.RadioButton RB_None;
        private System.Windows.Forms.GroupBox GB_Intensity;
        private System.Windows.Forms.RadioButton RB_HighIntensity;
        private System.Windows.Forms.RadioButton RB_MediumIntensity;
        private System.Windows.Forms.RadioButton RB_LowIntensity;
        private System.Windows.Forms.Button Button_LeftAlign;
        private System.Windows.Forms.Button Button_CenterAlign;
        private System.Windows.Forms.Button Button_RightAlign;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addSpeakerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCaptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectSettingsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenProjectDialog;
    }
}

