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
    public class HappyWordAnimation : WordAnimation
    {
        public HappyWordAnimation(CaptionWord w, TextBlock t) : base(w, t)
        {
        }

        public override void AddToMediaPlayer(Storyboard storyboard, TextBlock t)
        {
            throw new NotImplementedException();
        }
    }
}
