#pragma once

#include <bitset>
#include <array>

template <size_t size>
std::bitset<size> pBlock(const std::bitset<size>& bitset, const std::array<size_t, size>& rule)
{
	if (size == 0)
		throw std::invalid_argument("Bytes/pBlock array size cannot be 0");

	std::bitset<size> new_bytes;	
	for (size_t i = 0; i < size; ++i)
	{
		new_bytes[i] = bitset[rule[i]];
	}
	return new_bytes;
}
