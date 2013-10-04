using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LibEnACT;

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

        public void AddCaption(Caption c)
        {
            TextBlock t = new TextBlock
            {   
                //Name t so that it is unique
                Name = "Item" + CaptionGrid.Children.Count.ToString(), //Named in form of ItemXX
                Text = c.ToString(),

                //Confugure Speaker settings
                FontSize   = c.Speaker.Font.Size,
                FontFamily = new FontFamily(c.Speaker.Font.Family),

                //Set Background and Foreground Colours
                Background = Brushes.Black,
                Foreground = Brushes.White,

                //TODO Set alignment based on Caption Alignment
                //Set Alignments
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                //By default make t hidden
                Visibility = Visibility.Hidden,
            };

            int captionIndex = CaptionGrid.Children.Add(t);
            Grid.SetRow(t,GridLocation.BottomCentre.Row);
            Grid.SetRowSpan(t,GridLocation.BottomCentre.RowSpan);
            Grid.SetColumn(t, GridLocation.BottomCentre.Column);
            Grid.SetColumnSpan(t, GridLocation.BottomCentre.ColumnSpan);

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

            //Duration for all animations
            Duration duration = TimeSpan.FromSeconds(0.6);

            TextEffect e1 = new TextEffect
            {
                PositionStart = 0,
                PositionCount = t.Text.Length,
            };


            var formattedText = new FormattedText(
                t.Text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(t.FontFamily, t.FontStyle, t.FontWeight, t.FontStretch),
                t.FontSize,
                Brushes.Black);

            ScaleTransform st = new ScaleTransform 
            {
                CenterX = formattedText.Width/2,
                CenterY = formattedText.Height,
            };
            e1.Transform = st;

            t.TextEffects.Add(e1);

            DoubleAnimationUsingKeyFrames d1 = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(c.Begin),
                Duration = duration,
            };

            d1.KeyFrames.Add(new LinearDoubleKeyFrame(0.5, KeyTime.FromPercent(0)));
            d1.KeyFrames.Add(new LinearDoubleKeyFrame(1.3, KeyTime.FromPercent(0.5)));
            d1.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromPercent(1)));

            Storyboard.SetTargetName(d1,t.Name);
            Storyboard.SetTargetProperty(d1, new PropertyPath("TextEffects[0].Transform.ScaleX"));

            storyboard.Children.Add(d1);

            //Make second animation for y scale
            DoubleAnimationUsingKeyFrames d2 = d1.Clone();

            Storyboard.SetTargetName(d2, t.Name);
            Storyboard.SetTargetProperty(d2, new PropertyPath("TextEffects[0].Transform.ScaleY"));

            storyboard.Children.Add(d2);

            TextEffect e2 = new TextEffect //Yfinish effect
            {
                PositionStart = 0,
                PositionCount = t.Text.Length,
                Transform = new TranslateTransform(),
            };

            t.TextEffects.Add(e2);

            DoubleAnimationUsingKeyFrames d3 = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(c.Begin),
                Duration = duration,
            };

            d3.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(0)));
            d3.KeyFrames.Add(new LinearDoubleKeyFrame(-60, KeyTime.FromPercent(0.5)));
            d3.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(1)));

            Storyboard.SetTargetName(d3, t.Name);
            Storyboard.SetTargetProperty(d3, new PropertyPath("TextEffects[1].Transform.Y"));

            storyboard.Children.Add(d3);
        }
    }
}
