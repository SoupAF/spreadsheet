// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// (Daniel Kopta) 
// Version 1.2 (9/10/17) 

// Change log:
//  (Version 1.2) Changed the definition of equality with regards
//                to numeric tokens


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax (without unary preceeding '-' or '+'); 
    /// variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {
        //Stores each of the tokens in a given formula
        private List<String> tokens;

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
           //This constructor can be empty becasue it runs the other constructor anyways
        }

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {

            int openParCount = 0;
            int closeParCount = 0;

            //Split input string into individual tokens
            tokens = new List<String>(GetTokens(formula));

            //Check that all tokens are valid
            for (int i = 0; i < tokens.Count; i++)
            {
                
                //Check if a token is a number
                if (!double.TryParse(tokens.ElementAt(i), out _))
                {
                    //Check that a token is a valid operator
                    if (tokens.ElementAt(i) != "(" && tokens.ElementAt(i) != ")" && tokens.ElementAt(i) != "+" 
                        && tokens.ElementAt(i) != "-" && tokens.ElementAt(i) != "*" && tokens.ElementAt(i) != "/")
                    {
                        //Check if a token is a variable
                        char[] chars = tokens.ElementAt(i).ToCharArray();

                        //Check first char
                        if (!Char.IsLetter(chars[0]) || chars[0] == '_')
                            throw new ArgumentException("Invalid variable syntax. The first character needs to be a letter or underscore");
                        //Check the rest
                        for (int t = 1; t < chars.Length; t++)
                        {
                            if (!Char.IsDigit(chars[t]) && !Char.IsLetter(chars[0]) && chars[0] == '_')
                            {
                                throw new ArgumentException("Invalid variable syntax. Only letters, numbers, and underscores are permitted in variable names");
                            }
                        }

                        //Normalize the variable, then make sure it passes through the given validator
                        string temp = normalize(tokens.ElementAt(i));
                        tokens.RemoveAt(i);
                        tokens.Insert(i, temp);

                        if (!isValid(tokens.ElementAt(i)))
                            throw new FormulaFormatException("Your variable is not valid according to your validation settings. Either change the validation method, or rename the variable");

                    }
                }

                //Increment parenthesis counts
                if (tokens.ElementAt(i) == "(")
                    openParCount++;

                else if (tokens.ElementAt(i) == ")")
                {
                    closeParCount++;
                    //Right parenthesis rule check
                    if (closeParCount > openParCount)
                        throw new FormulaFormatException("You have too many right paranthesis in your formula. Remove them, or add the corresponding left parenthesis");
                }

            }

            //Evaluate resluts for syntactical errors
            //One token rule
            if (tokens == null)
                throw new FormulaFormatException("Your formula cannot be empty");

            //Balanced parenthises rule
            if (closeParCount != openParCount)
                throw new FormulaFormatException("Your formula has not enough right parenthesis. Either add more, or remove some left parenthesis");

            //Starting token rule
            if (tokens.ElementAt(0) != "_" && !Char.IsLetter(tokens.ElementAt(0)[0]) && tokens.ElementAt(0) != "(" && !double.TryParse(tokens.ElementAt(0), out _))
                throw new FormulaFormatException("Your formula cannot begin with any operators that are not (");

            //Ending token rule
            if ((tokens.ElementAt(tokens.Count - 1) != "_" && !Char.IsLetter(tokens.ElementAt(tokens.Count - 1)[0])) && tokens.ElementAt(tokens.Count - 1) != "(" && !double.TryParse(tokens.ElementAt(tokens.Count - 1), out _))
                throw new FormulaFormatException("Your formula cannot end with any operators that are not )");

            //Operator following rule and extra following rule
            for (int i = 0; i < tokens.Count - 1; i++)
            {
                string token1 = tokens.ElementAt(i);
                string token2 = tokens.ElementAt(i + 1);

                //If token1 is an operator or opening parenthesis
                if (token1 == "(" || token1 == "+" || token1 == "-" || token1 == "*" || token1 == "/")
                {
                    //Check that token2 is not also an operator
                    if (token2 == "+" || token2 == "-" || token2 == "*" || token2 == "/")
                    {
                        throw new FormulaFormatException("Your formula cannot have two operators or left parenthesis following one another");
                    }
                }
                //Make sure there isn't two parenthesis in a row
                else if(token1 == "(" && token2 == "(")
                    throw new FormulaFormatException("Your formula cannot have two operators or left parenthesis following one another");

                //Check the other case, if token1 is not one of the above catgories
                else
                {
                    
                    //Make sure token2 is not also in the same category as token1
                    if (token2 != ")" && token2 != "+" && token2 != "-" && token2 != "*" && token2 != "/")
                    {
                        throw new FormulaFormatException("Your formula cannot have two numbers, variables, or right parenthesis following one another");
                    }
                }
                
            }

        }

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            Stack<char> operators = new Stack<char>();
            Stack<double> values = new Stack<double>();

            //Keeps track of how many open parenthesis there are
            int openParCount = 0;
            int closeParCount = 0;



            //Divide characters to the correct stacks (remove whitespace chars)
            for (int i = 0; i < tokens.Count; i++)
            {
                String temp = tokens.ElementAt(i);

                if (temp != " " && temp != "")
                {
                    //If temp is a number
                    if (double.TryParse(temp, out _))
                    {
                        bool didMath = false;

                        //Check if there is an operator on the stack
                        if (operators.Count != 0)
                        {

                            if (operators.Peek() == '*')
                            {
                                double t = values.Pop();
                                double n = double.Parse(temp);
                                t = t * n;
                                values.Push(t);
                                operators.Pop();
                                didMath = true;
                            }

                            else if (operators.Peek() == '/')
                            {
                                double t = values.Pop();
                                double n = double.Parse(temp);
                                if (n == 0)
                                    return new FormulaError("Your formula cannot include division by zero");
                                t = t / n;
                                values.Push(t);
                                operators.Pop();
                                didMath = true;
                            }

                        }

                        //If no math was performed, push temp onto the values stack
                        if (!didMath)
                        {
                            values.Push(double.Parse(temp));
                        }
                    }


                    //If temp is an operator
                    else if (Char.TryParse(temp, out char op) && (op == '/' || op == '*' || op == '+' || op == '-' || op == '(' || op == ')'))
                    {
                        //For /, *, or ( operators, push them onto the operators stack
                        if (op == '/' || op == '*')
                        {
                            operators.Push(op);
                        }

                        if (op == '(')
                        {
                            operators.Push(op);
                            openParCount++;
                        }

                        //If the operator is + or -
                        else if (op == '+' || op == '-')
                        {
                            //Check if stack is empty
                            if (operators.Count != 0)
                            {
                                //If there is already a + or minus on the stack, evaluate it, and then remplace it with temp
                                if (operators.Peek() == '+' || operators.Peek() == '-')
                                {
                                    double num1 = values.Pop();
                                    double num2 = values.Pop();
                                    char pastOp = operators.Pop();
                                    if (pastOp == '-')
                                    {
                                        values.Push(num2 - num1);
                                    }


                                    else
                                    {
                                        values.Push(num1 + num2);
                                    }

                                }
                            }

                            operators.Push(op);

                        }


                        //For ) operators
                        else if (op == ')')
                        {
                            /*
                            //Check that there is a corrosponding open paranthesis already
                            closeParCount++;
                            if (openParCount < closeParCount)
                                throw new ArgumentException("Invalid formula sytax");
                            */

                            //Check if stack is empty
                            if (operators.Count != 0)
                            {
                                //Evaluate any addition or subratction operators if they are present ontop of the stack
                                if (operators.Peek() == '+' || operators.Peek() == '-')
                                {
                                    double num1 = values.Pop();
                                    double num2 = values.Pop();
                                    char op2 = operators.Pop();
                                    if (op2 == '-')
                                    {
                                        values.Push(num2 - num1);
                                    }
                                    else
                                    {
                                        values.Push(num1 + num2);
                                    }

                                }
                            }

                            

                            //Remove the ( operator
                            operators.Pop();
                            openParCount--;
                            closeParCount--;

                            //Check if stack is empty
                            if (operators.Count != 0)
                            {
                                //Evaluate Multiplication or division if it is ontop of the stack
                                if (operators.Peek() == '*' || operators.Peek() == '/')
                                {
                                    double num1 = values.Pop();
                                    double num2 = values.Pop();
                                    if (operators.Peek() == '(')
                                        operators.Pop();

                                    op = operators.Pop();

                                    if (op == '/')
                                    {
                                        if (num2 == 0)
                                            return new FormulaError("Your formula cannot inculde division by zero");
                                        values.Push(num1 / num2);
                                    }
                                    else
                                    {
                                        values.Push(num1 * num2);
                                    }

                                }
                            }
                        }
                    }

                    //If temp is a variable
                    else
                    {
                        /*
                        //Check for valid variabe format

                        char[] chars = temp.ToCharArray();

                        //Check first char
                        if (!Char.IsLetter(chars[0]))
                            throw new ArgumentException("Invalid variable syntax");
                        //Check the rest
                        for (int t = 1; t < chars.Length; t++)
                        {
                            if (!Char.IsDigit(chars[t]))
                            {
                                throw new ArgumentException("Invalid variable syntax");
                            }
                        }
                        */

                        //Get variable 
                        double n;
                        try
                        {
                            n = lookup(temp);
                        }
                        catch (ArgumentException e)
                        {
                            return new FormulaError("Your variable could not be found. Please check that your variable is defined and spelled correctly");
                        }
                        //Evaluate expression with the variable's value
                        bool didMath = false;

                        //Check if there is an operator on the stack
                        if (operators.Count != 0)
                        {
                            //Multiply
                            if (operators.Peek() == '*')
                            {
                                double t = values.Pop();
                                t = t * n;
                                values.Push(t);
                                operators.Pop();
                                didMath = true;
                            }

                            //Divide
                            else if (operators.Peek() == '/')
                            {
                                double t = values.Pop();
                                if (n == 0)
                                    return new FormulaError("Your formula canot include division by zero");
                                t = t / n;
                                values.Push(t);
                                operators.Pop();
                                didMath = true;
                            }

                        }

                        if (!didMath)
                        {
                            values.Push(n);
                        }
                    }
                }
            }

            //If there are no more operations
            if (operators.Count() == 0)
            {
                return values.Pop();
            }

            //If there is any remaining addition or subtraction, evaluate it
            else
            {
                

                char ch = operators.Pop();

                if (ch == '-')
                {
                    double num1 = values.Pop();
                    double num2 = values.Pop();
                    return num2 - num1;
                }

                else return values.Pop() + values.Pop();
            }
        }

        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            return null;
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            return null;
        }

        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            return false;
        }

        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            return false;
        }

        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            return false;
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }
}