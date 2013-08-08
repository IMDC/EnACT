using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using EnACT.Miscellaneous;

namespace EnACT.Core
{
    /// <summary>
    /// Contains all methods and fields related to reading in and parsing text.
    /// </summary>
    public static class ScriptParser
    {
        #region Constants
        /// <summary>
        /// Contains the known file extensions for script files.
        /// </summary>
        public static class FileExtensions
        {
            public const string Esr = ".esr";
            public const string Srt = ".srt";
            public const string Txt = ".txt";
        }
        #endregion Constants

        #region Parse
        /// <summary>
        /// Parses a script file when given a path to the script to it.
        /// </summary>
        /// <param name="path">The absolute path to the script file.</param>
        public static Tuple<List<EditorCaption>, Dictionary<string, Speaker>> Parse(string path)
        {
            //Get lowercase version of the extention
            string extension = Path.GetExtension(path).ToLower();

            switch (extension)
            {
                case FileExtensions.Esr: return ParseESRFile(path);
                case FileExtensions.Srt: return ParseSRTFile(path);
                case FileExtensions.Txt: return ParseScriptFile(path);
                default: throw new FormatException(string.Format("Extension \"{}\" is not a valid extension.",
                    extension));
            }
        }
        #endregion Parse

        #region ParseScriptFile
        /// <summary>
        /// Reads in the Script file located at scriptPath and parses it into CaptionList and
        /// SpeakerSet
        /// </summary>
        /// <param name="scriptPath">The path of the script file</param>
        public static Tuple<List<EditorCaption>,Dictionary<string, Speaker>> ParseScriptFile(string scriptPath)
        {
            List<EditorCaption> captionList = Utilities.ConstructCaptionList();
            Dictionary<string, Speaker> speakerSet = Utilities.ConstructSpeakerSet();

            string path = scriptPath; //Get path
            string[] lines = System.IO.File.ReadAllLines(@path); //Read in file

            //Start off with the Default speaker
            Speaker CurrentSpeaker = speakerSet[Speaker.DefaultName];
            //Set the Description Speaker to the description speaker contained in the set.
            Speaker DescriptionSpeaker = speakerSet[Speaker.DescriptionName];

            for (int i = 0; i < lines.Length; i++)
            {
                //Remove all leading and trailing whitespace from each line
                lines[i] = lines[i].Trim();

                //Ignore empty lines
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    //If the last character in the string is a ':', then this line is a speaker.
                    int lastChar = lines[i].Length - 1;
                    if (lines[i][lastChar] == ':')
                    {
                        string speakerName = lines[i].Substring(0, lines[i].Length - 1);

                        if (speakerSet.ContainsKey(speakerName))
                        {
                            CurrentSpeaker = speakerSet[speakerName];
                        }
                        else
                        {
                            CurrentSpeaker = new Speaker(speakerName);
                            speakerSet.Add(CurrentSpeaker.Name, CurrentSpeaker);
                        }
                    }
                        //If surrounded by [ and ], the word is a description
                    else if (lines[i][0] == '[' && lines[i][lastChar] == ']')
                    {
                        captionList.Add(new EditorCaption(lines[i], DescriptionSpeaker));
                    }
                    //If anything else then it is a dialogue line
                    else
                    {
                        captionList.Add(new EditorCaption(lines[i], CurrentSpeaker));
                    }
                }
            }//for
            return Tuple.Create(captionList, speakerSet);
        }//ParseScriptFile
        #endregion ParseScriptFile

        #region ParseESRFile
        /// <summary>
        /// Parses an ESR File
        /// </summary>
        /// <param name="scriptPath">The path of the script file</param>
        public static Tuple<List<EditorCaption>, Dictionary<string, Speaker>> ParseESRFile(string scriptPath)
        {
            List<EditorCaption> captionList = Utilities.ConstructCaptionList();
            Dictionary<string, Speaker> speakerSet = Utilities.ConstructSpeakerSet();

            string[] lines = System.IO.File.ReadAllLines(@scriptPath); //Read in file

            //Start off with the Default speaker
            Speaker CurrentSpeaker = speakerSet[Speaker.DefaultName];

            Regex numberLineRegex = new Regex(@"^\d+$");    //Line number
            //Time stamp regex, ex "00:00:35,895 --> 00:00:37,790" will match
            //Regex timeStampRegex = new Regex(@"^\d\d:\d\d:\d\d,\d\d\d --> \d\d:\d\d:\d\d,\d\d\d$");

            Regex timeStampRegex = new Regex(@"\d\d:\d\d:\d\d,\d\d\d");

            Boolean lastLineWasTimeStamp = false;

            string beginTime = "";
            string endTime = "";
            string captionLine = "";

            //Will continue a caption if set to true
            bool fullCaptionParsedFlag = false;

            for (int i = 0; i < lines.Length; i++)
            {
                //Remove unecessary whitespace from beginning and end of line
                lines[i] = lines[i].Trim();

                //Empty or iteration after last line
                if (string.IsNullOrEmpty(lines[i]))
                {
                    //If the flag is true, then we have already read in a caption
                    if (fullCaptionParsedFlag)
                    {
                        captionList.Add(new EditorCaption(captionLine, CurrentSpeaker, beginTime, endTime));
                        //Reset the captionFlag
                        fullCaptionParsedFlag = false;
                    }
                    //Console.WriteLine("Caption Line: {0}", captionLine);
                }
                //If the line is not empty
                else
                {
                    //If the line is a number line
                    if (numberLineRegex.IsMatch(lines[i]))
                    {
                        lastLineWasTimeStamp = false;
                    }
                    else
                    {
                        MatchCollection matches = timeStampRegex.Matches(lines[i]);
                        //If the line is a timestamp line
                        if (0 < matches.Count)
                        {
                            //Console.WriteLine("Timestamp Line. Begin: {0}, End: {1}", matches[0], matches[1]);

                            //Turn the SRT timestamps into EnACT-readable timestamps by removing the last two
                            //digits and replacing the comma with a period.
                            beginTime = matches[0].ToString();
                            beginTime = beginTime.Substring(0, beginTime.Length - 2).Replace(',', '.');

                            endTime = matches[1].ToString();
                            endTime = endTime.Substring(0, endTime.Length - 2).Replace(',', '.');
                            lastLineWasTimeStamp = true;
                        }
                        //If it ends with a colon the line is a speaker line
                        else if (lines[i].EndsWith(":") && lastLineWasTimeStamp)
                        {
                            string speakerName = lines[i].Substring(0, lines[i].Length - 1);
                            lastLineWasTimeStamp = false;
                            if (speakerSet.ContainsKey(speakerName))
                            {
                                CurrentSpeaker = speakerSet[speakerName];
                            }
                            else
                            {
                                speakerSet[speakerName] = new Speaker(speakerName);
                                CurrentSpeaker = speakerSet[speakerName];
                            }
                        }
                        //If the line is surrounded with square brackets it is a description
                        else if (lines[i][0] == '[' && lines[i][lines[i].Length - 1] == ']')
                        {
                            captionLine = lines[i].Substring(1, lines[i].Length - 2);
                            CurrentSpeaker = speakerSet[Speaker.DescriptionName];
                            fullCaptionParsedFlag = true;
                        }
                        //Else the line is a caption
                        else
                        {
                            if (fullCaptionParsedFlag)
                            {
                                captionLine += "\n" + lines[i];
                            }
                            else
                            {
                                captionLine = lines[i];
                                fullCaptionParsedFlag = true;
                            }
                            lastLineWasTimeStamp = false;
                        }
                    }
                    //If it's the last line and we have a full caption
                    if (i == lines.Length - 1 && fullCaptionParsedFlag)
                    {
                        captionList.Add(new EditorCaption(captionLine, CurrentSpeaker, beginTime, endTime));
                    }
                }//else
            }//for
            speakerSet[CurrentSpeaker.Name] = CurrentSpeaker;

            var t = Tuple.Create(captionList, speakerSet);
            return t; 
        }//ParseSRTFile
        #endregion ParseESRFile

        #region ParseSRTFile
        /// <summary>
        /// Parses an srt file into caption and speaker data useable by enact. NOTE. Will not look
        /// for speakers, and every caption will be attributed to the default speaker.
        /// </summary>
        /// <param name="scriptPath">The full path of the SRT file to be parsed</param>
        public static Tuple<List<EditorCaption>, Dictionary<string, Speaker>> ParseSRTFile(string scriptPath)
        {
            List<EditorCaption> captionList = Utilities.ConstructCaptionList();
            Dictionary<string, Speaker> speakerSet = Utilities.ConstructSpeakerSet();

            string[] lines = System.IO.File.ReadAllLines(@scriptPath); //Read in file

            //Start off with the Default speaker
            Speaker CurrentSpeaker = new Speaker("CARLO");
            //Set the Description Speaker to the description speaker contained in the set.
            Speaker DescriptionSpeaker = speakerSet[Speaker.DescriptionName];

            Regex numberLineRegex = new Regex(@"^\d+$");    //Line number
            //Time stamp regex, ex "00:00:35,895 --> 00:00:37,790" will match
            //Regex timeStampRegex = new Regex(@"^\d\d:\d\d:\d\d,\d\d\d --> \d\d:\d\d:\d\d,\d\d\d$");

            Regex timeStampRegex = new Regex(@"\d\d:\d\d:\d\d,\d\d\d");

            string beginTime = "";
            string endTime = "";
            string captionLine = "";

            //Will continue a caption if set to true
            bool continueCaptionFlag = false;

            for (int i = 0; i < lines.Length; i++)
            {
                //Remove unecessary whitespace from beginning and end of line
                lines[i] = lines[i].Trim();

                if (string.IsNullOrEmpty(lines[i]))
                {
                    //If the flag is true, then we have already read in a caption
                    if (continueCaptionFlag)
                    {
                        captionList.Add(new EditorCaption(captionLine, CurrentSpeaker, beginTime, endTime));
                        //Reset the captionFlag
                        continueCaptionFlag = false;
                    }
                    //Console.WriteLine("Caption Line: {0}", captionLine);
                }
                //If the line is not empty
                else
                {
                    //If the line is a number line
                    if (numberLineRegex.IsMatch(lines[i]))
                    {
                        //We don't really have any purpose for this line,
                        //but we want to be able to identify it
                        //Console.WriteLine("Number Line: {0}", lines[i]);
                    }
                    else
                    {
                        MatchCollection matches = timeStampRegex.Matches(lines[i]);
                        //If the line is a timestamp line
                        if (0 < matches.Count)
                        {
                            //Console.WriteLine("Timestamp Line. Begin: {0}, End: {1}", matches[0], matches[1]);

                            //Turn the SRT timestamps into EnACT-readable timestamps by removing the last two
                            //digits and replacing the comma with a period.
                            beginTime = matches[0].ToString();
                            beginTime = beginTime.Substring(0, beginTime.Length - 2).Replace(',', '.');

                            endTime = matches[1].ToString();
                            endTime = endTime.Substring(0, endTime.Length - 2).Replace(',', '.');
                        }

                        //Else the line is a caption
                        else
                        {
                            if (continueCaptionFlag)
                            {
                                captionLine += "\n" + lines[i];
                            }
                            else
                            {
                                captionLine = lines[i];
                                continueCaptionFlag = true;
                            }
                            //Console.WriteLine("Caption Line: {0}", lines[i]);
                        }
                    }
                }
            }//for
            speakerSet[CurrentSpeaker.Name] = CurrentSpeaker;

            return Tuple.Create(captionList, speakerSet);
        }//ParseESRFile
        #endregion ParseSRTFile
    }//Class
}//Namespace