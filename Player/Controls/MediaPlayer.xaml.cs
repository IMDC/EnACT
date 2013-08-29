﻿using System;
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
                FontSize = c.Speaker.Font.Size,
                FontFamily = new FontFamily(c.Speaker.Font.Family),

                //By default make t hidden
                Visibility = Visibility.Hidden,
            };

            int captionIndex = CaptionGrid.Children.Add(t);

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
