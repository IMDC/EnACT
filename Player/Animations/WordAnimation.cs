using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LibEnACT;
using Player.Controls;

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

        /// <summary>
        /// A collection of target strings used for setting the property paths of the Animations.
        /// </summary>
        public List<AnimationTargetString> AnimationTargets;

        protected WordAnimation(CaptionWord w, int beginIndex, CaptionTextBlock t)
        {
            //Construct lists
            TextEffects      = new List<TextEffect>();
            Animations       = new List<AnimationTimeline>();
            AnimationTargets = new List<AnimationTargetString>();
        }

        /// <summary>
        /// Adds this instance of the WordAnimation to the provided Storyboard and textblock.
        /// </summary>
        /// <param name="storyboard">The storyboard to add this WordAnimation to.</param>
        /// <param name="t">The textblock to add this WordAnimation to.</param>
        public void AddToMediaPlayer(Storyboard storyboard, TextBlock t)
        {
            //Remember the sizes of each collection before adding
            int oldTextEffectSize = t.TextEffects.Count;

            foreach (TextEffect effect in TextEffects)
            {
                t.TextEffects.Add(effect);
            }

            for (int i = 0; i < Animations.Count; i++)
            {
                Storyboard.SetTargetName(Animations[i],t.Name);
                Storyboard.SetTargetProperty(Animations[i],AnimationTargets[i].PropertyPath(oldTextEffectSize));
                storyboard.Children.Add(Animations[i]);
            }
        }
    }
}
