using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EnACT
{
    /// <summary>
    /// Contains all methods and fields related to reading in and parsing text.
    /// </summary>
    public class TextParser
    {
        //Object reference variables
        public Dictionary<String, Speaker> SpeakerSet { set; get; }
        public List<EditorCaption> CaptionList { set; get; }

        /// <summary>
        /// Constucts a new text parser with the specified parameters
        /// </summary>
        /// <param name="SpeakerSet">The Set of Speakers for the current project</param>
        /// <param name="CaptionList">A list of Captions contained in the current project</param>
        public TextParser(Dictionary<String, Speaker> SpeakerSet, List<EditorCaption> CaptionList)
        {
            this.SpeakerSet = SpeakerSet;
            this.CaptionList = CaptionList;
        }

        #region ParseScriptFile
        /// <summary>
        /// Reads in the Script file located at scriptPath and parses it into
        /// CaptionList and SpeakerSet
        /// </summary>
        /// <param name="scriptPath">The path of the script file</param>
        public void ParseScriptFile(String scriptPath)
        {
            String path = scriptPath; //Get path
            String[] lines = System.IO.File.ReadAllLines(@path); //Read in file
            String speakerName = "";

            //Start off with the Default speaker
            Speaker CurrentSpeaker = SpeakerSet[Speaker.DEFAULTNAME];
            //Set the Description Speaker to the description speaker contained in the set.
            Speaker DescriptionSpeaker = SpeakerSet[Speaker.DESCRIPTIONNAME];

            for (int i = 0; i < lines.Length; i++)
            {
                //Remove all leading and trailing whitespace from each line
                lines[i] = lines[i].Trim();

                //Ignore empty lines
                if (!String.IsNullOrEmpty(lines[i]))
                {
                    //If the last character in the string is a ':', then this line is a speaker.
                    int lastChar = lines[i].Length - 1;
                    if (lines[i][lastChar] == ':')
                    {
                        //Console.WriteLine("New Speaker: {0}", lines[i]);
                        speakerName = lines[i].Substring(0, lines[i].Length - 1);

                        if (SpeakerSet.ContainsKey(speakerName))
                        {
                            CurrentSpeaker = SpeakerSet[speakerName];
                        }
                        else
                        {
                            CurrentSpeaker = new Speaker(speakerName);
                            SpeakerSet.Add(CurrentSpeaker.Name, CurrentSpeaker);
                        }
                    }
                    //If surrounded by [ and ], the word is a description
                    else if (lines[i][0] == '[' && lines[i][lastChar] == ']')
                    {
                        CaptionList.Add(new EditorCaption(lines[i], DescriptionSpeaker));
                    }
                    //If anything else then it is a dialogue line
                    else
                    {
                        CaptionList.Add(new EditorCaption(lines[i], CurrentSpeaker));
                    }
                }
            }//for
        }//ParseScriptFile
        #endregion

        #region ParseESRFile
        /// <summary>
        /// Parses an ESR File
        /// </summary>
        /// <param name="scriptPath">The path of the script file</param>
        public void ParseESRFile(String scriptPath)
        {
            String[] lines = System.IO.File.ReadAllLines(@scriptPath); //Read in file

            //Start off with the Default speaker
            Speaker CurrentSpeaker = SpeakerSet[Speaker.DEFAULTNAME] ;

            Regex numberLineRegex = new Regex(@"^\d+$");    //Line number
            //Time stamp regex, ex "00:00:35,895 --> 00:00:37,790" will match
            //Regex timeStampRegex = new Regex(@"^\d\d:\d\d:\d\d,\d\d\d --> \d\d:\d\d:\d\d,\d\d\d$");

            Regex timeStampRegex = new Regex(@"\d\d:\d\d:\d\d,\d\d\d");

            Boolean lastLineWasTimeStamp = false;

            String beginTime = "";
            String endTime = "";
            String captionLine = "";
            String speakerName = "";

            //Will continue a caption if set to true
            bool fullCaptionParsedFlag = false;

            for (int i = 0; i < lines.Length; i++)
            {
                //Remove unecessary whitespace from beginning and end of line
                lines[i] = lines[i].Trim();

                //Empty or iteration after last line
                if (String.IsNullOrEmpty(lines[i]))
                {
                    //If the flag is true, then we have already read in a caption
                    if (fullCaptionParsedFlag)
                    {
                        CaptionList.Add(new EditorCaption(captionLine,CurrentSpeaker,beginTime, endTime));
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
                            speakerName = lines[i].Substring(0, lines[i].Length - 1);
                            lastLineWasTimeStamp = false;
                            if (SpeakerSet.ContainsKey(speakerName))
                            {
                                CurrentSpeaker = SpeakerSet[speakerName];
                            }
                            else
                            {
                                SpeakerSet[speakerName] = new Speaker(speakerName);
                                CurrentSpeaker = SpeakerSet[speakerName];
                            }
                        }
                        //If the line is surrounded with square brackets it is a description
                        else if(lines[i][0] == '[' && lines[i][lines[i].Length - 1] == ']')
                        {
                            captionLine = lines[i].Substring(1, lines[i].Length - 2);
                            CurrentSpeaker = SpeakerSet[Speaker.DESCRIPTIONNAME];
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
                        CaptionList.Add(new EditorCaption(captionLine, CurrentSpeaker, beginTime, endTime));
                    }
                }//else
            }//for
            SpeakerSet[CurrentSpeaker.Name] = CurrentSpeaker;
        }//ParseSRTFile
        #endregion

        #region ParseSRTFile
        /// <summary>
        /// Parses an srt file into caption and speaker data useable by enact.
        /// NOTE. Will not look for speakers, and every caption will be attributed to
        /// the default speaker.
        /// </summary>
        /// <param name="scriptPath">The full path of the SRT file to be parsed</param>
        public void ParseSRTFile(String scriptPath)
        {
            String[] lines = System.IO.File.ReadAllLines(@scriptPath); //Read in file

            //Start off with the Default speaker
            Speaker CurrentSpeaker = new Speaker("CARLO");
            //Set the Description Speaker to the description speaker contained in the set.
            Speaker DescriptionSpeaker = SpeakerSet[Speaker.DESCRIPTIONNAME];

            Regex numberLineRegex = new Regex(@"^\d+$");    //Line number
            //Time stamp regex, ex "00:00:35,895 --> 00:00:37,790" will match
            //Regex timeStampRegex = new Regex(@"^\d\d:\d\d:\d\d,\d\d\d --> \d\d:\d\d:\d\d,\d\d\d$");

            Regex timeStampRegex = new Regex(@"\d\d:\d\d:\d\d,\d\d\d");

            String beginTime = "";
            String endTime = "";
            String captionLine = "";

            //Will continue a caption if set to true
            bool continueCaptionFlag = false;

            for (int i = 0; i < lines.Length; i++)
            {
                //Remove unecessary whitespace from beginning and end of line
                lines[i] = lines[i].Trim();

                if (String.IsNullOrEmpty(lines[i]))
                {
                    //If the flag is true, then we have already read in a caption
                    if (continueCaptionFlag)
                    {
                        CaptionList.Add(new EditorCaption(captionLine, CurrentSpeaker, beginTime, endTime));
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
            SpeakerSet[CurrentSpeaker.Name] = CurrentSpeaker;
        }//ParseESRFile
        #endregion
    }//Class
}//Namespace
