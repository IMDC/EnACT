using System.ComponentModel;

namespace Player.Models
{
    public class PlayerModel : INotifyPropertyChanged
    {
        #region Constants and Enum Definition

        #endregion

        #region Fields and Properties
        private PlayerState bkCurrentState;

        public PlayerState CurrentState
        {
            get { return bkCurrentState; }
            set
            {
                bkCurrentState = value;
                NotifyPropertyChanged("CurrentState");
            }
        } 
        #endregion

        #region Constructor
        public PlayerModel()
        {
            CurrentState = PlayerState.Closed;
        }
        #endregion

        #region PropertyChanged
        /// <summary>
        /// An event that notifies a subscriber that a property in this class has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property changed.</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
