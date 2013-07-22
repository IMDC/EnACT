using System;
using System.Collections.Generic;
namespace EnACT
{
    /// <summary>
    /// Contains all the settings, values and options related to an EnACT editor proejct
    /// </summary>
    public class ProjectInfo
    {
        #region Fields and Properties
        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<String, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<EditorCaption> CaptionList { set; get; }

        /// <summary>
        /// The object that represents the EnACT engine xml settings file
        /// </summary>
        public SettingsXML Settings { set; get; }

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
        #endregion Fields and Properties

        #region Constructor
        /// <summary>
        /// Constructs a Project WITHOUT a given script path.
        /// </summary>
        /// <param name="name">Name of the project.</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path of this project on the computer.</param>
        public ProjectInfo(string name, string videoPath, string projectPath)
            : this(name: name, scriptPath: null, useExistingScript: false, videoPath: videoPath,
            projectPath: projectPath) { }

        /// <summary>
        /// Constructs a Project WITH a given script path.
        /// </summary>
        /// <param name="name">Name of the project.</param>
        /// <param name="scriptPath">Path of the script to use.</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path of this project on the computer.</param>
        public ProjectInfo(string name, string scriptPath, string videoPath, string projectPath)
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
        private ProjectInfo(string name, string scriptPath, bool useExistingScript, string videoPath, string projectPath)
        {
            this.Name = name;
            this.ScriptPath = scriptPath;
            this.UseExistingScript = useExistingScript;
            this.VideoPath = videoPath;
            this.ProjectPath = projectPath;

            //Construct Core data structures
            this.SpeakerSet  = Utilities.ConstructSpeakerSet();
            this.CaptionList = Utilities.ConstructCaptionList();
            this.Settings    = Utilities.ConstructSettingsXML();
        }
        #endregion Constructor
    }
}