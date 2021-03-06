﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using EnACT.Miscellaneous;
using LibEnACT;

namespace EnACT.Core
{
    /// <summary>
    /// Contains all the methods required for writing an EnACT project to
    /// XML files.
    /// </summary>
    public static class EnactXMLWriter
    {
        #region WriteSpeakers
        /// <summary>
        /// Writes the object referenced by SpeakerSet to an XML file at the path given by Speakerspath
        /// </summary>
        /// <param name="speakerSet">The SpeakersSet object to write to a file.</param>
        /// <param name="speakersPath">The full path (file name and extension included) to write 
        /// the SpeakersSet object to.</param>
        public static void WriteSpeakers(Dictionary<string, Speaker> speakerSet, string speakersPath)
        {
            //NOTE: XmlTextWriter does not require curly braces between writeStartElement and 
            //writeCloseElement. It is put there to easily see what belongs to what node.

            //Return if nothing to write
            if (speakerSet.Count == 0)
            {
                //TODO throw error?
                Console.WriteLine("Error: SpeakersList is empty");
                return;
            }

            //speakers.xml
            using (XmlTextWriter w = new XmlTextWriter(speakersPath, Encoding.UTF8))
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
                    foreach (Speaker s in speakerSet.Values)
                    {
                        w.WriteStartElement("speaker");
                        {
                            w.WriteAttributeString("name", s.Name);

                            w.WriteStartElement("background");
                            {
                                //visible is true if the background colour alpha is 0
                                bool visible = (s.Font.BackgroundColour.A != 0x00);
                                w.WriteAttributeString("visible", Convert.ToString(visible.GetHashCode()));

                                //Alpha is represented in the xml as a double value between 0 and 1
                                double alpha = s.Font.BackgroundColour.A/(double) 0xFF;
                                w.WriteAttributeString("alpha", Convert.ToString(alpha));

                                //Create a string form of the background colour
                                w.WriteAttributeString("colour", s.Font.BackgroundColour.ToRGBHexString());
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("font");
                            {
                                w.WriteAttributeString("name", s.Font.Family);
                                w.WriteAttributeString("size", Convert.ToString(s.Font.Size));
                                w.WriteAttributeString("colour", s.Font.ForegroundColour.ToRGBHexString());
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
        #endregion

        #region WriteCaptions
        /// <summary>
        /// Writes the object referenced by CaptionsList to an XML file at the path given by Captionspath
        /// </summary>
        /// <param name="captionList">The List of Caption objects to write to a file.</param>
        /// <param name="captionsPath">The full path (file name and extension included) to write 
        /// the CaptionList to.</param>
        public static void WriteCaptions(List<Caption> captionList, string captionsPath)
        {
            //NOTE: XmlTextWriter does not require curly braces between writeStartElement and 
            //writeCloseElement. It is put there to easily see what belongs to what node.

            //Return if nothing to write
            if (captionList.Count == 0)
            {
                //TODO throw error?
                Console.WriteLine("Error: CaptionList is empty");
                return;
            }

            //dialogues.xml
            using (XmlTextWriter w = new XmlTextWriter(captionsPath, Encoding.UTF8))
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
                    foreach (Caption c in captionList)
                    {
                        w.WriteStartElement("caption");
                        {
                            w.WriteAttributeString("begin", c.Begin.AsString);
                            w.WriteAttributeString("end", c.End.AsString);
                            w.WriteAttributeString("speaker", c.Speaker.Name);
                            //Both location and alignment refer to hashcode
                            w.WriteAttributeString("location", Convert.ToString(c.Location.GetHashCode()));
                            w.WriteAttributeString("align", Convert.ToString(c.Alignment.GetHashCode()));

                            foreach (CaptionWord cw in c.Words)
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
        #endregion

        #region WriteSettings
        /// <summary>
        /// Writes the object referenced by Settings to an XML file at the path given by Settingsspath
        /// </summary>
        /// <param name="settings">The Settings object to write to a Text File.</param>
        /// <param name="settingsPath">The full path (file name and extension included) to write 
        /// the Settings object to.</param>
        public static void WriteSettings(SettingsXml settings, string settingsPath)
        {
            //NOTE: XmlTextWriter does not require curly braces between writeStartElement and 
            //writeCloseElement. It is put there to easily see what belongs to what node.

            //Settings.xml
            using (XmlTextWriter w = new XmlTextWriter(settingsPath, Encoding.UTF8))
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
                        w.WriteAttributeString("content", settings.Spacing);
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("meta");
                    {
                        w.WriteAttributeString("name", "separate-emotion-words");
                        w.WriteAttributeString("content", settings.SeparateEmotionWords);
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("playback");
                    {
                        w.WriteAttributeString("autoPlay", Convert.ToString(settings.Playback.AutoPlay).ToLower());
                        w.WriteAttributeString("autoRewind", Convert.ToString(settings.Playback.AutoRewind).ToLower());
                        w.WriteAttributeString("seek", settings.Playback.Seek);
                        w.WriteAttributeString("autoSize", Convert.ToString(settings.Playback.AutoSize).ToLower());
                        w.WriteAttributeString("scale", Convert.ToString(settings.Playback.Scale));
                        w.WriteAttributeString("volume", Convert.ToString(settings.Playback.Volume));
                        w.WriteAttributeString("showCaptions", Convert.ToString(settings.Playback.ShowCaptions)
                            .ToLower());
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("skin");
                    {
                        w.WriteAttributeString("src", settings.Skin.Source);
                        w.WriteAttributeString("skinAutoHide", Convert.ToString(settings.Skin.AutoHide).ToLower());
                        w.WriteAttributeString("skinFadeTime", Convert.ToString(settings.Skin.FadeTime));
                        w.WriteAttributeString("skinBackgroundAlpha", Convert.ToString(settings.Skin.BackGroundAlpha));
                        w.WriteAttributeString("skinBackgroundColour", settings.Skin.BackgroundColour);
                    }
                    w.WriteEndElement();

                    w.WriteStartElement("content");
                    {
                        w.WriteStartElement("speakers");
                        {
                            w.WriteAttributeString("src", settings.SpeakersSource);
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("captions");
                        {
                            w.WriteAttributeString("src", settings.CaptionsSource);
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("video");
                        {
                            w.WriteAttributeString("src", settings.VideoSource);
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
                                w.WriteAttributeString("value", settings.Happy.Fps);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "dur");
                                w.WriteAttributeString("value", settings.Happy.Duration);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "alphaBegin");
                                w.WriteAttributeString("value", settings.Happy.AlphaBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "alphaFinish");
                                w.WriteAttributeString("value", settings.Happy.AlphaFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleBegin");
                                w.WriteAttributeString("value", settings.Happy.ScaleBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleFinish");
                                w.WriteAttributeString("value", settings.Happy.ScaleFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "yFinish");
                                w.WriteAttributeString("value", settings.Happy.YFinish);
                            }
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("sad");
                        {
                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "fps");
                                w.WriteAttributeString("value", settings.Sad.Fps);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "dur");
                                w.WriteAttributeString("value", settings.Sad.Duration);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "alphaBegin");
                                w.WriteAttributeString("value", settings.Sad.AlphaBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "alphaFinish");
                                w.WriteAttributeString("value", settings.Sad.AlphaFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleBegin");
                                w.WriteAttributeString("value", settings.Sad.ScaleBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleFinish");
                                w.WriteAttributeString("value", settings.Sad.ScaleFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "yFinish");
                                w.WriteAttributeString("value", settings.Sad.YFinish);
                            }
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("fear");
                        {
                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "fps");
                                w.WriteAttributeString("value", settings.Fear.Fps);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "dur");
                                w.WriteAttributeString("value", settings.Fear.Duration);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleBegin");
                                w.WriteAttributeString("value", settings.Fear.ScaleBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleFinish");
                                w.WriteAttributeString("value", settings.Fear.ScaleFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "vibrateX");
                                w.WriteAttributeString("value", settings.Fear.VibrateX);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "vibrateY");
                                w.WriteAttributeString("value", settings.Fear.VibrateY);
                            }
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();

                        w.WriteStartElement("anger");
                        {
                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "fps");
                                w.WriteAttributeString("value", settings.Anger.Fps);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "dur");
                                w.WriteAttributeString("value", settings.Anger.Duration);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleBegin");
                                w.WriteAttributeString("value", settings.Anger.ScaleBegin);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "scaleFinish");
                                w.WriteAttributeString("value", settings.Anger.ScaleFinish);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "vibrateX");
                                w.WriteAttributeString("value", settings.Anger.VibrateX);
                            }
                            w.WriteEndElement();

                            w.WriteStartElement("param");
                            {
                                w.WriteAttributeString("name", "vibrateY");
                                w.WriteAttributeString("value", settings.Anger.VibrateY);
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
        #endregion

        #region WriteProject
        /// <summary>
        /// Writes a Project object to a file. The location and name of the file is set as 
        /// DirectoryPath/Name.xml
        /// </summary>
        /// <param name="project">The Project object to write.</param>
        public static void WriteProject(ProjectInfo project)
        {
            using (XmlTextWriter w = new XmlTextWriter(Path.Combine(project.DirectoryPath, 
                project.Name + ProjectInfo.ProjectExtension), Encoding.UTF8))
            {
                //Set formatting so that the file will use newlines and 4-spaced indents
                w.Formatting = Formatting.Indented;
                w.IndentChar = '\t';
                w.Indentation = 1;

                w.WriteStartDocument();
                w.WriteStartElement("project");
                {
                    w.WriteStartElement("name");
                    w.WriteString(project.Name); 
                    w.WriteEndElement();

                    w.WriteStartElement("video");
                    w.WriteString(project.ExternalVideoPath);
                    w.WriteEndElement();

                    w.WriteStartElement("settings");
                    w.WriteString("Settings.xml");
                    w.WriteEndElement();

                    w.WriteStartElement("speakers");
                    w.WriteString("speakers.xml");
                    w.WriteEndElement();

                    w.WriteStartElement("dialogues");
                    w.WriteString("dialogues.xml");
                    w.WriteEndElement();
                }
                w.WriteEndElement();

                w.WriteEndDocument();
            }
        }
        #endregion

        #region WriteUnified
        /// <summary>
        /// Writes a unified engine XML file containing the settings, speakers and captions for the
        /// engine.
        /// </summary>
        /// <param name="project">The project that contains the information to write.</param>
        /// <param name="path">The path (including name and extension) to write the file to.</param>
        public static void WriteEngineXml(ProjectInfo project, string path)
        { WriteEngineXml(project.SpeakerSet, project.CaptionList, project.Settings, path); }

        /// <summary>
        /// Writes a unified engine XML file containing the settings, speakers and captions for the
        /// engine.
        /// </summary>
        /// <param name="speakerSet">The speakers to write to file.</param>
        /// <param name="captionList">The Caption list to write to file.</param>
        /// <param name="settings">The SettingsXML object to write to file.</param>
        /// <param name="path">The path (including name and extension) to write the file to.</param>
        public static void WriteEngineXml(Dictionary<string, Speaker> speakerSet, 
            List<Caption> captionList, SettingsXml settings, string path)
        {
            using (XmlTextWriter w = new XmlTextWriter(path, Encoding.UTF8))
            {
                //Set formatting so that the file will use newlines and 4-spaced indents
                w.Formatting = Formatting.Indented;
                w.IndentChar = ' ';
                w.Indentation = 4;

                //Start Document
                w.WriteStartDocument();

                w.WriteStartElement(XmlElements.Enact);
                    w.WriteStartElement(XmlElements.Settings);
                        w.WriteStartElement(XmlElements.Meta);
                        w.WriteAttributeString(XmlAttributes.Base, settings.Base);
                        w.WriteAttributeString(XmlAttributes.WordSpacing, settings.Spacing);
                        w.WriteAttributeString(XmlAttributes.SeparateEmotionWords, settings.SeparateEmotionWords);
                        w.WriteEndElement();

                        w.WriteStartElement(XmlElements.Playback);
                        w.WriteAttributeString(XmlAttributes.AutoPlay, settings.Playback.AutoPlay.ToLowerString());
                        w.WriteAttributeString(XmlAttributes.AutoRewind, settings.Playback.AutoRewind.ToLowerString());
                        w.WriteAttributeString(XmlAttributes.Seek, settings.Playback.Seek);
                        w.WriteAttributeString(XmlAttributes.AutoSize, settings.Playback.AutoSize.ToLowerString());
                        w.WriteAttributeString(XmlAttributes.Scale, settings.Playback.Scale.ToString());
                        w.WriteAttributeString(XmlAttributes.Volume, settings.Playback.Volume.ToString());
                        w.WriteAttributeString(XmlAttributes.ShowCaptions, 
                            settings.Playback.ShowCaptions.ToLowerString());
                        w.WriteEndElement();

                        w.WriteStartElement(XmlElements.Skin);
                        w.WriteAttributeString(XmlAttributes.Source, settings.Skin.Source);
                        w.WriteAttributeString(XmlAttributes.AutoHide, settings.Skin.AutoHide.ToLowerString());
                        w.WriteAttributeString(XmlAttributes.FadeTime, settings.Skin.FadeTime.ToString());
                        w.WriteAttributeString(XmlAttributes.BackgroundAlpha, settings.Skin.BackGroundAlpha.ToString());
                        w.WriteAttributeString(XmlAttributes.BackgroundColour, settings.Skin.BackgroundColour);
                        w.WriteEndElement();

                        w.WriteStartElement(XmlElements.Video);
                        w.WriteAttributeString(XmlAttributes.Source, settings.VideoSource);
                        w.WriteEndElement();

                        w.WriteStartElement(XmlElements.Emotions);
                            w.WriteStartElement(XmlElements.Happy);
                            w.WriteAttributeString(XmlAttributes.FPS, settings.Happy.Fps);
                            w.WriteAttributeString(XmlAttributes.Duration, settings.Happy.Duration);
                            w.WriteAttributeString(XmlAttributes.AlphaBegin, settings.Happy.AlphaBegin);
                            w.WriteAttributeString(XmlAttributes.AlphaFinish, settings.Happy.AlphaFinish);
                            w.WriteAttributeString(XmlAttributes.ScaleBegin, settings.Happy.ScaleBegin);
                            w.WriteAttributeString(XmlAttributes.ScaleFinish, settings.Happy.ScaleFinish);
                            w.WriteAttributeString(XmlAttributes.YFinish, settings.Happy.YFinish);
                            w.WriteEndElement();

                            w.WriteStartElement(XmlElements.Sad);
                            w.WriteAttributeString(XmlAttributes.FPS, settings.Sad.Fps);
                            w.WriteAttributeString(XmlAttributes.Duration, settings.Sad.Duration);
                            w.WriteAttributeString(XmlAttributes.AlphaBegin, settings.Sad.AlphaBegin);
                            w.WriteAttributeString(XmlAttributes.AlphaFinish, settings.Sad.AlphaFinish);
                            w.WriteAttributeString(XmlAttributes.ScaleBegin, settings.Sad.ScaleBegin);
                            w.WriteAttributeString(XmlAttributes.ScaleFinish, settings.Sad.ScaleFinish);
                            w.WriteAttributeString(XmlAttributes.YFinish, settings.Sad.YFinish);
                            w.WriteEndElement();

                            w.WriteStartElement(XmlElements.Fear);
                            w.WriteAttributeString(XmlAttributes.FPS, settings.Fear.Fps);
                            w.WriteAttributeString(XmlAttributes.Duration, settings.Fear.Duration);
                            w.WriteAttributeString(XmlAttributes.ScaleBegin, settings.Fear.ScaleBegin);
                            w.WriteAttributeString(XmlAttributes.ScaleFinish, settings.Fear.ScaleFinish);
                            w.WriteAttributeString(XmlAttributes.VibrateX, settings.Fear.VibrateX);
                            w.WriteAttributeString(XmlAttributes.VibrateY, settings.Fear.VibrateY);
                            w.WriteEndElement();

                            w.WriteStartElement(XmlElements.Anger);
                            w.WriteAttributeString(XmlAttributes.FPS, settings.Anger.Fps);
                            w.WriteAttributeString(XmlAttributes.Duration, settings.Anger.Duration);
                            w.WriteAttributeString(XmlAttributes.ScaleBegin, settings.Anger.ScaleBegin);
                            w.WriteAttributeString(XmlAttributes.ScaleFinish, settings.Anger.ScaleFinish);
                            w.WriteAttributeString(XmlAttributes.VibrateX, settings.Anger.VibrateX);
                            w.WriteAttributeString(XmlAttributes.VibrateY, settings.Anger.VibrateY);
                            w.WriteEndElement();
                        w.WriteEndElement();//Emotions
                    w.WriteEndElement();//Settings

                    w.WriteStartElement(XmlElements.Speakers);
                        foreach (Speaker s in speakerSet.Values)
                        {
                            w.WriteStartElement(XmlElements.Speaker);
                            {
                                w.WriteAttributeString(XmlAttributes.Name, s.Name);

                                w.WriteStartElement(XmlElements.Background);

                                //visible is true if the background colour alpha is 0
                                bool visible = (s.Font.BackgroundColour.A != 0x00);
                                w.WriteAttributeString(XmlAttributes.Visible, visible.ToLowerString());

                                //Alpha is represented in the xml as a double value between 0 and 1
                                double alpha = s.Font.BackgroundColour.A / (double)0xFF;
                                w.WriteAttributeString(XmlAttributes.Alpha, alpha.ToString());

                                //Create a string form of the background colour
                                w.WriteAttributeString(XmlAttributes.Colour, s.Font.BackgroundColour.ToRGBHexString());
                                w.WriteEndElement();

                                w.WriteStartElement(XmlElements.Font);
                                w.WriteAttributeString(XmlAttributes.Name, s.Font.Family);
                                w.WriteAttributeString(XmlAttributes.Size, s.Font.Size.ToString());
                                w.WriteAttributeString(XmlAttributes.Colour, s.Font.ForegroundColour.ToRGBHexString());
                                w.WriteAttributeString(XmlAttributes.Bold, s.Font.Bold.ToString());
                                w.WriteEndElement();
                            }
                            w.WriteEndElement();//Speaker
                        }
                    w.WriteEndElement();//Speakers

                    w.WriteStartElement(XmlElements.Captions);
                        foreach (Caption c in captionList)
                        {
                            w.WriteStartElement(XmlElements.Caption);
                            {
                                w.WriteAttributeString(XmlAttributes.Begin, c.Begin);
                                w.WriteAttributeString(XmlAttributes.End, c.End);
                                w.WriteAttributeString(XmlAttributes.Speaker, c.Speaker.Name);
                                w.WriteAttributeString(XmlAttributes.Location, c.Location.GetHashCode().ToString());
                                w.WriteAttributeString(XmlAttributes.Align, c.Alignment.GetHashCode().ToString());

                                foreach (CaptionWord cw in c.Words)
                                {
                                    w.WriteStartElement(XmlElements.Word);
                                    w.WriteAttributeString(XmlAttributes.Emotion, cw.Emotion.GetHashCode().ToString());
                                    w.WriteAttributeString(XmlAttributes.Intensity, 
                                        cw.Intensity.GetHashCode().ToString());
                                    w.WriteString(cw.Text);
                                    w.WriteEndElement();
                                }
                            }
                            w.WriteEndElement();//Caption
                        }
                    w.WriteEndElement();//Captions
                w.WriteEndElement();//Enact

                w.WriteEndDocument();
            }
        }
        #endregion
    }//Class
}//Namespace
