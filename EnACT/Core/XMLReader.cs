using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EnACT
{
    /// <summary>
    /// A static class that contains XML reading methods.
    /// </summary>
    public static class XMLReader
    {
        #region ParseProject
        /// <summary>
        /// Reads in and parses an enact Project file.
        /// </summary>
        /// <param name="path">The path of the project file.</param>
        /// <returns>The ProjectInfo class containted in the file.</returns>
        public static ProjectInfo ParseProject(string path)
        {
            string name;
            string videoPath;
            string settings;
            string speakers;
            string dialogues;
            string projectPath = Path.GetDirectoryName(path);

            ProjectInfo project;

            using (XmlTextReader r = new XmlTextReader(path))
            {
                r.ReadStartElement("project");
                {
                    r.ReadStartElement("name");
                    name = r.ReadString();
                    r.ReadEndElement();

                    r.ReadStartElement("video");
                    videoPath = r.ReadString();
                    r.ReadEndElement();

                    r.ReadStartElement("settings");
                    settings = r.ReadString();
                    r.ReadEndElement();

                    r.ReadStartElement("speakers");
                    speakers = r.ReadString();
                    r.ReadEndElement();

                    r.ReadStartElement("dialogues");
                    dialogues = r.ReadString();
                    r.ReadEndElement();
                }
                r.ReadEndElement();
            }

            //Construct the project
            project = new ProjectInfo(name, videoPath, projectPath);

            var tuple = ParseEngineXML(Path.Combine(projectPath, "engine" + ProjectInfo.EngineXMLExtension));

            project.CaptionList = tuple.Item1;
            project.SpeakerSet = tuple.Item2;
            project.Settings = tuple.Item3;
            return project;
        }
        #endregion

        #region ParseSettings
        /// <summary>
        /// Reads in a settings xml file and creates a SettingsXML object out of it.
        /// </summary>
        /// <param name="path">The full path to the xml file.</param>
        /// <returns>The SettingsXML object contained in the file.</returns>
        public static SettingsXML ParseSettings(string path)
        {
            SettingsXML settings = Utilities.ConstructSettingsXML();

            using (XmlTextReader r = new XmlTextReader(path))
            {
                //Ignore dtd as there is no need to validate it.
                r.DtdProcessing = DtdProcessing.Ignore;

                r.ReadStartElement("settings");
                {
                    r.ReadStartElement("meta");
                    r.GetAttribute("name");
                    settings.Base = r.GetAttribute("content");

                    r.ReadStartElement("meta");
                    r.GetAttribute("name");
                    settings.Spacing = r.GetAttribute("content");

                    r.ReadStartElement("meta");
                    r.GetAttribute("name");
                    settings.SeparateEmotionWords = r.GetAttribute("content");

                    r.ReadStartElement("playback");
                    settings.Playback.AutoPlay = Convert.ToBoolean(r.GetAttribute("autoPlay"));
                    settings.Playback.AutoRewind = Convert.ToBoolean(r.GetAttribute("autoRewind"));
                    settings.Playback.Seek = r.GetAttribute("seek");
                    settings.Playback.AutoSize = Convert.ToBoolean(r.GetAttribute("autoSize"));
                    settings.Playback.Scale = Convert.ToInt32(r.GetAttribute("scale"));
                    settings.Playback.Volume = Convert.ToInt32(r.GetAttribute("volume"));
                    settings.Playback.ShowCaptions = Convert.ToBoolean(r.GetAttribute("showCaptions"));

                    r.ReadStartElement("skin");
                    settings.Skin.Source = r.GetAttribute("src");
                    settings.Skin.AutoHide = Convert.ToBoolean(r.GetAttribute("skinAutoHide"));
                    settings.Skin.FadeTime = Convert.ToInt32(r.GetAttribute("skinFadeTime"));
                    settings.Skin.BackGroundAlpha = Convert.ToInt32(r.GetAttribute("skinBackgroundAlpha"));
                    settings.Skin.BackgroundColour = r.GetAttribute("skinBackgroundColor");

                    r.ReadStartElement("content");
                    {
                        r.ReadStartElement("speakers");
                        settings.SpeakersSource = r.GetAttribute("src");

                        r.ReadStartElement("captions");
                        settings.CaptionsSource = r.GetAttribute("src");

                        r.ReadStartElement("video");
                        settings.VideoSource = r.GetAttribute("src");
                    }
                    r.ReadEndElement();

                    //These can be left alone for now as the values should stay as the default for now.
                    r.ReadStartElement("emotions");
                    {
                        r.ReadStartElement("happy");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadEndElement();

                        r.ReadStartElement("sad");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadEndElement();

                        r.ReadStartElement("fear");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadEndElement();

                        r.ReadStartElement("anger");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadStartElement("param");
                        r.ReadEndElement();
                    }
                    r.ReadEndElement();
                }
                r.ReadEndElement();
            }

            return settings;
        }
        #endregion

        #region ParseEngineXML
        /// <summary>
        /// Reads in an engine.xml file and turns it into a CaptionList, SpeakerSet and engine 
        /// Settings.
        /// </summary>
        /// <param name="path">The Path to the engine.xml file.</param>
        /// <returns>A 3-Tuple containing a CaptionList, a SpeakerSet, and a Settings 
        /// object in that specific order.</returns>
        public static Tuple<List<EditorCaption>, Dictionary<string, Speaker>, SettingsXML> ParseEngineXML(string path)
        {
            var captionList = new List<EditorCaption>();
            var speakerSet = new Dictionary<string, Speaker>();
            var settings = new SettingsXML();

            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreWhitespace = true;
            readerSettings.IgnoreComments = true;

            using (XmlReader r = XmlReader.Create(path, readerSettings))
            {

                while(r.Read())
                {
                    //Look for start elements only.
                    if (!r.IsStartElement())
                        continue;
                        
                    // Get element name and switch on it.
                    switch (r.Name)
                    {
                        case XMLElements.Enact: break;
                        case XMLElements.Settings:

                            r.Read(); 
                            r.AssertNode(XMLElements.Meta);
                            settings.Base = r.GetNonNullAttribute(XMLAttributes.Base);
                            settings.Spacing = r.GetNonNullAttribute(XMLAttributes.WordSpacing);
                            settings.SeparateEmotionWords = r.GetNonNullAttribute(XMLAttributes.SeparateEmotionWords);

                            r.Read();
                            r.AssertNode(XMLElements.Playback);
                            settings.Playback.AutoPlay = r.GetBoolAttribute(XMLAttributes.AutoPlay);
                            settings.Playback.AutoRewind = r.GetBoolAttribute(XMLAttributes.AutoRewind);
                            settings.Playback.Seek = r.GetNonNullAttribute(XMLAttributes.Seek);
                            settings.Playback.AutoSize = r.GetBoolAttribute(XMLAttributes.AutoSize);
                            settings.Playback.Scale = r.GetIntAttribute(XMLAttributes.Scale);
                            settings.Playback.Volume = r.GetIntAttribute(XMLAttributes.Volume);
                            settings.Playback.ShowCaptions = r.GetBoolAttribute(XMLAttributes.ShowCaptions);

                            r.Read();
                            r.AssertNode(XMLElements.Skin);
                            settings.Skin.Source = r.GetNonNullAttribute(XMLAttributes.Source);
                            settings.Skin.AutoHide = r.GetBoolAttribute(XMLAttributes.AutoHide);
                            settings.Skin.FadeTime = r.GetIntAttribute(XMLAttributes.FadeTime);
                            settings.Skin.BackGroundAlpha = r.GetIntAttribute(XMLAttributes.BackgroundAlpha);
                            settings.Skin.BackgroundColour = r.GetNonNullAttribute(XMLAttributes.BackgroundColour);
                            
                            r.Read();
                            r.AssertNode(XMLElements.Video);
                            settings.VideoSource = r.GetNonNullAttribute(XMLAttributes.Source);
                           
                            r.Read();
                            r.AssertNode(XMLElements.Emotions);
                                r.Read();
                                r.AssertNode(XMLElements.Happy);
                                settings.Happy.Fps = r.GetNonNullAttribute(XMLAttributes.FPS);
                                settings.Happy.Duration = r.GetNonNullAttribute(XMLAttributes.Duration);
                                settings.Happy.AlphaBegin = r.GetNonNullAttribute(XMLAttributes.AlphaBegin);
                                settings.Happy.AlphaFinish = r.GetNonNullAttribute(XMLAttributes.AlphaFinish);
                                settings.Happy.ScaleBegin = r.GetNonNullAttribute(XMLAttributes.ScaleBegin);
                                settings.Happy.ScaleFinish = r.GetNonNullAttribute(XMLAttributes.ScaleFinish);
                                settings.Happy.YFinish = r.GetNonNullAttribute(XMLAttributes.YFinish);
                                
                                r.Read();
                                r.AssertNode(XMLElements.Sad);
                                settings.Sad.Fps = r.GetNonNullAttribute(XMLAttributes.FPS);
                                settings.Sad.Duration = r.GetNonNullAttribute(XMLAttributes.Duration);
                                settings.Sad.AlphaBegin = r.GetNonNullAttribute(XMLAttributes.AlphaBegin);
                                settings.Sad.AlphaFinish = r.GetNonNullAttribute(XMLAttributes.AlphaFinish);
                                settings.Sad.ScaleBegin = r.GetNonNullAttribute(XMLAttributes.ScaleBegin);
                                settings.Sad.ScaleFinish = r.GetNonNullAttribute(XMLAttributes.ScaleFinish);
                                settings.Sad.YFinish = r.GetNonNullAttribute(XMLAttributes.YFinish);
                                
                                r.Read();
                                r.AssertNode(XMLElements.Fear);
                                settings.Fear.Fps = r.GetNonNullAttribute(XMLAttributes.FPS);
                                settings.Fear.Duration = r.GetNonNullAttribute(XMLAttributes.Duration);
                                settings.Fear.ScaleBegin = r.GetNonNullAttribute(XMLAttributes.ScaleBegin);
                                settings.Fear.ScaleFinish = r.GetNonNullAttribute(XMLAttributes.ScaleFinish);
                                settings.Fear.VibrateX = r.GetNonNullAttribute(XMLAttributes.VibrateX);
                                settings.Fear.VibrateY = r.GetNonNullAttribute(XMLAttributes.VibrateY);
                                
                                r.Read();
                                r.AssertNode(XMLElements.Anger);
                                settings.Anger.Fps = r.GetNonNullAttribute(XMLAttributes.FPS);
                                settings.Anger.Duration = r.GetNonNullAttribute(XMLAttributes.Duration);
                                settings.Anger.ScaleBegin = r.GetNonNullAttribute(XMLAttributes.ScaleBegin);
                                settings.Anger.ScaleFinish = r.GetNonNullAttribute(XMLAttributes.ScaleFinish);
                                settings.Anger.VibrateX = r.GetNonNullAttribute(XMLAttributes.VibrateX);
                                settings.Anger.VibrateY = r.GetNonNullAttribute(XMLAttributes.VibrateY);
                            break;
                        case XMLElements.Speakers: break; //Do Nothing
                        case XMLElements.Speaker:
                            r.AssertNode(XMLElements.Speaker);
                            string name = r.GetNonNullAttribute(XMLAttributes.Name);
                            Speaker s = new Speaker(name);
                            
                            r.Read();
                            r.AssertNode(XMLElements.Background);
                            s.BG.Visible = Convert.ToBoolean(r.GetNonNullAttribute(XMLAttributes.Visible));
                            s.BG.Alpha = r.GetDoubleAttribute(XMLAttributes.Alpha);
                            s.BG.Colour = r.GetNonNullAttribute(XMLAttributes.Colour);
                           
                            r.Read();
                            r.AssertNode(XMLElements.Font);
                            s.Font.Family = r.GetNonNullAttribute(XMLAttributes.Name);
                            s.Font.Size = r.GetIntAttribute(XMLAttributes.Size);
                            s.Font.Colour = r.GetNonNullAttribute(XMLAttributes.Colour);
                            s.Font.Bold = r.GetIntAttribute(XMLAttributes.Bold);
                            r.ReadStartElement(XMLElements.Font);
                                    
                            //Add to speakerSet
                            speakerSet[s.Name] = s;
                            break;
                        case XMLElements.Captions: break; //Do Nothing
                        case XMLElements.Caption:
                            r.AssertNode(XMLElements.Caption);
                            EditorCaption c = new EditorCaption();
                            c.Begin = r.GetNonNullAttribute(XMLAttributes.Begin);
                            c.End = r.GetNonNullAttribute(XMLAttributes.End);
                            c.Speaker = speakerSet[r.GetNonNullAttribute(XMLAttributes.Speaker)];
                            c.Location = (ScreenLocation) r.GetIntAttribute(XMLAttributes.Location);
                            c.Alignment = (Alignment) r.GetIntAttribute(XMLAttributes.Align);

                            List<EditorCaptionWord> wordList = new List<EditorCaptionWord>();

                            while (r.Read())
                            {
                                //If the Node is an end element, then the reader has parsed
                                //through all of this caption's words.
                                if (r.NodeType == XmlNodeType.EndElement && r.Name.Equals(XMLElements.Caption))
                                    break;
                                else if (r.NodeType == XmlNodeType.Element && r.Name.Equals(XMLElements.Word))
                                {
                                    r.AssertNode(XMLElements.Word); //Doublecheck, it's the only way to be sure.

                                    Emotion e = (Emotion)r.GetIntAttribute(XMLAttributes.Emotion);
                                    Intensity i = (Intensity)r.GetIntAttribute(XMLAttributes.Intensity);

                                    //Get word from node and add it to the list
                                    EditorCaptionWord word = new EditorCaptionWord(e, i, r.ReadString(), 0);
                                    c.Words.Add(word);
                                }
                            }
                            c.ReindexWords(); //Set up proper indexes
                            captionList.Add(c);
                            break;
                        default: throw new ArgumentException("Value '" + r.Name + "' is not a valid node", r.Name);
                    }
                }//Enact
            }

            return Tuple.Create(captionList,speakerSet,settings);
        }
        #endregion
    }//Class
}//Namepace
