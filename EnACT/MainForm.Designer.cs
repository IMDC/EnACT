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
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.CaptionTextBox = new EnACT.CaptionTextBox();
            this.PlayheadLabel = new EnACT.PlayheadLabel();
            this.Timeline = new EnACT.Timeline();
            this.EngineView = new EnACT.EngineView();
            this.CaptionView = new EnACT.CaptionView();
            this.MenuStrip_MainForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_Timeline)).BeginInit();
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
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
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
            // CaptionTextBox
            // 
            this.CaptionTextBox.Location = new System.Drawing.Point(341, 48);
            this.CaptionTextBox.Name = "CaptionTextBox";
            this.CaptionTextBox.Size = new System.Drawing.Size(591, 96);
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
    }
}

