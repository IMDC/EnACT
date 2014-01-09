using System;

namespace EnACT.Miscellaneous
{
    public static class Paths
    {
        public static readonly string TestScript = @"C:\Users\imdc\Documents\enact\Testing\testScript.txt";

        public static readonly string TestEsr = @"C:\Users\imdc\Documents\enact\Testing\testScript_2.esr";

        public static readonly string DefaultSpeakers = @"C:\Users\imdc\Documents\enact\Testing\speakers.xml";

        public static readonly string DefaultDialogues = @"C:\Users\imdc\Documents\enact\Testing\dialogues.xml";

        public static readonly string DefaultSettings = @"C:\Users\imdc\Documents\enact\Testing\Settings.xml";

        public static String BlankSwf
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "blank.swf"; }
        }

        public static String EditorEngine
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "EditorEngine.swf"; }
        }
    }
}
