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

            /*
            //Texteffect
            TextEffect effect = new TextEffect
            {
                PositionStart = 0, 
                PositionCount = t.Text.Length,
            };

            // Tell the effect which character 
            // it applies to in the text.
            
            TransformGroup transGrp = new TransformGroup();
            transGrp.Children.Add(new TranslateTransform());
            transGrp.Children.Add(new RotateTransform());
            effect.Transform = transGrp;

            t.TextEffects.Add(effect);

            DoubleAnimation anim = (DoubleAnimation) this.FindResource("CharacterWaveAnimation");

            anim.BeginTime = TimeSpan.FromSeconds(c.Duration);

            string path = //String.Format(
                "TextEffects[0].Transform.Children[0].Y"
                //,charIndex);
                ;

            PropertyPath propPath = new PropertyPath(path);
            Storyboard.SetTargetName(anim,t.Name);
            Storyboard.SetTargetProperty(anim, propPath);

            storyboard.Children.Add(anim);
             */

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

            //DoubleAnimation d1 = new DoubleAnimation()
            //{
            //    BeginTime = TimeSpan.FromSeconds(c.Begin),
            //    Duration = TimeSpan.FromSeconds(0.6/2),
            //    From = 0.5,
            //    To = 1.3,
            //    AutoReverse = true,
            //};

            DoubleAnimationUsingKeyFrames d1 = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(c.Begin),
                Duration = TimeSpan.FromSeconds(0.6 / 2),
            };

            d1.KeyFrames.Add(new LinearDoubleKeyFrame(0.5, KeyTime.FromPercent(0)));
            d1.KeyFrames.Add(new LinearDoubleKeyFrame(1.3, KeyTime.FromPercent(0.5)));
            d1.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromPercent(1)));

            Storyboard.SetTargetName(d1,t.Name);
            Storyboard.SetTargetProperty(d1, new PropertyPath("TextEffects[0].Transform.ScaleX"));

            storyboard.Children.Add(d1);

            //Make second animation for y scale
            //DoubleAnimation d2 = d1.Clone();
            DoubleAnimationUsingKeyFrames d2 = d1.Clone();

            Storyboard.SetTargetName(d2, t.Name);
            Storyboard.SetTargetProperty(d2, new PropertyPath("TextEffects[0].Transform.ScaleY"));

            storyboard.Children.Add(d2);
        }
    }
}
