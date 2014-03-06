using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using LibEnACT;
using Player.Animations;
using Player.Views;
using System.Collections.Specialized;

namespace Player.Controls
{
    /// <summary>
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayer : UserControl
    {
        private MediaPlayerViewModel _viewModel;

        public MediaPlayerViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                //Unsubscribe from previous VM events
                if (_viewModel != null)
                {
                    _viewModel.PropertyChanged -= this.ViewModel_PropertyChanged;
                    if (_viewModel.CaptionList != null)
                        _viewModel.CaptionList.CollectionChanged -= this.CaptionList_CollectionChanged;
                }

                _viewModel = value;

                //Subscribe to events on new view model
                _viewModel.PropertyChanged += this.ViewModel_PropertyChanged;
                if (_viewModel.CaptionList != null)
                    _viewModel.CaptionList.CollectionChanged += this.CaptionList_CollectionChanged;
            }
        }

        /// <summary>
        /// Volume property. Wraps the Media.Volume property so that it can be binded.
        /// </summary>
        public double Volume
        {
            get { return Media.Volume; }
            set { Media.Volume = value; }
        }

        public Storyboard Storyboard { get; private set; }

        public MediaPlayer()
        {
            InitializeComponent();

            Storyboard = (Storyboard)this.FindResource("CaptionStoryboard");

            ViewModel = new MediaPlayerViewModel();
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
            this.RegisterName(t.Name, t);

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

            Storyboard.Children.Add(visibilityAnimation);

            foreach (CaptionWord w in c.Words)
            {
                //Skip words with no emotion in them
                if (w.Emotion == Emotion.None || w.Emotion == Emotion.Unknown)
                    continue;

                WordAnimation a = WordAnimationFactory.CreateWordAnimation(w, t);

                a.AddToMediaPlayer(Storyboard, t);
            }
        }

        /// <summary>
        /// Sets the position of the given caption on a grid.
        /// </summary>
        /// <param name="t">The caption texblock to set the position of.</param>
        private void SetCaptionPosition(CaptionTextBlock t)
        {
            GridLocation l = GridLocation.GetGridLocation(t.Caption.Location);

            Grid.SetRow(t, l.Row);
            Grid.SetRowSpan(t, l.RowSpan);
            Grid.SetColumn(t, l.Column);
            Grid.SetColumnSpan(t, l.ColumnSpan);
        }

        private void ResetCaptions()
        {
            //Hold onto the mediatimeline from before
            var mediaTimeline = Storyboard.Children[0];

            //Clear all children from the storyboard and add them again.
            Storyboard.Children.Clear();

            Storyboard.Children.Add(mediaTimeline);

            foreach (Caption c in ViewModel.CaptionList)
            {
                this.AddCaption(c);
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CaptionList":
                    ResetCaptions();
                    break;
                default:
                    throw new NotImplementedException("Property " + e.PropertyName + " is not handled.");
            }
        }

        private void CaptionList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
    }
}
