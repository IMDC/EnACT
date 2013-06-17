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
        #region Fields and Properties
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

        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<String, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<Caption> CaptionList { set; get; }

        /// <summary>
        /// The object that represents the EnACT engine xml settings file
        /// </summary>
        public SettingsXML Settings { set; get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs an EngineController.
        /// </summary>
        public EngineController() 
        {
            //Construct the speakerset with a comparator that ignores case;
            SpeakerSet = new Dictionary<String, Speaker>(StringComparer.OrdinalIgnoreCase);

            //Add the default speaker to the set of speakers
            SpeakerSet[Speaker.Default.Name] = Speaker.Default;
            //Add the Description Speaker to the set of speakers
            SpeakerSet[Speaker.Description.Name] = Speaker.Description;

            CaptionList = new List<Caption>();
            Settings = new SettingsXML();
        }
        #endregion

        #region Init
        /// <summary>
        /// Initializes the controls that are controlled by this controller.
        /// </summary>
        public void InitControls()
        {
            //Hook up event handlers to methods in this controller.
            SubscribeToEvents();

            //Set up the CaptionView
            InitCaptionView();
            //Set up VideoPlayer
            InitVideoPlayer();
            //Set up Timeline
            InitTimeline();
        }

        public void SubscribeToEvents()
        {
            this.Timeline.PlayheadChanged += new System.EventHandler<EnACT.TimelinePlayheadChangedEventArgs>(this.Timeline_PlayheadChanged);
            this.Timeline.CaptionTimestampChanged += new System.EventHandler<EnACT.TimelineCaptionTimestampChangedEventArgs>(this.Timeline_CaptionTimestampChanged);
            this.Timeline.CaptionMoved += new System.EventHandler(this.Timeline_CaptionMoved);
        }

        /// <summary>
        /// Creates the table used by CaptionView and then sets as CaptionView's DataSource
        /// </summary>
        private void InitCaptionView()
        {
            CaptionView.InitColumns();  //Set up columns
            CaptionView.SpeakerSet = SpeakerSet;
            CaptionView.CaptionSource = CaptionList;
        }

        /// <summary>
        /// Initializes the Video Player
        /// </summary>
        private void InitVideoPlayer()
        {
            //This method can not be called in the EngineView constructor, so we have to call it here.
            EngineView.LoadMovie(0, Paths.EditorEngine);
        }

        /// <summary>
        /// Initialization for the Timeline
        /// </summary>
        private void InitTimeline()
        {
            Timeline.SpeakerSet = SpeakerSet;
            Timeline.CaptionList = CaptionList;
        }
        #endregion

        #region Timeline Events
        private void Timeline_PlayheadChanged(object sender, TimelinePlayheadChangedEventArgs e)
        {
            Console.WriteLine("Playhead Changed!");

            bool wasPlaying = EngineView.IsPlaying();

            //Pause video, change time, and then play to prevent confusing the player
            EngineView.Pause();
            EngineView.SetPlayHeadTime(e.PlayheadTime);

            //Update label
            PlayheadLabel.PlayheadTime = e.PlayheadTime;

            //Only play if Engine was playing previously
            if (wasPlaying)
                EngineView.Play();
        }

        private void Timeline_CaptionTimestampChanged(object sender, TimelineCaptionTimestampChangedEventArgs e)
        {
            //Console.WriteLine("Caption Timestamp Changed!");

            //Force Captionview to be repainted
            CaptionView.Invalidate();
        }

        private void Timeline_CaptionMoved(object sender, EventArgs e)
        {
            //Force Captionview to be repainted
            CaptionView.Invalidate();
        }
        #endregion
    }
}
