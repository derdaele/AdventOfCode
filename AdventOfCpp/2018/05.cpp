#include "pch.h"
#include "05.h"
#include <string>
#include <iostream>
#include <algorithm>
#include <list>
#include <cctype>
#include <limits>

using namespace std;
using namespace _2018::_05;

bool should_destroy(char c1, char c2)
{
	return c1 != c2 && tolower(c1) == tolower(c2);
}

int fully_react(string &input, char blacklist)
{
	list<char> polymer(input.begin(), input.end());
	polymer.remove_if([&](char c) { return tolower(c) == tolower(blacklist); });
	list<char>::iterator first = polymer.begin(), second = ++polymer.begin();

	while (second != polymer.end())
	{
		if (should_destroy(*first, *second))
		{
			second = polymer.erase(second);
			first = polymer.erase(first);
			if (first == polymer.begin())
			{
				second++;
			}
			else
			{
				first--;
			}
		}
		else
		{
			first++;
			second++;
		}
	}

	return polymer.size();
}

void _2018::_05::solve(string input)
{
	cout << "Part 1 = " << fully_react(input, '\0') << endl;

	int min_size = numeric_limits<int>::max();
	int result;
	for (char blacklist = 'a'; blacklist < 'z'; blacklist++)
	{
		result = fully_react(input, blacklist);
		if (result < min_size)
		{
			min_size = result;
		}
	}
	cout << "Part 2 = " << min_size << endl;
}
