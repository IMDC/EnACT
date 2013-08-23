using EnACT.Core;
using LibEnACT;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnACTUnitTestProject
{
    /// <summary>
    /// This class contains tests for the Caption class
    /// </summary>
    [TestClass]
    public class CaptionUnitTest
    {
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

            EditorCaption c;

            //Act
            c = new EditorCaption("Line,", s, begin1, end1);

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

            EditorCaption c;

            //Act
            c = new EditorCaption("", new Speaker(), new Timestamp(beginTime), new Timestamp(endTime));

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

            EditorCaption c;

            //Act
            c = new EditorCaption("", new Speaker(), new Timestamp(beginTime), new Timestamp(endTime));

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

            EditorCaption c;

            //Act
            c = new EditorCaption("", new Speaker(), new Timestamp(beginTime), new Timestamp(endTime));

            c.Duration = newDurationTime;

            //Assert
            Assert.AreEqual(expectedBegin, c.Begin.AsDouble);
            Assert.AreEqual(expectedEnd, c.End.AsDouble);
            Assert.AreEqual(expectedDuration, c.Duration.AsDouble);
        }
    }
}
