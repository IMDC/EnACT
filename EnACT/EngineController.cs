using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
{
    /// <summary>
    /// The video controller for enact. Controls interaction between user controls.
    /// </summary>
    public class EngineController
    {
        #region Controllable Objects
        /// <summary>
        /// The CaptionView used by EnACT to display captions in a table.
        /// </summary>
        public CaptionView CaptionView { set; get; }
        
        /// <summary>
        /// The video player used by EnACT to play the video
        /// </summary>
        public EngineView EngineView { set; get; }

        /// <summary>
        /// The label used by EnACT to show playhead position and video length
        /// </summary>
        public PlayheadLabel PlayheadLabel { set; get; }

        /// <summary>
        /// The Timeline used by EnACT used to visually display captions in a timeline.
        /// </summary>
        public Timeline Timeline { set; get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs an EngineController.
        /// </summary>
        public EngineController() {}
        #endregion
    }
}
