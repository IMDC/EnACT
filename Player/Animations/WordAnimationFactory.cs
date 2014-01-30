using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using LibEnACT;
using Player.Controls;

namespace Player.Animations
{
    public class WordAnimationFactory
    {
        public static WordAnimation CreateWordAnimation(CaptionWord w, int beginIndex, CaptionTextBlock t)
        {
            WordAnimation animation;

            animation = null; //TODO: remove this when all classes created

            switch (w.Emotion)
            {
                case Emotion.Happy:
                    animation = new HappyWordAnimation(w,beginIndex,t);
                    break;
                case Emotion.Sad:
                    break;
                case Emotion.Fear:
                    break;
                case Emotion.Anger:
                    break;
                default: //unkown or no emotion
                    //TODO: explicitly define this behaviour
                    throw new Exception("No emotion specified");
            }
            return animation;
        }
    }
}
