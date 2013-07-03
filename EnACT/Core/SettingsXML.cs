using System;

//This File contains all classes related to the contents of the Settings.xml file.
namespace EnACT
{
    /// <summary>
    /// Represents the settings.xml file.
    /// </summary>
    public class SettingsXML
    {
        public String Base { set; get; }
        public String Spacing { set; get; }
        public String SeparateEmotionWords { set; get; }
        public Playback Playback { set; get; }
        public Skin Skin { set; get; }
        public String SpeakersSource { set; get; }
        public String CaptionsSource { set; get; }
        public String VideoSource { set; get; }

        public AlphaEmotionXML Happy { set; get; }
        public AlphaEmotionXML Sad { set; get; }
        public VibrateEmotionXML Fear { set; get; }
        public VibrateEmotionXML Anger { set; get; }

        /// <summary>
        /// Constructs a SettingsXML object with a video name "video.flv"
        /// </summary>
        public SettingsXML() : this("video.flv") { }

        /// <summary>
        /// Constructs a SettingsXML object with a given name for the video source.
        /// </summary>
        /// <param name="vidsrc">The file name of the video source file</param>
        public SettingsXML(String vidsrc)
        {
            this.Base = "";
            this.Spacing = "1.5";
            this.SeparateEmotionWords = "no";
            this.Playback = new Playback();
            this.Skin = new Skin();
            this.SpeakersSource = "speakers.xml";
            this.CaptionsSource = "dialogues.xml";
            this.VideoSource = vidsrc;

            //Default values for each Emotion class. Values should
            //most likely not be changed.
            this.Happy = new AlphaEmotionXML(
                "48,48,48",         //FPS
                "0.75,0.65,0.60",   //Duration
                "0.5,0.5,0.5",      //ScaleBegin
                "1.1,1.2,1.3",      //ScaleFinish
                "0.5,0.5,0.5",      //AlphaBegin
                "1,1,1",            //AlphaFinish
                "20,40,60");        //YFinish

            this.Sad = new AlphaEmotionXML(
                "48,48,48",         //FPS
                "0.75,1.00,1.25",   //Duration
                "1,1,1",            //ScaleBegin
                "0.70,0.60,0.50",   //ScaleFinish
                "1,1,1",            //AlphaBegin
                "0.60,0.50,0.40",   //AlphaFinish
                "10,15,20");        //YFinish

            this.Fear = new VibrateEmotionXML(
                "72,96,120",        //FPS
                "5.00,5.00,5.00",   //Duration
                "1.04,1.06,1.08",   //ScaleBegin
                "1,1,1",            //ScaleFinish
                "0.25,0.375,0.75",  //VibrateX
                "0.50,0.75,1.00");  //VibrateY

            this.Anger = new VibrateEmotionXML(
                "84,132,180",       //FPS
                "0.625,0.625,0.625",//Duration
                "1,1,1",            //ScaleBegin
                "1.17,1.27,1.37",   //ScaleFinish
                "0.50,0.75,1.00",   //VibrateX
                "1.00,1.25,1.50");  //VibrateY
        }
    }

    /// <summary>
    /// Represents the playback node in Settings.xml
    /// </summary>
    public class Playback
    {
        public bool AutoPlay { set; get; }
        public bool AutoRewind { set; get; }
        public String Seek { set; get; }
        public bool AutoSize { set; get; }
        public int Scale { set; get; }
        public int Volume { set; get; }
        public bool ShowCaptions { set; get; }

        /// <summary>
        /// Constructs a Playback class with all default values
        /// </summary>
        public Playback()
        {
            this.AutoPlay = true;
            this.AutoRewind = false;
            this.Seek = "00:00:00";
            this.AutoSize = false;
            this.Scale = 180;
            this.Volume = 1;
            this.ShowCaptions = true;
        }
    }

    /// <summary>
    /// Represents the Skin node in the Settings.xml file.
    /// </summary>
    public class Skin
    {
        public String Source { set; get; }
        public bool AutoHide { set; get; }
        public int FadeTime { set; get; }
        public int BackGroundAlpha { set; get; }
        public String BackgroundColour { set; get; }  //RGB in the form 0x008040

        /// <summary>
        /// Constructs a Skin object with a default src of "SkinOverPlayMute.swf".
        /// </summary>
        public Skin() : this("SkinOverPlayMute.swf"){}  //Default constructor

        /// <summary>
        /// Constructs a Skin object with a given name for the file of the skin.
        /// </summary>
        /// <param name="src"></param>
        public Skin(String src)
        {
            this.Source = src;
            this.AutoHide = true;
            this.FadeTime = 0;
            this.BackGroundAlpha = 0;
            this.BackgroundColour = "0x000000";
        }
    }

    /// <summary>
    /// Represents an abstract base class with attributes that all 4 emotions 
    /// share in common
    /// </summary>
    public abstract class EmotionXML
    {
        public String Fps { set; get; }
        public String Duration { set; get; }
        public String ScaleBegin { set; get; }
        public String ScaleFinish { set; get; }

        /// <summary>
        /// Constructor for abstract EmotionXML. Fills in the intance variables 
        /// attributed to EmotionXML.
        /// </summary>
        /// <param name="fps">The FPS of the emotion</param>
        /// <param name="dur">The Duration of the emotion</param>
        /// <param name="sb">The ScaleBegin value of the emotion</param>
        /// <param name="sf">The ScaleFinish value of the emotion</param>
        public EmotionXML(String fps, String dur, String sb, String sf)
        {
            this.Fps = fps;
            this.Duration = dur;
            this.ScaleBegin = sb;
            this.ScaleFinish = sf;
        }
    }

    /// <summary>
    /// Represents an emotion that uses Alpha begin and end values as well as a 
    /// Y finish value. Examples: Happy or Sad.
    /// </summary>
    public class AlphaEmotionXML : EmotionXML
    {
        public String AlphaBegin { set; get; }
        public String AlphaFinish { set; get; }
        public String YFinish { set; get; }

        /// <summary>
        /// Constructs an AlphaEmotion with the specified parameters.
        /// </summary>
        /// <param name="fps">The FPS of the emotion</param>
        /// <param name="dur">The Duration of the emotion</param>
        /// <param name="sb">The ScaleBegin value of the emotion</param>
        /// <param name="sf">The ScaleFinish value of the emotion</param>
        /// <param name="ab">The AlphaBegin value of the emotion</param>
        /// <param name="af">The AlphaFinish value of the emotion</param>
        /// <param name="yf">The YFinish value of the emotion</param>
        public AlphaEmotionXML(String fps, String dur, String sb, String sf, 
            String ab, String af, String yf)
            : base(fps, dur, sb, sf)
        {
            this.AlphaBegin = ab;
            this.AlphaFinish = af;
            this.YFinish = yf;
        }
    }

    /// <summary>
    /// Represents an emotion that uses Vibrate X and Y values. 
    /// Examples: Fear and Anger
    /// </summary>
    public class VibrateEmotionXML : EmotionXML
    {
        public String VibrateX { set; get; }
        public String VibrateY { set; get; }

        /// <summary>
        /// Constructs a VibrateEmotionXML with the specified parameters.
        /// </summary>
        /// <param name="fps">The FPS of the emotion</param>
        /// <param name="dur">The Duration of the emotion</param>
        /// <param name="sb">The ScaleBegin value of the emotion</param>
        /// <param name="sf">The ScaleFinish value of the emotion</param>
        /// <param name="vx">The VibrateX value of the emotion</param>
        /// <param name="vy">The VibrateY value of the emotion</param>
        public VibrateEmotionXML(String fps, String dur, String sb, String sf,
            String vx, String vy)
            : base(fps, dur, sb, sf)
        {
            this.VibrateX = vx;
            this.VibrateY = vy;
        }
    }
}