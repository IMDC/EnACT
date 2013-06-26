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
        #region Fields and Properties
        /// <summary>
        /// Boolean that signifies if a caption has been selected by the user to mark-up.
        /// </summary>
        private bool captionLoaded;

        /// <summary>
        /// The caption selected by the user to mark up with emotions.
        /// </summary>
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
        public MarkupController() 
        {
            captionLoaded = false;
        }
        #endregion

        #region Load Caption
        /// <summary>
        /// Loads a Caption into the controls assosiated with this class.
        /// </summary>
        /// <param name="c">The caption to load.</param>
        public void LoadCaption(Caption c)
        {
            //Set Caption
            this.SelectedCaption = c;

            //Load Textbox
            CaptionTextBox.Caption = c;

            //Clear Groups
            ClearGB_EmotionType();
            ClearGB_Intensity();

            //Set Location
            switch (c.Location)
            {
                case ScreenLocation.TopLeft:        RB_TopLeft.Checked      = true; break;
                case ScreenLocation.TopCentre:      RB_TopCenter.Checked    = true; break;
                case ScreenLocation.TopRight:       RB_TopRight.Checked     = true; break;
                case ScreenLocation.MiddleLeft:     RB_MiddleLeft.Checked   = true; break;
                case ScreenLocation.MiddleCenter:   RB_MiddleCenter.Checked = true; break;
                case ScreenLocation.MiddleRight:    RB_MiddleRight.Checked  = true; break;
                case ScreenLocation.BottomLeft:     RB_BottomLeft.Checked   = true; break;
                case ScreenLocation.BottomCentre:   RB_BottomCenter.Checked = true; break;
                case ScreenLocation.BottomRight:    RB_BottomRight.Checked  = true; break;
                default: throw new Exception("Invalid Location: " + c.Location.GetHashCode());
            }

            //Enable Location Radio Button
            GB_Location.Enabled = true;

            captionLoaded = true;
        }
        #endregion

        #region Clear Methods
        /// <summary>
        /// Unchecks all radioboxes in GB_EmotionType
        /// </summary>
        private void ClearGB_EmotionType()
        {
            foreach (RadioButton rb in GB_EmotionType.Controls) { rb.Checked = false; }
        }

        /// <summary>
        /// Unchecks all radioboxes in GB_Intensity
        /// </summary>
        private void ClearGB_Intensity()
        {
            foreach (RadioButton rb in GB_Intensity.Controls) { rb.Checked = false; }
        }

        /// <summary>
        /// Unchecks all radioboxes in GB_Location
        /// </summary>
        private void ClearGB_Location()
        {
            foreach (RadioButton rb in GB_Location.Controls) { rb.Checked = false; }
        }

        /// <summary>
        /// Clears the Selected caption and its properties from all controls
        /// </summary>
        public void ClearCaption()
        {
            //Clear SelectedCaption
            SelectedCaption = null;

            //Clear CaptionTextBox
            CaptionTextBox.Caption = null;

            //Clear GroupBox radio buttons
            ClearGB_EmotionType();
            ClearGB_Intensity();
            ClearGB_Location();

            //Disable Radio buttons when nothing is selected
            GB_EmotionType.Enabled = false;
            GB_Intensity.Enabled = false;
            GB_Location.Enabled = false;

            captionLoaded = false;
        }
        #endregion

        /// <summary>
        /// Changes the emotion of the selected caption.
        /// </summary>
        /// <param name="e"></param>
        public void ChangeEmotion(Emotion e)
        {
            //TODO implement this
        }

        public void ChangeIntensity(Intensity i)
        {
            //TODO implement this
        }

        public void ChangeLocation(ScreenLocation l)
        {
            SelectedCaption.Location = l;
        }
    }
}
