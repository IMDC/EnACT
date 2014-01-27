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
    public class AngerWordAnimation : WordAnimation
    {
        public AngerWordAnimation(CaptionWord w, Caption c, TextBlock t) : base(w, c, t)
        {
        }

        public override void AddToMediaPlayer(Storyboard storyboard, TextBlock t)
        {
            throw new NotImplementedException();
        }
    }
}
