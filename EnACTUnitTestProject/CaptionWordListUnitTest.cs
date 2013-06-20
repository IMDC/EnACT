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
        /// <summary>
        /// Tests the Feed method of CaptionWordList.
        /// </summary>
        [TestMethod]
        public void FeedTest()
        {
            //Arrange
            CaptionWordList l1;
            CaptionWordList l2;

            string s1 = "Hello World this is a caption line.";

            CaptionWord[] expectedCaptionWords =
            {
                new CaptionWord("Hello"),
                new CaptionWord("World"),
                new CaptionWord("this"),
                new CaptionWord("is"),
                new CaptionWord("a"),
                new CaptionWord("caption"),
                new CaptionWord("line."),
            };

            //Act
            l1 = new CaptionWordList();
            l1.Feed(s1);

            l2 = new CaptionWordList(s1);

            //Assert
            for (int i = 0; i < expectedCaptionWords.Length; i++)
            {
                Assert.AreEqual(expectedCaptionWords[i].Text, l1[i].Text);
                Assert.AreEqual(expectedCaptionWords[i].Text, l2[i].Text);
            }
        }
    }//Class
}//Namespace
