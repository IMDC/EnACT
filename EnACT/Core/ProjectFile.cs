using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT.Core
{
    /// <summary>
    /// A class used to contain path strings for a specific file in a project.
    /// </summary>
    public class ProjectFile
    {
        /// <summary>
        /// The path of the project file relative to the project directory.
        /// Example is @"engine.enact".
        /// </summary>
        public string RelativePath { get; private set; }

        /// <summary>
        /// The full path of the project file starting from its drive letter.
        /// Example is @"C:\Users\imdc\Documents\Test Projects\qwert\engine.enact".
        /// </summary>
        public string AbsolutePath { get; private set; }

        /// <summary>
        /// Creates a Project file with the given paths.
        /// </summary>
        /// <param name="directoryPath">The absolute path of the project directory.</param>
        /// <param name="relativeFilePath">The path of the file relative to the project 
        /// directory.</param>
        public ProjectFile(string directoryPath, string relativeFilePath)
        {
            this.RelativePath = relativeFilePath;
            this.AbsolutePath = Path.Combine(directoryPath, relativeFilePath);
        }

        /// <summary>
        /// Returns the absolute path of this file as a string. It has the same result as the
        /// AbsolutePath property
        /// </summary>
        /// <returns>The absolute path of file.</returns>
        public override string ToString()
        {
            return AbsolutePath;
        }
    }
}
