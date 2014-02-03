using System.Drawing;

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
        public Color ForegroundColour { set; get; }

        public Color BackgroundColour { set; get; }

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
            this.ForegroundColour = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            this.BackgroundColour = Color.FromArgb(0x0F, 0xFF, 0xFF, 0xFF);
            this.Bold = 1;
        }
    }
}
