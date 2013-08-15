using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.View_Models
{
    public class SpeedRatioChangeRequestedEventArgs : EventArgs
    {
        public double SpeedRatio { get; private set; }

        public SpeedRatioChangeRequestedEventArgs(double speedRatio)
        {
            SpeedRatio = speedRatio;
        }
    }
}
