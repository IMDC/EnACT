using System;
using System.Xml;
using AxShockwaveFlashObjects;

namespace EnACT.Controls
{
    /// <summary>
    /// A Flashplayer control designed to communicate with the swf that it has loaded.
    /// </summary>
    public partial class EngineView : AxShockwaveFlash
    {
        /// <summary>
        /// An event that is invoked when the Flash Video is finished loading
        /// </summary>
        public event EventHandler VideoLoaded;

        public EngineView() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invokes the VideoLoaded event, provided it is not null
        /// </summary>
        /// <param name="e">Event Arguments</param>
        public virtual void OnVideoLoaded(EventArgs e)
        {
            /* Make a local copy of the event to prevent the case where the handler
             * will be set as null in-between the null check and the handler call.
             */
            EventHandler handler = VideoLoaded;

            Console.WriteLine("VideoLoaded Event Fired!");
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Pauses the video
        /// </summary>
        public void Pause()
        {
            CallFunction("<invoke name=\"" + "pause" + "\" returntype=\"xml\"></invoke>");
        }

        /// <summary>
        /// Plays the video
        /// </summary>
        public override void Play()
        {
            CallFunction("<invoke name=\"" + "play" + "\" returntype=\"xml\"></invoke>");
        }

        /// <summary>
        /// Toggles the play state of the video.
        /// </summary>
        public void TogglePlay()
        {
            CallFunction("<invoke name=\"" + "togglePlay" + "\" returntype=\"xml\"></invoke>");
        }

        /// <summary>
        /// Returns true or false if the Engine is playing a video or not.
        /// </summary>
        /// <returns>true or false</returns>
        public override Boolean IsPlaying()
        {
            string returnXml = CallFunction("<invoke name=\"" + "isPlaying" + "\" returntype=\"xml\"></invoke>");
            switch (returnXml)
            {
                case @"<true/>": return true;
                default: return false;
            }
        }

        /// <summary>
        /// Returns the length of the flash video in seconds
        /// </summary>
        /// <returns>A double</returns>
        public double VideoLength()
        {
            string returnString = CallFunction("<invoke name=\"" + "videoLength" + "\" returntype=\"xml\"></invoke>");

            //Turn the string into an xml doc
            XmlDocument xmlRequest = new XmlDocument();
            xmlRequest.LoadXml(returnString);

            //Get the value from the tag wrappers
            double length = Convert.ToDouble(xmlRequest["number"].InnerText);
            return length;
        }

        public double GetPlayheadTime()
        {
            string returnString = CallFunction("<invoke name=\"" + "getPlayheadTime" 
                + "\" returntype=\"xml\"></invoke>");
                

            //Turn the string into an xml doc
            XmlDocument xmlRequest = new XmlDocument();
            xmlRequest.LoadXml(returnString);

            //Get the value from the tag wrappers
            double length = Convert.ToDouble(xmlRequest["number"].InnerText);
            return length;
        }

        public void SetPlayHeadTime(double time)
        {
            CallFunction("<invoke name=\"" + "setPlayheadTime" + "\" returntype=\"xml\">"
                + "<arguments><number>" + time + "</number></arguments></invoke>");
        }

        /// <summary>
        /// Handles the recieving of method calls from the .swf object.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void EngineView_FlashCall(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
        {
            Console.WriteLine(e.request);

            //Get the request
            XmlDocument xmlRequest = new XmlDocument();
            xmlRequest.LoadXml(e.request);

            //Get the name of the message
            string messageName = xmlRequest.FirstChild.Attributes[0].InnerText;

            switch (messageName)
            {
                case "Done Loading": OnVideoLoaded(EventArgs.Empty); break;
                default: Console.WriteLine("Unrecognized message: {0}", messageName); break;
            }
        }
    }//Class
}//Namespace
