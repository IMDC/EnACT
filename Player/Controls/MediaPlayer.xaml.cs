using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LibEnACT;
using Player.Animations;

namespace Player.Controls
{
    /// <summary>
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayer : UserControl
    {
        /// <summary>
        /// Volume property. Wraps the Media.Volume property so that it can be binded.
        /// </summary>
        public double Volume
        {
            get { return Media.Volume; }
            set { Media.Volume = value; }
        }

        public MediaPlayer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds an emotive caption to this MediaPlayer.
        /// </summary>
        /// <param name="c">The Caption to add.</param>
        public void AddCaption(Caption c)
        {
            //Create a new CaptionTextBlock with a unique name in the form "ItemXX"
            CaptionTextBlock t = new CaptionTextBlock(c, "Item" + CaptionGrid.Children.Count);

            int captionIndex = CaptionGrid.Children.Add(t);

            SetCaptionPosition(t);

            //Give the control a name so that it can be used by the storyboard.
            this.RegisterName(t.Name,t); 

            var storyboard = (Storyboard) this.FindResource("CaptionStoryboard");

            //Create animation for visibility
            ObjectAnimationUsingKeyFrames visibilityAnimation = new ObjectAnimationUsingKeyFrames
            {
                Duration = TimeSpan.FromSeconds(c.Duration),
                BeginTime = TimeSpan.FromSeconds(c.Begin),
            };

            //Set visibility keyframes
            visibilityAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromPercent(0)));
            visibilityAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Collapsed, KeyTime.FromPercent(1)));

            //Bind animation to property
            Storyboard.SetTargetName(visibilityAnimation, t.Name);
            Storyboard.SetTargetProperty(visibilityAnimation, new PropertyPath(TextBlock.VisibilityProperty));

            storyboard.Children.Add(visibilityAnimation);

            foreach (CaptionWord w in c.Words)
            {
                //Skip words with no emotion in them
                if(w.Emotion == Emotion.None || w.Emotion == Emotion.Unknown)
                    continue;

                WordAnimation a = WordAnimationFactory.CreateWordAnimation(w,t);

                a.AddToMediaPlayer(storyboard, t);
            }
        }

        /// <summary>
        /// Sets the position of the given caption on a grid.
        /// </summary>
        /// <param name="t">The caption texblock to set the position of.</param>
        private void SetCaptionPosition(CaptionTextBlock t)
        {
            GridLocation l  = GridLocation.GetGridLocation(t.Caption.Location);

            Grid.SetRow(t, l.Row);
            Grid.SetRowSpan(t, l.RowSpan);
            Grid.SetColumn(t, l.Column);
            Grid.SetColumnSpan(t, l.ColumnSpan);
        }
    }
}
