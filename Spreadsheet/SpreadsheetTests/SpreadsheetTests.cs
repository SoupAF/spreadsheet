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
            s1.SetCellContents("a1", 15);
            Assert.AreEqual(15.0, s1.GetCellContents("a1"));
        }


        [TestMethod]
        public void TestAddString()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", "potato");
            Assert.AreEqual("potato", s1.GetCellContents("a1"));
        }

        [TestMethod]
        public void TestAddFormula()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", new Formula("6+9"));
            Assert.AreEqual("6+9", s1.GetCellContents("a1").ToString());
        }

        [TestMethod]
        public void TestAddAll()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", 15);
            s1.SetCellContents("a2", "potato");
            s1.SetCellContents("a3", new Formula("6+9"));

            Assert.AreEqual(15.0, s1.GetCellContents("a1"));
            Assert.AreEqual("potato", s1.GetCellContents("a2"));
            Assert.AreEqual("6+9", s1.GetCellContents("a3").ToString());
        }

        [TestMethod]
        public void TestChangeValueAllTypes()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", 15);
            s1.SetCellContents("a2", "potato");
            s1.SetCellContents("a3", new Formula("6+9"));

            s1.SetCellContents("a1", 10);
            s1.SetCellContents("a2", "apple");
            s1.SetCellContents("a3", new Formula("12-13"));


            Assert.AreEqual(10.0, s1.GetCellContents("a1"));
            Assert.AreEqual("apple", s1.GetCellContents("a2"));
            Assert.AreEqual("12-13", s1.GetCellContents("a3").ToString());

        }

        [TestMethod]
        public void TestAddFormulaWithVars()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", 15);
            s1.SetCellContents("a2", new Formula("a1+3"));
            s1.SetCellContents("a3", new Formula("a2-6"));
            Assert.AreEqual("a1+3", s1.GetCellContents("a2").ToString());
            Assert.AreEqual("a2-6", s1.GetCellContents("a3").ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestDetectCircularDependenciesOneCell()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", new Formula("a1+1"));
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestDetectCircularDependenciesMultiCell()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", new Formula("a2+1"));
            s1.SetCellContents("a2", new Formula("a1-2"));
        }

        [TestMethod]
        public void TestGetNames()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", new Formula("a2+1"));
            s1.SetCellContents("_l", new Formula("a2+1"));
            s1.SetCellContents("b9", new Formula("a2+1"));
            s1.SetCellContents("__", new Formula("a2+1"));
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
            s1.SetCellContents("19", new Formula("a2+1"));

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameString()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("__&", "potato");

        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameDouble()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("NUll329484alshfjzs%%", 23);

        }

        [TestMethod]
        public void TestChangingDependenciesDouble()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", 124);
            s1.SetCellContents("a2", new Formula("a1-13"));
            s1.SetCellContents("a1", 13);
            Assert.AreEqual(13.0, s1.GetCellContents("a1"));
        }

        [TestMethod]
        public void TestChangingDependenciesString()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", 124.0);
            s1.SetCellContents("a2", new Formula("a1-13"));
            s1.SetCellContents("a1", "apple");
            Assert.AreEqual("apple", s1.GetCellContents("a1"));
        }

        [TestMethod]
        public void TestChangingDependenciesFormula()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", 124.0);
            s1.SetCellContents("a2", new Formula("a1-13"));
            s1.SetCellContents("a3", new Formula("a2*3"));
            s1.SetCellContents("a2", new Formula("13-4"));
            Assert.AreEqual(new Formula("13-4"), s1.GetCellContents("a2"));
        }

        [TestMethod]
        public void TestChangingValueAllTypesSameCell()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            s1.SetCellContents("a1", 15);
            Assert.AreEqual(15.0, s1.GetCellContents("a1"));

            s1.SetCellContents("a1", "potato");
            Assert.AreEqual("potato", s1.GetCellContents("a1"));

            s1.SetCellContents("a1", new Formula("6+9"));
            Assert.AreEqual("6+9", s1.GetCellContents("a1").ToString());

            s1.SetCellContents("a1", 12456);
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

       














    }
}
