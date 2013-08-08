using System.Collections.Generic;
using EnACT.Miscellaneous;

namespace EnACT.Core
{
    /// <summary>
    /// Contains all the settings, values and options related to an EnACT editor proejct
    /// </summary>
    public class ProjectInfo
    {
        #region Constants
        /// <summary>
        /// An empty ProjectInfo object that is meant to represent no current project.
        /// </summary>
        public static readonly ProjectInfo NoProject = new ProjectInfo(string.Empty, string.Empty, string.Empty);

        /// <summary>
        /// The extension for project files.
        /// </summary>
        public const string ProjectExtension = ".enproj";

        /// <summary>
        /// The extension for Engine XML files.
        /// </summary>
        public const string EngineXMLExtension = ".enact";
        #endregion

        #region Fields and Properties
        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<string, Speaker> SpeakerSet { set; get; }

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
        /// The path of this project directory on the computer.
        /// </summary>
        public string DirectoryPath { set; get; }
        #endregion Fields and Properties

        #region Constructor
        /// <summary>
        /// Constructs a Project WITHOUT a given script path.
        /// </summary>
        /// <param name="name">Name of the project and directory.</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path that the project directory will be placed in.</param>
        public ProjectInfo(string name, string videoPath, string projectPath)
            : this(name: name, scriptPath: null, useExistingScript: false, videoPath: videoPath,
            projectPath: projectPath) { }

        /// <summary>
        /// Constructs a Project WITH a given script path.
        /// </summary>
        /// <param name="name">Name of the project and directory.</param>
        /// <param name="scriptPath">Path of the script to use.</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path that the project directory will be placed in.</param>
        public ProjectInfo(string name, string scriptPath, string videoPath, string projectPath)
            : this(name: name, scriptPath: scriptPath, useExistingScript: true, videoPath: videoPath,
            projectPath: projectPath) { }

        /// <summary>
        /// Constructs a Project with the specified parameters.
        /// </summary>
        /// <param name="name">Name of the project and directory.</param>
        /// <param name="scriptPath">Path of the script to use.</param>
        /// <param name="useExistingScript">Whether or not a script has been supplied</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path that the project directory will be placed in.</param>
        private ProjectInfo(string name, string scriptPath, bool useExistingScript, string videoPath, string projectPath)
        {
            this.Name = name;
            this.ScriptPath = scriptPath;
            this.UseExistingScript = useExistingScript;
            this.VideoPath = videoPath;
            this.DirectoryPath = projectPath;

            //Construct Core data structures
            this.SpeakerSet  = Utilities.ConstructSpeakerSet();
            this.CaptionList = Utilities.ConstructCaptionList();
            this.Settings    = Utilities.ConstructSettingsXML();
        }
        #endregion Constructor
    }
}