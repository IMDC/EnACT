using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnACT.Core;

namespace EnACT.Forms
{
    /// <summary>
    /// A Form that essentially just displays an enlarged instance of the flash engine, so that it
    /// can be previewed in actual size.
    /// </summary>
    public partial class PreviewForm : Form
    {
        public PreviewForm(ProjectInfo projectInfo)
        {
            InitializeComponent();

            if (projectInfo != null)
            {
                PreviewEngine.LoadMovie(0,projectInfo.EngineFile.AbsolutePath);
            }
        }
    }
}
