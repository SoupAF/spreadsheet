using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

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

        //Used to keep track of if the spreadsheet has been modified since last save. This is the underlying value of the property Changed
        private bool wasChanged;

        public override bool Changed { get { return wasChanged; } protected set { wasChanged = value; } }


        public Spreadsheet() : base(s => true, s => s, "default")
        {
            lookup = GetVarValue;
            cells = new Dictionary<string, Cell>();
            Dependencies = new DependencyGraph();
            Changed = false;
        }

        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            lookup = GetVarValue;
            cells = new Dictionary<string, Cell>();
            Dependencies = new DependencyGraph();
            Changed = false;

        }

        public Spreadsheet(string filename, Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            lookup = GetVarValue;
            cells = new Dictionary<string, Cell>();
            Dependencies = new DependencyGraph();
            Changed = false;
            Version = version;

            //Attempt to read the spreadsheet from the file. If there are isues, throw an exception
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                //If spreadsheet or cells was read, they are just used for structuring the file, no action is needed
                                case "spreadsheet":
                                    break;

                                case "cells":
                                    break;

                                //If cell was read, then we need to add that cell to the new spreadsheet
                                //NOTE: all of the "reader.Read()" lines are to advance the reader past indentation and the ends of elements that we want to skip over
                                case "cell":
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    string cellName = reader.Value;
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    string cellVal = reader.Value;
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    this.SetContentsOfCell(cellName, cellVal);
                                    break;
                            }
                        }
                        //If the read element isnt a start element, it should be the end of the cells block, and we don't need to continue reading the file
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new SpreadsheetReadWriteException("The specified filename/path could not be found. Please ensure it is correct");
            }









            //TODO load spreadsheet from a file and make a new spreadsheet using that
        }

        public override object GetCellContents(string name)
        {
            //Normalize the name first
            name = Normalize(name);

            //Check if the cell is empty or not
            if (cells.ContainsKey(name))
                return cells[name].Value;

            else return "";


        }



        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            List<string> result = new List<string>();
            foreach (string s in cells.Keys)
            {
                result.Add(s);
            }
            return result;
        }


        public override IList<String> SetContentsOfCell(String name, String content)
        {
            //Normalize the cell name
            name = Normalize(name);

            //Check validity of the name
            if (!checkValidity(name))
                throw new InvalidNameException();

            //TODO run the validator on the string
            if (!IsValid(name))
                throw new InvalidNameException();

            //Check if content is a double
            if (Double.TryParse(content, out double result))
            {
                return SetCellContents(name, result);
            }

            //Check if content is a double
            if (content.StartsWith("="))
            {
                //remove the "=" and create a new formula
                return SetCellContents(name, new Formula(content.Substring(1)));
            }

            //If content is not a formula or a double, treat it as a string
            else return SetCellContents(name, content);
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
        protected override IList<string> SetCellContents(string name, double number)
        {



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

            //Set changed to true
            Changed = true;
            return outdatedCells;
        }

        protected override IList<string> SetCellContents(string name, string text)
        {



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

            Changed = true;

            return result;
        }

        protected override IList<string> SetCellContents(string name, Formula formula)
        {



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

            Changed = true;

            return result;
        }

        //Comment to test branching
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            return Dependencies.GetDependents(name);
        }


        /// <summary>
        /// Private helper method used to check cell names for validity based on the following rules:
        ///   (1) its first character is a letter
        ///   (2) its last character is a digit
        ///   (3) all characters in between are letters or numbers
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
            if (!Char.IsLetter(chars[0]))
                return false;

            //Check the rest of the characters
            for (int t = 1; t < chars.Length; t++)
            {
                if (!Char.IsDigit(chars[t]) && !Char.IsLetter(chars[t]))
                {
                    return false;
                }
            }

            //Check the last character
            if (!Char.IsDigit(chars[chars.Length - 1]))
                return false;

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
            else if (result is SpreadsheetUtilities.Formula)
            {
                Formula f1 = (Formula)GetCellContents(name);
                object val = (double)f1.Evaluate(lookup);
                if (val is double)
                    return (double)val;
                else throw new ArgumentException();
            }
            else throw new ArgumentException();
        }

        public override string GetSavedVersion(string filename)
        {
            //Attempt to start reading the file. If it doesn't work, throw an exception
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    //read the version attribute of spreadsheet. If the spreadsheet file is in the wrong format, this will throw an exception
                    if (reader.IsStartElement())
                    {
                        if (reader.Name == "spreadsheet")
                            return reader["version"];
                        else throw new SpreadsheetReadWriteException("The specified file is not in the correct format, or may be corrupted. Please try again");
                    }
                    else throw new SpreadsheetReadWriteException("The specified file is not in the correct format, or may be corrupted. Please try again");
                }
            }
            catch (FileNotFoundException)
            {
                throw new SpreadsheetReadWriteException("The specified file could not be found. Please ensure it was entered correctly and that the file exists");
            }
        }



        // ADDED FOR PS5
        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>cell name goes here</name>
        /// <contents>cell contents goes here</contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.  
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// 
        /// If there are any problems opening, writing, or closing the file, the method should throw a
        /// SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override void Save(string filename)
        {
            //Check that there is data to save
            if (cells.Count == 0)
                throw new SpreadsheetReadWriteException("This Spreadsheet is empty! Please add data to at least one cell before saving");

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "   ";

            //Attempt to write the file. If the filename/path is incorrect for any reason, throw an exception
            try
            {
                using (XmlWriter writer = XmlWriter.Create(filename, settings))
                {
                    //Write the start of the document and version info
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    writer.WriteAttributeString("version", Version);

                    //Start the cells section
                    writer.WriteStartElement("cells");

                    //Write all cells
                    foreach (Cell c in cells.Values)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("name", c.Name);

                        //If the content of a cell is a formula, it needs to have "=" appended
                        if (c.Value is SpreadsheetUtilities.Formula)
                        {
                            string formula = "=" + c.Value.ToString();
                            writer.WriteElementString("contents", formula);
                        }
                        //Otherwise just write the contents as a string
                        else writer.WriteElementString("contents", c.Value.ToString());

                        writer.WriteEndElement();
                    }

                    //End Cells block
                    writer.WriteEndElement();
                    //End spreadsheet block
                    writer.WriteEndElement();
                    writer.WriteEndDocument();


                }
            }
            catch (DirectoryNotFoundException)
            {
                throw new SpreadsheetReadWriteException("Filename or path is invalid! Please make sure your specified filename/path is correct");
            }

            //Reset the Changed attribute, as all changes are now saved
            Changed = false;

        }

        public override object GetCellValue(string name)
        {

            //Normalize the name first
            name = Normalize(name);

            object result;

            if (!cells.ContainsKey(name))
                return "";

            //If the value of the cell is a formula, evaluate it and get its value
            if (cells[name].Value is SpreadsheetUtilities.Formula)
            {
                Formula f1 = (Formula)cells[name].Value;
                result = f1.Evaluate(lookup);
            }

            //Otherwise,the contents can just be returned as eithe a double or a string
            else result = cells[name].Value;

            return result;

        }




        /// <summary>
        /// Represents a single cell in a spreadsheet
        /// Each cell has a name, a value, and a DependencyGraph of all its dependents
        /// Methods are provided to change the value and update dependencies
        /// </summary>
        class Cell
        {
            //Once set, the name of any given cell can never be changed
            private string name;
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

            public string Name
            {
                get { return name; }
                private set { name = value; }
            }

            

        }


    }
}
