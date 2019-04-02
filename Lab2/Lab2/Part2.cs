using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Part2
    {
        public static void Do()
        {
            Console.WriteLine("input first number");
            int num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("input second number");
            int num2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Binary division:");
            var result = Divide(num1, num2);
            Console.WriteLine(result.explanation);
            Console.WriteLine($"answ : remainder = {result.remainder} quotient = {result.quotient}");
        }
        public static Result Divide(int divident, int divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException();
            var exp = "";
            int a = Math.Abs(divident), b = Math.Abs(divisor);
            int r = 0, q = 0;
            if (a < b)
            {
                q = divident;
                exp += "divident < divisor, so quotient = divident and remainder = 0\n";
            }
            else
            {
                exp += $"{Convert.ToString(a, 2)} mod divident\n";
                exp += $"{Convert.ToString(b, 2)} mod divisor\n\n";
                int lenA, lenB, k;
                int tmp = a;
                for (lenA = 0; tmp != 0; tmp >>= 1, lenA++) ;
                tmp = b;
                for (lenB = 0; tmp != 0; tmp >>= 1, lenB++) ;
                k = lenA - lenB;
                b <<= k;
                exp += $"align divisor\n";
                exp += $"{Convert.ToString(a, 2)} mod divident\n";
                exp += $"{Convert.ToString(b, 2)} mod divisor\n\n";
                q = a + -b;
                r = q < 0 ? 0 : 1;
                exp += $"add divident and divisor in additional code\n";
                exp += $"quotient = {Convert.ToString(q, 2)} \n";
                exp += $"remainder = {Convert.ToString(r, 2)} \n\n";
                for (int i = 0; i < k; i++)
                {
                    b >>= 1;
                    exp += $"{Convert.ToString(b, 2)} right shift divisor\n";
                    if (q < 0)
                    {
                        q += b;
                        exp += $"{Convert.ToString(q, 2)} add qoutient and divisor\n";
                    }
                    else
                    {
                        q += -b;
                        exp += $"{Convert.ToString(q, 2)} sub qoutient and divisor\n";
                    }
                    r <<= 1;
                    if (q >= 0)
                        r++;
                    exp += $"{Convert.ToString(r, 2)} left shift remainder {(q >= 0 ? "and add 1" : "")}\n\n";
                }
                if (q < 0)
                {
                    exp += "determine qoutient\n";
                    q += b;
                    exp += $"{Convert.ToString(q, 2)} q = q + b because q < 0 \n\n";
                }
                exp += "analyze sign bit of dividend and divisor and set sign to remainder and quotient\n";
                if (divident < 0)
                {
                    if (divisor > 0)
                        r = -r;
                    q = -q;
                }
                if (divident > 0 && divisor < 0)
                    r = -r;
                exp += $"remainder = {Convert.ToString(r, 2)} quotient = {Convert.ToString(q, 2)}\n";
            }
            return new Result { explanation = exp, remainder = r, quotient = q };
        }
    }
    class Result
    {
        public string explanation;
        public int remainder;
        public int quotient;
    }
}
