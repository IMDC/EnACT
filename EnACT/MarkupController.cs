using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnACT
{
    /// <summary>
    /// The Caption Markup controller for the EnACT Editor. It handles functionality related to
    /// customizing emotions.
    /// </summary>
    public class MarkupController
    {
        #region Properties
        public Caption SelectedCaption { set; get; }
        #endregion

        #region Control Properties
        //Location Controls
        public GroupBox GB_Location             { set; get; }
        public RadioButton RB_BottomRight       { set; get; }
        public RadioButton RB_BottomCenter      { set; get; }
        public RadioButton RB_BottomLeft        { set; get; }
        public RadioButton RB_MiddleRight       { set; get; }
        public RadioButton RB_MiddleCenter      { set; get; }
        public RadioButton RB_MiddleLeft        { set; get; }
        public RadioButton RB_TopRight          { set; get; }
        public RadioButton RB_TopCenter         { set; get; }
        public RadioButton RB_TopLeft           { set; get; }

        //Emotion Type Controls
        public GroupBox GB_EmotionType          { set; get; }
        public RadioButton RB_Anger             { set; get; }
        public RadioButton RB_Fear              { set; get; }
        public RadioButton RB_Sad               { set; get; }
        public RadioButton RB_Happy             { set; get; }
        public RadioButton RB_None              { set; get; }

        //Emotion Intensity Controls
        public GroupBox GB_Intensity            { set; get; }
        public RadioButton RB_HighIntensity     { set; get; }
        public RadioButton RB_MediumIntensity   { set; get; }
        public RadioButton RB_LowIntensity      { set; get; }

        //Caption Text Alignment Controls
        public Button Button_LeftAlign          { set; get; }
        public Button Button_CenterAlign        { set; get; }
        public Button Button_RightAlign          { set; get; }

        //CaptionTextBox
        public CaptionTextBox CaptionTextBox    { set; get; }
        #endregion

        #region Constructor
        public MarkupController() { }
        #endregion
    }
}
