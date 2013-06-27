﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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
        /// The CaptionWord selected by the user if there is only one word selected.
        /// </summary>
        public CaptionWord SelectedCaptionWord { set; get; }

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

        #region Constructor and Init Methods
        public MarkupController() 
        {
            captionLoaded = false;
        }

        /// <summary>
        /// Hooks up events for controls associated with this controller.
        /// </summary>
        public void HookUpEvents()
        {
            CaptionTextBox.CaptionWordSelected += new EventHandler<CaptionWordSelectedEventArgs>
                (this.CaptionTextBox_CaptionWordSelected);
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

            //Select alignment button
            SelectAlignmentButton(SelectedCaption.Alignment);

            //Enable alignment buttons
            Button_LeftAlign.Enabled = true;
            Button_CenterAlign.Enabled = true;
            Button_RightAlign.Enabled = true;

            captionLoaded = true;
        }
        #endregion

        #region Load Word
        /// <summary>
        /// Loads a single CaptionWord into the controls assosiated with this class.
        /// </summary>
        /// <param name="cw">The CaptionWord to load.</param>
        public void LoadWord(CaptionWord cw)
        {
            //Set CaptionWord
            SelectedCaptionWord = cw;

            //Set Emotion
            switch (cw.Emotion)
            {
                case Emotion.None: RB_None.Checked = true; break;
                case Emotion.Happy: RB_Happy.Checked = true; break;
                case Emotion.Sad: RB_Sad.Checked = true; break;
                case Emotion.Fear: RB_Fear.Checked = true; break;
                case Emotion.Anger: RB_Anger.Checked = true; break;
                default: break;
            }
            
            //Enable Emotion Radio Buttons
            GB_EmotionType.Enabled = true;

            //Set Intensity only if there is an emotion set for this word.
            if (cw.Emotion != Emotion.None && cw.Emotion != Emotion.Unknown)
            {
                //Set Intensity
                switch (cw.Intensity)
                {
                    case Intensity.High: RB_HighIntensity.Checked = true; break;
                    case Intensity.Medium: RB_MediumIntensity.Checked = true; break;
                    case Intensity.Low: RB_LowIntensity.Checked = true; break;
                    case Intensity.None: ClearGB_Intensity(); break;
                    default: break;
                }

                //Enable Intensity Radio Buttons
                GB_Intensity.Enabled = true;
            }
        }
        #endregion

        #region Clear GroupBox Methods
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
        #endregion

        #region ClearCaption
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

            //Deselect Alignment buttons
            Button_LeftAlign.UseVisualStyleBackColor = true;
            Button_CenterAlign.UseVisualStyleBackColor = true;
            Button_RightAlign.UseVisualStyleBackColor = true;

            //Disable alignment buttons
            Button_LeftAlign.Enabled = false;
            Button_CenterAlign.Enabled = false;
            Button_RightAlign.Enabled = false;

            captionLoaded = false;
        }
        #endregion

        #region ClearWord
        /// <summary>
        /// Clears the current SelectedCaptionWord from this and the controls associated with it.
        /// </summary>
        public void ClearWord()
        {
            //Clear SelectedCaptionWord
            SelectedCaptionWord = null;

            //Clear GroupBoxes related to CaptionWord
            ClearGB_EmotionType();
            ClearGB_Intensity();
        }
        #endregion

        #region Change SelectedCaption Properties
        /// <summary>
        /// Changes the emotion of the selected caption.
        /// </summary>
        /// <param name="e"></param>
        public void ChangeEmotion(Emotion e)
        {
            SelectedCaptionWord.Emotion = e;
        }

        public void ChangeIntensity(Intensity i)
        {
            SelectedCaptionWord.Intensity = i;
        }

        /// <summary>
        /// Changes the Location of the selected Caption.
        /// </summary>
        /// <param name="l">The location to set the caption to.</param>
        public void ChangeLocation(ScreenLocation l)
        {
            SelectedCaption.Location = l;
        }

        /// <summary>
        /// Changes the Alignment of the selected Caption.
        /// </summary>
        /// <param name="a">The alignment to set the caption to.</param>
        public void ChangeAlignment(Alignment a)
        {
            SelectedCaption.Alignment = a;
            SelectAlignmentButton(a);
        }
        #endregion

        #region SelectAlignmentButton
        /// <summary>
        /// Highlights the selected alignment button with a color to signify that it is the button
        /// that has been selected.
        /// </summary>
        /// <param name="a">The alignment to select</param>
        private void SelectAlignmentButton(Alignment a)
        {
            Color c = Color.BlanchedAlmond;

            switch (a)
            {
                case Alignment.Left:
                    Button_LeftAlign.BackColor = c;
                    Button_CenterAlign.UseVisualStyleBackColor = true;
                    Button_RightAlign.UseVisualStyleBackColor = true;
                    break;
                case Alignment.Center:
                    Button_LeftAlign.UseVisualStyleBackColor = true;
                    Button_CenterAlign.BackColor = c;
                    Button_RightAlign.UseVisualStyleBackColor = true;
                    break;
                case Alignment.Right:
                    Button_LeftAlign.UseVisualStyleBackColor = true;
                    Button_CenterAlign.UseVisualStyleBackColor = true;
                    Button_RightAlign.BackColor = c;
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Event Handler for CaptionTextBox.CaptionWordSelected event. Loads the selected 
        /// CaptionWord into the associated controls.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void CaptionTextBox_CaptionWordSelected(object sender, CaptionWordSelectedEventArgs e)
        {
            if (e.SelectedWord != SelectedCaptionWord)
            {
                Console.WriteLine("New Word: {0}", e.SelectedWord);
                LoadWord(e.SelectedWord);
            }
        }
    }
}
