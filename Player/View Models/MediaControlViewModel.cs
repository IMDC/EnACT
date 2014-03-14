using System.Collections.ObjectModel;
using LibEnACT;
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
    /// View model for the MediaControl control.
    /// </summary>
    public class MediaControlViewModel : ViewModelBase
    {
        private ObservableCollection<Caption> _captionList;

        public MediaPlayerModel MediaPlayerModel { get; private set; }

        public ObservableCollection<Caption> CaptionList
        {
            get { return _captionList; }
            set
            {
                _captionList = value;
                RaisePropertyChanged("CaptionList");
            }
        }


        public MediaControlViewModel()
        {}

        //TODO: Add control commands for media player?
    }
}
