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
        #region
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

            project.Settings = ParseSettings(Path.Combine(projectPath, settings));

            return project;
        }
        #endregion

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
                    //r.ReadEndElement();

                    r.ReadStartElement("meta");
                    r.GetAttribute("name");
                    settings.Spacing = r.GetAttribute("content");
                    //r.ReadEndElement();

                    r.ReadStartElement("meta");
                    r.GetAttribute("name");
                    settings.SeparateEmotionWords = r.GetAttribute("content");
                    //r.ReadEndElement();

                    r.ReadStartElement("playback");
                    settings.Playback.AutoPlay = Convert.ToBoolean(r.GetAttribute("autoPlay"));
                    settings.Playback.AutoRewind = Convert.ToBoolean(r.GetAttribute("autoRewind"));
                    settings.Playback.Seek = r.GetAttribute("seek");
                    settings.Playback.AutoSize = Convert.ToBoolean(r.GetAttribute("autoSize"));
                    settings.Playback.Scale = Convert.ToInt32(r.GetAttribute("scale"));
                    settings.Playback.Volume = Convert.ToInt32(r.GetAttribute("volume"));
                    settings.Playback.ShowCaptions = Convert.ToBoolean(r.GetAttribute("showCaptions"));
                    //r.ReadEndElement();

                    r.ReadStartElement("skin");
                    settings.Skin.Source = r.GetAttribute("src");
                    settings.Skin.AutoHide = Convert.ToBoolean(r.GetAttribute("skinAutoHide"));
                    settings.Skin.FadeTime = Convert.ToInt32(r.GetAttribute("skinFadeTime"));
                    settings.Skin.BackGroundAlpha = Convert.ToInt32(r.GetAttribute("skinBackgroundAlpha"));
                    settings.Skin.BackgroundColour = r.GetAttribute("skinBackgroundColor");
                    //r.ReadEndElement();

                    r.ReadStartElement("content");
                    {
                        r.ReadStartElement("speakers");
                        settings.SpeakersSource = r.GetAttribute("src");
                        //r.ReadEndElement();

                        r.ReadStartElement("captions");
                        settings.CaptionsSource = r.GetAttribute("src");
                        //r.ReadEndElement();

                        r.ReadStartElement("video");
                        settings.VideoSource = r.GetAttribute("src");
                        //r.ReadEndElement();
                    }
                    r.ReadEndElement();

                    //These can be left alone for now as the values should stay as the default.
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
    }//Class
}//Namepace
