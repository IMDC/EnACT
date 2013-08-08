using System;
using System.Windows.Forms;
using EnACT.Controls;

namespace EnACT.Forms
{
    /// <summary>
    /// The video controller for enact. Controls interaction between user controls.
    /// </summary>
    public partial class MainForm
    {
        #region Fields and Properties
        /// <summary>
        /// A Boolean that states whether the video is playing or not.
        /// </summary>
        public bool IsPlaying { set; get; }
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

        #region SubscribeToEngineEvents
        /// <summary>
        /// Hooks up event handlers from controls.
        /// </summary>
        public void SubscribeToEngineEvents()
        {
            //CaptionView Events
            this.CaptionView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler
                (this.CaptionView_CellValueChanged);

            //EngineView Events
            this.EngineView.VideoLoaded += new System.EventHandler(this.EngineView_VideoLoaded);

            //PlayheadTimer Events
            this.PlayheadTimer.Tick += new System.EventHandler(this.PlayheadTimer_Tick);

            //Timeline Events
            this.Timeline.PlayheadChanged += new System.EventHandler<TimelinePlayheadChangedEventArgs>
                (this.Timeline_PlayheadChanged);
            this.Timeline.CaptionTimestampChanged += 
                new System.EventHandler<TimelineCaptionTimestampChangedEventArgs>
                    (this.Timeline_CaptionTimestampChanged);
            this.Timeline.CaptionMoved += new System.EventHandler(this.Timeline_CaptionMoved);
        }
        #endregion SubscribeToEngineEvents

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
        public void TogglePlayer() 
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
