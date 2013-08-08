namespace EnACT.Forms
{
    partial class JorgeForm
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
            this.JorgeSRTPathBox = new System.Windows.Forms.TextBox();
            this.JorgeOutputPathBox = new System.Windows.Forms.TextBox();
            this.JorgeSRTPathButton = new System.Windows.Forms.Button();
            this.JorgeOutputPathButton = new System.Windows.Forms.Button();
            this.JorgeButton = new System.Windows.Forms.Button();
            this.JorgeLabel1 = new System.Windows.Forms.Label();
            this.JorgeLabel2 = new System.Windows.Forms.Label();
            this.JorgeSRTFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.JorgeOutputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // JorgeSRTPathBox
            // 
            this.JorgeSRTPathBox.Location = new System.Drawing.Point(114, 11);
            this.JorgeSRTPathBox.Name = "JorgeSRTPathBox";
            this.JorgeSRTPathBox.Size = new System.Drawing.Size(308, 20);
            this.JorgeSRTPathBox.TabIndex = 0;
            // 
            // JorgeOutputPathBox
            // 
            this.JorgeOutputPathBox.Location = new System.Drawing.Point(114, 40);
            this.JorgeOutputPathBox.Name = "JorgeOutputPathBox";
            this.JorgeOutputPathBox.Size = new System.Drawing.Size(308, 20);
            this.JorgeOutputPathBox.TabIndex = 1;
            // 
            // JorgeSRTPathButton
            // 
            this.JorgeSRTPathButton.Location = new System.Drawing.Point(428, 9);
            this.JorgeSRTPathButton.Name = "JorgeSRTPathButton";
            this.JorgeSRTPathButton.Size = new System.Drawing.Size(91, 23);
            this.JorgeSRTPathButton.TabIndex = 2;
            this.JorgeSRTPathButton.Text = "Choose Path";
            this.JorgeSRTPathButton.UseVisualStyleBackColor = true;
            this.JorgeSRTPathButton.Click += new System.EventHandler(this.JorgeSRTPathButton_Click);
            // 
            // JorgeOutputPathButton
            // 
            this.JorgeOutputPathButton.Location = new System.Drawing.Point(428, 38);
            this.JorgeOutputPathButton.Name = "JorgeOutputPathButton";
            this.JorgeOutputPathButton.Size = new System.Drawing.Size(91, 23);
            this.JorgeOutputPathButton.TabIndex = 3;
            this.JorgeOutputPathButton.Text = "Choose Path";
            this.JorgeOutputPathButton.UseVisualStyleBackColor = true;
            this.JorgeOutputPathButton.Click += new System.EventHandler(this.JorgeOutputPathButton_Click);
            // 
            // JorgeButton
            // 
            this.JorgeButton.Location = new System.Drawing.Point(12, 67);
            this.JorgeButton.Name = "JorgeButton";
            this.JorgeButton.Size = new System.Drawing.Size(92, 23);
            this.JorgeButton.TabIndex = 4;
            this.JorgeButton.Text = "Write XML files";
            this.JorgeButton.UseVisualStyleBackColor = true;
            this.JorgeButton.Click += new System.EventHandler(this.JorgeButton_Click);
            // 
            // JorgeLabel1
            // 
            this.JorgeLabel1.AutoSize = true;
            this.JorgeLabel1.Location = new System.Drawing.Point(12, 14);
            this.JorgeLabel1.Name = "JorgeLabel1";
            this.JorgeLabel1.Size = new System.Drawing.Size(73, 13);
            this.JorgeLabel1.TabIndex = 5;
            this.JorgeLabel1.Text = "SRT File Path";
            // 
            // JorgeLabel2
            // 
            this.JorgeLabel2.AutoSize = true;
            this.JorgeLabel2.Location = new System.Drawing.Point(12, 43);
            this.JorgeLabel2.Name = "JorgeLabel2";
            this.JorgeLabel2.Size = new System.Drawing.Size(96, 13);
            this.JorgeLabel2.TabIndex = 6;
            this.JorgeLabel2.Text = "Output Folder Path";
            // 
            // JorgeSRTFileDialog
            // 
            this.JorgeSRTFileDialog.Filter = "SRT files|*.srt|All Files|*.*";
            // 
            // JorgeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 102);
            this.Controls.Add(this.JorgeLabel2);
            this.Controls.Add(this.JorgeLabel1);
            this.Controls.Add(this.JorgeButton);
            this.Controls.Add(this.JorgeOutputPathButton);
            this.Controls.Add(this.JorgeSRTPathButton);
            this.Controls.Add(this.JorgeOutputPathBox);
            this.Controls.Add(this.JorgeSRTPathBox);
            this.Name = "JorgeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Jorge Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox JorgeSRTPathBox;
        private System.Windows.Forms.TextBox JorgeOutputPathBox;
        private System.Windows.Forms.Button JorgeSRTPathButton;
        private System.Windows.Forms.Button JorgeOutputPathButton;
        private System.Windows.Forms.Button JorgeButton;
        private System.Windows.Forms.Label JorgeLabel1;
        private System.Windows.Forms.Label JorgeLabel2;
        private System.Windows.Forms.OpenFileDialog JorgeSRTFileDialog;
        private System.Windows.Forms.FolderBrowserDialog JorgeOutputFolderDialog;
    }
}