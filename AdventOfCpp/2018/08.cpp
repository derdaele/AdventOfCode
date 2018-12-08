#include "pch.h"
#define _CRT_SECURE_NO_WARNINGS
#include "08.h"
#include <vector>
#include <sstream>
#include <iostream>
#include <numeric>

using namespace std;
using namespace _2018::_8;

struct TreeNode
{
	vector<int> metadata;
	vector<TreeNode *> children;
};

TreeNode* parse_node(stringstream &input)
{
	TreeNode* result = new TreeNode();

	int children_count, metadata_count;
	input >> children_count >> metadata_count;

	for (int i = 0; i < children_count; i++)
	{
		result->children.push_back(parse_node(input));
	}

	for (int i = 0; i < metadata_count; i++)
	{
		int metadata;
		input >> metadata;
		result->metadata.push_back(metadata);
	}

	return result;
}

int sum_metadata(TreeNode *node)
{
	int res = std::accumulate(node->metadata.begin(), node->metadata.end(), 0);

	for (auto child : node->children)
	{
		res += sum_metadata(child);
	}

	return res;
}

int compute_real_checksum(TreeNode *node)
{
	int res = 0;

	if (node->children.size() == 0)
	{
		res += std::accumulate(node->metadata.begin(), node->metadata.end(), 0);
	}
	else
	{
		for (int idx : node->metadata)
		{
			idx--;

			if (idx >= 0 && idx < node->children.size())
			{
				res += compute_real_checksum(node->children[idx]);
			}
		}
	}

	return res;
}

void _2018::_8::solve(string input)
{
	auto input_stream = stringstream(input);
	TreeNode *license = parse_node(input_stream);

	cout << "Part 1 = " << sum_metadata(license) << endl;
	cout << "Part 2 = " << compute_real_checksum(license) << endl;
}
