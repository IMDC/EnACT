using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LibEnACT;

namespace Player.Animations
{
    /// <summary>
    /// A set of animations use to animate a CaptionWord.
    /// </summary>
    public abstract class WordAnimation
    {
        /// <summary>
        /// List of text effects for an animated caption Word
        /// </summary>
        public List<TextEffect> TextEffects;

        /// <summary>
        /// List of animations for an animated caption.
        /// </summary>
        public List<AnimationTimeline> Animations;

        public WordAnimation(CaptionWord w, Caption c, TextBlock t)
        {
            //Construct lists
            TextEffects = new List<TextEffect>();
            Animations = new List<AnimationTimeline>();
        }

        /// <summary>
        /// Adds this instance of the WordAnimation to the provided Storyboard and textblock.
        /// </summary>
        /// <param name="storyboard">The storyboard to add this WordAnimation to.</param>
        /// <param name="t">The textblock to add this WordAnimation to.</param>
        public abstract void AddToMediaPlayer(Storyboard storyboard, TextBlock t);
    }
}
