using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Part1
    {
        public static void Do()
        {
            Console.WriteLine("input first number");
            var num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("input second number");
            var num2 = int.Parse(Console.ReadLine());
            Multiplicate(num1, num2);
        }
        public static void Multiplicate(int multiplicand=0, int multiplier=0)
        {
            Int64 product = 0, startMultiplicand, startMultiplier;
            startMultiplicand = multiplicand;
            startMultiplier = multiplier;
            for (int i = 0; i < 32; ++i)
            {
                Console.WriteLine("Step #" + (i + 1) + ":\n");

                Console.WriteLine("Multiplicand:\n" + FinishStringWithZeros(Convert.ToString(multiplicand, 2)) +
                    "\nMultiplier:\n" + FinishStringWithZeros(Convert.ToString(multiplier, 2)) + "\n");

                short lsb = (short)(multiplier & 1);

                if (lsb == 1)
                {
                    Console.WriteLine("Add multiplicand and product.\n");
                    product += multiplicand;
                }
                Console.WriteLine("Product:\n" + FinishStringWithZeros(Convert.ToString(product, 2)) + "\n");
                Console.WriteLine("Shift multiplicand left");
                Console.WriteLine("Shift multiplier right");

                multiplicand <<= 1;
                multiplier >>= 1;
            }

            Console.WriteLine("\n" + FinishStringWithZeros(Convert.ToString(startMultiplicand, 2)) +
                "\nx\n" +
                FinishStringWithZeros(Convert.ToString(startMultiplier, 2)) +
                "\n=\n" +
                FinishStringWithZeros(Convert.ToString(product, 2)) + "\n");

            Console.WriteLine(startMultiplicand + " x " + startMultiplier + " = " + product);
        }

        static string FinishStringWithZeros(string val)
        {
            int count = 64 - val.Length;
            string head = "";
            for (int i = 0; i < count; ++i)
                head += "0";
            return head + val;
        }
    }
}
