using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath1 = @"kazan.txt";
            var filePath2 = @"kavkaz.txt";
            var filePath3 = @"mane.txt";

            Console.WriteLine("\n--------1ST FILE----------\n");
            Compute(filePath1);
            Console.WriteLine("\n--------2ND FILE----------\n");
            Compute(filePath2);
            Console.WriteLine("\n--------3RD FILE----------\n");
            Compute(filePath3);
        }

        static void Compute(string filePath)
        {
            AmountAndEntropy(filePath);
            Console.WriteLine("{0} -> Base64\n", filePath);
            var fileText = File.ReadAllText(filePath, Encoding.GetEncoding(1251));

            var array = Encoding.UTF8.GetBytes(fileText);
            var encoder = new MyEncoder(array);
            var encodedFilePath = filePath.Split('.')[0] + "Base64.txt";
            var archiveFilePath = filePath.Split('.')[0] + ".gz";


            if (!File.Exists(encodedFilePath))
            {
                using (var fileStream = File.Create(encodedFilePath))
                {
                    var info = new UTF8Encoding(true).GetBytes(encoder.getEncoded());
                    fileStream.Write(info, 0, info.Length);
                }
            }
            Console.WriteLine("\n--------ENCODED FILE----------\n");
            AmountAndEntropy(encodedFilePath);

            Console.WriteLine("\n--------ARCHIVE FILE----------\n");
            AmountAndEntropy(archiveFilePath);

            Console.WriteLine("\n------------------------------------------------------\n");
        }

        static void AmountAndEntropy(string filePath)
        {
            var chars = new List<char>();
            var num = new List<int>();
            double sumOfsym = 0;
            double frequency = 0;
            double entropy = 0;
            using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(1251)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int count = 1;
                    foreach (char ch in line)
                    {
                        if (chars.Count == 0)
                        {
                            chars.Add(ch);
                            num.Add(count);
                        }
                        else
                        {
                            for (int i = 0; i < chars.Count; i++)
                            {
                                if (chars[i] == ch)
                                {
                                    num[i] += count;
                                    count *= -1;
                                }
                            }
                            if (count > 0)
                            {
                                chars.Add(ch);
                                num.Add(count);
                            }

                        }
                        count = 1;
                    }
                }

                for (int i = 0; i < chars.Count; i++)
                    sumOfsym += num[i];

                Console.WriteLine(filePath);
                Console.WriteLine("\nGeneral amount of symbols in the text  {0}", sumOfsym);
                for (int i = 0; i < chars.Count; i++)
                {
                    frequency = num[i] / sumOfsym;

                    Console.WriteLine("Frequency of symbol(max 1) '{0}' = {1:f6}", chars[i], frequency);

                    if (frequency != 0)
                        entropy += frequency * (Math.Log(1 / frequency, 2));

                }
                Console.WriteLine("\nAverage entropy in this text -> {0:f3} bit", entropy);
                Console.WriteLine("Amount of information in this text -> {0:f3} byte\n", (entropy * sumOfsym) / 8);
            }
        }
    }

    public class MyEncoder
    {
        private readonly byte[] arrayToBeEncoded;
        private readonly int length1;
        private readonly int length2;
        private readonly int block;
        private readonly int padding;
        public MyEncoder(byte[] arr)
        {
            arrayToBeEncoded = arr;
            length1 = arr.Length;
            if ((length1 % 3) == 0)
            {
                padding = 0;
                block = length1 / 3;
            }
            else
            {
                padding = 3 - (length1 % 3);
                block = (length1 + padding) / 3;
            }
            length2 = length1 + padding;
        }

        public char[] getEncoded()
        {
            byte[] source;
            source = new byte[length2];
            for (int x = 0; x < length2; x++)
            {
                if (x < length1)
                {
                    source[x] = arrayToBeEncoded[x];
                }
                else
                {
                    source[x] = 0;
                }
            }

            byte byte1, byte2, byte3;
            byte temp0, temp1, temp2, temp3, temp4;
            var stack = new byte[block * 4];
            var result = new char[block * 4];
            for (int x = 0; x < block; x++)
            {
                byte1 = source[x * 3];
                byte2 = source[x * 3 + 1];
                byte3 = source[x * 3 + 2];

                temp1 = (byte)((byte1 & 252) >> 2);

                temp0 = (byte)((byte1 & 3) << 4);
                temp2 = (byte)((byte2 & 240) >> 4);
                temp2 += temp0;

                temp0 = (byte)((byte2 & 15) << 2);
                temp3 = (byte)((byte3 & 192) >> 6);
                temp3 += temp0;

                temp4 = (byte)(byte3 & 63);

                stack[x * 4] = temp1;
                stack[x * 4 + 1] = temp2;
                stack[x * 4 + 2] = temp3;
                stack[x * 4 + 3] = temp4;

            }

            for (int x = 0; x < block * 4; x++)
            {
                result[x] = toResult(stack[x]);
            }
            

            switch (padding)
            {
                case 0: break;
                case 1: result[block * 4 - 1] = '='; break;
                case 2:
                    result[block * 4 - 1] = '=';
                    result[block * 4 - 2] = '=';
                    break;
                default: break;
            }



            return result;
        }

        private char toResult(byte b)
        {
            var symbols = new char[64]
                {  'A','B','C','D','E','F','G','H','I','J','K','L','M',
            'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m',
            'n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0','1','2','3','4','5','6','7','8','9','+','/'};

            if ((b >= 0) && (b <= 63))
            {
                return symbols[(int)b];
            }
            else
            {
                return ' ';
            }
        }
    }  
}
