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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainFormMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CaptionView = new System.Windows.Forms.DataGridView();
            this.PopulateButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.InsertRowBut = new System.Windows.Forms.Button();
            this.DeleteRowBut = new System.Windows.Forms.Button();
            this.MoveRowUpBut = new System.Windows.Forms.Button();
            this.MoveRowDownBut = new System.Windows.Forms.Button();
            this.PlayAndPause = new System.Windows.Forms.Button();
            this.JorgeButton = new System.Windows.Forms.Button();
            this.GhettoTimeLine = new System.Windows.Forms.TrackBar();
            this.PlayheadTimer = new System.Windows.Forms.Timer(this.components);
            this.Timeline = new EnACT.Timeline();
            this.FlashVideoPlayer = new EnACT.EngineView();
            this.MainFormMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CaptionView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GhettoTimeLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlashVideoPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // MainFormMenu
            // 
            this.MainFormMenu.BackColor = System.Drawing.SystemColors.Control;
            this.MainFormMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.MainFormMenu.Location = new System.Drawing.Point(0, 0);
            this.MainFormMenu.Name = "MainFormMenu";
            this.MainFormMenu.Size = new System.Drawing.Size(944, 24);
            this.MainFormMenu.TabIndex = 0;
            this.MainFormMenu.Text = "MainMenuForm";
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
            this.parseTestToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // parseTestToolStripMenuItem
            // 
            this.parseTestToolStripMenuItem.Name = "parseTestToolStripMenuItem";
            this.parseTestToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.parseTestToolStripMenuItem.Text = "Parse Test";
            // 
            // CaptionView
            // 
            this.CaptionView.AllowUserToAddRows = false;
            this.CaptionView.AllowUserToDeleteRows = false;
            this.CaptionView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CaptionView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CaptionView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CaptionView.Location = new System.Drawing.Point(12, 478);
            this.CaptionView.Name = "CaptionView";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CaptionView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.CaptionView.Size = new System.Drawing.Size(864, 192);
            this.CaptionView.TabIndex = 1;
            this.CaptionView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.CaptionView_CellValueChanged);
            // 
            // PopulateButton
            // 
            this.PopulateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PopulateButton.Location = new System.Drawing.Point(807, 85);
            this.PopulateButton.Name = "PopulateButton";
            this.PopulateButton.Size = new System.Drawing.Size(125, 23);
            this.PopulateButton.TabIndex = 4;
            this.PopulateButton.Text = "Populate";
            this.PopulateButton.UseVisualStyleBackColor = true;
            this.PopulateButton.Click += new System.EventHandler(this.PopulateButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(807, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Write XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.WriteXML);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(807, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Parse Script";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ParseText);
            // 
            // InsertRowBut
            // 
            this.InsertRowBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InsertRowBut.Location = new System.Drawing.Point(882, 584);
            this.InsertRowBut.Name = "InsertRowBut";
            this.InsertRowBut.Size = new System.Drawing.Size(50, 40);
            this.InsertRowBut.TabIndex = 7;
            this.InsertRowBut.Text = "Insert";
            this.InsertRowBut.UseVisualStyleBackColor = true;
            this.InsertRowBut.Click += new System.EventHandler(this.InsertRowBut_Click);
            // 
            // DeleteRowBut
            // 
            this.DeleteRowBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteRowBut.Location = new System.Drawing.Point(882, 630);
            this.DeleteRowBut.Name = "DeleteRowBut";
            this.DeleteRowBut.Size = new System.Drawing.Size(50, 40);
            this.DeleteRowBut.TabIndex = 8;
            this.DeleteRowBut.Text = "Delete";
            this.DeleteRowBut.UseVisualStyleBackColor = true;
            this.DeleteRowBut.Click += new System.EventHandler(this.DeleteRowBut_Click);
            // 
            // MoveRowUpBut
            // 
            this.MoveRowUpBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveRowUpBut.Location = new System.Drawing.Point(882, 478);
            this.MoveRowUpBut.Name = "MoveRowUpBut";
            this.MoveRowUpBut.Size = new System.Drawing.Size(50, 40);
            this.MoveRowUpBut.TabIndex = 9;
            this.MoveRowUpBut.Text = "Up";
            this.MoveRowUpBut.UseVisualStyleBackColor = true;
            this.MoveRowUpBut.Click += new System.EventHandler(this.MoveRowUpBut_Click);
            // 
            // MoveRowDownBut
            // 
            this.MoveRowDownBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveRowDownBut.Location = new System.Drawing.Point(882, 524);
            this.MoveRowDownBut.Name = "MoveRowDownBut";
            this.MoveRowDownBut.Size = new System.Drawing.Size(50, 40);
            this.MoveRowDownBut.TabIndex = 10;
            this.MoveRowDownBut.Text = "Down";
            this.MoveRowDownBut.UseVisualStyleBackColor = true;
            this.MoveRowDownBut.Click += new System.EventHandler(this.MoveRowDownBut_Click);
            // 
            // PlayAndPause
            // 
            this.PlayAndPause.Location = new System.Drawing.Point(562, 193);
            this.PlayAndPause.Name = "PlayAndPause";
            this.PlayAndPause.Size = new System.Drawing.Size(125, 23);
            this.PlayAndPause.TabIndex = 12;
            this.PlayAndPause.Text = "Play";
            this.PlayAndPause.UseVisualStyleBackColor = true;
            this.PlayAndPause.Click += new System.EventHandler(this.TogglePlay);
            // 
            // JorgeButton
            // 
            this.JorgeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JorgeButton.Location = new System.Drawing.Point(807, 114);
            this.JorgeButton.Name = "JorgeButton";
            this.JorgeButton.Size = new System.Drawing.Size(125, 23);
            this.JorgeButton.TabIndex = 13;
            this.JorgeButton.Text = "Jorge Button";
            this.JorgeButton.UseVisualStyleBackColor = true;
            this.JorgeButton.Click += new System.EventHandler(this.JorgeButton_Click);
            // 
            // GhettoTimeLine
            // 
            this.GhettoTimeLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GhettoTimeLine.Location = new System.Drawing.Point(338, 222);
            this.GhettoTimeLine.Name = "GhettoTimeLine";
            this.GhettoTimeLine.Size = new System.Drawing.Size(594, 45);
            this.GhettoTimeLine.TabIndex = 14;
            this.GhettoTimeLine.ValueChanged += new System.EventHandler(this.TimeLine_ValueChanged);
            // 
            // PlayheadTimer
            // 
            this.PlayheadTimer.Tick += new System.EventHandler(this.PlayheadTimer_Tick);
            // 
            // Timeline
            // 
            this.Timeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Timeline.AutoScroll = true;
            this.Timeline.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Timeline.Location = new System.Drawing.Point(12, 273);
            this.Timeline.MinimumSize = new System.Drawing.Size(0, 199);
            this.Timeline.Name = "Timeline";
            this.Timeline.Size = new System.Drawing.Size(920, 199);
            this.Timeline.TabIndex = 15;
            // 
            // FlashVideoPlayer
            // 
            this.FlashVideoPlayer.Enabled = true;
            this.FlashVideoPlayer.Location = new System.Drawing.Point(12, 27);
            this.FlashVideoPlayer.Name = "FlashVideoPlayer";
            this.FlashVideoPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("FlashVideoPlayer.OcxState")));
            this.FlashVideoPlayer.Size = new System.Drawing.Size(320, 240);
            this.FlashVideoPlayer.TabIndex = 11;
            this.FlashVideoPlayer.VideoLoaded += new EnACT.EngineView.VideoLoadedHandler(this.FlashVideoPlayer_VideoLoaded);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 682);
            this.Controls.Add(this.Timeline);
            this.Controls.Add(this.GhettoTimeLine);
            this.Controls.Add(this.JorgeButton);
            this.Controls.Add(this.PlayAndPause);
            this.Controls.Add(this.FlashVideoPlayer);
            this.Controls.Add(this.MoveRowDownBut);
            this.Controls.Add(this.MoveRowUpBut);
            this.Controls.Add(this.DeleteRowBut);
            this.Controls.Add(this.InsertRowBut);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PopulateButton);
            this.Controls.Add(this.CaptionView);
            this.Controls.Add(this.MainFormMenu);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainMenuStrip = this.MainFormMenu;
            this.MinimumSize = new System.Drawing.Size(960, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EnACT";
            this.MainFormMenu.ResumeLayout(false);
            this.MainFormMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CaptionView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GhettoTimeLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlashVideoPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainFormMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parseTestToolStripMenuItem;
        private System.Windows.Forms.DataGridView CaptionView;
        private System.Windows.Forms.Button PopulateButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button InsertRowBut;
        private System.Windows.Forms.Button DeleteRowBut;
        private System.Windows.Forms.Button MoveRowUpBut;
        private System.Windows.Forms.Button MoveRowDownBut;
        private EngineView FlashVideoPlayer;
        private System.Windows.Forms.Button PlayAndPause;
        private System.Windows.Forms.Button JorgeButton;
        private System.Windows.Forms.TrackBar GhettoTimeLine;
        private System.Windows.Forms.Timer PlayheadTimer;
        private Timeline Timeline;
    }
}

