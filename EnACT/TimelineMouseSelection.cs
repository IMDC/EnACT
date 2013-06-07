using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
{
    /// <summary>
    /// An enum representing what action is being performed by the mouse.
    /// </summary>
    public enum TimelineMouseAction
    {
        movePlayhead,
        moveCaption,
        changeCaptionBegin,
        changeCaptionEnd,
        noAction
    };

    /// <summary>
    /// A class containing data relating to what has been clicked on by the mouse in Timeline.
    /// </summary>
    class TimelineMouseSelection
    {
        /// <summary>
        /// An instance of TimelineMouseSelection representing nothing currently being selected.
        /// </summary>
        public static readonly TimelineMouseSelection NoSelection = new TimelineMouseSelection(TimelineMouseAction.noAction);

        /// <summary>
        /// The selected caption, if any
        /// </summary>
        public Caption Caption { set; get; }

        /// <summary>
        /// The difference between the selected Caption.Begin and the mouseClickTime
        /// </summary>
        public double MouseClickTimeDifference { set; get; }

        /// <summary>
        /// Which action the mouse is currently doing
        /// </summary>
        public TimelineMouseAction Action { set; get; }

        /// <summary>
        /// Constructs a TimelineMouseSelection with only an Action
        /// </summary>
        /// <param name="action">The TimelineMouseAction that is being performed</param>
        public TimelineMouseSelection(TimelineMouseAction action) : this(action, null) { }

        /// <summary>
        /// Constructs a TImelineMouseSelection with an Action and selected Caption
        /// </summary>
        /// <param name="action">The TimelineMouseAction that is being performed</param>
        /// <param name="selectedCaption">The caption selected for this TimelineMouseAction</param>
        public TimelineMouseSelection(TimelineMouseAction action, Caption selectedCaption)
            : this(action, selectedCaption, 0) { }

        /// <summary>
        /// Constructs a TimelineMouseSelection with an Action, selected caption and time difference.
        /// </summary>
        //// <param name="action">The TimelineMouseAction that is being performed</param>
        /// <param name="selectedCaption">The caption selected for this TimelineMouseAction</param>
        /// <param name="mouseClickTimeDifference">The difference between the selected Caption.Begin 
        /// and the mouseClickTime</param>
        public TimelineMouseSelection(TimelineMouseAction action, Caption mouseClickTimeDifference, 
            double selectedCaptionTimeDifference)
        {
            this.Action = action;
            this.Caption = mouseClickTimeDifference;
            this.MouseClickTimeDifference = selectedCaptionTimeDifference;
        }
    }
}
