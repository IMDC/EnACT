namespace Player
{
    /// <summary>
    /// Defines potential states of the player.
    /// </summary>
    public enum PlayerState
    {
        /// <summary>
        /// The default state. There is no media currently loaded in the player.
        /// </summary>
        Closed,

        /// <summary>
        /// The Player is attempting to open the media.
        /// </summary>
        Opening,

        /// <summary>
        /// The Player is buffering the media.
        /// </summary>
        Buffering,

        /// <summary>
        /// The player is playing the media.
        /// </summary>
        Playing,

        /// <summary>
        /// The media is not being played, but retains the position of the media prior to pausing.
        /// </summary>
        Paused,

        /// <summary>
        /// The media is loaded, but is not playing, and its current position is at 0.
        /// </summary>
        Stopped,
    }
}
