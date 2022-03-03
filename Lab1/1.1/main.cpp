#include <iostream>
#include <bitset>
#include <exception>
#include <vector>
#include <algorithm>

template <size_t MAX>
void	bitwiseShuffle(std::bitset<MAX> bytes, std::vector<size_t> rule)
{
	size_t	temp;

	for (size_t i = 0; i < MAX; i++)
	{
		if (i >= MAX)
			throw std::out_of_range("Index error");
		else if (rule[i] < rule[rule[i]])
		{
			temp = bytes[i];
			bytes[i] = bytes[rule[i]];
			bytes[rule[i]] = temp;
		}
	}
}

int main()
{
	std::bitset<32>	bytes {144};
	std::vector<size_t>	rule{4,	1,	2,	3,	0,	5,	6,	7,
							8,	9,	10,	11,	12,	13,	14,	15,
							16,	17,	18,	19,	20,	21,	22,	23,
							24,	25,	26,	27,	28,	29,	30,	31};
	
	std::cout << "Before shuffle:\t";
	std::cout << bytes << std::endl;
	
	bitwiseShuffle(bytes, rule);
	std::cout << "After shuffle:\t";
	std::cout << bytes << std::endl;
	return 0;
}