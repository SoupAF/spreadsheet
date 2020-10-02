using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod]
        public void TestAddDouble()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "15");
            Assert.AreEqual(15.0, s1.GetCellContents("a1"));
        }


        [TestMethod]
        public void TestAddString()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "potato");
            Assert.AreEqual("potato", s1.GetCellContents("a1"));
        }

        [TestMethod]
        public void TestAddFormula()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "=6+9");
            Assert.AreEqual("6+9", s1.GetCellContents("a1").ToString());
        }

        [TestMethod]
        public void TestAddAll()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "15");
            s1.SetContentsOfCell("a2", "potato");
            s1.SetContentsOfCell("a3", "=6+9");

            Assert.AreEqual(15.0, s1.GetCellContents("a1"));
            Assert.AreEqual("potato", s1.GetCellContents("a2"));
            Assert.AreEqual("6+9", s1.GetCellContents("a3").ToString());
        }

        [TestMethod]
        public void TestChangeValueAllTypes()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "15");
            s1.SetContentsOfCell("a2", "potato");
            s1.SetContentsOfCell("a3", "=6+9");

            s1.SetContentsOfCell("a1", "10");
            s1.SetContentsOfCell("a2", "apple");
            s1.SetContentsOfCell("a3", "=12-13");


            Assert.AreEqual(10.0, s1.GetCellContents("a1"));
            Assert.AreEqual("apple", s1.GetCellContents("a2"));
            Assert.AreEqual("12-13", s1.GetCellContents("a3").ToString());

        }

        [TestMethod]
        public void TestAddFormulaWithVars()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "15");
            s1.SetContentsOfCell("a2", "=a1+3");
            s1.SetContentsOfCell("a3", "=a2-6");
            Assert.AreEqual("a1+3", s1.GetCellContents("a2").ToString());
            Assert.AreEqual("a2-6", s1.GetCellContents("a3").ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestDetectCircularDependenciesOneCell()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "=a1+1");
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestDetectCircularDependenciesMultiCell()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "=a2+1");
            s1.SetContentsOfCell("a2", "=a1-2");
        }

        [TestMethod]
        public void TestGetNames()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1","=a2+1");
            s1.SetContentsOfCell("bb1", "=a2+1");
            s1.SetContentsOfCell("b99", "=a2+1");
            s1.SetContentsOfCell("aaazasdhfasg346345", "=a2+1");
            List<string> l1 = new List<string>();
            l1.Add("a1");
            l1.Add("_l");
            l1.Add("b9");
            l1.Add("__");

            Assert.AreEqual(l1.ToString(), s1.GetNamesOfAllNonemptyCells().ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameFormula()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("19", "=a2+1");

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameString()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("__&", "potato");

        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameDouble()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("NUll329484alshfjzs%%", "23");

        }

        [TestMethod]
        public void TestChangingDependenciesDouble()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "124");
            s1.SetContentsOfCell("a2", "=a1-13");
            s1.SetContentsOfCell("a1", "13");
            Assert.AreEqual(13.0, s1.GetCellContents("a1"));
        }

        [TestMethod]
        public void TestChangingDependenciesString()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "124.0");
            s1.SetContentsOfCell("a2", "=a1-13");
            s1.SetContentsOfCell("a1", "apple");
            Assert.AreEqual("apple", s1.GetCellContents("a1"));
        }

        [TestMethod]
        public void TestChangingDependenciesFormula()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "124.0");
            s1.SetContentsOfCell("a2", "=a1-13");
            s1.SetContentsOfCell("a3", "=a2*3");
            s1.SetContentsOfCell("a2", "=13-4");


            Assert.AreEqual(new Formula("13-4"), s1.GetCellContents("a2"));
        }

        [TestMethod]
        public void TestChangingValueAllTypesSameCell()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "15");
            Assert.AreEqual(15.0, s1.GetCellContents("a1"));

            s1.SetContentsOfCell("a1", "potato");
            Assert.AreEqual("potato", s1.GetCellContents("a1"));

            s1.SetContentsOfCell("a1", "=6+9");
            Assert.AreEqual("6+9", s1.GetCellContents("a1").ToString());

            s1.SetContentsOfCell("a1", "12456");
            Assert.AreEqual(12456.0, s1.GetCellContents("a1"));
        }

        [TestMethod]
        public void TestGetContentsEmpty()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            Assert.AreEqual("", s1.GetCellContents("a1"));
        }

        [TestMethod]
        public void TestGetNonEmptyCellNamesWhenEmpty()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            Assert.AreEqual(new List<string>().ToString(), s1.GetNamesOfAllNonemptyCells().ToString());
        }

        [TestMethod]
        public void TestGetCellValue()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "124.0");
            s1.SetContentsOfCell("a2", "=a1-13");
            s1.SetContentsOfCell("a3", "potato");
            s1.SetContentsOfCell("a4", "=a2-4");
            s1.SetContentsOfCell("a5", "=a4+a2");

            Assert.AreEqual(124.0, s1.GetCellValue("a1"));
            Assert.AreEqual(111.0, s1.GetCellValue("a2"));
            Assert.AreEqual("potato", s1.GetCellValue("a3"));
            Assert.AreEqual(107.0, s1.GetCellValue("a4"));
            Assert.AreEqual(218.0, s1.GetCellValue("a5"));
        }

        [TestMethod]
        public void TestWriteToDisk()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetContentsOfCell("a1", "124.0");
            s1.SetContentsOfCell("a2", "=a1-13");
            s1.SetContentsOfCell("a3", "potato");
            s1.SetContentsOfCell("a4", "=a2-4");
            s1.SetContentsOfCell("a5", "=a4+a2");

            s1.Save("Test.txt");

            Spreadsheet s2 = new Spreadsheet("Test.txt", s => true, s => s, "Copy of s1");
            s2.Save("result.txt");

        }














    }
}
