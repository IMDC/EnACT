namespace EnACT.Forms
{
    partial class PreviewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewForm));
            this.PreviewEngine = new AxShockwaveFlashObjects.AxShockwaveFlash();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewEngine)).BeginInit();
            this.SuspendLayout();
            // 
            // PreviewEngine
            // 
            this.PreviewEngine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewEngine.Enabled = true;
            this.PreviewEngine.Location = new System.Drawing.Point(0, 0);
            this.PreviewEngine.Name = "PreviewEngine";
            this.PreviewEngine.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("PreviewEngine.OcxState")));
            this.PreviewEngine.Size = new System.Drawing.Size(1280, 720);
            this.PreviewEngine.TabIndex = 0;
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.PreviewEngine);
            this.Name = "PreviewForm";
            this.Text = "PreviewForm";
            ((System.ComponentModel.ISupportInitialize)(this.PreviewEngine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxShockwaveFlashObjects.AxShockwaveFlash PreviewEngine;
    }
}