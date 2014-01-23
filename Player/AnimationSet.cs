using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LibEnACT;

namespace Player
{
    public class AnimationSet
    {
        /// <summary>
        /// List of text effects for an animated caption.
        /// </summary>
        public List<TextEffect> TextEffects;

        /// <summary>
        /// List of animations for an animated caption.
        /// </summary>
        public List<AnimationTimeline> Animations;

        public AnimationSet(Caption c, TextBlock t)
        {
            //Construct lists
            TextEffects = new List<TextEffect>();
            Animations = new List<AnimationTimeline>();

            //Duration for all animations
            Duration duration = TimeSpan.FromSeconds(0.6);
            Duration halfDuration = TimeSpan.FromSeconds(0.6/2);

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
                CenterX = formattedText.Width / 2,
                CenterY = formattedText.Height,
            };
            e1.Transform = st;

            this.TextEffects.Add(e1);

            DoubleAnimationUsingKeyFrames d1 = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(c.Begin),
                Duration = duration,
            };

            d1.KeyFrames.Add(new LinearDoubleKeyFrame(0.5, KeyTime.FromPercent(0)));
            d1.KeyFrames.Add(new LinearDoubleKeyFrame(1.3, KeyTime.FromPercent(0.5)));
            d1.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromPercent(1)));

            this.Animations.Add(d1);

            //Make second animation for y scale
            DoubleAnimationUsingKeyFrames d2 = d1.Clone();

            this.Animations.Add(d2);

            TextEffect e2 = new TextEffect //Yfinish effect
            {
                PositionStart = 0,
                PositionCount = t.Text.Length,
                Transform = new TranslateTransform(),
            };

            this.TextEffects.Add(e2);

            DoubleAnimationUsingKeyFrames d3 = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(c.Begin),
                Duration = duration,
            };

            d3.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(0)));
            d3.KeyFrames.Add(new LinearDoubleKeyFrame(-10, KeyTime.FromPercent(0.5)));
            d3.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(1)));

            this.Animations.Add(d3);

            //alpha effect
            TextEffect e3 = new TextEffect()
            {
                PositionStart = 0,
                PositionCount = t.Text.Length,
            };

            e3.Foreground = new SolidColorBrush(Colors.White);

            this.TextEffects.Add(e3);

            ColorAnimationUsingKeyFrames c1 = new ColorAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(c.Begin),
                Duration = duration,
            };

            c1.KeyFrames.Add(new LinearColorKeyFrame(Colors.White, KeyTime.FromPercent(0)));
            c1.KeyFrames.Add(new LinearColorKeyFrame(Color.FromArgb(100, 255, 255, 255), KeyTime.FromPercent(0.5)));
            c1.KeyFrames.Add(new LinearColorKeyFrame(Colors.White, KeyTime.FromPercent(1)));

            this.Animations.Add(c1);
        }
    }
}
