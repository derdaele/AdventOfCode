// AdventOfCpp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include "2018/2018.h"
#include <iostream>
#include <istream>
#include <fstream>
#include <streambuf>
#include <string>
#include <sstream>
#include <iomanip>

using namespace std;

namespace current_day = _2018::_8;

int main()
{
	stringstream ss;
	ss << current_day::year() << "/" << setfill('0') << setw(2) << current_day::day() << ".txt";
	ifstream ifs(ss.str());
	std::string input((std::istreambuf_iterator<char>(ifs)),std::istreambuf_iterator<char>());

	current_day::solve(input);
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started:
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
