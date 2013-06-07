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

    class TimelineMouseSelection
    {
        public static readonly TimelineMouseSelection NoSelection = new TimelineMouseSelection(TimelineMouseAction.noAction);

        public Caption SelectedCaption { set; get; }

        public double SelectedCaptionTimeDifference { set; get; }

        public TimelineMouseAction Action { set; get; }

        public TimelineMouseSelection(TimelineMouseAction action) : this(action, null) { }

        public TimelineMouseSelection(TimelineMouseAction action, Caption selectedCaption)
            : this(action, selectedCaption, 0) { }

        public TimelineMouseSelection(TimelineMouseAction action, Caption selectedCaption, 
            double selectedCaptionTimeDifference)
        {
            this.Action = action;
            this.SelectedCaption = selectedCaption;
            this.SelectedCaptionTimeDifference = selectedCaptionTimeDifference;
        }
    }
}
