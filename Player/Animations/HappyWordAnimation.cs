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
        /// <param name="c">The Caption that the CaptionWord belongs to.</param>
        /// <param name="t">The Textblock that this animation will be applied to.</param>
        public HappyWordAnimation(CaptionWord w, Caption c, TextBlock t) : base(w, c, t)
        {
            //Duration for all animations
            Duration duration = TimeSpan.FromSeconds(0.6);
            Duration halfDuration = TimeSpan.FromSeconds(0.6 / 2);

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
        #endregion

        //// <summary>
        /// Adds this instance of the HappyWordAnimation to the provided Storyboard and textblock.
        /// </summary>
        /// <param name="storyboard">The storyboard to add this HappyWordAnimation to.</param>
        /// <param name="t">The textblock to add this HappyWordAnimation to.</param>
        public override void AddToMediaPlayer(Storyboard storyboard, TextBlock t)
        {
            //Remember the sizes of each collection before adding
            int oldTextblockSize = t.TextEffects.Count;

            //Set target property for animations and add them to the storyboard

            /* Target the specific properties by specifiying the element at the old last index +
             * the offset. This has to be manually specified, because as far as I know there is not
             * a simple way to automate this.
             */
            Storyboard.SetTargetProperty(Animations[0], new PropertyPath(
                String.Format("TextEffects[{0}].Transform.ScaleX", oldTextblockSize + 0)));
            Storyboard.SetTargetProperty(Animations[1], new PropertyPath(
                String.Format("TextEffects[{0}].Transform.ScaleY", oldTextblockSize + 0)));
            Storyboard.SetTargetProperty(Animations[2], new PropertyPath(
                String.Format("TextEffects[{0}].Transform.Y", oldTextblockSize + 1)));
            Storyboard.SetTargetProperty(Animations[3], new PropertyPath(
                String.Format("TextEffects[{0}].Foreground.{1}", oldTextblockSize + 2, 
                SolidColorBrush.ColorProperty)));

            /* Do the rest of the animation processing. This function call must be performed after
             * the oldTextblockSize is set.
             */
            base.AddToMediaPlayer(storyboard,t);
        }
    }
}
