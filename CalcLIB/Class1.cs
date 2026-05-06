using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace CalcLIB
{
    public class Class1
    {
        public static dynamic Execute(dynamic first, char op, dynamic second) 
        {
            dynamic result = 0;
            switch (op)
            {
                case '+':
                    result = first + second; break;
                case '-':
                    result = first - second; break;
                case '*':
                    result = first * second; break;
                case '/':
                    if (second == 0)
                        throw new DivideByZeroException("Деление на 0");
                    result = first / second; break;
                case '^':
                    result = Math.Pow(first, second); break;
                default:
                    result = 0; break;
            }
            return result;
        }

        public double Execute(double firstNumber, double secondNumber, char currentOperator)
        {
            throw new NotImplementedException();
        }
    }
}
