using System.Collections.Generic;
using EnACT.Core;
using LibEnACT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caption = EnACT.Core.Caption;

namespace EnACTUnitTestProject
{
    /// <summary>
    /// This class contains tests for the Caption class
    /// </summary>
    [TestClass]
    public class CaptionUnitTest
    {
        private const string NoTimeStamp = "00:00:00.0";

        /// <summary>
        /// This method tests the Timestamp properties of Caption
        /// </summary>
        [TestMethod]
        public void CaptionTimestampPropertiesTest()
        {
            //Arrange
            Timestamp begin1 = new Timestamp(5);
            Timestamp end1 = new Timestamp(10);

            double expectedDuration = 5;

            Speaker s = new Speaker("Guy");

            Caption c;

            //Act
            c = new Caption("Line,", s, begin1, end1);

            //Assert
            Assert.AreEqual(expectedDuration, c.Duration.AsDouble);
        }

        /// <summary>
        /// This method tests setting the Caption.Begin property
        /// </summary>
        [TestMethod]
        public void CaptionChangeBeginTimestampTest()
        {
            //Arrange
            double beginTime = 5;
            double endTime = 20;

            double newBeginTime = 15;

            double expectedBegin = 15;
            double expectedEnd = 20;
            double expectedDuration = 5;

            Caption c;

            //Act
            c = new Caption("", new Speaker(), new Timestamp(beginTime), new Timestamp(endTime));

            c.Begin = newBeginTime;

            //Assert
            Assert.AreEqual(expectedBegin, c.Begin.AsDouble);
            Assert.AreEqual(expectedEnd, c.End.AsDouble);
            Assert.AreEqual(expectedDuration, c.Duration.AsDouble);
        }

        /// <summary>
        /// This method tests setting the Caption.End property
        /// </summary>
        [TestMethod]
        public void CaptionChangeEndTimestampTest()
        {
            //Arrange
            double beginTime = 5;
            double endTime = 20;

            double newEndTime = 25;

            double expectedBegin = 5;
            double expectedEnd = 25;
            double expectedDuration = 20;

            Caption c;

            //Act
            c = new Caption("", new Speaker(), new Timestamp(beginTime), new Timestamp(endTime));

            c.End = newEndTime;

            //Assert
            Assert.AreEqual(expectedBegin, c.Begin.AsDouble);
            Assert.AreEqual(expectedEnd, c.End.AsDouble);
            Assert.AreEqual(expectedDuration, c.Duration.AsDouble);
        }

        /// <summary>
        /// This method tests setting the Caption.Duration property
        /// </summary>
        [TestMethod]
        public void CaptionChangeDurationTimestampTest()
        {
            //Arrange
            double beginTime = 5;
            double endTime = 20;

            double newDurationTime = 30;

            double expectedBegin = 5;
            double expectedEnd = 35;
            double expectedDuration = 30;

            Caption c;

            //Act
            c = new Caption("", new Speaker(), new Timestamp(beginTime), new Timestamp(endTime));

            c.Duration = newDurationTime;

            //Assert
            Assert.AreEqual(expectedBegin, c.Begin.AsDouble);
            Assert.AreEqual(expectedEnd, c.End.AsDouble);
            Assert.AreEqual(expectedDuration, c.Duration.AsDouble);
        }

        /// <summary>
        /// This method tests the Caption.Text and Caption.Words properties.
        /// </summary>
        [TestMethod]
        public void CaptionTextTest()
        {
            //Arrange
            string[] testStrings =
            {
                "",
                "Hello",
                "Hello how are you?",
            };

            string twoSpaceWidth = "Hello  how  are  you?";

            //Act and Assert
            foreach (string s in testStrings)
            {
                LibEnACT.Caption c = new LibEnACT.Caption(s, new Speaker(), NoTimeStamp, NoTimeStamp, 1);

                var tokens = s.Split();

                //Test that each token is equal to each caption word.
                for (int i = 0; i < tokens.Length; i++)
                {
                    Assert.AreEqual(tokens[i], c.Words[i].Text, false);
                }

                //Test that each string is equal to each caption
                Assert.AreEqual(s, c.Text, false);
            }

            LibEnACT.Caption tswCaption = new LibEnACT.Caption(twoSpaceWidth, new Speaker(), NoTimeStamp, NoTimeStamp, 2);
            Assert.AreEqual(twoSpaceWidth,tswCaption.Text,false);
        }
    }
}
