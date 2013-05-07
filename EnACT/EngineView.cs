using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxShockwaveFlashObjects;
using System.Xml;

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
            CallFunction("<invoke name=\"" + "pause" + "\" returntype=\"xml\"></invoke>");
        }

        /// <summary>
        /// Plays the video
        /// </summary>
        public override void Play()
        {
            CallFunction("<invoke name=\"" + "play" + "\" returntype=\"xml\"></invoke>");
        }

        /// <summary>
        /// Toggles the play state of the video.
        /// </summary>
        public void TogglePlay()
        {
            CallFunction("<invoke name=\"" + "togglePlay" + "\" returntype=\"xml\"></invoke>");
        }

        /// <summary>
        /// Returns true or false if the Engine is playing a video or not.
        /// </summary>
        /// <returns>true or false</returns>
        public override Boolean IsPlaying()
        {
            String returnXML = CallFunction("<invoke name=\"" + "isPlaying" + "\" returntype=\"xml\"></invoke>");
            //Console.WriteLine(returnXML);
            switch (returnXML)
            {
                case @"<true/>": return true;
                default: return false;
            }
        }
    }//Class
}//Namespace
