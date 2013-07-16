using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnACT
{
    /// <summary>
    /// The video controller for enact. Controls interaction between user controls.
    /// </summary>
    public class EngineController
    {
        #region Fields and Properties
        /// <summary>
        /// A Boolean that states whether the video is playing or not.
        /// </summary>
        public bool IsPlaying { set; get; }

        /// <summary>
        /// The CaptionView used by EnACT to display captions in a table.
        /// </summary>
        public CaptionView CaptionView { set; get; }
        
        /// <summary>
        /// The video player used by EnACT to play the video.
        /// </summary>
        public EngineView EngineView { set; get; }

        /// <summary>
        /// The label used by EnACT to show playhead position and video length.
        /// </summary>
        public PlayheadLabel PlayheadLabel { set; get; }

        /// <summary>
        /// The timer used to update components when the video is playing.
        /// </summary>
        public Timer PlayheadTimer { set; get; }

        /// <summary>
        /// The Timeline used by EnACT used to visually display captions in a timeline.
        /// </summary>
        public Timeline Timeline { set; get; }

        /// <summary>
        /// A Simple Timeline made from a Trackbar.
        /// </summary>
        public TrackBar TrackBar_Timeline { set; get; }

        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name.
        /// </summary>
        public Dictionary<string, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<EditorCaption> CaptionList { set; get; }

        /// <summary>
        /// The object that represents the EnACT engine xml settings file.
        /// </summary>
        public SettingsXML Settings { set; get; }
        #endregion

        #region Events
        /// <summary>
        /// An event that is called when the video is played.
        /// </summary>
        public event EventHandler VideoPlayed;

        /// <summary>
        /// An event that is called when the video is paused.
        /// </summary>
        public event EventHandler VideoPaused;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineController" /> class.
        /// </summary>
        public EngineController() 
        {
            //Construct the speakerset with a comparator that ignores case
            this.SpeakerSet = new Dictionary<string, Speaker>(StringComparer.OrdinalIgnoreCase);

            //Add the default speaker to the set of speakers
            this.SpeakerSet[Speaker.Default.Name] = Speaker.Default;
            //Add the Description Speaker to the set of speakers
            this.SpeakerSet[Speaker.Description.Name] = Speaker.Description;

            this.CaptionList = new List<EditorCaption>();
            this.Settings = new SettingsXML();
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

            //Set the timer interval to 10 miliseconds
            PlayheadTimer.Interval = 10;
        }

        /// <summary>
        /// Hooks up event handlers from controls.
        /// </summary>
        public void SubscribeToEvents()
        {
            //CaptionView Events
            this.CaptionView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler
                (this.CaptionView_CellValueChanged);

            //EngineView Events
            this.EngineView.VideoLoaded += new System.EventHandler(this.EngineView_VideoLoaded);

            //PlayheadTimer Events
            this.PlayheadTimer.Tick += new System.EventHandler(this.PlayheadTimer_Tick);

            //Timeline Events
            this.Timeline.PlayheadChanged += new System.EventHandler<EnACT.TimelinePlayheadChangedEventArgs>
                (this.Timeline_PlayheadChanged);
            this.Timeline.CaptionTimestampChanged += 
                new System.EventHandler<EnACT.TimelineCaptionTimestampChangedEventArgs>
                    (this.Timeline_CaptionTimestampChanged);
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

        #region Play and Pause
        /// <summary>
        /// Plays the video
        /// </summary>
        public void Play()
        {
            EngineView.Play();
            PlayheadTimer.Start();
            IsPlaying = true;
            //Invoke VideoPlayed Event
            OnVideoPlayed(EventArgs.Empty);
        }

        /// <summary>
        /// Pauses the video
        /// </summary>
        public void Pause()
        {
            EngineView.Pause();
            PlayheadTimer.Stop();
            IsPlaying = false;
            //Invoke VideoPaused Event
            OnVideoPaused(EventArgs.Empty);
        }

        /// <summary>
        /// Toggles between the play and pause state.
        /// </summary>
        public void TogglePlay() 
        {
            if (IsPlaying) Pause();
            else Play();
        }
        #endregion

        #region CaptionView Events
        /// <summary>
        /// Handles the CellValueChanged Event. Redraws the timeline.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void CaptionView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Timeline.Redraw();
        }
        #endregion

        #region EngineView Events
        /// <summary>
        /// Handles the event fired when FlashVideoPlayer is done loading
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void EngineView_VideoLoaded(object sender, EventArgs e)
        {
            double vidLength = EngineView.VideoLength();
            TrackBar_Timeline.Maximum = (int)vidLength * 10;

            //Set Label
            PlayheadLabel.VideoLength = vidLength;

            Timeline.VideoLength = vidLength;
            Timeline.Redraw();
            Timeline.SetScrollBarValues();
        }
        #endregion

        #region PlayheadTimer Events
        /// <summary>
        /// Handles the Tick event. Updates ui controls with relevant information when the video
        /// is playing.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void PlayheadTimer_Tick(object sender, EventArgs e)
        {
            double playheadTime = EngineView.GetPlayheadTime();
            int vidPos = (int)playheadTime * 10;
            if (TrackBar_Timeline.Minimum <= vidPos && vidPos <= TrackBar_Timeline.Maximum)
                TrackBar_Timeline.Value = vidPos;

            //Set playhead time for label
            PlayheadLabel.PlayheadTime = playheadTime;

            Timeline.UpdatePlayheadPosition(playheadTime);

            //Redraw Timeline
            Timeline.Redraw();

            TrackBar_Timeline.Update();
        }
        #endregion

        #region Timeline Events
        /// <summary>
        /// Handles the PlayheadChanged Event. Updates the playhead in various controls.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Timeline_PlayheadChanged(object sender, TimelinePlayheadChangedEventArgs e)
        {
            Console.WriteLine("Playhead Changed!");

            bool wasPlaying = EngineView.IsPlaying();

            //Pause video, change time, and then play to prevent confusing the player
            Pause();
            EngineView.SetPlayHeadTime(e.PlayheadTime);

            //Update label
            PlayheadLabel.PlayheadTime = e.PlayheadTime;

            //Only play if Engine was playing previously
            if (wasPlaying)
                Play();
        }

        /// <summary>
        /// Handles the CaptionTimestampChanged Event. Updates the captions in CaptionView.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Timeline_CaptionTimestampChanged(object sender, TimelineCaptionTimestampChangedEventArgs e)
        {
            //Console.WriteLine("Caption Timestamp Changed!");

            //Force Captionview to be repainted
            CaptionView.Invalidate();
        }

        /// <summary>
        /// Handles the CaptionMoved Event. Updates the captions in CaptionView.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void Timeline_CaptionMoved(object sender, EventArgs e)
        {
            //Force Captionview to be repainted
            CaptionView.Invalidate();
        }
        #endregion

        #region Event Invokation Methods
        /// <summary>
        /// Invokes the VideoPlayed event, which happens when the video is played.
        /// </summary>
        /// <param name="e">Event Args</param>
        private void OnVideoPlayed(EventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = VideoPlayed;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Invokes the VideoPaused event, which happens when the video is paused.
        /// </summary>
        /// <param name="e">Event Args</param>
        private void OnVideoPaused(EventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = VideoPaused;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
    }
}
