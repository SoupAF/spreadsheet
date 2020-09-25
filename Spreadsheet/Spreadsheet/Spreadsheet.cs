using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace SS
{
    class Spreadsheet : AbstractSpreadsheet
    {
        //HashSet of all named cells in the spreadsheet. There is infinite empty cells, but only named ones are kept track of here
        HashSet<string> cells = new HashSet<string>();

        public override object GetCellContents(string name)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            throw new NotImplementedException();
        }

        public override IList<string> SetCellContents(string name, double number)
        {
            throw new NotImplementedException();
        }

        public override IList<string> SetCellContents(string name, string text)
        {
            throw new NotImplementedException();
        }

        public override IList<string> SetCellContents(string name, Formula formula)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Represents a single cell in a spreadsheet
    /// Each cell has a name, a value, and a DependencyGraph of all its dependents
    /// </summary>
    class Cell
    {
        private string Name;
        private Object Value;
        private DependencyGraph Dependencies;

        public Cell(string name, Object value)
        {
            Name = name;
            Value = value;
            Dependencies = new DependencyGraph();
        }


    }
}
