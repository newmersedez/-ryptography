#pragma once

#include <bitset>
#include <array>

template <size_t size>
std::bitset<size> pBlock(const std::bitset<size>& bytes, const std::array<size_t, size>& rule)
{
	if (size == 0)
		throw std::invalid_argument("Bytas/rule array size cannot be 0");

	std::bitset<size> new_bytes;	
	for (size_t i = 0; i < size; ++i)
	{
		new_bytes[i] = bytes[rule[i]];
	}
	return new_bytes;
}
