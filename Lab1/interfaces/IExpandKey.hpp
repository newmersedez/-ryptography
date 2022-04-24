#pragma once

#include <bitset>
#include <array>

template <size_t bitset_size, size_t key_size, size_t key_count>
class IExpandKey
{
public:
	typedef std::bitset<bitset_size> bitset;
	typedef std::bitset<key_size> key;
	typedef std::array<key, key_count> key_array;

    virtual ~IExpandKey()
	{};

protected:
    virtual key_array generateRoundKeys(const key& key) = 0;
};
