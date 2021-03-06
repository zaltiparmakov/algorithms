// LZW.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <fstream>
#include <map>
#include <unordered_map>
#include <string>
#include <bitset>

using namespace std;

void compression(string inputFile, int dictSize);
void decompression(string inputFile, int dictSize);

#define CHARACTERS 256

int main(int argc, char** argv)
{
	if (argc != 4) {
		cout << "Usage: vaja4 <opcija> <max velikost slovarja> <vhodna datoteka>";
		return -1;
	}

	char option = argv[1][0];
	int dictSize = atoi(argv[2]);
	string inputFile = argv[3];

	if (option == 'c') {
		compression(inputFile, dictSize);
	}
	else if (option == 'd') {
		decompression(inputFile, dictSize);
	}
	else {
		cout << "Only c and d arguments are allowed.";
		return -1;
	}

	cout << "Operation was successfull" << endl;

	getchar();
	return 0;
}

void compression(string inputFile, int dictSize) {
	int code_length = 8;
	int byteSize = 1;
	// dictionary data structure maps ASCII character into its encoded decimal representation
	unordered_map<string, int> dict;
	string outputFile = "out-compressed.bin";

	ifstream infile;
	ofstream outfile;

	// open as binary files
	infile.open(inputFile, ios::binary | ios::in);
	outfile.open(outputFile, ios::binary | ios::out);

	if (!infile.is_open()) {
		cout << "File not found";
		return;
	}
	if (!outfile.is_open()) {
		cout << "Can't open file for writing..";
		return;
	}

	// fill the dictionary with ASCII characters starting from 0 - 255 (256 ASCII characters)
	for (int i = 0; i < CHARACTERS; i++) {
		dict[string(1, i)] = i;
	}

	string buffer;
	string word;
	char symbol;
	while (!infile.eof()) {
		infile.read(&symbol, 1);
		// read sequentially every character from the file, and append it to the word string
		string newString = word + symbol;
		// check if new string, which represents current character in file appended to previously founded charater(s)
		// if word it is contained in dictionary, append character to the word string
		if (dict.count(newString)) {
			word += symbol;
		}
		// if new string does not exist in the dictionary, write Value of Key (=encoded value)
		else {
			// Key is ASCII character, while Value is decimal representation
			outfile.write((char*)&dict[word], 1);
			//buffer.append((char*)dict[word]);
			// insert it into dictionary
			dict[newString] = dictSize++;
			// reset word string to contain just new encoded character from file
			word = symbol;
		}

		// dinamically adapt on size of bits
		code_length < ceil(log2(dict.size())) ? code_length++ : code_length;
		byteSize = (int)ceil((float)code_length / 8);
	}

	// release resources
	infile.close();
	outfile.close();
}

void decompression(string inputFile, int dictSize) {
	int code_length = 8;
	int byteSize = 1;
	// reverse dictionary, where Key is decimal representation of character, and character(s) is Value
	map<int, string> dict;
	string outputFile = "out-decompressed.bin";

	ifstream infile;
	ofstream outfile;

	// open as binary files
	infile.open(inputFile, ios::binary | ios::in);
	outfile.open(outputFile, ios::binary | ios::out);

	if (!infile.is_open()) {
		cout << "File not found";
		return;
	}
	if (!outfile.is_open()) {
		cout << "Can't open file for writing..";
		return;
	}

	// fill the dictionary with ASCII characters starting from 0 - 255 (256 ASCII characters)
	for (int i = 0; i < CHARACTERS; i++) {
		dict[i] = string(1, i);
	}

	string word, entry;
	char code;
	while (!infile.eof()) {
		infile.read(&code, 1);
		if (dict.count(code)) {
			entry = dict[(int)code];
			dict[dict.size()] = word + entry[0];
		}
		else {
			entry = word + word[0];
			dict[dict.size()] = entry;
		}
		outfile.write((char*)&entry, 2);
		word = entry;

		code_length < ceil(log2(dict.size())) ? code_length++ : code_length;
		byteSize = (int)ceil((float)code_length / 8);
	}

	// release resources
	infile.close();
	outfile.close();
}
