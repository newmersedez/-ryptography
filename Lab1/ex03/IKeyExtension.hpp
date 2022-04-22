#pragma once

#include <array>
#include <bitset>

template <size_t bitset_size, size_t bitset_count>
class IKeyExtension
{
typedef std::bitset<bitset_size> bitset;
typedef std::array<bitset, bitset_count> bitset_array;

public:
	virtual ~IKeyExtension();
	virtual bitset_array keyExtension(const bitset& bitset) = 0;
};
