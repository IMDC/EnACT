using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EnACT.Core;
using LibEnACT;

namespace EnACT.Forms
{
    public partial class JorgeForm : Form
    {
        public Dictionary<string, Speaker> SpeakerSet { set; get; }
        public List<Caption> CaptionList { set; get; }
        public SettingsXml Settings { set; get; }

        public MainForm M { set; get; }

        public JorgeForm(Dictionary<string,Speaker> speakerSet, List<Caption> captionList,SettingsXml settings, MainForm m)
        {
            InitializeComponent();
            this.SpeakerSet = speakerSet;
            this.CaptionList = captionList;
            this.Settings = settings;
            this.M = m;

            JorgeOutputFolderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        private void JorgeSRTPathButton_Click(object sender, EventArgs e)
        {
            JorgeSRTFileDialog.ShowDialog();
            JorgeSRTPathBox.Text = JorgeSRTFileDialog.FileName;
        }

        private void JorgeOutputPathButton_Click(object sender, EventArgs e)
        {
            JorgeOutputFolderDialog.ShowDialog();
            JorgeOutputPathBox.Text = JorgeOutputFolderDialog.SelectedPath;
        }

        private void JorgeButton_Click(object sender, EventArgs e)
        {
            M.JorgeMethod(JorgeSRTPathBox.Text, JorgeOutputPathBox.Text);
        }
    }
}
