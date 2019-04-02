using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Part3
    {
        public static void Do()
        {
            Console.WriteLine("input first number");
            var num1 = float.Parse(Console.ReadLine());
            Console.WriteLine("input second number");
            var num2 = float.Parse(Console.ReadLine());
            FloatingPoint(num1, num2);
        }
        public static void FloatingPoint(float first, float second)
        {

            long firstBits = GetFloatBits(first),
                secondBits = GetFloatBits(second);

            int sign1 = (int)((firstBits >> 31) & 1),
                sign2 = (int)((secondBits >> 31) & 1);

            int expo1 = (int)((firstBits >> 23) & 255),
                expo2 = (int)((secondBits >> 23) & 255);

            long mantissa1 = firstBits & ((int)Math.Pow(2, 23) - 1),
                mantissa2 = secondBits & ((int)Math.Pow(2, 23) - 1);

            long mantissaMultiplication = ((1 << 23) | mantissa1) * ((1 << 23) | mantissa2);

            Console.WriteLine("Mantissa multiplication:");
            Console.WriteLine("mantissa1 " + FinishStringWithZeros(Convert.ToString(mantissa1, 2), 24) + "\nx");
            Console.WriteLine("mantissa2 " + FinishStringWithZeros(Convert.ToString(mantissa2, 2), 24) + "\n=");
            Console.WriteLine("mantissa " + FinishStringWithZeros(Convert.ToString(mantissaMultiplication, 2), 48));

            mantissaMultiplication >>= 23;
            int expoAddition = ((1 << 24) & mantissaMultiplication) > 0 ? 1 : 0;
            Console.WriteLine();
            if (expoAddition > 0)
            {
                Console.WriteLine("Normalization:");
                mantissaMultiplication >>= 1;
                mantissaMultiplication &= ~(1 << 23);
            }
            else
            {
                Console.WriteLine("Normalization is not needed:");
                mantissaMultiplication &= ~(3 << 23);
            }
            Console.WriteLine();
            Console.WriteLine(FinishStringWithZeros(Convert.ToString(mantissaMultiplication, 2), 23));
            Console.WriteLine();

            int sign = 1 & (sign1 + sign2);
            Console.WriteLine("Sign:");
            Console.WriteLine(sign1.ToString() + " XOR " + sign2 + " = " + sign);

            Console.WriteLine();
            Console.WriteLine("Exponent: ");
            Console.WriteLine("exp1 " + FinishStringWithZeros(Convert.ToString(expo1, 2), 8) + " ( " + expo1 + " ) \n+");
            Console.WriteLine("exp2 " + FinishStringWithZeros(Convert.ToString(expo2, 2), 8) + " ( " + expo2 + " )");
            int expo = expo1 + expo2 - 127 + expoAddition;
            Console.WriteLine("- 127 + " + expoAddition + "\n= " + FinishStringWithZeros(Convert.ToString(expo, 2), 8) + " ( " + expo + " ) ");


            int resultMask = (int)mantissaMultiplication;
            resultMask |= expo << 23;
            resultMask |= sign << 31;

            byte[] bytes = new byte[4];
            bytes[0] = (byte)(resultMask & 255);
            bytes[1] = (byte)((resultMask >> 8) & 255);
            bytes[2] = (byte)((resultMask >> 16) & 255);
            bytes[3] = (byte)((resultMask >> 24) & 255);

            float result = BitConverter.ToSingle(bytes, 0);
            Console.WriteLine("Result: " + FinishStringWithZeros(Convert.ToString(resultMask, 2), 32) + " ( " + result + " ) ");
        }
        static int GetFloatBits(float num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            int res = 0;
            res |= bytes[0];
            res |= bytes[1] << 8;
            res |= bytes[2] << 16;
            res |= bytes[3] << 24;
            return res;
        }
        static string FinishStringWithZeros(string val, int bitcount)
        {
            int count = bitcount - val.Length;
            string head = "";
            for (int i = 0; i < count; ++i)
                head += "0";
            return head + val;
        }
    }
}
