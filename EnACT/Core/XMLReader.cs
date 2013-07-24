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

            //Construct the project with given info and return it
            project = new ProjectInfo(name, videoPath, projectPath);
            return project;
        }
    }
}
