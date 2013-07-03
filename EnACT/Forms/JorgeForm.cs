using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnACT
{
    public partial class JorgeForm : Form
    {
        public Dictionary<String, Speaker> SpeakerSet { set; get; }
        public List<EditorCaption> CaptionList { set; get; }
        public SettingsXML Settings { set; get; }

        public MainForm m { set; get; }

        public JorgeForm(Dictionary<String,Speaker> SpeakerSet, List<EditorCaption> CaptionList,SettingsXML Settings, MainForm m)
        {
            InitializeComponent();
            this.SpeakerSet = SpeakerSet;
            this.CaptionList = CaptionList;
            this.Settings = Settings;
            this.m = m;

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
            m.JorgeMethod(JorgeSRTPathBox.Text, JorgeOutputPathBox.Text);
        }
    }
}
