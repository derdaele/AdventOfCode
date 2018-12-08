#include "pch.h"
#define _CRT_SECURE_NO_WARNINGS
#include "07.h"
#include <string>
#include <iostream>
#include <algorithm>
#include <numeric>
#include <list>
#include <cctype>
#include <limits>
#include <sstream>
#include <cstdio>
#include <unordered_map>
#include <unordered_set>
#include <queue>
#include <cassert>

using namespace std;
using namespace _2018::_7;

std::unordered_multimap<char, char> parse(std::string input)
{
	std::unordered_multimap<char, char> result;
	stringstream ss(input);
	string line;

	while (std::getline(ss, line))
	{
		char from, to;
		sscanf(line.c_str(), "Step %c must be finished before step %c can begin.", &from, &to);
		result.emplace(from, to);
	}

	return result;
}

unordered_set<char> find_roots(std::unordered_multimap<char, char> &graph)
{
	unordered_set<char> candidates, blacklist;

	for (auto& elem : graph)
	{
		if (blacklist.find(elem.first) == blacklist.end())
		{
			candidates.emplace(elem.first);
		}

		blacklist.emplace(elem.second);
		candidates.erase(elem.second);
	}

	return candidates;
}

std::string topological_min_sort(std::unordered_multimap<char, char> &graph)
{
	auto roots = find_roots(graph);
	priority_queue<char, vector<char>, std::greater<char>> visit_next(roots.begin(), roots.end());
	unordered_set<char> visited;
	string result;

	while (visit_next.size() > 0)
	{
		char cur = visit_next.top();
		visit_next.pop();

		if (visited.find(cur) != visited.end())
		{
			continue;
		}

		visited.emplace(cur);
		result.push_back(cur);

		auto neighbors = graph.equal_range(cur);
		for (auto it = neighbors.first; it != neighbors.second; it++)
		{
			bool fulfilled = true;

			for (auto req = graph.begin(); req != graph.end(); req++)
			{
				if (req->second == it->second && it->first != req->first && visited.find(req->first) == visited.end())
				{
					fulfilled = false;
					break;
				}
			}

			if (fulfilled)
			{
				visit_next.emplace(it->second);
			}
		}
	}

	return result;
}

const int NUM_WORKER = 5;
const int BASE_TASK_DURATION = 60;

constexpr int duration(char c)
{
	return BASE_TASK_DURATION + (c - 'A') + 1;
}

int min_running_time(std::unordered_multimap<char, char> &graph)
{
	vector<int> workers(NUM_WORKER, 0);
	vector<char> jobs(NUM_WORKER, '.');
	auto roots = find_roots(graph);
	priority_queue<char, vector<char>, std::greater<char>> waiting(roots.begin(), roots.end());
	unordered_set<char> visited;
	int result = 0;

	int cur = 0;
	while (waiting.size() > 0 && cur < workers.size())
	{
		char root = waiting.top();
		waiting.pop();
		workers[cur] = duration(root);
		jobs[cur] = root;
		cur++;
	}

	while (std::accumulate(workers.begin(), workers.end(), 0) > 0)
	{
		// Do the work
		auto min = workers.end();
		for (auto it = workers.begin(); it != workers.end(); it++)
		{
			if (*it > 0)
			{
				if (min == workers.end() || *min > *it)
				{
					min = it;
				}
			}
		}

		result += *min;

		int min_val = *min;

		for (int i = 0; i < workers.size(); i++)
		{
			if (workers[i] == 0) continue;

			workers[i] -= min_val;

			if (workers[i] == 0)
			{
				visited.emplace(jobs[i]);
				auto neighbors = graph.equal_range(jobs[i]);
				for (auto it = neighbors.first; it != neighbors.second; it++)
				{
					bool fulfilled = true;

					for (auto req = graph.begin(); req != graph.end(); req++)
					{
						if (req->second == it->second && it->first != req->first && visited.find(req->first) == visited.end())
						{
							fulfilled = false;
							break;
						}
					}

					if (fulfilled)
					{
						waiting.emplace(it->second);
					}
				}
				jobs[i] = '.';
			}
		}

		for (int i = 0; i < workers.size(); i++)
		{
			if (waiting.size() == 0) break;

			if (workers[i] == 0)
			{
				// Assign work
				char task;
				do
				{
					task = waiting.top();
					waiting.pop();
				}
				while (waiting.size() > 0 && visited.find(task) != visited.end());

				if (visited.find(task) == visited.end())
				{
					workers[i] = duration(task);
					jobs[i] = task;
				}
			}
		}
	}

	return result;
}

void _2018::_7::solve(string input)
{
	std::unordered_multimap<char, char> graph = parse(input);
	auto res1 = topological_min_sort(graph);
	cout << "Part 1 = " << res1 << endl;

	auto res2 = min_running_time(graph);
	cout << "Part 2 = " << res2 << endl;
}
