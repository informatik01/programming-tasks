/* The solution for the test task "Bicoloring".
*  Task decription can be found at:
*  http://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=8&category=12&page=show_problem&problem=945
*  
*  Author: Levan Kekelidze
*  e-mail: informatik0101@gmail.com
*/


#include <iostream>
#include <vector>
#include "BicoloringAnalizer.h"

int main(int argc, char* argv[])
{
	std::string inputFile;
	if (argc > 1)
		inputFile = argv[1];
	else {
		std::cerr << "No input file provided.\nUsage: program.exe <input_file_name>" << std::endl;
		exit(EXIT_FAILURE);
	}

	BicoloringAnalizer ba;
	try {
		ba.analizeGraphs(inputFile);
	}
	catch (std::runtime_error &e) {
		std::cerr << e.what() << std::endl;
		std::cerr << "Terminating operation." << std::endl;
		exit(EXIT_FAILURE);
	}

	return 0;
}