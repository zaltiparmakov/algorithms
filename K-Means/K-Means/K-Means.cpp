// K-Means.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <fstream>
#include <string>
#include <map>
#include <vector>
#include <sstream>
#include <ctime>

using namespace std;

float calculateDistance(struct point point, struct cluster cluster);
void updateCentroids(vector<struct point> points, vector<struct cluster> clusters);

struct cluster {
	float x, y;
};

struct point {
	float x, y;
	int cluster;
};

int number_of_points = 0;
int number_of_clusters = 0;

int main(int argc, char** argv)
{
	if (argc != 4) {
		cout << "Wrong number of arguments";
		return -1;
	}

	int iterations = atoi(argv[1]);
	number_of_clusters = atoi(argv[2]);
	string input_filename = argv[3];
	string output_filename = "out.txt";
	ifstream input_file;
	ofstream output_file;

	input_file.open(input_filename);
	if (!input_file.is_open()) {
		cout << "Cant open file for reading";
		return -1;
	}

	// Get all points from the file
	int kMax = 0;
	vector<struct point> points;
	struct point point;
	string line;
	while(getline(input_file, line)) {
		istringstream in(line);

		float x, y;
		in >> point.x >> point.y;

		if (kMax < point.x) {
			kMax = point.x;
		}

		point.cluster = NULL;
		points.push_back(point);
		number_of_points++;
	}
	input_file.close();

	// Initialize K-Means centroids
	vector<struct cluster> clusters;
	struct cluster cluster;
	srand(time(NULL));
	for (int i = 0; i < number_of_clusters; i++) {
		cluster.x = rand() % kMax;
		cluster.y = rand() % kMax;
		clusters.push_back(cluster);
	}

	map<int, int> points_in_cluster;
	int index = 0;
	while (iterations > 0) {
		for(int p = 0; p < number_of_points; p++) {
			vector<float> distances;
			// Calculate all distances from certain point to centroids
			for (int c = 0; c < number_of_clusters; c++) {
				distances.push_back(calculateDistance(points.at(p), clusters.at(c)));
			}

			// Find minimum distance from particular point to cluster centroid, and bind to that centroid
			// Every point belongs to closest K mean
			float min = distances[0];
			for (int i = 0; i < number_of_clusters; i++) {
				if (distances[i] < min) {
					min = distances[i];
					points[p].cluster = i;
				}
				else {
					points[p].cluster = 0;
				}
			}
		}

		// calculate new position of K centroids, where centroid location is average of all close points
		updateCentroids(points, clusters);
		iterations--;
	}

	output_file.open(output_filename);
	if (!output_file.is_open()) {
		cout << "Cant open file for writing";
		return -1;
	}

	for (int i = 0; i < number_of_points; i++) {
		output_file << i << " " << points.at(i).cluster << "\n";
	}
	output_file.close();

	cout << "Operation was successfull" << endl;
	getchar();

    return 0;
}

// calculate euclidean distance
float calculateDistance(struct point point, struct cluster cluster) {
	return sqrt(pow((point.x - cluster.x), 2) + pow((point.y - cluster.y), 2));
}

void updateCentroids(vector<struct point> points, vector<struct cluster> clusters) {
	for (int c = 0; c < number_of_clusters; c++) {
		float avgX = 0, avgY = 0;
		for (int p = 0; p < number_of_points; p++) {
			int num_points = 1;
			if (points[p].cluster == c) {
				avgX = (avgX + points[p].x) / num_points;
				avgY = (avgY + points[p].y) / num_points;
				num_points++;
			}
		}
		clusters[0].x = avgX;
		clusters[0].y = avgY;
	}
}