using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnACT;


namespace EnACTUnitTestProject
{
    /// <summary>
    /// This class contains unit tests for Timestamp
    /// </summary>
    [TestClass]
    public class TimeStampUnitTest
    {
        #region Assignment
        /// <summary>
        /// Tests the assignment of strings and doubles to timestamps
        /// </summary>
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
        #endregion

        #region Timestamp Validation
        /// <summary>
        /// Tests Timestamp string validation with strings that should pass validation
        /// </summary>
        [TestMethod]
        public void TimestampValidationPassTest()
        {
            //Arrange
            string goodString = "00:01:30.5";

            Timestamp tg;
            
            //Act
            try
            {
                tg = new Timestamp(goodString);
            }
            catch (InvalidTimestampException e)
            {
                //Assert
                Assert.Fail(e.Message); //We don't want a goodString to throw an exception
            }
        }

        /// <summary>
        /// Tests Timestamp string validation with strings that should fail validation
        /// </summary>
        [TestMethod]
        public void TimestampValidationFailTest()
        {
            //Arrange
            string badString1 = "String";
            string badString2 = "200:00:00.1";
            string badString3 = "10:1.0:10.1";

            Timestamp tb1;
            Timestamp tb2;
            Timestamp tb3;

            //Act
            try 
            { 
                tb1 = new Timestamp(badString1);
                //Assert
                Assert.Fail(); //If the line above doesn't throw an exception then the test fails
            }
            catch (InvalidTimestampException) { }

            try 
            { 
                tb2 = new Timestamp(badString2);
                //Assert
                Assert.Fail(); //If the line above doesn't throw an exception then the test fails
            }
            catch (InvalidTimestampException) { }

            try 
            {
                tb3 = new Timestamp(badString3);
                //Assert
                Assert.Fail(); //If the line above doesn't throw an exception then the test fails
            }
            catch (InvalidTimestampException) { }
        }
        #endregion
    }
}
