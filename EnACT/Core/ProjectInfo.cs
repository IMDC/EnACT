using System.Collections.Generic;
using EnACT.Miscellaneous;
using LibEnACT;

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
        public const string EngineXmlExtension = ".enact";

        /// <summary>
        /// The name of the engine file
        /// </summary>
        public const string EngineFileName = "Engine.swf";
        /// <summary>
        /// The name of the engine file meant for the editor
        /// </summary>
        public const string EditorEngineFileName = "EditorEngine.swf";
        /// <summary>
        /// The name of the skin file required by Engine.swf
        /// </summary>
        public const string EngineSkinName = "SkinOverPlayFullscreen.swf";

        public const string SpeakersFileName = "speakers.xml";
        public const string SettingsFileName = "Settings.xml";
        public const string CaptionsFileName = "dialogues.xml";
        #endregion

        #region Fields and Properties
        /// <summary>
        /// A set of Speaker objects, each speaker being mapped to by its name
        /// </summary>
        public Dictionary<string, Speaker> SpeakerSet { set; get; }

        /// <summary>
        /// A list of captions retrieved from a transcript file.
        /// </summary>
        public List<Caption> CaptionList { set; get; }

        /// <summary>
        /// The object that represents the EnACT engine xml settings file
        /// </summary>
        public SettingsXml Settings { set; get; }

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
        public string ExternalScriptPath { set; get; }

        /// <summary>
        /// The path of the video supplied by the user.
        /// </summary>
        public string ExternalVideoPath { set; get; }

        /// <summary>
        /// The path of this project directory on the computer.
        /// </summary>
        public string DirectoryPath { set; get; }

        /// <summary>
        /// The captions file used by this project.
        /// </summary>
        public ProjectFile CaptionsFile { private set; get; }

        /// <summary>
        /// The captions file used by this project.
        /// </summary>
        public ProjectFile EditorEngineFile { private set; get; }

        /// <summary>
        /// The engine file used by this project.
        /// </summary>
        public ProjectFile EngineFile { private set; get; }

        /// <summary>
        /// The engine Skin File file used by this project.
        /// </summary>
        public ProjectFile EngineSkinFile { private set; get; }

        /// <summary>
        /// The project file used by this project.
        /// </summary>
        public ProjectFile ProjectFile { private set; get; }

        /// <summary>
        /// The settings file used by this project.
        /// </summary>
        public ProjectFile SettingsFile { private set; get; }

        /// <summary>
        /// The speakers file used by this project.
        /// </summary>
        public ProjectFile SpeakersFile { private set; get; }

        /// <summary>
        /// The unifiedxml file used by this project.
        /// </summary>
        public ProjectFile UnifiedXmlFile { private set; get; }

        /// <summary>
        /// The video file used by this project.
        /// </summary>
        public ProjectFile VideoFile { private set; get; }
        #endregion Fields and Properties

        #region Constructor
        /// <summary>
        /// Constructs a Project WITHOUT a given script path.
        /// </summary>
        /// <param name="name">Name of the project and directory.</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path that the project directory will be placed in.</param>
        public ProjectInfo(string name, string videoPath, string projectPath)
            : this(name, null, false, videoPath, projectPath) { }

        /// <summary>
        /// Constructs a Project WITH a given script path.
        /// </summary>
        /// <param name="name">Name of the project and directory.</param>
        /// <param name="externalScriptPath">Path of the script to use.</param>
        /// <param name="videoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path that the project directory will be placed in.</param>
        public ProjectInfo(string name, string externalScriptPath, string videoPath, string projectPath)
            : this(name, externalScriptPath, true, videoPath, projectPath) { }

        /// <summary>
        /// Constructs a Project with the specified parameters.
        /// </summary>
        /// <param name="name">Name of the project and directory.</param>
        /// <param name="externalScriptPath">Path of the script to use.</param>
        /// <param name="useExistingScript">Whether or not a script has been supplied</param>
        /// <param name="externalVideoPath">The path of the video supplied by the user.</param>
        /// <param name="projectPath">The path that the project directory will be placed in.</param>
        private ProjectInfo(string name, string externalScriptPath, bool useExistingScript, string externalVideoPath, string projectPath)
        {
            this.Name = name;
            this.ExternalScriptPath = externalScriptPath;
            this.UseExistingScript = useExistingScript;
            this.ExternalVideoPath = externalVideoPath;
            this.DirectoryPath = projectPath;

            //Create project files
            CaptionsFile = new ProjectFile(DirectoryPath,CaptionsFileName);
            EditorEngineFile = new ProjectFile(DirectoryPath, EditorEngineFileName);
            EngineFile = new ProjectFile(DirectoryPath, EngineFileName);
            EngineSkinFile = new ProjectFile(DirectoryPath, EngineSkinName);
            ProjectFile = new ProjectFile(DirectoryPath, Name + ProjectExtension);
            SettingsFile = new ProjectFile(DirectoryPath, SettingsFileName);
            SpeakersFile = new ProjectFile(DirectoryPath, SpeakersFileName);
            UnifiedXmlFile = new ProjectFile(DirectoryPath, "engine" + ProjectInfo.EngineXmlExtension);
            VideoFile = new ProjectFile(DirectoryPath, "video.flv");

            //Construct Core data structures
            this.SpeakerSet  = Utilities.ConstructSpeakerSet();
            this.CaptionList = Utilities.ConstructCaptionList();
            this.Settings    = Utilities.ConstructSettingsXml();
        }
        #endregion Constructor
    }
}