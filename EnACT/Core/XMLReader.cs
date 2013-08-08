using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using EnACT.Miscellaneous;

namespace EnACT.Core
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
            ProjectInfo project = new ProjectInfo(name, videoPath, projectPath);

            var tuple = ParseEngineXml(Path.Combine(projectPath, "engine" + ProjectInfo.EngineXmlExtension));

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
        public static SettingsXml ParseSettings(string path)
        {
            SettingsXml settings = Utilities.ConstructSettingsXml();

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
        public static Tuple<List<EditorCaption>, Dictionary<string, Speaker>, SettingsXml> ParseEngineXml(string path)
        {
            var captionList = new List<EditorCaption>();
            var speakerSet = new Dictionary<string, Speaker>();
            var settings = new SettingsXml();

            XmlReaderSettings readerSettings = new XmlReaderSettings
            {
                IgnoreWhitespace = true, 
                IgnoreComments = true
            };

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
                        case XmlElements.Enact: break;
                        case XmlElements.Settings:

                            r.Read(); 
                            r.AssertNode(XmlElements.Meta);
                            settings.Base = r.GetNonNullAttribute(XmlAttributes.Base);
                            settings.Spacing = r.GetNonNullAttribute(XmlAttributes.WordSpacing);
                            settings.SeparateEmotionWords = r.GetNonNullAttribute(XmlAttributes.SeparateEmotionWords);

                            r.Read();
                            r.AssertNode(XmlElements.Playback);
                            settings.Playback.AutoPlay = r.GetBoolAttribute(XmlAttributes.AutoPlay);
                            settings.Playback.AutoRewind = r.GetBoolAttribute(XmlAttributes.AutoRewind);
                            settings.Playback.Seek = r.GetNonNullAttribute(XmlAttributes.Seek);
                            settings.Playback.AutoSize = r.GetBoolAttribute(XmlAttributes.AutoSize);
                            settings.Playback.Scale = r.GetIntAttribute(XmlAttributes.Scale);
                            settings.Playback.Volume = r.GetIntAttribute(XmlAttributes.Volume);
                            settings.Playback.ShowCaptions = r.GetBoolAttribute(XmlAttributes.ShowCaptions);

                            r.Read();
                            r.AssertNode(XmlElements.Skin);
                            settings.Skin.Source = r.GetNonNullAttribute(XmlAttributes.Source);
                            settings.Skin.AutoHide = r.GetBoolAttribute(XmlAttributes.AutoHide);
                            settings.Skin.FadeTime = r.GetIntAttribute(XmlAttributes.FadeTime);
                            settings.Skin.BackGroundAlpha = r.GetIntAttribute(XmlAttributes.BackgroundAlpha);
                            settings.Skin.BackgroundColour = r.GetNonNullAttribute(XmlAttributes.BackgroundColour);
                            
                            r.Read();
                            r.AssertNode(XmlElements.Video);
                            settings.VideoSource = r.GetNonNullAttribute(XmlAttributes.Source);
                           
                            r.Read();
                            r.AssertNode(XmlElements.Emotions);
                                r.Read();
                                r.AssertNode(XmlElements.Happy);
                                settings.Happy.Fps = r.GetNonNullAttribute(XmlAttributes.FPS);
                                settings.Happy.Duration = r.GetNonNullAttribute(XmlAttributes.Duration);
                                settings.Happy.AlphaBegin = r.GetNonNullAttribute(XmlAttributes.AlphaBegin);
                                settings.Happy.AlphaFinish = r.GetNonNullAttribute(XmlAttributes.AlphaFinish);
                                settings.Happy.ScaleBegin = r.GetNonNullAttribute(XmlAttributes.ScaleBegin);
                                settings.Happy.ScaleFinish = r.GetNonNullAttribute(XmlAttributes.ScaleFinish);
                                settings.Happy.YFinish = r.GetNonNullAttribute(XmlAttributes.YFinish);
                                
                                r.Read();
                                r.AssertNode(XmlElements.Sad);
                                settings.Sad.Fps = r.GetNonNullAttribute(XmlAttributes.FPS);
                                settings.Sad.Duration = r.GetNonNullAttribute(XmlAttributes.Duration);
                                settings.Sad.AlphaBegin = r.GetNonNullAttribute(XmlAttributes.AlphaBegin);
                                settings.Sad.AlphaFinish = r.GetNonNullAttribute(XmlAttributes.AlphaFinish);
                                settings.Sad.ScaleBegin = r.GetNonNullAttribute(XmlAttributes.ScaleBegin);
                                settings.Sad.ScaleFinish = r.GetNonNullAttribute(XmlAttributes.ScaleFinish);
                                settings.Sad.YFinish = r.GetNonNullAttribute(XmlAttributes.YFinish);
                                
                                r.Read();
                                r.AssertNode(XmlElements.Fear);
                                settings.Fear.Fps = r.GetNonNullAttribute(XmlAttributes.FPS);
                                settings.Fear.Duration = r.GetNonNullAttribute(XmlAttributes.Duration);
                                settings.Fear.ScaleBegin = r.GetNonNullAttribute(XmlAttributes.ScaleBegin);
                                settings.Fear.ScaleFinish = r.GetNonNullAttribute(XmlAttributes.ScaleFinish);
                                settings.Fear.VibrateX = r.GetNonNullAttribute(XmlAttributes.VibrateX);
                                settings.Fear.VibrateY = r.GetNonNullAttribute(XmlAttributes.VibrateY);
                                
                                r.Read();
                                r.AssertNode(XmlElements.Anger);
                                settings.Anger.Fps = r.GetNonNullAttribute(XmlAttributes.FPS);
                                settings.Anger.Duration = r.GetNonNullAttribute(XmlAttributes.Duration);
                                settings.Anger.ScaleBegin = r.GetNonNullAttribute(XmlAttributes.ScaleBegin);
                                settings.Anger.ScaleFinish = r.GetNonNullAttribute(XmlAttributes.ScaleFinish);
                                settings.Anger.VibrateX = r.GetNonNullAttribute(XmlAttributes.VibrateX);
                                settings.Anger.VibrateY = r.GetNonNullAttribute(XmlAttributes.VibrateY);
                            break;
                        case XmlElements.Speakers: break; //Do Nothing
                        case XmlElements.Speaker:
                            r.AssertNode(XmlElements.Speaker);
                            string name = r.GetNonNullAttribute(XmlAttributes.Name);
                            Speaker s = new Speaker(name);
                            
                            r.Read();
                            r.AssertNode(XmlElements.Background);
                            s.Bg.Visible = Convert.ToBoolean(r.GetNonNullAttribute(XmlAttributes.Visible));
                            s.Bg.Alpha = r.GetDoubleAttribute(XmlAttributes.Alpha);
                            s.Bg.Colour = r.GetNonNullAttribute(XmlAttributes.Colour);
                           
                            r.Read();
                            r.AssertNode(XmlElements.Font);
                            s.Font.Family = r.GetNonNullAttribute(XmlAttributes.Name);
                            s.Font.Size = r.GetIntAttribute(XmlAttributes.Size);
                            s.Font.Colour = r.GetNonNullAttribute(XmlAttributes.Colour);
                            s.Font.Bold = r.GetIntAttribute(XmlAttributes.Bold);
                            r.ReadStartElement(XmlElements.Font);
                                    
                            //Add to speakerSet
                            speakerSet[s.Name] = s;
                            break;
                        case XmlElements.Captions: break; //Do Nothing
                        case XmlElements.Caption:
                            r.AssertNode(XmlElements.Caption);
                            EditorCaption c = new EditorCaption
                            {
                                Begin = r.GetNonNullAttribute(XmlAttributes.Begin),
                                End = r.GetNonNullAttribute(XmlAttributes.End),
                                Speaker = speakerSet[r.GetNonNullAttribute(XmlAttributes.Speaker)],
                                Location = (ScreenLocation) r.GetIntAttribute(XmlAttributes.Location),
                                Alignment = (Alignment) r.GetIntAttribute(XmlAttributes.Align)
                            };

                            List<EditorCaptionWord> wordList = new List<EditorCaptionWord>();

                            while (r.Read())
                            {
                                //If the Node is an end element, then the reader has parsed
                                //through all of this caption's words.
                                if (r.NodeType == XmlNodeType.EndElement && r.Name.Equals(XmlElements.Caption))
                                    break;
                                else if (r.NodeType == XmlNodeType.Element && r.Name.Equals(XmlElements.Word))
                                {
                                    r.AssertNode(XmlElements.Word); //Doublecheck, it's the only way to be sure.

                                    Emotion e = (Emotion)r.GetIntAttribute(XmlAttributes.Emotion);
                                    Intensity i = (Intensity)r.GetIntAttribute(XmlAttributes.Intensity);

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
