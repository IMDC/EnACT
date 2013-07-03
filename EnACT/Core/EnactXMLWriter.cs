using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EnACT
{
    /// <summary>
    /// Contains all the methods required for writing an EnACT project to
    /// XML files.
    /// </summary>
    public class EnactXMLWriter
    {
        //Path Variables
        public String SpeakersPath { set; get; }
        public String CaptionsPath { set; get; }
        public String SettingsPath { set; get; }

        //Object reference variables
        public Dictionary<String, Speaker> SpeakerSet { set; get; }
        public List<Caption> CaptionList { set; get; }
        public SettingsXML Settings { set; get; }

        /// <summary>
        /// Constructs an EnactXMLWriter object with references to the objects that need to be written.
        /// The paths to write to are set to a default value.
        /// </summary>
        /// <param name="SpeakerSet">A reference to the SpeakerSet object that is to be written</param>
        /// <param name="CaptionList">A reference to the CaptionList object that is to be written</param>
        /// <param name="Settings">A reference to the Settings object that is to be written</param>
        public EnactXMLWriter(Dictionary<String, Speaker> SpeakerSet, List<Caption> CaptionList, 
            SettingsXML Settings) :
            this(Paths.DefaultSpeakers, SpeakerSet, 
                 Paths.DefaultDialogues, CaptionList, 
                 Paths.DefaultSettings,  Settings) {}

        /// <summary>
        /// Constructs an EnactXMLWriter object with object references to the objects that need to be written
        /// as well as the paths that need to be written to.
        /// </summary>
        /// <param name="SpeakersPath">The path that speakers.xml is to be written to</param>
        /// <param name="SpeakerSet">A reference to the SpeakerSet object that is to be written</param>
        /// <param name="CaptionsPath">The path that dialogues.xml is to be written to</param>
        /// <param name="CaptionList">A reference to the CaptionList object that is to be written</param>
        /// <param name="SettingsPath">The path that Settings.xml is to be written to</param>
        /// <param name="Settings">A reference to the Settings object that is to be written</param>
        public EnactXMLWriter(String SpeakersPath, Dictionary<String, Speaker> SpeakerSet, String CaptionsPath, 
            List<Caption> CaptionList, String SettingsPath, SettingsXML Settings)
        {
            this.SpeakersPath = SpeakersPath;
            this.SpeakerSet = SpeakerSet;
            this.CaptionsPath = CaptionsPath;
            this.CaptionList = CaptionList;
            this.SettingsPath = SettingsPath;
            this.Settings = Settings;
        }

        /// <summary>
        /// Calls the WriteSpeakers, WriteCaptions, and WriteSettings methods in that specific order
        /// </summary>
        public void WriteAll()
        {
            WriteSpeakers();
            WriteCaptions();
            WriteSettings();
            Console.WriteLine("XML Files written");
        }

        /// <summary>
        /// Writes the object referenced by SpeakerSet to an XML file at the path given by Speakerspath
        /// </summary>
        public void WriteSpeakers()
        {
            //NOTE: XmlTextWriter does not require curly braces between writeStartElement and 
            //writeCloseElement. It is put there to easily see what belongs to what node.

            //Return if nothing to write
            if (SpeakerSet.Count == 0)
            {
                //TODO throw error?
                Console.WriteLine("Error: SpeakersList is empty");
                return;
            }

            //speakers.xml
            using (XmlTextWriter w = new XmlTextWriter(SpeakersPath, Encoding.UTF8))
            {
                //Set formatting so that the file will use newlines and 4-spaced indents
                w.Formatting = Formatting.Indented;
                w.IndentChar = '\t';
                w.Indentation = 1;

                w.WriteStartDocument();
                //XML header file
                w.WriteDocType("speakers", null, "../speakers.dtd", null);
                w.WriteStartElement("speakers");
                {
                    foreach (Speaker s in SpeakerSet.Values)
                    {
                        w.WriteStartElement("speaker");
                        {
                            w.WriteAttributeString("name", s.Name);

                            w.WriteStartElement("background");
                            {
                                //bool hashcode returns a 0 or 1
                                w.WriteAttributeString("visible", Convert.ToString(s.BG.Visible.GetHashCode()));
                                w.WriteAttributeString("alpha", Convert.ToString(s.BG.Alpha));
                                w.WriteAttributeString("colour", s.BG.Colour);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("font");
                            {
                                w.WriteAttributeString("name", s.Font.Family);
                                w.WriteAttributeString("size", Convert.ToString(s.Font.Size));
                                w.WriteAttributeString("colour", s.Font.Colour);
                                w.WriteAttributeString("bold", Convert.ToString(s.Font.Bold));
                            }
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();
                    }
                }
                w.WriteEndElement();

                w.WriteEndDocument();
            }
        }

        /// <summary>
        /// Writes the object referenced by CaptionsList to an XML file at the path given by Captionspath
        /// </summary>
        public void WriteCaptions()
        {
            //NOTE: XmlTextWriter does not require curly braces between writeStartElement and 
            //writeCloseElement. It is put there to easily see what belongs to what node.

            //Return if nothing to write
            if (CaptionList.Count == 0)
            {
                //TODO throw error?
                Console.WriteLine("Error: CaptionList is empty");
                return;
            }

            //dialogues.xml
            using (XmlTextWriter w = new XmlTextWriter(CaptionsPath, Encoding.UTF8))
            {
                //Set formatting so that the file will use newlines and 4-spaced indents
                w.Formatting = Formatting.Indented;
                w.IndentChar = '\t';
                w.Indentation = 1;

                w.WriteStartDocument();
                //XML header file
                w.WriteDocType("captions", null, "../captions.dtd", null);
                w.WriteStartElement("captions");
                {
                    foreach (Caption c in CaptionList)
                    {
                        w.WriteStartElement("caption");
                        {
                            w.WriteAttributeString("begin", c.Begin.AsString);
                            w.WriteAttributeString("end", c.End.AsString);
                            w.WriteAttributeString("speaker", c.Speaker.Name);
                            //Both location and alignment refer to hashcode
                            w.WriteAttributeString("location", Convert.ToString(c.Location.GetHashCode()));
                            w.WriteAttributeString("align", Convert.ToString(c.Alignment.GetHashCode()));

                            foreach (EditorCaptionWord cw in c.Words)
                            {
                                w.WriteStartElement("emotion");
                                {
                                    w.WriteAttributeString("type", Convert.ToString(cw.Emotion.GetHashCode()));
                                    w.WriteAttributeString("intensity", Convert.ToString(cw.Intensity.GetHashCode()));
                                    w.WriteString(cw.Text);
                                }
                                w.WriteEndElement();
                            }
                        }
                        w.WriteEndElement();
                    }
                }
                w.WriteEndElement();

                w.WriteEndDocument();
            }
        }

        /// <summary>
        /// Writes the object referenced by Settings to an XML file at the path given by Settingsspath
        /// </summary>
        public void WriteSettings()
        {
            //NOTE: XmlTextWriter does not require curly braces between writeStartElement and 
            //writeCloseElement. It is put there to easily see what belongs to what node.

            //Settings.xml
            using (XmlTextWriter w = new XmlTextWriter(SettingsPath, Encoding.UTF8))
            {
                //Set formatting so that the file will use newlines and 4-spaced indents
                w.Formatting = Formatting.Indented;
                w.Indentation = 2;

                w.WriteStartDocument();
                //XML header file
                w.WriteDocType("settings", null, "Resources/settings.dtd", null);
                w.WriteStartElement("settings");
                {
                    w.WriteStartElement("meta");
                    {
                        w.WriteAttributeString("name", "base");
                        w.WriteAttributeString("content", "");
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("meta");
                    {
                        w.WriteAttributeString("name", "word-spacing");
                        w.WriteAttributeString("content", Settings.Spacing);
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("meta");
                    {
                        w.WriteAttributeString("name", "separate-emotion-words");
                        w.WriteAttributeString("content", Settings.SeparateEmotionWords);
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("playback");
                    {
                        w.WriteAttributeString("autoPlay", Convert.ToString(Settings.Playback.AutoPlay).ToLower());
                        w.WriteAttributeString("autoRewind", Convert.ToString(Settings.Playback.AutoRewind).ToLower());
                        w.WriteAttributeString("seek", Settings.Playback.Seek);
                        w.WriteAttributeString("autoSize", Convert.ToString(Settings.Playback.AutoSize).ToLower());
                        w.WriteAttributeString("scale", Convert.ToString(Settings.Playback.Scale));
                        w.WriteAttributeString("volume", Convert.ToString(Settings.Playback.Volume));
                        w.WriteAttributeString("showCaptions", Convert.ToString(Settings.Playback.ShowCaptions)
                            .ToLower());
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("skin");
                    {
                        w.WriteAttributeString("src", Settings.Skin.Source);
                        w.WriteAttributeString("skinAutoHide", Convert.ToString(Settings.Skin.AutoHide).ToLower());
                        w.WriteAttributeString("skinFadeTime", Convert.ToString(Settings.Skin.FadeTime));
                        w.WriteAttributeString("skinBackgroundAlpha", Convert.ToString(Settings.Skin.BackGroundAlpha));
                        w.WriteAttributeString("skinBackgroundColour", Settings.Skin.BackgroundColour);
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("content");
                    {
                        w.WriteStartElement("speakers");
                        {
                            w.WriteAttributeString("src", Settings.SpeakersSource);
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("captions");
                        {
                            w.WriteAttributeString("src", Settings.CaptionsSource);
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("video");
                        {
                            w.WriteAttributeString("src", Settings.VideoSource);
                        }
                        w.WriteEndElement();
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("emotions");
                    {
                        w.WriteStartElement("happy");
                        {
                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "fps");
                                w.WriteAttributeString("value", Settings.Happy.Fps);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "dur");
                                w.WriteAttributeString("value", Settings.Happy.Duration);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "alphaBegin");
                                w.WriteAttributeString("value", Settings.Happy.AlphaBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "alphaFinish");
                                w.WriteAttributeString("value", Settings.Happy.AlphaFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleBegin");
                                w.WriteAttributeString("value", Settings.Happy.ScaleBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleFinish");
                                w.WriteAttributeString("value", Settings.Happy.ScaleFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "yFinish");
                                w.WriteAttributeString("value", Settings.Happy.YFinish);
                            }
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("sad");
                        {
                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "fps");
                                w.WriteAttributeString("value", Settings.Sad.Fps);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "dur");
                                w.WriteAttributeString("value", Settings.Sad.Duration);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "alphaBegin");
                                w.WriteAttributeString("value", Settings.Sad.AlphaBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "alphaFinish");
                                w.WriteAttributeString("value", Settings.Sad.AlphaFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleBegin");
                                w.WriteAttributeString("value", Settings.Sad.ScaleBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleFinish");
                                w.WriteAttributeString("value", Settings.Sad.ScaleFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "yFinish");
                                w.WriteAttributeString("value", Settings.Sad.YFinish);
                            }
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("fear");
                        {
                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "fps");
                                w.WriteAttributeString("value", Settings.Fear.Fps);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "dur");
                                w.WriteAttributeString("value", Settings.Fear.Duration);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleBegin");
                                w.WriteAttributeString("value", Settings.Fear.ScaleBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleFinish");
                                w.WriteAttributeString("value", Settings.Fear.ScaleFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "vibrateX");
                                w.WriteAttributeString("value", Settings.Fear.VibrateX);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "vibrateY");
                                w.WriteAttributeString("value", Settings.Fear.VibrateY);
                            }
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("anger");
                        {
                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "fps");
                                w.WriteAttributeString("value", Settings.Anger.Fps);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "dur");
                                w.WriteAttributeString("value", Settings.Anger.Duration);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleBegin");
                                w.WriteAttributeString("value", Settings.Anger.ScaleBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleFinish");
                                w.WriteAttributeString("value", Settings.Anger.ScaleFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "vibrateX");
                                w.WriteAttributeString("value", Settings.Anger.VibrateX);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "vibrateY");
                                w.WriteAttributeString("value", Settings.Anger.VibrateY);
                            }
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();
                    }
                    w.WriteEndElement();
                }
                w.WriteEndElement();

                w.WriteEndDocument();
            }
        }
    }
}
