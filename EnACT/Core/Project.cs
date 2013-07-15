using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
{
    /// <summary>
    /// Contains all the settings, values and options related to an EnACT editor proejct
    /// </summary>
    public class Project
    {
        #region Fields and Properties
        /// <summary>
        /// A bool that represents whether or not the user has supplied an existing script to base
        /// the captions off of.
        /// </summary>
        public bool UseExistingScript { private set; get; }

        /// <summary>
        /// The name of the project
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// The path of the script supplied by the user, if given.
        /// </summary>
        public string ScriptPath { set; get; }

        /// <summary>
        /// The path of the video supplied by the user.
        /// </summary>
        public string VideoPath { set; get; }

        /// <summary>
        /// The path of this project on the computer.
        /// </summary>
        public string ProjectPath { set; get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a Project WITHOUT a given script path.
        /// </summary>
        /// <param name="name">Name of the project.</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path of this project on the computer.</param>
        public Project(string name, string videoPath, string projectPath)
            : this(name: name, scriptPath: null, useExistingScript: false, videoPath: videoPath,
            projectPath: projectPath) { }

        /// <summary>
        /// Constructs a Project WITH a given script path.
        /// </summary>
        /// <param name="name">Name of the project.</param>
        /// <param name="scriptPath">Path of the script to use.</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path of this project on the computer.</param>
        public Project(string name, string scriptPath, string videoPath, string projectPath)
            : this(name: name, scriptPath: scriptPath, useExistingScript: true, videoPath: videoPath,
            projectPath: projectPath) { }

        /// <summary>
        /// Constructs a Project with the specified parameters.
        /// </summary>
        /// <param name="name">Name of the project.</param>
        /// <param name="scriptPath">Path of the script to use.</param>
        /// <param name="useExistingScript">Whether or not a script has been supplied</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path of this project on the computer.</param>
        private Project(string name, string scriptPath, bool useExistingScript, string videoPath, string projectPath)
        {
            this.Name = name;
            this.ScriptPath = scriptPath;
            this.UseExistingScript = useExistingScript;
            this.VideoPath = videoPath;
            this.ProjectPath = projectPath;
        }
        #endregion
    }
}
