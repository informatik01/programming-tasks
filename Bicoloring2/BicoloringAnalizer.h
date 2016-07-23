#ifndef BICOLORINGANALIZER_H
#define BICOLORINGANALIZER_H

#include <string>
#include <vector>
using std::string;
using std::vector;

class BicoloringAnalizer {
public:
	BicoloringAnalizer();
	~BicoloringAnalizer();

	void analizeGraphs(string);

private:
	vector< vector<int> > graph;
	bool isBipartite(int);
};

#endif