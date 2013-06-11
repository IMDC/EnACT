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
        #region Constructor Test
        /// <summary>
        /// Tests the constructors of Timestamp to see that they construct Timestamp properly
        /// </summary>
        [TestMethod]
        public void TimestampConstuctorTest()
        {
            //Arrange
            double d = 100;
            String s = "12:34:56.7";

            Timestamp t;
            Timestamp td;
            Timestamp ts;

            //Act
            t = new Timestamp();
            td = new Timestamp(d);
            ts = new Timestamp(s);

            //Assert
            Assert.AreEqual(0, t.AsDouble);
            Assert.AreEqual(d, td.AsDouble);
            Assert.AreEqual(s, ts.AsString);
        }
        #endregion

        #region Assignment
        /// <summary>
        /// Tests the assignment and implicit conversion of strings and doubles to timestamps
        /// </summary>
        [TestMethod]
        public void TimestampSimpleAssignmentTest()
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

        /// <summary>
        /// Tests the assignment of a negative double value to a Timestamp
        /// </summary>
        [TestMethod]
        public void TimestampNegativeAssignmentTest()
        {
            //Arrange
            double negativeD = -5;

            Timestamp t;
            //Act and Assert
            try
            {
                t = new Timestamp(negativeD);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail();
            }
            catch (InvalidTimestampException) { }
        }

        /// <summary>
        /// Tests the assignment of several double corner cases to Timestamp.AsDouble
        /// </summary>
        [TestMethod]
        public void InvalidDoubleTimestampTest()
        {
            //Arrange
            Timestamp t1;
            Timestamp t2;
            Timestamp t3;
            Timestamp t4;
            Timestamp t5;
            Timestamp t6;
            Timestamp t7;
            Timestamp t8;

            //Act & Assert
            try
            {
                t1 = new Timestamp(Double.NaN);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail(); 
            }
            catch (InvalidTimestampException) { }

            try
            {
                t2 = new Timestamp(Double.PositiveInfinity);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail(); 
            }
            catch (InvalidTimestampException) { }

            try
            {
                t3 = new Timestamp(Double.NegativeInfinity);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail();
            }
            catch (InvalidTimestampException) { }

            try
            {
                t4 = new Timestamp(Double.MaxValue);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail();
            }
            catch (InvalidTimestampException) { }

            try
            {
                t5 = new Timestamp(Double.MinValue);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail(); 
            }
            catch (InvalidTimestampException) { }

            try { t6 = new Timestamp(Double.Epsilon); }
            //If the above throws an exception, then the test fails
            catch (InvalidTimestampException) { Assert.Fail(); }

            try { t7 = new Timestamp(Timestamp.MAX_TIME_DOUBLE); }
            //If the above throws an exception, then the test fails
            catch (InvalidTimestampException) { Assert.Fail(); }

            try
            {
                t8 = new Timestamp(Timestamp.MAX_TIME_DOUBLE + Timestamp.MIN_TIME_DIFFERENCE);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail();
            }
            catch (InvalidTimestampException) { }
            
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

            //Act and Assert
            try 
            { 
                tb1 = new Timestamp(badString1);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail(); 
            }
            catch (InvalidTimestampException) { }

            try 
            { 
                tb2 = new Timestamp(badString2);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail(); 
            }
            catch (InvalidTimestampException) { }

            try 
            {
                tb3 = new Timestamp(badString3);
                //If the line above doesn't throw an exception then the test fails
                Assert.Fail(); 
            }
            catch (InvalidTimestampException) { }
        }
        #endregion

        #region Equality
        /// <summary>
        /// Tests the Timestamp.Equals() method
        /// </summary>
        [TestMethod]
        public void TimestampEqualityTest()
        {
            //Arrange
            double d1 = 10;
            double d2 = 15;
            double d3 = 5;

            String s1 = "12:34:56.7";
            String s2 = "30:30:30.3";
            String s3 = "00:00:05.0";

            Timestamp td;
            Timestamp ts;
            Timestamp t3;
            Timestamp t4;

            Object o5;

            //Act
            td = new Timestamp(d1);
            ts = new Timestamp(s1);
            t3 = new Timestamp(d3);
            t4 = new Timestamp(s3);
            o5 = new Timestamp(d3);

            //Assert
            Assert.IsTrue(td.Equals(d1));
            Assert.IsFalse(td.Equals(d2));

            Assert.IsTrue(ts.Equals(s1));
            Assert.IsFalse(ts.Equals(s2));

            Assert.IsTrue(t3.Equals(d3));
            Assert.IsTrue(t3.Equals(s3));
            //Test it against itself
            Assert.IsTrue(t3.Equals(t3));
            //Test it agaist different Timestamp with same value
            Assert.IsTrue(t3.Equals(t4));
            //Test it agaist object
            Assert.IsTrue(t3.Equals(o5));

            //Assert double and string equality
            Assert.IsTrue(d3.Equals(t3));
            Assert.IsTrue(s3.Equals(t3));
        }

        /// <summary>
        /// Tests the MAX_TIME constants to ensure that they are equal when set
        /// or retrieved.
        /// </summary>
        [TestMethod]
        public void TimestampMaxValueEqualityTest()
        {
            //Arrange
            double expectedDouble = Timestamp.MAX_TIME_DOUBLE;
            string expectedString = Timestamp.MAX_TIME_STRING;

            Timestamp t1;
            Timestamp t2;

            //Act
            t1 = new Timestamp(Timestamp.MAX_TIME_DOUBLE);
            t2 = new Timestamp(Timestamp.MAX_TIME_STRING);

            //Assert
            Assert.AreEqual(expectedDouble, t1.AsDouble);
            Assert.AreEqual(expectedDouble, t2.AsDouble);
            Assert.AreEqual(expectedString, t1.AsString);
            Assert.AreEqual(expectedString, t1.AsString);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Tests the Timestamp's overridden + operator
        /// </summary>
        [TestMethod]
        public void TimestampOperatorAdditionTest()
        {
            //Arrange
            double d1 = 5;
            double d2 = 6;
            double expected = 11;

            Timestamp tExpected;
            Timestamp t1;
            Timestamp t2;
            Timestamp t3;
            Timestamp t4;
            Timestamp t5;
            Timestamp t6;

            //Act
            tExpected = new Timestamp(expected);
            t1 = new Timestamp(d1);
            t2 = new Timestamp(d2);
            t3 = d1 + d2;
            t4 = t1 + d2;
            t5 = d1 + t2;
            t6 = t1 + t2;

            //Assert
            Assert.AreEqual(expected, t3.AsDouble);
            Assert.AreEqual(expected, t4.AsDouble);
            Assert.AreEqual(expected, t5.AsDouble);
            Assert.AreEqual(expected, t6.AsDouble);
        }
        #endregion
    }
}
