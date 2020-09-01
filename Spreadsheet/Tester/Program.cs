using FormulaEvaluator;
using System;

namespace Tester
{
    class Program
    {
        public static int VarRef(String var)
        {
            if (var == "a")
                return 5;
            else if (var == "b")
                return 3;
            else if (var == "c")
                return 8;
            else return 0;
        }

       

        static void Main(string[] args)
        {
            String example = "(1+2+3)-7";
            Console.WriteLine(Evaluator.Evaluate(example, VarRef));
        }
    }
}
