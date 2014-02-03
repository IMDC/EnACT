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
            //Settings dependent on intensity
            double dur = 0;
            double scalefinish = 0;
            double yFinish = 0;

            switch (w.Intensity)
            {
                case Intensity.Low:
                    dur = 0.75;
                    scalefinish = 1.1;
                    yFinish = 20;
                    break;
                case  Intensity.Medium:
                    dur = 0.65;
                    scalefinish = 1.2;
                    yFinish = 40;
                    break;
                case Intensity.High:
                    dur = 0.60;
                    scalefinish = 1.3;
                    yFinish = 40;
                    break;
                case Intensity.None:
                default:
                    //TODO handle exception
                    break;
            }

            //Animation duration
            Duration duration = TimeSpan.FromSeconds(dur);

            StringBuilder b = new StringBuilder(t.Caption.Text);

            //Formatted text up until the index of the caption
            var captionFT = new FormattedText
            (
                b.ToString(0,beginIndex),
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(t.FontFamily, t.FontStyle, t.FontWeight, t.FontStretch),
                t.FontSize,
                Brushes.Black //Colour does not matter here
            );

            //Formatted text of the emotive caption word
            var wordFT = new FormattedText
            (
                w.Text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(t.FontFamily, t.FontStyle, t.FontWeight, t.FontStretch),
                t.FontSize,
                Brushes.Black //Colour does not matter here
            );


            TextEffect e1 = new TextEffect
            {
                PositionStart = beginIndex,
                PositionCount = w.Length,
                Transform = new ScaleTransform
                {
                    //TODO: Fix centering
                    CenterX = captionFT.Width + wordFT.Width/2,
                    CenterY = captionFT.Height,
                },
            };

            this.TextEffects.Add(e1);

            DoubleAnimationUsingKeyFrames a1 = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromSeconds(t.Caption.Begin),
                Duration = duration,
            };

            a1.KeyFrames.Add(new LinearDoubleKeyFrame(0.5, KeyTime.FromPercent(0)));
            a1.KeyFrames.Add(new LinearDoubleKeyFrame(scalefinish, KeyTime.FromPercent(0.5)));
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
            a3.KeyFrames.Add(new LinearDoubleKeyFrame(-yFinish, KeyTime.FromPercent(0.5)));
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
