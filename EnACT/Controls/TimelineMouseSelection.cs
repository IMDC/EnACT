using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
{
    #region TimelineMouseAction Enum
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
    #endregion

    #region TimelineMouseSelection Class
    /// <summary>
    /// A class containing data relating to what has been clicked on by the mouse in Timeline.
    /// </summary>
    class TimelineMouseSelection
    {
        #region Fields and Properties
        /// <summary>
        /// An instance of TimelineMouseSelection representing nothing currently being selected.
        /// </summary>
        public static readonly TimelineMouseSelection NoSelection = new TimelineMouseSelection(TimelineMouseAction.noAction);

        /// <summary>
        /// The selected caption, if any
        /// </summary>
        public EditorCaption Caption { set; get; }

        /// <summary>
        /// The difference between the selected Caption.Begin and the mouseClickTime
        /// </summary>
        public double MouseClickTimeDifference { set; get; }

        /// <summary>
        /// Which action the mouse is currently doing
        /// </summary>
        public TimelineMouseAction Action { set; get; }
        #endregion

        #region Constructors
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
        public TimelineMouseSelection(TimelineMouseAction action, EditorCaption selectedCaption)
            : this(action, selectedCaption, 0) { }

        /// <summary>
        /// Constructs a TimelineMouseSelection with an Action, selected caption and time difference.
        /// </summary>
        //// <param name="action">The TimelineMouseAction that is being performed</param>
        /// <param name="selectedCaption">The caption selected for this TimelineMouseAction</param>
        /// <param name="mouseClickTimeDifference">The difference between the selected Caption.Begin 
        /// and the mouseClickTime</param>
        public TimelineMouseSelection(TimelineMouseAction action, EditorCaption mouseClickTimeDifference, 
            double selectedCaptionTimeDifference)
        {
            this.Action = action;
            this.Caption = mouseClickTimeDifference;
            this.MouseClickTimeDifference = selectedCaptionTimeDifference;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Moves the selected caption based on the mouseClickTime.
        /// </summary>
        /// <param name="mouseClickTime">The time represented by the mouse click location</param>
        public void MoveSelectedCaption(double mouseClickTime)
        {
            //Move caption with a minimum time of 0
            Caption.MoveTo(Math.Max(0,mouseClickTime - MouseClickTimeDifference));
        }
        #endregion
    }//Class
    #endregion
}//Namespace
