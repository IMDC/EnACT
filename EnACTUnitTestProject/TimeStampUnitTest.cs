using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnACT;


namespace EnACTUnitTestProject
{
    [TestClass]
    public class TimeStampUnitTest
    {
        [TestMethod]
        public void AssignmentTest()
        {
            //Arrange
            double d = 5;
            string s = "00:00:05.0";

            double expected = 5;
            
            Timestamp td;
            Timestamp ts;

            //Act
            td = new Timestamp();
            td = d;

            ts = new Timestamp();
            ts = s;

            //Assert
            Assert.AreEqual(expected, td.AsDouble);
            Assert.AreEqual(expected, ts.AsDouble);
        }
    }
}
