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
using Player.Controls;

namespace Player.Animations
{
    public class HappyWordAnimation : WordAnimation
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of a HappyWordAnimation. Initializes the animation based on the
        /// specified paramaters.
        /// </summary>
        /// <param name="w">The CaptionWord to base the animation off of.</param>
        /// <param name="beginIndex">The index of the caption string at which the CaptionWord
        /// begins.</param>
        /// <param name="t">The Textblock that this animation will be applied to.</param>
        public HappyWordAnimation(CaptionWord w, int beginIndex, CaptionTextBlock t) : base(w, beginIndex, t)
        {
            //Duration for all animations
            Duration duration = TimeSpan.FromSeconds(0.6);
            Duration halfDuration = TimeSpan.FromSeconds(0.6 / 2);

            var formattedText = new FormattedText
            (
                w.Text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(t.FontFamily, t.FontStyle, t.FontWeight, t.FontStretch),
                t.FontSize,
                Brushes.Black
            );

            TextEffect e1 = new TextEffect
            {
                PositionStart = beginIndex,
                PositionCount = w.Length,
                Transform = new ScaleTransform
                {
                    //TODO: Fix centering
                    CenterX = formattedText.Width / 2,
                    CenterY = formattedText.Height,
                },
            };

            this.TextEffects.Add(e1);

            DoubleAnimationUsingKeyFrames a1 = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(t.Caption.Begin),
                Duration = duration,
            };

            a1.KeyFrames.Add(new LinearDoubleKeyFrame(0.5, KeyTime.FromPercent(0)));
            a1.KeyFrames.Add(new LinearDoubleKeyFrame(1.3, KeyTime.FromPercent(0.5)));
            a1.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromPercent(1)));

            this.Animations.Add(a1);
            this.AnimationTargets.Add(new AnimationTargetString("Transform.ScaleX",0));

            //Make second animation for y scale
            DoubleAnimationUsingKeyFrames a2 = a1.Clone();

            this.Animations.Add(a2);
            this.AnimationTargets.Add(new AnimationTargetString("Transform.ScaleY", 0));

            TextEffect e2 = new TextEffect //Yfinish effect
            {
                PositionStart = beginIndex,
                PositionCount = w.Length,
                Transform = new TranslateTransform(),
            };

            this.TextEffects.Add(e2);

            DoubleAnimationUsingKeyFrames a3 = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(t.Caption.Begin),
                Duration = duration,
            };

            a3.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(0)));
            a3.KeyFrames.Add(new LinearDoubleKeyFrame(-10, KeyTime.FromPercent(0.5)));
            a3.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(1)));

            this.Animations.Add(a3);
            this.AnimationTargets.Add(new AnimationTargetString("Transform.Y", 1));

            //alpha effect
            TextEffect e3 = new TextEffect()
            {
                PositionStart = beginIndex,
                PositionCount = w.Length,
            };

            e3.Foreground = new SolidColorBrush(Colors.White);

            this.TextEffects.Add(e3);

            ColorAnimationUsingKeyFrames a4 = new ColorAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(t.Caption.Begin),
                Duration = duration,
            };

            a4.KeyFrames.Add(new LinearColorKeyFrame(Colors.White, KeyTime.FromPercent(0)));
            a4.KeyFrames.Add(new LinearColorKeyFrame(Color.FromArgb(100, 255, 255, 255), KeyTime.FromPercent(0.5)));
            a4.KeyFrames.Add(new LinearColorKeyFrame(Colors.White, KeyTime.FromPercent(1)));

            this.Animations.Add(a4);
            this.AnimationTargets.Add(new AnimationTargetString("Foreground."
                + SolidColorBrush.ColorProperty.ToString(), 2));
        }
        #endregion
    }
}
