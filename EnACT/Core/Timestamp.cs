using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace EnACT
{
    #region Timestamp Class
    /// <summary>
    /// Represents a timestamp in the form XX:XX:XX.X where X is a digit from 0-9.
    /// It can be set or retrieved as either a String or a Double, but is internally 
    /// represented as a double in the form of seconds. A Timestamp can not be negative.
    /// If a negative value is assigned to a timestamp, then an InvalidException will 
    /// be thrown.
    /// </summary>
    public class Timestamp
    {
        #region Constants
        /// <summary>
        /// The highest time value that can be assigned to a Timestamp as a double
        /// </summary>
        public const double MaximumDoubleValue = 359999.9;

        /// <summary>
        /// The highest time value that can be assigned to a Timestamp as a string
        /// </summary>
        public const string MaximumTimeValue = "99:59:59.9";

        /// <summary>
        /// The smallest amount of time that will differentiate one caption from another.
        /// </summary>
        public const double MinimumTimeDifference = 0.1;
        #endregion

        #region Regex
        /// <summary>
        /// A regular expression that will validate a correct timestamp
        /// </summary>
        private static Regex validTimestamp = new Regex(@"^[0-9][0-9]:[0-5][0-9]:[0-5][0-9]\.[0-9]$");
        #endregion

        #region Private Fields
        /// <summary>
        /// The internal storage of the timestamp. Stored as the number of seconds. Backing field 
        /// for AsDouble.
        /// </summary>
        private double time;

        /// <summary>
        /// Backing field for AsString
        /// </summary>
        private string timestampString;
        #endregion

        #region AsDouble
        /// <summary>
        /// A property representing the timestamp as a double. Get this property when  the Timestamp 
        /// has to explicitly be represented as a double.
        /// </summary>
        public double AsDouble
        {
            get { return time; }
            set 
            {
                //Check for a valid time
                if (value < 0.0)
                    throw new InvalidTimestampException("Timestamp.AsDouble value can not be negative: " + value);
                else if (Double.IsNaN(value))
                    throw new InvalidTimestampException("Timestamp.AsDouble value can not be NaN");
                else if (Double.IsPositiveInfinity(value))
                    throw new InvalidTimestampException("Timestamp.AsDouble value can not be Positive Infinity");
                else if (Double.IsNegativeInfinity(value))
                    throw new InvalidTimestampException("Timestamp.AsDouble value can not be Negative Infinity");
                else if (MaximumDoubleValue < value)
                    throw new InvalidTimestampException("Timestamp.AsDouble is too large");

                time = value;
                //Need to regenerate the string
                ResetTimestampString();
            }
        }
        #endregion

        #region AsString
        /// <summary>
        /// A property representing the timestamp in string form. Will produce
        /// a string in the form XX:XX:XX.X where X is a digit from 0-9. Get this
        /// property when the Timestamp has to expiclitly be represented as a String.
        /// </summary>
        public String AsString
        {
            get
            {
                //If null, generate String
                if (timestampString == null)
                {
                    double timeDouble = time;
                    string ts;

                    //Get time value without any decimal places
                    int timeInt = (int)time;

                    //Subtract everything in the ones column and greater
                    timeDouble -= timeInt;

                    //Get hours
                    int hours = timeInt / 3600;
                    timeInt %= 3600;

                    //Get minues
                    int minutes = timeInt / 60;
                    timeInt %= 60;

                    //Get Seconds as the remainder of timeInt and the decimal part left in timedouble earlier
                    timeDouble += timeInt;

                    //Format string
                    ts = hours.ToString("00:") + minutes.ToString("00:") + timeDouble.ToString("00.0") ;

                    //Check to ensure Timestamp validates, and isn't something like "00:-35791394:NaN"
                    if (!TimeStampValidates(ts))
                        throw new InvalidTimestampException("String value is not a valid Timestamp");

                    //Store the generated value for future retrieval
                    timestampString = ts;
                }

                return timestampString;
            }
            set
            {
                if (!TimeStampValidates(value))
                    throw new InvalidTimestampException("String value is not a valid Timestamp");

                if (value == null || value == String.Empty)
                    time = 0;

                //If value is lexicographically greater than MaximumTimeValue
                if(0 < String.Compare(value, MaximumTimeValue))
                    throw new InvalidTimestampException("Timestamp.AsString is too large");

                double seconds = 0;

                string[] time_array = value.Split(':');

                double tvalue = 0;
                double weight = 1;

                for (int i = time_array.Length - 1; i >= 0; i--)
                {
                    try     { tvalue = double.Parse(time_array[i]); }
                    catch   { tvalue = 0; }
                    finally
                    {
                        seconds += tvalue * weight;
                        weight *= 60;
                    }
                }
                //Set double value
                time = seconds;

                //Set string value
                timestampString = value;
            }
        }
        #endregion

        #region Constructor
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
        #endregion

        #region ResetTimestampString
        /// <summary>
        /// Resets the timestampString so that it will need to be regenerated.
        /// </summary>
        private void ResetTimestampString()
        {
            timestampString = null;
        }
        #endregion

        #region Operator Overrides
        /// <summary>
        /// Adds two timestamps together in the form t1.AsDouble + t2.AsDouble.
        /// </summary>
        /// <param name="t1">First timestamp to add</param>
        /// <param name="t2">Second timestamp to add</param>
        /// <returns></returns>
        public static Timestamp operator +(Timestamp t1, Timestamp t2)
        {
            return t1.AsDouble + t2.AsDouble;
        }
        #endregion

        #region Validation
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
                //Console.WriteLine("Timestamp {0} Validated!", ts);
                return true;
            }
            else
            {
                //Console.WriteLine("Timestamp {0} failed to Validate!", ts);
                return false;
            }
        }
        #endregion

        #region Conversion
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
        #endregion

        #region Equality
        /// <summary>
        /// Determines whether the specified Object is equal to the current Timestamp. 
        /// </summary>
        /// <param name="obj">The Object to compare with the current Timestamp</param>
        /// <returns>true if the specified Object is equal to the current Timestamp; 
        /// otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Timestamp)
            {
                Timestamp t = (Timestamp)obj;
                return time.Equals(t.AsDouble);
            }
            else if (obj is Double)
            {
                Double d = (Double) obj;
                return time.Equals(d);
            }
            else if (obj is String)
            {
                String s = (String)obj;
                return AsString.Equals(s);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether two Timestamps are equal
        /// </summary>
        /// <param name="t">The Timestamp to compare with the current Timestamp.</param>
        /// <returns> true if the specified Timestamp is equal to the current Timestamp; 
        /// otherwise, false. </returns>
        public bool Equals(Timestamp t)
        {
            if (t == null)
                return false;

            return time.Equals(t.AsDouble);
        }

        /// <summary>
        /// Determines whether a Timestamp and a Double are equal
        /// </summary>
        /// <param name="t">The double to compare with the current Timestamp.</param>
        /// <returns> true if the specified double is equal to the current Timestamp; 
        /// otherwise, false. </returns>
        public bool Equals(double d)
        {
            return time.Equals(d);
        }

        /// <summary>
        /// Determines whether a Timestamp and a String are equal
        /// </summary>
        /// <param name="t">The String to compare with the current Timestamp.</param>
        /// <returns> true if the specified String is equal to the current Timestamp; 
        /// otherwise, false. </returns>
        public bool Equals(String s)
        {
            return AsString.Equals(s);
        }
        #endregion

        #region Object Override Methods
        /// <summary>
        /// Returns the string value of this TimeStamp. Returns the same as
        /// the AsString property
        /// </summary>
        /// <returns>This timestamp as a String</returns>
        public override string ToString()
        {
            return AsString;
        }

        /// <summary>
        /// Gets the Hash of the timestamp. Returns the Double value's hash value
        /// </summary>
        /// <returns>The hash code of the double representation of this timestamp</returns>
        public override int GetHashCode()
        {
            return time.GetHashCode();
        }
        #endregion
    }
    #endregion

    #region InvalidTimestampException Class
    public class InvalidTimestampException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidTimestampException class.
        /// </summary>
        public InvalidTimestampException() : base() { }
        /// <summary>
        /// Initializes a new instance of the InvalidTimestampException class with a 
        /// specified error message.
        /// </summary>
        /// <param name="message"></param>
        public InvalidTimestampException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the InvalidTimestampException class with 
        /// serialized data.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InvalidTimestampException(string message, System.Exception innerException)
            : base(message, innerException) { }
    }
    #endregion
}