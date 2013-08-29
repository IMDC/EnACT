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
            Speaker s = c.Speaker;

            TextBlock t = new TextBlock
            {
                Name = "Item" + CaptionGrid.Children.Count.ToString(), //ItemX
                Text = c.ToString(),
                FontSize = s.Font.Size,
                FontFamily = new FontFamily(s.Font.Family),
                Visibility = Visibility.Hidden,
            };

            int captionIndex = CaptionGrid.Children.Add(t);
            this.RegisterName(t.Name,t);

            var storyboard = (Storyboard) this.FindResource("CaptionStoryboard");
            
            ObjectAnimationUsingKeyFrames visibilityAnimation = new ObjectAnimationUsingKeyFrames();
            visibilityAnimation.Duration = TimeSpan.FromSeconds(c.Duration);
            visibilityAnimation.BeginTime = TimeSpan.FromSeconds(c.Begin);

            visibilityAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromPercent(0)));
            visibilityAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Collapsed, KeyTime.FromPercent(1)));
            Storyboard.SetTargetName(visibilityAnimation, t.Name);
            Storyboard.SetTargetProperty(visibilityAnimation, new PropertyPath(TextBlock.VisibilityProperty));

            storyboard.Children.Add(visibilityAnimation);
        }

        private int i = -1;
        public void AddCaption(string s)
        {
            if (0 <= i && i < CaptionGrid.Children.Count)
            {
                CaptionGrid.Children.RemoveAt(i);
                i = -1;
            }
            else
            {
                TextBlock t = new TextBlock
                {
                    Text = s,
                    FontSize = 20,
                };
                i = CaptionGrid.Children.Add(t);
                
                Grid.SetZIndex(t,-1); //Should go before Children.Add?
            }
        }
    }
}
