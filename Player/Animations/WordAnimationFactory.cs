using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using LibEnACT;

namespace Player.Animations
{
    public class WordAnimationFactory
    {
        public static WordAnimation CreateWordAnimation(CaptionWord w, Caption c, TextBlock t)
        {
            WordAnimation animation;

            animation = null; //TODO: remove this when all classes created

            switch (w.Emotion)
            {
                case Emotion.Happy:
                    animation = new HappyWordAnimation(w,c,t);
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
