using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                if (Double.IsNaN(time) || time < 0.0)
                    return "00:00:00.0";

                string timeStamp = string.Empty;

                int hour = (int)time / 3600;
                timeStamp += hour.ToString("00:");

                time %= 3600;

                int minutes = (int)time / 60;
                timeStamp += minutes.ToString("00:");

                time %= 60;

                timeStamp += time.ToString("00.0");

                return timeStamp;
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
