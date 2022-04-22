#pragma once

#include <array>
#include <bitset>

template <size_t bitset_size, size_t bitset_count>
class KeyExtensionClass
{
typedef std::bitset<bitset_size> bitset;
typedef std::array<bitset_size, bitset_count> bitset_array;

public:
	virtual ~KeyExtensionClass();
	virtual bitset_array keyExtension(const bitset& bitset) = 0;
};
