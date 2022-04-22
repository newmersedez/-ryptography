#include <iostream>
#include "sBlock.hpp"

int main()
{
	const size_t old_size = 2;
	const size_t new_size = 3;

	std::bitset<8> bytes(201);
	rule_map<old_size, new_size> rule{
		{std::bitset<old_size>("00"), std::bitset<new_size>("000")},
		{std::bitset<old_size>("01"), std::bitset<new_size>("001")},
		{std::bitset<old_size>("10"), std::bitset<new_size>("010")},
		{std::bitset<old_size>("11"), std::bitset<new_size>("011")}
	};
	std::bitset<12> new_bytes = sBlock(bytes, rule);
	
	std::cout << "Old bytes: " << bytes << std::endl;
	std::cout << "New bytes: " << new_bytes << std::endl;
	return 0;
}
