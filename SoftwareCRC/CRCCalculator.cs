/*  The solution for the test task "Software CRC".
 *  Task decription can be found at:
 *  http://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=8&category=3&page=show_problem&problem=64
 *  
 *  Author: Levan Kekelidze
 *  e-mail: informatik0101@gmail.com
 */

using System;
using System.Text;
using System.Numerics;

namespace SoftwareCRC
{
    /// <summary>
    /// This simple class calculates CRC value for the message to be sent.
    /// </summary>
    /// <remarks>
    /// Sample input file (input.txt) can be found in the project directory
    /// </remarks>
    class CRCCalculator
    {
        private const ushort genValue = 34943;     //the generator value

        /// <summary>
        /// This method calculates CRC
        /// </summary>
        /// <param name="number">Byte array containing input message</param>
        /// <returns>Calculated CRC value</returns>
        public static ushort CalculateCRC(byte[] number)
        {
            BigInteger bigNum = new BigInteger(number);
            int forCRC = 1 << 16;   //CRC value needs to bytes
            bigNum *= forCRC;       //add two bytes for the CRC value

            ushort crcValue = (ushort)(bigNum % genValue);
            if (crcValue == 0)
                return crcValue;
            else
                return (ushort)(genValue - crcValue);  // if crcValue != 0, calculate, how many to add to bigNum
                                                       // in order to get "bigNum % genValue == 0".
        }

        /// <summary>
        /// This method outputs hexadecimal CRC value in the form shown
        /// at the task description page.
        /// </summary>
        /// <param name="crc">CRC value in decimal</param>
        public static void PrintCRC(ushort crc)
        {
            byte[] arr = BitConverter.GetBytes(crc);
            Array.Reverse(arr);     //because BitConverter.GetBytes() returns bytes in little-endian

            foreach (byte value in arr)
                Console.Write("{0:X2} ", value);
            Console.WriteLine();
        }

        /// <summary>
        /// Main program
        /// </summary>
        /// <param name="args">Program arguments(not used)</param>
        /// <remarks>
        /// Program reads message from standard input. Either type message at the command line
        /// or use redirection operator to read message from input file(must be ASCII).
        /// </remarks>
        //  Redirection operator usage example: softwareCRC.exe < <input_file_name>
        static void Main(string[] args)
        {
            string line;
            byte[] message;
            ushort crc;

            try
            {
                while ((line = Console.In.ReadLine()) != "#")
                {
                    message = Encoding.ASCII.GetBytes(line); //message contains ASCII characters
                    if (message.Length > 1024)
                        continue;   //discard message that contains more than 1024 ASCII characters
                                    //(according to task description)

                    Array.Reverse(message);  //BigInteger requires byte array to appear in little-endian order
                    crc = CalculateCRC(message);
                    PrintCRC(crc);
                }
            }
            catch (Exception e) //
            {
                Console.WriteLine("Terminating operation. Error reading from input." +
								Environment.NewLine + e.ToString());
            }
        }
    }
}
