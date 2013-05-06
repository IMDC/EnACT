using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxShockwaveFlashObjects;

namespace EnACT
{
    /// <summary>
    /// A Flashplayer control designed to communicate with the swf that it has loaded.
    /// </summary>
    public partial class EngineView : AxShockwaveFlash
    {
        public EngineView() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Pauses the video
        /// </summary>
        public void Pause()
        {
            CallFunction("<invoke name=\"pause\" returntype=\"xml\"></invoke>");
        }

        /// <summary>
        /// Plays the video
        /// </summary>
        public override void Play()
        {
            CallFunction("<invoke name=\"play\" returntype=\"xml\"></invoke>");
        }
    }
}
