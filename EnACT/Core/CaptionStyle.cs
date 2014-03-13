using System.ComponentModel;
using System.Drawing;
using LibEnACT;

namespace EnACT.Core
{
    /// <summary>
    /// Represents the style used to mark up and display captions in the EnACT editor.
    /// </summary>
    public class CaptionStyle
    {
        #region Predefined CaptionColourSets
        //None Style
        public static readonly CaptionStyle None_Style  = new CaptionStyle(SystemColors.ControlText,
            Color.White);

        //Happy Styles
        public static readonly CaptionStyle Happy_Low    = new CaptionStyle(SystemColors.ControlText, 
                                                                    Color.LightYellow);
        public static readonly CaptionStyle Happy_Medium = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.Yellow);
        public static readonly CaptionStyle Happy_High   = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.Goldenrod);

        //Sad Styles
        public static readonly CaptionStyle Sad_Low    = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.LightBlue);
        public static readonly CaptionStyle Sad_Medium = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.Blue);
        public static readonly CaptionStyle Sad_High   = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.DarkBlue);

        //Fear Styles
        public static readonly CaptionStyle Fear_Low    = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.LightGreen);
        public static readonly CaptionStyle Fear_Medium = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.Green);
        public static readonly CaptionStyle Fear_High   = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.DarkGreen);

        //Anger Styles
        public static readonly CaptionStyle Anger_Low    = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.Pink);
        public static readonly CaptionStyle Anger_Medium = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.Red);
        public static readonly CaptionStyle Anger_High   = new CaptionStyle(SystemColors.ControlText,
                                                                    Color.DarkRed);

        //Highlight
        public static readonly CaptionStyle Highlighted  = new CaptionStyle(SystemColors.HighlightText, 
                                                                    SystemColors.Highlight);
        #endregion

        #region Properties and Fields
        /// <summary>
        /// The Text Colour of the Caption Text that uses this Style.
        /// </summary>
        public Color TextColour { set; get; }
        /// <summary>
        /// The Background(Highlight) Colour of the Caption Text that uses this Style.
        /// </summary>
        public Color BackColour { set; get; }
        #endregion

        #region Contstructor
        /// <summary>
        /// Constructs a CaptionStyle object with the specified paramaters.
        /// </summary>
        /// <param name="textColor">The Text Colour of this Style.</param>
        /// <param name="backColour">The Background(Highlight) Colour of this Style.</param>
        public CaptionStyle(Color textColor, Color backColour)
        {
            this.TextColour = textColor;
            this.BackColour = backColour;
        }
        #endregion

        #region GetColour
        /// <summary>
        /// Gets the Color of a CaptionWord given the specified CaptionWord.
        /// </summary>
        /// <param name="cw">The CaptionWord to get the style of.</param>
        /// <returns></returns>
        public static CaptionStyle GetColourOf(CaptionWord cw)
        { return GetStyleOf(cw.Emotion, cw.Intensity); }

        /// <summary>
        /// Gets the Color of a CaptionWord given the specified Emotion type and Intensity.
        /// </summary>
        /// <param name="e">The emotion of the colour.</param>
        /// <param name="i">The intensity of the colour.</param>
        /// <returns></returns>
        public static CaptionStyle GetStyleOf(Emotion e, Intensity i)
        {
            switch (e)
            {
                case Emotion.Happy:
                    switch (i)
                    {
                        case Intensity.Low:     return Happy_Low;
                        case Intensity.Medium:  return Happy_Medium;
                        case Intensity.High:    return Happy_High;
                        case Intensity.None:    return None_Style;
                        default: throw new InvalidEnumArgumentException("i", i.GetHashCode(), typeof(Intensity));
                    }
                case Emotion.Sad:
                    switch (i)
                    {
                        case Intensity.Low:     return Sad_Low;
                        case Intensity.Medium:  return Sad_Medium;
                        case Intensity.High:    return Sad_High;
                        case Intensity.None:    return None_Style;
                        default: throw new InvalidEnumArgumentException("i", i.GetHashCode(), typeof(Intensity));
                    }
                case Emotion.Fear:
                    switch (i)
                    {
                        case Intensity.Low:     return Fear_Low;
                        case Intensity.Medium:  return Fear_Medium;
                        case Intensity.High:    return Fear_High;
                        case Intensity.None:    return None_Style;
                        default: throw new InvalidEnumArgumentException("i", i.GetHashCode(), typeof(Intensity));
                    }
                case Emotion.Anger:
                    switch (i)
                    {
                        case Intensity.Low:     return Anger_Low;
                        case Intensity.Medium:  return Anger_Medium;
                        case Intensity.High:    return Anger_High;
                        case Intensity.None:    return None_Style;
                        default: throw new InvalidEnumArgumentException("i", i.GetHashCode(), typeof(Intensity));
                    }
                case Emotion.None:
                    return None_Style;
                default: throw new InvalidEnumArgumentException("e", e.GetHashCode(), typeof(Emotion));
            }
        }
        #endregion
    }
}
