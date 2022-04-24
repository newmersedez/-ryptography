#pragma once

#include <bitset>
#include <array>

template <size_t key_size, size_t round_key_size, size_t round_key_count>
class IExpandKey
{
public:
	typedef std::bitset<key_size> key_type;
	typedef std::bitset<round_key_size> round_key_type;
	typedef std::array<round_key_type, round_key_count> round_key_array_type;

    virtual ~IExpandKey()
	{};
	
    virtual round_key_array_type generateRoundKeys(const key_type& key) = 0;
};
