using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
{
    /// <summary>
    /// The Emotion enum represents which type of emotion the caption will
    /// be displayed in.
    /// </summary>
    public enum Emotion
    {
        /// <summary>
        /// Used when the emotion is unknown or erroneous.
        /// </summary>
        Unknown = -1,
        
        /// <summary>
        /// Represents no emotion.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents the happiness emotion.
        /// </summary>
        Happy = 1,

        /// <summary>
        /// Represents the sadness emotion.
        /// </summary>
        Sad = 2,

        /// <summary>
        /// Represents the fear emotion.
        /// </summary>
        Fear = 3,

        /// <summary>
        /// Represents the anger emotion.
        /// </summary>
        Anger = 4,
    };
}
