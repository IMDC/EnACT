using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEnACT;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnACTUnitTestProject
{
    /// <summary>
    /// This class contains tests for the CaptionWord class
    /// </summary>
    [TestClass]
    public class CaptionWordUnitTest
    {
        private const string NoTimeStamp = "00:00:00.0";

        [TestMethod]
        public void CaptionWordIndeciesTest()
        {
            //Arrange
            string s = "Hello how are you?";
            int[] sBegin = {0, 6, 10, 14};
            int[] sEnd = {5, 9, 13, 18};

            string s2 = "Hello  how  are  you?";
            int[] s2Begin = { 0, 7, 12, 17 };
            int[] s2End = { 5, 10, 15, 21 };

            //Act
            Caption c = new Caption(s,new Speaker(), NoTimeStamp, NoTimeStamp, 1);
            Caption c2 = new Caption(s2, new Speaker(), NoTimeStamp, NoTimeStamp, 1);
            
            //Assert
            for (int i = 0; i < c.Words.Count; i++)
            {
                Assert.AreEqual(sBegin[i],c.Words[i].BeginIndex);
                Assert.AreEqual(sEnd[i], c.Words[i].EndIndex);
            }

            for (int i = 0; i < c2.Words.Count; i++)
            {
                Assert.AreEqual(s2Begin[i], c2.Words[i].BeginIndex);
                Assert.AreEqual(s2End[i], c2.Words[i].EndIndex);
            }
        }
    }
}
