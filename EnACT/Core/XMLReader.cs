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

        /// <summary>
        /// Reads in an engine.xml file and turns it into a CaptionList, SpeakerSet and engine 
        /// Settings.
        /// </summary>
        /// <param name="path">The Path to the engine.xml file.</param>
        /// <returns>A 3-Tuple containing a CaptionList, a SpeakerSet, and a Settings 
        /// object in that specific order.</returns>
        public static Tuple<List<EditorCaption>, Dictionary<String, Speaker>, SettingsXML> ParseEngineXML(string path)
        {
            var captionList = new List<EditorCaption>();
            var speakerSet = new Dictionary<String, Speaker>();
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
                            AssertNode(r, XMLElements.Meta);
                            settings.Base = r[XMLAttributes.Base];
                            settings.Spacing = r[XMLAttributes.WordSpacing];
                            settings.SeparateEmotionWords = r[XMLAttributes.SeparateEmotionWords];

                            r.Read();
                            AssertNode(r, XMLElements.Playback);
                            settings.Playback.AutoPlay = Convert.ToBoolean(r[XMLAttributes.AutoPlay]);
                            settings.Playback.AutoRewind = Convert.ToBoolean(r[XMLAttributes.AutoRewind]);
                            settings.Playback.Seek = r[XMLAttributes.Seek];
                            settings.Playback.AutoSize = Convert.ToBoolean(r[XMLAttributes.AutoSize]);
                            settings.Playback.Scale = Convert.ToInt32(r[XMLAttributes.Scale]);
                            settings.Playback.Volume = Convert.ToInt32(r[XMLAttributes.Volume]);
                            settings.Playback.ShowCaptions = Convert.ToBoolean(r[XMLAttributes.ShowCaptions]);

                            r.Read();
                            AssertNode(r, XMLElements.Skin);
                            settings.Skin.Source = r[XMLAttributes.Source];
                            settings.Skin.AutoHide = Convert.ToBoolean(r[XMLAttributes.AutoHide]);
                            settings.Skin.FadeTime = Convert.ToInt32(r[XMLAttributes.FadeTime]);
                            settings.Skin.BackGroundAlpha = Convert.ToInt32(r[XMLAttributes.BackgroundAlpha]);
                            settings.Skin.BackgroundColour = r[XMLAttributes.BackgroundColour];
                            
                            r.Read();
                            AssertNode(r, XMLElements.Video);
                            settings.VideoSource = r[XMLAttributes.Source];
                           
                            r.Read();
                            AssertNode(r, XMLElements.Emotions);
                                r.Read();
                                AssertNode(r, XMLElements.Happy);
                                settings.Happy.Fps = r[XMLAttributes.FPS];
                                settings.Happy.Duration = r[XMLAttributes.Duration];
                                settings.Happy.AlphaBegin = r[XMLAttributes.AlphaBegin];
                                settings.Happy.AlphaFinish = r[XMLAttributes.AlphaFinish];
                                settings.Happy.ScaleBegin = r[XMLAttributes.ScaleBegin];
                                settings.Happy.ScaleFinish = r[XMLAttributes.ScaleFinish];
                                settings.Happy.YFinish = r[XMLAttributes.YFinish];
                                
                                r.Read();
                                AssertNode(r, XMLElements.Sad);
                                settings.Sad.Fps = r[XMLAttributes.FPS];
                                settings.Sad.Duration = r[XMLAttributes.Duration];
                                settings.Sad.AlphaBegin = r[XMLAttributes.AlphaBegin];
                                settings.Sad.AlphaFinish = r[XMLAttributes.AlphaFinish];
                                settings.Sad.ScaleBegin = r[XMLAttributes.ScaleBegin];
                                settings.Sad.ScaleFinish = r[XMLAttributes.ScaleFinish];
                                settings.Sad.YFinish = r[XMLAttributes.YFinish];
                                
                                r.Read();
                                AssertNode(r, XMLElements.Fear);
                                settings.Fear.Fps = r[XMLAttributes.FPS];
                                settings.Fear.Duration = r[XMLAttributes.Duration];
                                settings.Fear.ScaleBegin = r[XMLAttributes.ScaleBegin];
                                settings.Fear.ScaleFinish = r[XMLAttributes.ScaleFinish];
                                settings.Fear.VibrateX = r[XMLAttributes.VibrateX];
                                settings.Fear.VibrateY = r[XMLAttributes.VibrateY];
                                
                                r.Read();
                                AssertNode(r, XMLElements.Anger);
                                settings.Anger.Fps = r[XMLAttributes.FPS];
                                settings.Anger.Duration = r[XMLAttributes.Duration];
                                settings.Anger.ScaleBegin = r[XMLAttributes.ScaleBegin];
                                settings.Anger.ScaleFinish = r[XMLAttributes.ScaleFinish];
                                settings.Anger.VibrateX = r[XMLAttributes.VibrateX];
                                settings.Anger.VibrateY = r[XMLAttributes.VibrateY];
                            break;
                        case XMLElements.Speakers: break; //Do Nothing
                        case XMLElements.Speaker:
                            AssertNode(r, XMLElements.Speaker);
                            string name = r[XMLAttributes.Name];
                            Speaker s = new Speaker(name);
                            
                            r.Read();
                            AssertNode(r, XMLElements.Background);
                            s.BG.Visible = Convert.ToBoolean(r[XMLAttributes.Visible]);
                            s.BG.Alpha = Convert.ToDouble(r[XMLAttributes.Alpha]);
                            s.BG.Colour = r[XMLAttributes.Colour];
                           
                            r.Read();
                            AssertNode(r, XMLElements.Font);
                            s.Font.Family = r[XMLAttributes.Name];
                            s.Font.Size = Convert.ToInt32(r[XMLAttributes.Size]);
                            s.Font.Colour = r[XMLAttributes.Colour];
                            s.Font.Bold = Convert.ToInt32(r[XMLAttributes.Bold]);
                            r.ReadStartElement(XMLElements.Font);
                                    
                            //Add to speakerSet
                            speakerSet[s.Name] = s;
                            break;
                        case XMLElements.Captions: break; //Do Nothing
                        case XMLElements.Caption:
                            AssertNode(r, XMLElements.Caption);
                            EditorCaption c = new EditorCaption();
                            c.Begin = r[XMLAttributes.Begin];
                            c.End = r[XMLAttributes.End];
                            c.Speaker = speakerSet[r[XMLAttributes.Speaker]];
                            c.Location = (ScreenLocation) Convert.ToInt32(r[XMLAttributes.Location]);
                            c.Alignment = (Alignment) Convert.ToInt32(r[XMLAttributes.Align]);

                            List<EditorCaptionWord> wordList = new List<EditorCaptionWord>();

                            while (r.Read())
                            {
                                //If the Node is an end element, then the reader has parsed
                                //through all of this caption's words.
                                if (r.NodeType == XmlNodeType.EndElement && r.Name.Equals(XMLElements.Caption))
                                    break;

                                else if (r.NodeType == XmlNodeType.Element && r.Name.Equals(XMLElements.Word))
                                {
                                    AssertNode(r, XMLElements.Word); //Doublecheck, it's the only way to be sure.

                                    //Get word from node and add it to the list
                                    EditorCaptionWord word = new EditorCaptionWord(text: r.ReadString());
                                    word.Emotion = (Emotion)Convert.ToInt32(r[XMLAttributes.Emotion]);
                                    word.Intensity = (Intensity)Convert.ToInt32(r[XMLAttributes.Intensity]);
                                    c.Words.Add(word);

                                    r.ReadEndElement(); //Read the end of the word node
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

        /// <summary>
        /// Asserts that the current node name in the XmlReader is the same as the expected node 
        /// name. If the two do not match, an ArgumentException is thrown.
        /// </summary>
        /// <param name="r">The XmlReader to with the current node to compare.</param>
        /// <param name="expectedNodeName">The name that the current node is expected to have.</param>
        private static void AssertNode(XmlReader r, string expectedNodeName)
        {
            /* When using XmlReader, trying to access attributes that it can't find will return 
             * null, which can be confusing if you don't realize the current node is not the 
             * correct node.
             */
            if (!r.IsStartElement(expectedNodeName))
                throw new ArgumentException(String.Format("Xml node '{0}' is not the current node.", 
                    expectedNodeName));
        }
    }//Class
}//Namepace
