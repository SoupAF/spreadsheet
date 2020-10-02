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

        //Lookup delegate, uses the private helper method GetVarValue defined below
        public Func<string, double> lookup;

        //Dictionary of all named cells in the spreadsheet. There is infinite empty cells, but only named ones are kept track of here
        private Dictionary<string, Cell> cells;

        //Dependency graph for the spredsheet. Accessable only by this class
        private DependencyGraph Dependencies;




        public Spreadsheet()
        {
            lookup = GetVarValue;
            cells = new Dictionary<string, Cell>();
            Dependencies = new DependencyGraph();
        }
        
        public override object GetCellContents(string name)
        {
            //Check if the cell is empty or not
            if (cells.ContainsKey(name))
                return cells[name].Value;

            else return "";

            //This code here gets the resulting value of a formula in a cell, but it is not neccessary for PS4. 
            //I wrote it thinking it was needed right now, but I'm keeping it in case it becomes useful later
            /*
            object result; 
            if (cells[name].Value is SpreadsheetUtilities.Formula)
            {
                Formula f1 = (Formula)cells[name].Value;
                result = f1.Evaluate(lookup);
            }
                
            else result = cells[name].Value;

            return result;
            */
        }



        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            List<string> result = new List<string>();
            foreach(string s in cells.Keys)
            {
                result.Add(s);
            }
            return result;
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


            //Get list of cells to update
            List<string> outdatedCells = new List<string>(GetCellsToRecalculate(name));

            //If the cell has been previously named and given a value, change its value and re-calcualte formulas
            if (cells.ContainsKey(name))
            {
                cells[name].Value = number;



                //For each cell in outdatedCells that contains a formuala, update its value. (IN ORDER)
                foreach (string s in outdatedCells)
                {
                    if (cells[s].Value is SpreadsheetUtilities.Formula)
                    {
                        //Create a new function with the same value as the one in the cell, evaluate it, and replace it in the cell
                        Formula f1 = (Formula)cells[s].Value;
                        f1.Evaluate(lookup);
                        cells[s].Value = f1;
                        
                    }
                }

                //Cells containing numbers do not depend on other cells, so remove all its dependees if ot has any

            }

            //If the cell has never been used, add it to the cells set, and give it a value
            else
            {
                Cell newCell = new Cell(name, number);
                cells.Add(name, newCell);
                //No dependencies should exist for a cell with no previous value
            }


            return outdatedCells;
        }

        public override IList<string> SetCellContents(string name, string text)
        {
            //Check validity of the name
            if (!checkValidity(name))
                throw new InvalidNameException();


            List<string> result = new List<string>();



            //If the cell has been previously named and given a value, change it and update dependencies
            if (cells.ContainsKey(name))
            {
                cells[name].Value = text;

                //TODO: UPDATE CELL DEPENDENCIES
                //Get list of cells to recalculate
                IEnumerable<string> outdatedCells = GetCellsToRecalculate(name);

                //For each cell in outdatedCells that contains a formuala, update its value. (IN ORDER)
                foreach (string s in outdatedCells)
                {
                    if (cells[s].Value is SpreadsheetUtilities.Formula)
                    {
                        //Create a new function with the same value as the one in the cell, evaluate it, and replace it in the cell
                        Formula f1 = (Formula)cells[s].Value;
                        f1.Evaluate(lookup);
                        cells[s].Value = f1;
                    }
                }
            }

            //If the cell has never been used, add it to the cells set, and give it a value
            else
            {
                Cell newCell = new Cell(name, text);
                cells.Add(name, newCell);
                //No dependencies should exist for a cell with no previous value, but the program still needs to check for cicular dependencies
            }


            return result;
        }

        public override IList<string> SetCellContents(string name, Formula formula)
        {

            //Check validity of the name
            if (!checkValidity(name))
                throw new InvalidNameException();

            List<string> result = new List<string>();


            //Get a list of all variables the formula references 
            List<string> vars = new List<string>(formula.GetVariables());

            //The Visit() method doesn not check for circular dependencies involving only one cell, so we need to check for that right now
            if (vars.Contains(name))
                throw new CircularException();

            //Replace old dependees with new ones based on what variables are part of the formula (if it has any)
            if (Dependencies.HasDependees(name))
            {
                Dependencies.ReplaceDependees(name, vars);
            }

            //Otherwise add the new dependencies
            else
            {
                foreach (string s in vars)
                {
                    Dependencies.AddDependency(s, name);
                }
            }
            

            //If the cell has been previously named and given a value, change it and update dependencies
            if (cells.ContainsKey(name))
            {
                cells[name].Value = formula;

                //TODO: UPDATE CELL DEPENDENCIES
                //Get list of cells to recalculate
                IEnumerable<string> outdatedCells = GetCellsToRecalculate(name);

                //For each cell in outdatedCells that contains a formuala, update its value. (IN ORDER)
                foreach (string s in outdatedCells)
                {
                    if (cells[s].Value is SpreadsheetUtilities.Formula)
                    {
                        //Create a new function with the same value as the one in the cell, evaluate it, and replace it in the cell
                        Formula f1 = (Formula)cells[s].Value;
                        f1.Evaluate(lookup);
                        cells[s].Value = f1;
                    }
                }
            }

            //If the cell has never been used, add it to the cells set, and give it a value
            else
            {
                Cell newCell = new Cell(name, formula);
                cells.Add(name, newCell);
                //No dependencies should exist for a cell with no previous value, but we need to check for circular dependencies anyways
                GetCellsToRecalculate(name);
            }


            return result;
        }

        //Comment to test branching
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
                if (!Char.IsDigit(chars[t]) && !Char.IsLetter(chars[t]) && chars[t] != '_')
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Lookup function for getting the double value of cells. If the cell does not contain a double, this throws an ArgumentException to be handeled by the formula class
        /// </summary>
        /// <param name="name">The name of the variable to lookup</param>
        /// <returns>The double value of the cell</returns>
        private double GetVarValue(string name)
        {

            object result = GetCellContents(name);
            if (result is double)
                return (double)result;
            else if(result is SpreadsheetUtilities.Formula)
            {
                Formula f1 = (Formula)GetCellContents(name);
                object val = (double)f1.Evaluate(lookup);
                if (val is double)
                    return (double)val;
                else throw new ArgumentException();
            }
            else throw new ArgumentException();
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
            private object contents;

            public Cell(string name, string val)
            {
                Name = name;
                Value = val;
            }
            public Cell(string name, double val)
            {
                Name = name;
                Value = val;
            }
            public Cell(string name, Formula val)
            {
                Name = name;
                Value = val;
            }


            /// <summary>
            /// Getter and setter for the value property of a cell. 
            /// </summary>
            public object Value
            {
                get { return contents; }
                set { contents = value; }
            }


        }


    }
}
