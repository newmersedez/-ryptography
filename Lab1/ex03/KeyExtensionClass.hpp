#pragma once

#include <array>
#include <bitset>

template <size_t key_size, size_t key_count>
class KeyExtensionClass
{
typedef std::bitset<key_size> bitset;
typedef std::array<key_size, key_count> bitset_array;

public:
	virtual ~KeyExtensionClass();
	virtual bitset_array keyExtension(const bitset& bitset) = 0;
};
