using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace LibEnACT
{
    /// <summary>
    /// A static class that contains XML reading methods.
    /// </summary>
    public static class XMLReader
    {
        #region ParseEngineXML
        /// <summary>
        /// Reads in an engine.xml file and turns it into a CaptionList, SpeakerSet and engine 
        /// Settings.
        /// </summary>
        /// <param name="path">The Path to the engine.xml file.</param>
        /// <returns>A 3-Tuple containing a CaptionList, a SpeakerSet, and a Settings 
        /// object in that specific order.</returns>
        public static Tuple<List<Caption>, Dictionary<string, Speaker>, SettingsXml> ParseEngineXml(string path)
        {
            var captionList = new List<Caption>();
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
                            Caption c = new Caption
                            {
                                Begin = r.GetNonNullAttribute(XmlAttributes.Begin),
                                End = r.GetNonNullAttribute(XmlAttributes.End),
                                Speaker = speakerSet[r.GetNonNullAttribute(XmlAttributes.Speaker)],
                                Location = (ScreenLocation) r.GetIntAttribute(XmlAttributes.Location),
                                Alignment = (Alignment) r.GetIntAttribute(XmlAttributes.Align)
                            };

                            List<CaptionWord> wordList = new List<CaptionWord>();

                            while (r.Read())
                            {
                                //If the Node is an end element, then the reader has parsed
                                //through all of this caption's words.
                                if (r.NodeType == XmlNodeType.EndElement && r.Name.Equals(XmlElements.Caption))
                                    break;
                                if (r.NodeType == XmlNodeType.Element && r.Name.Equals(XmlElements.Word))
                                {
                                    r.AssertNode(XmlElements.Word); //Doublecheck, it's the only way to be sure.

                                    Emotion e = (Emotion)r.GetIntAttribute(XmlAttributes.Emotion);
                                    Intensity i = (Intensity)r.GetIntAttribute(XmlAttributes.Intensity);

                                    //Get word from node and add it to the list
                                    CaptionWord word = new CaptionWord(e, i, r.ReadString());
                                    c.Words.Add(word);
                                }
                            }
                            captionList.Add(c);
                            break;
                        default: throw new ArgumentException("Value '" + r.Name + "' is not a valid node", r.Name);
                    }
                }//Enact
            }

            return Tuple.Create(captionList,speakerSet,settings);
        }
        #endregion

        #region XmlReader Extention Methods

        /// <summary>
        /// Asserts that the current node name in the XmlReader is the same as the expected node 
        /// name. If the two do not match, an ArgumentException is thrown.
        /// </summary>
        /// <param name="r">The XmlReader to with the current node to compare.</param>
        /// <param name="expectedNodeName">The name that the current node is expected to have.</param>
        public static void AssertNode(this XmlReader r, string expectedNodeName)
        {
            /* When using XmlReader, trying to access attributes that it can't find will return 
             * null, which can be confusing if you don't realize the current node is not the 
             * correct node.
             */
            if (!r.IsStartElement(expectedNodeName))
                throw new ArgumentException(String.Format("Xml node '{0}' is not the current node.",
                    expectedNodeName));
        }

        /// <summary>
        /// Reads in an attribute from the current node and checks to make sure that the returned
        /// string is not null. If the string is null, an ArgumentException is thrown, signifying
        /// that the attribute was not found.
        /// </summary>
        /// <param name="r">The XmlReader being used to read xml.</param>
        /// <param name="attributeName">The name of the attribute to read.</param>
        /// <returns>The attribute string.</returns>
        public static string GetNonNullAttribute(this XmlReader r, string attributeName)
        {
            string attributeString = r[attributeName];
            if (attributeString == null)
                throw new ArgumentException("Attribute " + attributeName + " not found.");
            return attributeString;
        }

        /// <summary>
        /// Reads in an attribute from the current node and checks to make sure that the returned
        /// value is not null. If the value is null, an ArgumentException is thrown, signifying
        /// that the attribute was not found. Returns the attribute as a boolean.
        /// </summary>
        /// <param name="r">The XmlReader being used to read xml.</param>
        /// <param name="attributeName">The name of the attribute to read.</param>
        /// <returns>The attribute as a boolean.</returns>
        public static bool GetBoolAttribute(this XmlReader r, string attributeName)
        {
            return Convert.ToBoolean(r.GetNonNullAttribute(attributeName));
        }

        /// <summary>
        /// Reads in an attribute from the current node and checks to make sure that the returned
        /// value is not null. If the value is null, an ArgumentException is thrown, signifying
        /// that the attribute was not found. Returns the attribute as an integer.
        /// </summary>
        /// <param name="r">The XmlReader being used to read xml.</param>
        /// <param name="attributeName">The name of the attribute to read.</param>
        /// <returns>The attribute as an integer.</returns>
        public static int GetIntAttribute(this XmlReader r, string attributeName)
        {
            return Convert.ToInt32(r.GetNonNullAttribute(attributeName));
        }

        /// <summary>
        /// Reads in an attribute from the current node and checks to make sure that the returned
        /// value is not null. If the value is null, an ArgumentException is thrown, signifying
        /// that the attribute was not found. Returns the attribute as a double.
        /// </summary>
        /// <param name="r">The XmlReader being used to read xml.</param>
        /// <param name="attributeName">The name of the attribute to read.</param>
        /// <returns>The attribute as a double.</returns>
        public static double GetDoubleAttribute(this XmlReader r, string attributeName)
        {
            return Convert.ToDouble(r.GetNonNullAttribute(attributeName));
        }

        #endregion
    }//Class
}//Namepace
