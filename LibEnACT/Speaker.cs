namespace LibEnACT
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
        public const string DefaultName = "No Speaker";
        /// <summary>
        /// The name of the description speaker
        /// </summary>
        public const string DescriptionName = "Description";

        /// <summary>
        /// Default speaker, used when no speaker is currently specified
        /// </summary>
        public static readonly Speaker Default = new Speaker();

        /// <summary>
        /// Description speaker, used when a caption is a description such as [laughter] or [music]
        /// </summary>
        public static readonly Speaker Description = new Speaker(DescriptionName);

        /// <summary>
        /// The name of this speaker
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The background settings (color, transparency, etc) associated with captions 
        /// spoken by this speaker
        /// </summary>
        public SpeakerBg Bg { get; set; }

        /// <summary>
        /// The font settings (type, size, etc) associated with captions spoken by 
        /// this speaker
        /// </summary>
        public SpeakerFont Font { get; set; }

        /// <summary>
        /// Constructs a Speaker object with a name of DEFAULT
        /// </summary>
        public Speaker():this(DefaultName){}

        /// <summary>
        /// Constructs a speaker object with a given name.
        /// </summary>
        /// <param name="name">The name of the speaker</param>
        public Speaker(string name)
        {
            this.Name = name;
            Bg = new SpeakerBg();
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
    public class SpeakerBg
    {
        //TODO Change Colour to type Color and get rid of visible, Alpha
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
        public string Colour { set; get; } //Colour is represented as hex eg 0x008040

        /// <summary>
        /// Constucts a SpeakerBG object with visibility set to true, alpha set
        /// to 0.5 and Colour set to white.
        /// </summary>
        public SpeakerBg()
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
        //TODO Change Colour to type Color
        /// <summary>
        /// The font family/type of this font
        /// </summary>
        public string Family { set; get; }

        /// <summary>
        /// The size of this font
        /// </summary>
        public int Size { set; get; }

        /// <summary>
        /// The colour of the text displayed in this font
        /// </summary>
        public string Colour { set; get; }

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
