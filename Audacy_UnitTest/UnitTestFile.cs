using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Audacy_Competency_2018;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Audacy_UnitTest
{
    [TestClass]
    public class UnitTestFile
    {
        [TestMethod]
        public void invalidInputTest()
        {
            List<string> invalidInputList = new List<string>();
            string invalidInput1 = "abc12345-@"; //Has characters and special characters
            string invalidInput2 = "167948798090767825"; //Does not follow (n*3 + (n-1)) rule
            string invalidInput3 = "12341234123412"; //Fails (n*3 + (n-1)) rule by 1 character
            invalidInputList.Add(invalidInput1);
            invalidInputList.Add(invalidInput2);
            invalidInputList.Add(invalidInput3);
            for (int index = 0; index < invalidInputList.Count; index++)
            {
                Assert.IsFalse(Audacy_Business_Logic.isValidInput(invalidInputList[index]));
            }                
        }

        [TestMethod]
        public void validInputTest()
        {
            List<string> validInputList = new List<string>();
            string validInput1 = "12312333333"; //Valid as 3 distinct numbers are there and has a character length of (3*3+(3-1))
            string validInput2 = "9146046190019469140"; //Valid as 5 distinct numbers are present and has a character length of (5*3+(5-1))
            string validInput3 = "979798743763  87673874638"; //Same as above with multiple spaces
            string validInput4 = "979798743763 87673874638"; //Same as above with single space
            validInputList.Add(validInput1);
            validInputList.Add(validInput2);
            validInputList.Add(validInput3);
            validInputList.Add(validInput4);
            for (int index = 0; index < validInputList.Count; index++)
            {
                Assert.IsTrue(Audacy_Business_Logic.isValidInput(validInputList[index]));
            }
        }

        [TestMethod]
        public void invalidInputLineItemCount()
        {
            string invalidInput = "1546\r\n7879709809\r\n68766979798\r\n876769000";
            string[] invalidInputArray = Regex.Split(invalidInput, "\\r\\n");
            Assert.IsFalse(Audacy_Business_Logic.isValidInputLines(invalidInputArray));
        }

        [TestMethod]
        public void validInputLineItemCount()
        {
            string validInput = "1546\r\n7879709809\r\n68766979798";
            string[] validInputArray = Regex.Split(validInput, "\\r\\n");
            Assert.IsTrue(Audacy_Business_Logic.isValidInputLines(validInputArray));
        }
    }
}
