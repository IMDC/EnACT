using System;
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

            //Texteffect
            TextEffect effect = new TextEffect();

            // Tell the effect which character 
            // it applies to in the text.
            effect.PositionStart = 0;
            effect.PositionCount = t.Text.Length;

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
        }
    }
}
