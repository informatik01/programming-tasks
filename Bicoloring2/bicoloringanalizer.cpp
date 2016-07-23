#include <fstream>
#include <iostream>
#include <stdexcept>
#include <queue>
#include "BicoloringAnalizer.h"

BicoloringAnalizer::BicoloringAnalizer(){}
BicoloringAnalizer::~BicoloringAnalizer(){}

void BicoloringAnalizer::analizeGraphs(string fileName)
{
	int numOfVertices;
	int numOfEdges;
	int vertex;
	int adjacentVertex;

	std::ifstream input;
	input.exceptions(std::ios_base::failbit | std::ios_base::badbit);
	try {
		input.open(fileName, std::ios_base::in);

		input >> numOfVertices;

		while (numOfVertices != 0) {
			if (numOfVertices < 2 || numOfVertices > 199) {
				input.close();
				throw std::runtime_error("Wrong number of nodes. Must be \"1 < nodes < 200\"\n");
			}

			input >> numOfEdges;
			if (numOfEdges < 1) {
				input.close();
				throw std::runtime_error("Wrong data in file.\n");
			}

			graph.clear();
			graph.resize(numOfVertices);

			for (int i = 0; i < numOfEdges; i++) {
				input >> vertex >> adjacentVertex;
				if (vertex >= 0 && vertex < numOfVertices && adjacentVertex >= 0 && adjacentVertex < numOfVertices)
					graph[vertex].push_back(adjacentVertex);
				else {
					input.close();
					throw std::runtime_error("Wrong vertex label. Must be \"0 <= vertex_label < numOfVertices\"\n");
				}
			}

			if (isBipartite(numOfVertices))
				std::cout << "BICOLORABLE." << std::endl;
			else
				std::cout << "NOT BICOLORABLE." << std::endl;

			input >> numOfVertices;
		}
	}
	catch (std::ios_base::failure) {
		std::cerr << "Error reading file: ";
		input.close();
		throw;
	}
	input.close();
}

bool BicoloringAnalizer::isBipartite(int n)
{
	int startVertex = 0;
	int currentColor = 0;
	std::queue<int> queueOfVertices;
	std::vector<bool> visited(n, false);
	std::vector<int> vertexColor(n, 1);

	queueOfVertices.push(startVertex);
	visited[startVertex] = true;
	vertexColor[startVertex] = currentColor;

	while (!queueOfVertices.empty()) {
		int currentVertex = queueOfVertices.front();
		queueOfVertices.pop();

		for (size_t i = 0; i < graph[currentVertex].size(); i++) {
			int adjacentVertex = graph[currentVertex][i];
			if (!visited[adjacentVertex]) {
				visited[adjacentVertex] = true;
				vertexColor[adjacentVertex] = ~currentColor;
				queueOfVertices.push(adjacentVertex);
			}
			else {
				if (vertexColor[currentVertex] == vertexColor[adjacentVertex])
					return false;
			}
		}
		currentColor = ~currentColor;
	}

	return true;
}