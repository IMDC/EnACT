using Microsoft.TeamFoundation.MVVM;
using Player.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Views
{
    /// <summary>
    /// View model for the MediaPlayer control.
    /// </summary>
    public class MediaPlayerViewModel : ViewModelBase
    {
        public MediaPlayerModel MediaPlayerModel { get; private set; }

        public MediaPlayerViewModel()
        {}
    }
}
