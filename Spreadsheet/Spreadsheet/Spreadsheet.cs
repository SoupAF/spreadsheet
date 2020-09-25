using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        //Dictionary of all named cells in the spreadsheet. There is infinite empty cells, but only named ones are kept track of here
        private Dictionary<string, Cell> cells = new Dictionary<string, Cell>();

        //Dependency graph for the spredsheet. Accessable only by this class
        private DependencyGraph Dependencies = new DependencyGraph();

        public override object GetCellContents(string name)
        {
            return cells[name].Value;
        }

        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            return cells.Keys;
        }


        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        public override IList<string> SetCellContents(string name, double number)
        {
            //Check validity of the name
            if (!checkValidity(name))
                throw new InvalidNameException();

            List<string> result = new List<string>();


            
            //If the cell has been previously named and given a value, change it and update dependencies
            if (cells.ContainsKey(name))
            {
                cells[name].Value = number;

                //TODO: UPDATE CELL DEPENDENCIES

            }

            //If the cell has never been used, add it to the cells set, and give it a value
            else
            {
                Cell newCell = new Cell(name, number);
                cells.Add(name, newCell);
                //TODO: UPDATE CELL DEPENDENCIES
            }


            return result;
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
            return Dependencies.GetDependents(name);
        }


        /// <summary>
        /// Private helper method used to check cell names for validity based on the following rules:
        ///   (1) its first character is an underscore or a letter
        ///   (2) its remaining characters (if any) are underscores and/or letters and/or digits
        /// NOTE: This method works on variable names as well, since the naming rules are the same for both
        /// 
        /// </summary>
        /// <param name="name">Input cell name to be tested</param>
        /// <returns>a boolean value for if the given name is valid or not</returns>
        private bool checkValidity(string name)
        {
            //Split name into characters
            char[] chars = name.ToCharArray();


            //Check first character in name
            if (!Char.IsLetter(chars[0]) && chars[0] != '_')
                return false;

            //Check the rest of the characters
            for (int t = 1; t < chars.Length; t++)
            {
                if (!Char.IsDigit(chars[t]) && !Char.IsLetter(chars[0]) && chars[0] != '_')
                {
                    return false;
                }
            }
            return true;
        }



        /// <summary>
        /// Represents a single cell in a spreadsheet
        /// Each cell has a name, a value, and a DependencyGraph of all its dependents
        /// Methods are provided to change the value and update dependencies
        /// </summary>
        class Cell
        {
            //Once set, the name of any given cell can never be changed
            private string Name;

            

            public Cell(string name, Object value)
            {
                Name = name;
                Value = value;
                
            }


            /// <summary>
            /// Getter and setter for the value property of a cell. 
            /// </summary>
            public Object Value
            {
                get { return Value; }
                set { Value = value; }
            }


        }
    }
}
