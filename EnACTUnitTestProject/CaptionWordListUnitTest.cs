using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnACT;

namespace EnACTUnitTestProject
{
    /// <summary>
    /// Contains Tests for the CaptionWordList class.
    /// </summary>
    [TestClass]
    public class CaptionWordListUnitTest
    {
        #region Feed Test
        /// <summary>
        /// Tests the Feed method of CaptionWordList.
        /// </summary>
        [TestMethod]
        public void CaptionWordListFeedTest()
        {
            /* Arrange */
            CaptionWordList l1;
            CaptionWordList l2;
            CaptionWordList l3;

            //           0         10        20        30
            //           01234567890123456789012345678901234
            string s1 = "Hello World this is a caption line.";

            EditorCaptionWord[] expectedCaptionWords =
            {
                new EditorCaptionWord("Hello",    0),
                new EditorCaptionWord("World",    6),
                new EditorCaptionWord("this",     12),
                new EditorCaptionWord("is",       17),
                new EditorCaptionWord("a",        20),
                new EditorCaptionWord("caption",  22),
                new EditorCaptionWord("line.",    30),
            };

            /* Act */
            l1 = new CaptionWordList();
            l1.Feed(s1);

            l2 = new CaptionWordList(s1);

            l3 = new CaptionWordList();
            l3.AsString = s1;

            /* Assert */
            for (int i = 0; i < expectedCaptionWords.Length; i++)
            {
                Assert.AreEqual(expectedCaptionWords[i].Text, l1[i].Text);
                Assert.AreEqual(expectedCaptionWords[i].Text, l2[i].Text);
                Assert.AreEqual(expectedCaptionWords[i].Text, l3[i].Text);
            }
        }
        #endregion

        #region Output Test
        /// <summary>
        /// Tests the Text()/ToString() method of CaptionWordList
        /// </summary>
        [TestMethod]
        public void CaptionWordListTextOutputTest()
        {
            /* Arrange */
            CaptionWordList l1;

            string s1 = "Hello World this is a caption line.";

            /* Act */
            l1 = new CaptionWordList(s1);

            /* Assert */
            Assert.AreEqual(s1, l1.GetAsString());
            Assert.AreEqual(s1, l1.ToString());
            Assert.AreEqual(s1, l1.AsString);
        }
        #endregion
    }//Class
}//Namespace
