using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
	public static class Evaluator
	{
		public delegate int Lookup(String v);

		/// <summary>
		/// Evaluates an input expression in standard infix notation
		/// </summary>
		/// <param name="exp">Input expression in standard infix notation. Variables are supported</param>
		/// <param name="variableEvaluator">Method for returning an intger value for an input variable name</param>
		/// <returns>The integer result of the input expression</returns>
		public static int Evaluate(String exp, Lookup variableEvaluator)
        {
			//Split input expression string into individual characters
			string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
			
			Stack<char> operators = new Stack<char>();
			Stack<int> values = new Stack<int>();

			
			
			//Divide characters to the correct stacks (remove whitespace chars)
			for (int i = 0; i < substrings.Length; i++)
			{
				String temp = substrings[i];

				if (temp != " " && temp != "")
                {
					//If temp is an integer
					if (int.TryParse(temp, out _))
					{
						bool didMath = false;
						 
						//Check if there is an operator on the stack
						if (operators.Count != 0)
						{

							if (operators.Peek() == '*')
							{
								int t = values.Pop();
								int n = int.Parse(temp);
								t = t * n;
								values.Push(t);
								operators.Pop();
								didMath = true;
							}

							else if (operators.Peek() == '/')
							{
								int t = values.Pop();
								int n = int.Parse(temp);
								t = t / n;
								values.Push(t);
								operators.Pop();
								didMath = true;
							}

						}

						//If no math was performed, push temp onto the values stack
						if(!didMath)
						{
							values.Push(int.Parse(temp));
						}
					}


					//If temp is an operator
					else if (Char.TryParse(temp, out char op) && (op == '/' || op == '*' || op == '+' || op == '-' || op == '(' || op == ')'))
					{
						//For /, *, or ( operators, push them onto the operators stack
						if (op == '/' || op == '*' || op == '(')
						{
							operators.Push(op);
						}

						//If the operator is + or -
						else if(op == '+' || op == '-')
                        {
							//Check if stack is empty
							if (operators.Count != 0)
							{
								//If there is already a + or minus on the stack, evaluate it, and then remplace it with temp
								if (operators.Peek() == '+' || operators.Peek() == '-')
								{
									int num1 = values.Pop();
									int num2 = values.Pop();
									operators.Pop();
									if (op == '-')
									{
										values.Push(num1 - num2);
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
						else if(op == ')')
                        {
							//Check if stack is empty
							if (operators.Count != 0)
							{
								//Evaluate any addition or subratction operators if they are present ontop of the stack
								if (operators.Peek() == '+' || operators.Peek() == '-')
								{
									int num1 = values.Pop();
									int num2 = values.Pop();
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

							//Check if stack is empty
							if (operators.Count != 0)
							{
								//Evaluate Multiplication or division if it is ontop of the stack
								if (operators.Peek() == '*' || operators.Peek() == '/')
								{
									int num1 = values.Pop();
									int num2 = values.Pop();
									operators.Pop();
									if (op == '/')
									{
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
						//Get variable value
						int n = variableEvaluator(temp);

						//Evaluate expression with the variable's value
						bool didMath = false;

						//Check if there is an operator on the stack
						if (operators.Count != 0)
						{
							//Multiply
							if (operators.Peek() == '*')
							{
								int t = values.Pop();
								t = t * n;
								values.Push(t);
								operators.Pop();
								didMath = true;
							}

							//Divide
							else if (operators.Peek() == '/')
							{
								int t = values.Pop();
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
			if(operators.Count() == 0)
            {
				return values.Pop();
            }

			//If there is any remaining addition or subtraction, evaluate it
            else
            {
				char ch = operators.Pop();

				if (ch == '-')
                {
					int num1 = values.Pop();
					int num2 = values.Pop();
					return num2 - num1;
				}
					
				else return values.Pop() + values.Pop();
            }

			
			
        }
	}
}
