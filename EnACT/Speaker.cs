using System;

namespace EnACT
{
    /// <summary>
    /// Represents the speaker of a line of dialogue. Speakers can be customized
    /// in the size of the font and colour that their lines are displayed in.
    /// </summary>
    public class Speaker
    {
        /// <summary>
        /// The name of the default speaker.
        /// </summary>
        public const String DEFAULTNAME = "NOSPEAKER";
        /// <summary>
        /// The name of the description speaker
        /// </summary>
        public const String DESCRIPTIONNAME = "DESCRIPTION";

        /// <summary>
        /// The name of this speaker
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The background settings (color, transparency, etc) associated with captions 
        /// spoken by this speaker
        /// </summary>
        public SpeakerBG BG { get; set; }

        /// <summary>
        /// The font settings (type, size, etc) associated with captions spoken by 
        /// this speaker
        /// </summary>
        public SpeakerFont Font { get; set; }

        /// <summary>
        /// Constructs a Speaker object with a name of DEFAULT
        /// </summary>
        public Speaker():this(DEFAULTNAME){}

        /// <summary>
        /// Constructs a speaker object with a given name.
        /// </summary>
        /// <param name="name">The name of the speaker</param>
        public Speaker(String name)
        {
            this.Name = name;
            BG = new SpeakerBG();
            Font = new SpeakerFont();
        }

        /// <summary>
        /// Displays the name of the speaker.
        /// </summary>
        /// <returns>The speaker's name</returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Represents the background highlighting of a caption.
    /// </summary>
    public class SpeakerBG
    {
        /// <summary>
        /// Whether or not the background is visible or not
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        /// The transparency setting for the background
        /// </summary>
        public double Alpha { set; get; }

        /// <summary>
        /// The colour of the background.
        /// </summary>
        public String Colour { set; get; } //Colour is represented as hex eg 0x008040

        /// <summary>
        /// Constucts a SpeakerBG object with visibility set to true, alpha set
        /// to 0.5 and Colour set to white.
        /// </summary>
        public SpeakerBG()
        {
            this.Visible = true;
            this.Alpha = 0.5;
            this.Colour = "0x000000";
        }
    }

    /// <summary>
    /// Represents the font a speaker's dialog is displayed in.
    /// </summary>
    public class SpeakerFont
    {
        /// <summary>
        /// The font family/type of this font
        /// </summary>
        public String Family { set; get; }

        /// <summary>
        /// The size of this font
        /// </summary>
        public int Size { set; get; }

        /// <summary>
        /// The colour of the text displayed in this font
        /// </summary>
        public String Colour { set; get; }

        /// <summary>
        /// The boldness value of this font
        /// </summary>
        public int Bold { set; get; }

        /// <summary>
        /// Constructs a SpeakerFont object with the family set to Segoe UI,
        /// the size to 22, the colour to black, and the boldness to 1.
        /// </summary>
        public SpeakerFont()
        {
            this.Family = "Segoe UI";
            this.Size = 22;
            this.Colour = "0xFFFFFF";
            this.Bold = 1;
        }
    }
}
