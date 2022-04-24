#pragma once

#include <bitset>
#include <array>

template <size_t key_size, size_t round_key_size, size_t round_key_count>
class IExpandKey
{
public:
	typedef std::bitset<key_size> key;
	typedef std::bitset<round_key_size> round_key;
	typedef std::array<round_key, round_key_count> round_key_array;

    virtual ~IExpandKey()
	{};

protected:
    virtual round_key_array generateRoundKeys(const key& key) = 0;
};
