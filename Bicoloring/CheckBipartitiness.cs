/*  The solution for the test task "Bicoloring".
 *  Task decription can be found at:
 *  http://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=8&category=12&page=show_problem&problem=945
 *  
 *  Author: Levan Kekelidze
 *  e-mail: informatik0101@gmail.com
 */

using System;

namespace Bicoloring
{
    /// <summary>
    /// Main program.
    /// </summary>
    /// <remarks>
    /// Sample input file (input.txt) can be found in the "Debug" and "Release" folders.
    /// </remarks>
    class CheckBipartitiness
    {
        static void Main(string[] args)
        {
            string fileName = "";
            if (args.Length > 0)
                fileName = args[0];
            else
            {
                Console.Error.WriteLine("No input file provided.");
                Console.Error.WriteLine("Usage: program.exe <input_file_name>");

                return;
            }

            BicoloringAnalizer ba = new BicoloringAnalizer();
            try
            {
                ba.AnalizeGraphs(fileName);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine("Terminating operation.");
                Environment.Exit(1);
            }
        }
    }
}
