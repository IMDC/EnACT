using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnACT;

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

            Caption c;

            //Act
            c = new Caption("Line,", s, begin1, end1);

            //Assert
            Assert.AreEqual(expectedDuration, c.Duration.AsDouble);
        }
    }
}
