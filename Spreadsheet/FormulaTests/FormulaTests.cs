using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;

namespace FormulaTests
{
    [TestClass]
    public class FormulaTests
    {
        [TestMethod(), Timeout(5000)]
        [TestCategory("1")]
        public void TestSingleNumber()
        {
            Formula f1 = new Formula("5");
            Assert.AreEqual(5.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("2")]
        public void TestSingleVariable()
        {
            Formula f1 = new Formula("X5");
            Assert.AreEqual(13.0, f1.Evaluate(s => 13));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("3")]
        public void TestAddition()
        {
            Formula f1 = new Formula("5+3");
            Assert.AreEqual(8.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("4")]
        public void TestSubtraction()
        {
            Formula f1 = new Formula("18-10");
            Assert.AreEqual(8.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("5")]
        public void TestMultiplication()
        {
            Formula f1 = new Formula("2*4");
            Assert.AreEqual(8.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("6")]
        public void TestDivision()
        {
            Formula f1 = new Formula("16/2");
            Assert.AreEqual(8.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("7")]
        public void TestArithmeticWithVariable()
        {
            Formula f1 = new Formula("2+X1");
            Assert.AreEqual(6.0, f1.Evaluate(s => 4));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("8")]
        public void TestUnknownVariable()
        {
            Formula f1 = new Formula("2+X1");
            FormulaError err = new FormulaError("reason");
            Assert.AreEqual(err.GetType(), f1.Evaluate(s => { throw new ArgumentException("Unknown variable"); }).GetType());
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("9")]
        public void TestLeftToRight()
        {
            Formula f1 = new Formula("2*6+3");
            Assert.AreEqual(15.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("10")]
        public void TestOrderOperations()
        {
            Formula f1 = new Formula("2+6*3");
            Assert.AreEqual(20.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("11")]
        public void TestParenthesesTimes()
        {
            Formula f1 = new Formula("(2+6)*3");
            Assert.AreEqual(24.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("12")]
        public void TestOperatorBeforeParentheses()
        {
            Formula f1 = new Formula("2*(3+5)");
            Formula f2 = new Formula("64/(3+5)");
            Assert.AreEqual(16.0, f1.Evaluate(s => 0));
            Assert.AreEqual(8.0, f2.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("13")]
        public void TestPlusParentheses()
        {
            Formula f1 = new Formula("2+(3+5)");
            Assert.AreEqual(10.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("14")]
        public void TestPlusComplex()
        {
            Formula f1 = new Formula("2+(3+5*9)");
            Assert.AreEqual(50.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("15")]
        public void TestOperatorAfterParens()
        {
            Formula f1 = new Formula("(1*1)-2/2");
            Assert.AreEqual(0.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("16")]
        public void TestComplexTimesParentheses()
        {
            Formula f1 = new Formula("2+3*(3+5)");
            Assert.AreEqual(26.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("17")]
        public void TestComplexAndParentheses()
        {
            Formula f1 = new Formula("2+3*5+(3+4*8)*5+2");
            Assert.AreEqual(194.0, f1.Evaluate(s => 0));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("18")]
        public void TestDivideByZero()
        {
            Formula f1 = new Formula("5/0");
            Formula f2 = new Formula("5/a2");
            Formula f3 = new Formula("10/(5-5)");
            FormulaError err = new FormulaError("Your formula cannot inculde division by zero");
            Assert.AreEqual(err.GetType(), f1.Evaluate(s => 0).GetType());
            Assert.AreEqual(err.GetType(), f2.Evaluate(s => 0).GetType());
            Assert.AreEqual(err.GetType(), f3.Evaluate(s => 0).GetType());


        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("19")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSingleOperator()
        {
            Formula f1 = new Formula("+");
            f1.Evaluate(s => 0);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("20")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraOperator()
        {
            Formula f1 = new Formula("2+5+");
            f1.Evaluate(s => 0);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("21")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraParentheses()
        {
            Formula f1 = new Formula("2+5*7)");
            f1.Evaluate(s => 0);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("22")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidVariable()
        {
            Formula f1 = new Formula("$");
            f1.Evaluate(s => 0);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("23")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestPlusInvalidVariable()
        {
            Formula f1 = new Formula("5+$");
            f1.Evaluate(s => 0);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("24")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParensNoOperator()
        {
            Formula f1 = new Formula("5+7+(5)8");
            f1.Evaluate(s => 0);
        }


        [TestMethod(), Timeout(5000)]
        [TestCategory("25")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmpty()
        {
            Formula f1 = new Formula("");
            f1.Evaluate(s => 0);
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("26")]
        public void TestComplexMultiVar()
        {
            Formula f1 = new Formula("y1*3-8/2+4*(8-9*2)/14*x7");
            Assert.AreEqual(5.142857142857142, f1.Evaluate(s => (s == "x7") ? 1 : 4));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("27")]
        public void TestComplexNestedParensRight()
        {
            Formula f1 = new Formula("x1+(x2+(x3+(x4+(x5+x6))))");
            Assert.AreEqual(6.0, f1.Evaluate(s => 1));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("28")]
        public void TestComplexNestedParensLeft()
        {
            Formula f1 = new Formula("((((x1+x2)+x3)+x4)+x5)+x6");
            Assert.AreEqual(12.0, f1.Evaluate(s => 2));
        }

        [TestMethod(), Timeout(5000)]
        [TestCategory("29")]
        public void TestRepeatedVar()
        {
            Formula f1 = new Formula("a4-a4*a4/a4");
            Assert.AreEqual(0.0, f1.Evaluate(s => 3));
        }

        [TestMethod()]
        [TestCategory("29")]
        public void TestGetHashCode()
        {
            Formula f1 = new Formula("12+13+3");
            Formula f2 = new Formula("12+13+3-a4");
            Formula f3 = new Formula("12+13+3");
            Formula f4 = new Formula("6+3");

            Assert.AreEqual(12138, f1.GetHashCode());
            Assert.AreEqual(f1.GetHashCode(), f3.GetHashCode());
            Assert.AreNotEqual(f1.GetHashCode(), f2.GetHashCode());
            Assert.AreNotEqual(f4.GetHashCode(), f2.GetHashCode());


        }

        [TestMethod()]
        [TestCategory("30")]
        public void TestGetVariables()
        {
            Formula f1 = new Formula("a1+b5-x4");
            Formula f2 = new Formula("a1-a1");
            List<string> l1 = new List<string>();
            List<string> l2 = new List<string>();

            l1.Add("a1");
            l1.Add("b5");
            l1.Add("x4");
            l2.Add("a1");

            Assert.AreEqual(l1.ToString(), f1.GetVariables().ToString());
            Assert.AreEqual(l2.ToString(), f2.GetVariables().ToString());
        }

        [TestMethod()]
        [TestCategory("31")]
        public void TestToString()
        {
            Formula f1 = new Formula("5+1");
            Formula f2 = new Formula("5 + 1");

            Assert.AreEqual("5+1", f1.ToString());
            Assert.AreEqual("5+1", f2.ToString());

        }

        [TestMethod()]
        [TestCategory("32")]
        public void TestEqualsAndOperators()
        {
            //Tests == and != as well as .Equals()
            Formula f1 = new Formula("5+1");
            Formula f2 = new Formula("5 + 1");
            Formula f3 = new Formula("a3-1*10/4");

            Assert.IsFalse(f1.Equals(f3));
            Assert.IsTrue(f1.Equals(f2));
            Assert.IsTrue(f1.Equals(f1));
            Assert.IsTrue(f1 == f2);
            Assert.IsFalse(f1 == f3);
            Assert.IsTrue(f1 != f3);
            Assert.IsFalse(f1 != f2);
        }

        [TestMethod()]
        [TestCategory("32")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParenthesisRule()
        {
            Formula f1 = new Formula("((2+3)");
        }

        [TestMethod()]
        [TestCategory("32")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestDoubleOperatorRules()
        {
            Formula f1 = new Formula("++");
        }



    }
}
