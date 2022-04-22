#include <iostream>
#include <random>
#include "pBlock.hpp"

int main()
{
	const size_t size = 8;
	const int test_count = 3;
	
	std::random_device device;
    std::mt19937 generator(device());
    std::uniform_int_distribution<int> distribution(1, 1024);

	for (int i = 0; i < test_count; ++i)
	{
		std::bitset<size> bytes(distribution(generator));
		std::array<size_t, size> rule{0, 1, 2, 3, 4, 5, 6, 7};
		std::bitset<size> new_bytes = pBlock(bytes, rule);

		std::cout << "Old bytes array: " << bytes << std::endl;
		std::cout << "New bytes array: " << new_bytes << "\n" << std::endl;
	}
	
	return 0;
}