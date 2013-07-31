using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EnACT
{
    /// <summary>
    /// The Caption Markup controller for the EnACT Editor. It handles functionality related to
    /// customizing emotions.
    /// </summary>
    public partial class MainForm
    {
        #region Fields and Properties
        /// <summary>
        /// The EditorCaptionWord selected by the user if there is only one word selected.
        /// </summary>
        public EditorCaptionWord SelectedCaptionWord { set; get; }

        /// <summary>
        /// The caption selected by the user to mark up with emotions.
        /// </summary>
        public EditorCaption SelectedCaption { set; get; }
        #endregion Fields and Properties

        #region SubScribeToMarkupEvents
        /// <summary>
        /// Hooks up events for controls associated with this controller.
        /// </summary>
        public void SubscribeToMarkupEvents()
        {
            CaptionTextBox.NothingSelected += new EventHandler(this.CaptionTextBox_NothingSelected);
            CaptionTextBox.CaptionWordSelected += new EventHandler<CaptionWordSelectedEventArgs>
                (this.CaptionTextBox_CaptionWordSelected);
            CaptionTextBox.MultipleCaptionWordsSelected += new EventHandler
                (this.CaptionTextBox_MultipleCaptionWordsSelected);
        }
        #endregion SubScribeToMarkupEvents

        #region Load Caption
        /// <summary>
        /// Loads a Caption into the controls assosiated with this class.
        /// </summary>
        /// <param name="c">The caption to load.</param>
        public void LoadCaption(EditorCaption c)
        {
            //Clear Selected CaptionWord
            if (SelectedCaption != null)
            {
                SelectedCaption.PropertyChanged -= SelectedCaption_PropertyChanged;
                SelectedCaption = null;
            }

            //Set Caption
            SelectedCaption = c;
            SelectedCaption.PropertyChanged += SelectedCaption_PropertyChanged;

            //Load Textbox
            CaptionTextBox.Clear();
            CaptionTextBox.Caption = c;

            //Clear Groups
            ClearGB_EmotionType();
            ClearGB_Intensity();

            //Disable Groups
            GB_EmotionType.Enabled = false;
            GB_Intensity.Enabled   = false;

            SetGB_Location(c.Location);

            //Select alignment button
            SetAlignmentButton(SelectedCaption.Alignment);

            //Enable alignment buttons
            Button_LeftAlign.Enabled   = true;
            Button_CenterAlign.Enabled = true;
            Button_RightAlign.Enabled  = true;
        }
        #endregion Load Caption

        #region Load CaptionWord
        /// <summary>
        /// Loads a single EditorCaptionWord into the controls assosiated with this class.
        /// </summary>
        /// <param name="cw">The EditorCaptionWord to load.</param>
        public void LoadWord(EditorCaptionWord cw)
        {
            //Set EditorCaptionWord
            SelectedCaptionWord = cw;

            //Set Emotion
            SetGB_EmotionType(cw.Emotion);

            //Set Intensity only if there is an emotion set for this word.
            if (cw.Emotion == Emotion.None || cw.Emotion == Emotion.Unknown)
            {
                ClearGB_Intensity();
                GB_Intensity.Enabled = false;
            }
            else
            {
                SetGB_Intensity(cw.Intensity);
            }
        }
        #endregion Load CaptionWord

        #region Set Groupbox Methods
        /// <summary>
        /// Sets a radiobutton in GB_Emotiontype to checked based on which Emotion is given and
        /// enables the GroupBox.
        /// </summary>
        /// <param name="e">The Emotion to set.</param>
        private void SetGB_EmotionType(Emotion e)
        {
            switch (e)
            {
                case Emotion.None:  RB_None.Checked  = true; break;
                case Emotion.Happy: RB_Happy.Checked = true; break;
                case Emotion.Sad:   RB_Sad.Checked   = true; break;
                case Emotion.Fear:  RB_Fear.Checked  = true; break;
                case Emotion.Anger: RB_Anger.Checked = true; break;
                default: throw new InvalidEnumArgumentException("e", e.GetHashCode(), typeof(Emotion));
            }

            //Enable Emotion Radio Buttons
            GB_EmotionType.Enabled = true;
        }

        /// <summary>
        /// Sets a radiobutton in GB_Intensity to checked based on which Intensity is given and
        /// enables the GroupBox.
        /// </summary>
        /// <param name="i">The Intensity to set.</param>
        private void SetGB_Intensity(Intensity i)
        {
            //Set Intensity
            switch (i)
            {
                case Intensity.High:    RB_HighIntensity.Checked   = true; break;
                case Intensity.Medium:  RB_MediumIntensity.Checked = true; break;
                case Intensity.Low:     RB_LowIntensity.Checked    = true; break;
                case Intensity.None:    ClearGB_Intensity(); break;
                default: throw new InvalidEnumArgumentException("i", i.GetHashCode(), typeof(Intensity));
            }

            //Enable Intensity Radio Buttons
            GB_Intensity.Enabled = true;
        }

        /// <summary>
        /// Sets a radiobutton in GB_Location to checked based on which Location is given and
        /// enables the GroupBox.
        /// </summary>
        /// <param name="l">The ScreenLocation to set.</param>
        private void SetGB_Location(ScreenLocation l)
        {
            //Set Location
            switch (l)
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
                default: throw new InvalidEnumArgumentException("l", l.GetHashCode(), typeof(ScreenLocation));
            }

            //Enable Location Radio Button
            GB_Location.Enabled = true;
        }
        #endregion Set Groupbox Methods

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
        #endregion Clear GroupBox Methods

        #region ClearCaption
        /// <summary>
        /// Clears the Selected caption and its properties from all controls
        /// </summary>
        public void ClearCaption()
        {
            //Clear SelectedCaption
            if (SelectedCaption != null)
            {
                SelectedCaption.PropertyChanged -= SelectedCaption_PropertyChanged;
                SelectedCaption = null;
            }

            //Clear CaptionTextBox
            CaptionTextBox.Clear();

            //Clear GroupBox radio buttons
            ClearGB_EmotionType();
            ClearGB_Intensity();
            ClearGB_Location();

            //Disable Radio buttons when nothing is selected
            GB_EmotionType.Enabled = false;
            GB_Intensity.Enabled   = false;
            GB_Location.Enabled    = false;

            //Deselect Alignment buttons
            Button_LeftAlign.UseVisualStyleBackColor   = true;
            Button_CenterAlign.UseVisualStyleBackColor = true;
            Button_RightAlign.UseVisualStyleBackColor  = true;

            //Disable alignment buttons
            Button_LeftAlign.Enabled   = false;
            Button_CenterAlign.Enabled = false;
            Button_RightAlign.Enabled  = false;
        }
        #endregion ClearCaption

        #region ClearWord
        /// <summary>
        /// Clears the current SelectedCaptionWord from this and the controls associated with it.
        /// </summary>
        public void ClearWord()
        {
            //Clear SelectedCaptionWord
            SelectedCaptionWord = null;

            //Clear GroupBoxes related to EditorCaptionWord
            ClearGB_EmotionType();
            ClearGB_Intensity();
        }
        #endregion ClearWord

        #region Change SelectedCaption Properties
        /// <summary>
        /// Changes the emotion of the selected EditorCaptionWord or words.
        /// </summary>
        /// <param name="e">The Emotion to set the Caption with.</param>
        public void ChangeEmotion(Emotion e)
        {
            //Determine MarkupType
            switch (CaptionTextBox.SelectionMode)
            {
                case CaptionTextBoxSelectionMode.NoSelection:
                    throw new Exception("No selected caption to markup."); //Should not happen.
                case CaptionTextBoxSelectionMode.SingleWordSelection:
                    SelectedCaptionWord.Emotion = e;
                    if (e == Emotion.None || e == Emotion.Unknown)
                    {
                        ClearGB_Intensity();
                        GB_Intensity.Enabled = false;
                    }
                    else
                    {
                        SetGB_Intensity(SelectedCaptionWord.Intensity);
                    }
                    break;
                case CaptionTextBoxSelectionMode.MultiWordSelection:
                    foreach (EditorCaptionWord cw in SelectedCaption.Words)
                    {
                        if (cw.IsSelected) { cw.Emotion = e; }  //Set emotion only if selected
                    }

                    //Clear the Intensity GB and enable/disable it based on emotion
                    ClearGB_Intensity();
                    if (e == Emotion.None || e == Emotion.Unknown)
                        GB_Intensity.Enabled = false;
                    else
                        GB_Intensity.Enabled = true;
                    break;
                default: throw new InvalidEnumArgumentException("e", e.GetHashCode(), typeof(Emotion));
            }
        }

        /// <summary>
        /// Changes the Intensity of the selected EditorCaptionWord or words.
        /// </summary>
        /// <param name="i">The Intensity to set the Caption with.</param>
        public void ChangeIntensity(Intensity i)
        {
            switch (CaptionTextBox.SelectionMode)
            {
                case CaptionTextBoxSelectionMode.NoSelection:
                    throw new Exception("No selected caption to markup."); //Should not happen.
                case CaptionTextBoxSelectionMode.SingleWordSelection:
                    SelectedCaptionWord.Intensity = i;
                    break;
                case CaptionTextBoxSelectionMode.MultiWordSelection:
                    foreach (EditorCaptionWord cw in SelectedCaption.Words)
                    {
                        if (cw.IsSelected) { cw.Intensity = i; } //Set intensity only if selected.
                    }
                    break;
                default: throw new InvalidEnumArgumentException("i", i.GetHashCode(), typeof(Intensity));
            }
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
            SetAlignmentButton(a);
        }
        #endregion Change SelectedCaption Properties

        #region SetAlignmentButton
        /// <summary>
        /// Highlights the selected alignment button with a color to signify that it is the button
        /// that has been selected.
        /// </summary>
        /// <param name="a">The alignment to select</param>
        private void SetAlignmentButton(Alignment a)
        {
            Color c = Color.BlanchedAlmond;

            switch (a)
            {
                case Alignment.Left:
                    Button_LeftAlign.BackColor = c;
                    Button_CenterAlign.UseVisualStyleBackColor = true;
                    Button_RightAlign.UseVisualStyleBackColor  = true;
                    break;
                case Alignment.Center:
                    Button_LeftAlign.UseVisualStyleBackColor  = true;
                    Button_CenterAlign.BackColor = c;
                    Button_RightAlign.UseVisualStyleBackColor = true;
                    break;
                case Alignment.Right:
                    Button_LeftAlign.UseVisualStyleBackColor   = true;
                    Button_CenterAlign.UseVisualStyleBackColor = true;
                    Button_RightAlign.BackColor = c;
                    break;
                default: throw new InvalidEnumArgumentException("a", a.GetHashCode(), typeof(Alignment));
            }
        }
        #endregion SetAlignmentButton

        #region CaptionTextBox Event Handlers
        /// <summary>
        /// Event Handler for CaptionTextBox.NothingSelected event. Clears and disables the Emotion
        /// and Intensity groupboxes.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void CaptionTextBox_NothingSelected(object sender, EventArgs e)
        {
            //Clear previous setting and disable GB
            ClearGB_EmotionType();
            GB_EmotionType.Enabled = false;

            //Clear previous setting and disable GB
            ClearGB_Intensity();
            GB_Intensity.Enabled = false;
        }

        /// <summary>
        /// Event Handler for CaptionTextBox.CaptionWordSelected event. Loads the selected
        /// EditorCaptionWord into the associated controls.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void CaptionTextBox_CaptionWordSelected(object sender, CaptionWordSelectedEventArgs e)
        {
            if (e.SelectedWord != SelectedCaptionWord)
            {
                Console.WriteLine("New Word Selection: {0}", e.SelectedWord);
                LoadWord(e.SelectedWord);
            }
        }

        /// <summary>
        /// Event Handler for CaptionTextBox.MultipleCaptionWordsSelected event. Sets controls up
        /// for marking up multiple words at the same time.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void CaptionTextBox_MultipleCaptionWordsSelected(object sender, EventArgs e)
        {
            Console.WriteLine("Multiple Words Selected!");
            //Clear previous setting
            ClearGB_EmotionType();
            //Enable if not enabled already
            GB_EmotionType.Enabled = true;

            //Clear and disable intensity, as no current emotion is selected.
            ClearGB_Intensity();
            GB_Intensity.Enabled = false;
        }
        #endregion CaptionTextBox Event Handlers

        #region SelectedCaption_PropertyChanged
        /// <summary>
        /// Event Handler for SelectedCaption.PropertyChanged. Updates controls when the selected
        /// Caption is changed from other parts of the program.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void SelectedCaption_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Switch based on property name
            switch (e.PropertyName)
            {
                case EditorCaption.PropertyNames.Alignment:     break;
                case EditorCaption.PropertyNames.Begin:         break;
                case EditorCaption.PropertyNames.End:           break;
                case EditorCaption.PropertyNames.Location: SetGB_Location(SelectedCaption.Location); break;
                case EditorCaption.PropertyNames.Duration:      break;
                case EditorCaption.PropertyNames.Speaker:       break;
                case EditorCaption.PropertyNames.Text: CaptionTextBox.Text = SelectedCaption.GetAsString(); break;
                case EditorCaption.PropertyNames.Words:         break;
                default: throw new ArgumentException(string.Format("Property Name '{0}' isn't a valid property",
                        e.PropertyName), "PropertyName");
            }

            //Update Caption in CaptionView
            CaptionView.Invalidate();
            //Update Timeline
            Timeline.Redraw();
        }
        #endregion SelectedCaption_PropertyChanged
    }
}