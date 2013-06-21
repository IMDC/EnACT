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

        #region GetCaptionWordIndex Test
        /// <summary>
        /// Tests the GetCaptionWordIndexAt method of CaptionWordList
        /// </summary>
        [TestMethod]
        public void CaptionWordListGetCaptionWordIndexTest()
        {
            /* Arrange */
            CaptionWordList l1;
            //           0         10        20        30
            //           01234567890123456789012345678901234
            string s1 = "Hello World this is a caption line.";

            //String indexes that will cause an ArgumentOutOfRangeException
            int [] errorStringIndexes = 
            {
                -1,
                int.MinValue,
            };

            //String indexes that will be greater than the length of the CaptionWordList text
            int[] overLimitStringIndexes = 
            {
                100,
                int.MaxValue,
                s1.Length,
            };

            //Expected values
            string[] expectedStrings = s1.Split(' ');

            /* Act and Assert */
            l1 = new CaptionWordList(s1);

            //Test in-range numbers
            for (int i = 0; i < s1.Length; i++)
            {
                int wordIndex = l1.GetCaptionWordIndexAt(i);
                Assert.AreEqual(expectedStrings[wordIndex], l1[wordIndex].Text);
            }

            //Test errors
            foreach (int stringIndex in errorStringIndexes)
            {
                try
                {
                    int x = l1.GetCaptionWordIndexAt(stringIndex);
                    Assert.Fail();  //Should fail if no exception is thrown
                }
                catch (ArgumentOutOfRangeException) { }
            }

            //Test over range limits
            foreach (int i in overLimitStringIndexes)
            {
                //Anything over the size of the string should select the last word
                int wordIndex = l1.GetCaptionWordIndexAt(i);
                Assert.AreEqual(expectedStrings[wordIndex], l1[wordIndex].Text);
            }
        }
        #endregion

        #region GetStringStartIndexAt Test
        /// <summary>
        /// Tests the GetStringStartIndexAt method of CaptionWordList.
        /// </summary>
        [TestMethod]
        public void CaptionWordList_GetStringStartIndexAt_Test()
        {
            /* Arrange */
            CaptionWordList l1;
            //           0         10        20        30
            //           01234567890123456789012345678901234
            string s1 = "Hello World this is a caption line.";

            //Expected values
            string[] expectedStrings = s1.Split(' ');

            //String indexes that will cause an ArgumentOutOfRangeException
            int[] errorWordIndexes = 
            {
                -1,
                int.MinValue,
                100,
                int.MaxValue,
                s1.Length,
                expectedStrings.Length,
            };

            /* Act and Assert */
            l1 = new CaptionWordList(s1);

            //Test in-range numbers
            for (int i = 0; i < l1.Count; i++)
            {
                int stringStartIndex = l1.GetStringStartIndexAt(i);
                Assert.AreEqual(s1[stringStartIndex], l1.AsString[stringStartIndex]);
            }

            //Test errors
            foreach (int wordIndex in errorWordIndexes)
            {
                try
                {
                    int x = l1.GetStringStartIndexAt(wordIndex);
                    Assert.Fail();  //Should fail if no exception is thrown
                }
                catch (ArgumentOutOfRangeException) { }
            }
        }
        #endregion
    }//Class
}//Namespace
