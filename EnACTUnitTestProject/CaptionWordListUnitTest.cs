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
            //Arrange
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

            //Act
            l1 = new CaptionWordList();
            l1.Feed(s1);

            l2 = new CaptionWordList(s1);

            l3 = new CaptionWordList();
            l3.AsString = s1;

            //Assert
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
            //Arrange
            CaptionWordList l1;

            string s1 = "Hello World this is a caption line.";

            //Act
            l1 = new CaptionWordList(s1);

            //Assert
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
            //Act
            CaptionWordList l1;
            //           0         10        20        30
            //           01234567890123456789012345678901234
            string s1 = "Hello World this is a caption line.";

            int[] inputIndexes =
            {
                0,
                7,
                14,
                18,
                26,
                34,
            };

            int[] captionWordIndexes = new int[inputIndexes.Length];

            int [] errorIndexes = 
            {
                100,
                -1,
                int.MaxValue,
                int.MinValue,
                s1.Length,
            };

            string[] expectedStrings = s1.Split(' ');

            //Arrange
            l1 = new CaptionWordList(s1);

            for (int i = 0; i < inputIndexes.Length; i++)
            {
                captionWordIndexes[i] = l1.GetCaptionWordIndexAt(inputIndexes[i]);
            }

            //Assert
            for (int i = 0; i < inputIndexes.Length; i++)
            {
                int index = captionWordIndexes[i];
                Assert.AreEqual(expectedStrings[i],l1[i].Text);
            }

            foreach (int index in errorIndexes)
            {
                try
                {
                    int x = l1.GetCaptionWordIndexAt(index);
                    Assert.Fail();  //Should fail if no exception is thrown
                }
                catch (ArgumentOutOfRangeException) { }
            }
        }
        #endregion
    }//Class
}//Namespace
