using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EnACT
{
    /// <summary>
    /// Represents a timestamp in the form XX:XX:XX.X where X is a digit from 0-9.
    /// It can be set or retrieved as either a String or a Double, but is internally 
    /// represented as a double in the form of seconds.
    /// </summary>
    public class Timestamp
    {
        /// <summary>
        /// A regular expression that will validate a correct timestamp
        /// </summary>
        private static Regex validTimestamp = new Regex(@"^[0-9][0-9]:[0-9][0-9]:[0-9][0-9].[0-9]$");

        /// <summary>
        /// The internal storage of the timestamp. Stored as the number of seconds.
        /// </summary>
        private double time;

        /// <summary>
        /// A property representing the timestamp as a double.
        /// </summary>
        public double AsDouble
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// A property representing the timestamp in string form. Will produce
        /// a string in the form XX:XX:XX.X where X is a digit from 0-9.
        /// </summary>
        public String AsString
        {
            get
            {
                double timeDouble = time;
                if (Double.IsNaN(timeDouble) || timeDouble < 0.0)
                    return "00:00:00.0";

                string timeString = string.Empty;

                int hour = (int)timeDouble / 3600;
                timeString += hour.ToString("00:");

                timeDouble %= 3600;

                int minutes = (int)timeDouble / 60;
                timeString += minutes.ToString("00:");

                timeDouble %= 60;

                timeString += timeDouble.ToString("00.0");

                return timeString;
            }
            set
            {
                if (value == null || value == String.Empty)
                    time = 0;

                double seconds = 0;

                string[] time_array = value.Split(':');

                double tvalue = 0;
                double weight = 1;

                for (int i = time_array.Length - 1; i >= 0; i--)
                {
                    try
                    {
                        tvalue = double.Parse(time_array[i]);
                    }
                    catch
                    {
                        tvalue = 0;
                    }
                    finally
                    {
                        seconds += tvalue * weight;
                        weight *= 60;
                    }
                }
                time = seconds;
            }
        }

        /// <summary>
        /// Constucts a timestamp with a time value of 0, or a timestamp
        /// value of 00:00:00.0
        /// </summary>
        public Timestamp() : this(0) { }

        /// <summary>
        /// Constructs a timestamp using a string
        /// </summary>
        /// <param name="timestamp">The value to set this timestamp to</param>
        public Timestamp(String timestamp)
        {
            AsString = timestamp;
        }

        /// <summary>
        /// Constructs a timestamp using a double
        /// </summary>
        /// <param name="time">The value to set this timestamp to</param>
        public Timestamp(double time)
        {
            AsDouble = time;
        }

        /// <summary>
        /// Checks to see if a possible timestamp is valid or not. If the timestamp
        /// is in the form XX:XX:XX.X where X is a numerical digit from 0-9, it will
        /// return true, and false otherwise.
        /// </summary>
        /// <param name="ts">The timestamp to validate</param>
        /// <returns>true if valid, false if not valid</returns>
        public static bool TimeStampValidates(String ts)
        {
            if (validTimestamp.IsMatch(ts))
            {
                Console.WriteLine("Timestamp {0} Validated!", ts);
                return true;
            }
            else
            {
                Console.WriteLine("Timestamp {0} failed to Validate!", ts);
                return false;
            }
        }

        /// <summary>
        /// Implicitly converts a Timestamp to a double
        /// </summary>
        /// <param name="t">Timestamp to convert</param>
        /// <returns>The timestamp's double representation</returns>
        public static implicit operator double(Timestamp t)
        {
            return t.AsDouble;
        }

        /// <summary>
        /// Implicitly converts a double into a Timestamp
        /// </summary>
        /// <param name="d">The double to convert</param>
        /// <returns>The Timestamp with the double's value</returns>
        public static implicit operator Timestamp(double d)
        {
            return new Timestamp(d);
        }

        /// <summary>
        /// Implicitly converts a Timestamp to a string
        /// </summary>
        /// <param name="t">The Timestamp to convert</param>
        /// <returns>The Timestamp's string representation</returns>
        public static implicit operator string(Timestamp t)
        {
            return t.AsString;
        }

        /// <summary>
        /// Implicitly converts a string to a Timestamp
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <returns>The timestamp with the string's value</returns>
        public static implicit operator Timestamp(string s)
        {
            return new Timestamp(s);
        }
        
        /// <summary>
        /// Returns the string value of this TimeStamp. Returns the same as
        /// the AsString property
        /// </summary>
        /// <returns>This timestamp as a String</returns>
        public override string ToString()
        {
            return AsString;
        }
    }
}
