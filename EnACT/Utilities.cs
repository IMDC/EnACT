using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnACT
{
    /// <summary>
    /// A utilities class meant for extending classes
    /// </summary>
    public static class Utilities
    {
        public static Boolean ContainsKeyIgnoreCase(this Dictionary<String, Speaker> SpeakerSet, String name)
        {
            return SpeakerSet.ContainsKey(name.ToUpper());
        }
    }
}
