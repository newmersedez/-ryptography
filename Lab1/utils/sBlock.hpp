#pragma once

#include <map>
#include <bitset>
#include <algorithm>

template<size_t size>
struct bitset_less
{
	bool operator()(const std::bitset<size>& left, const std::bitset<size>& right) const
	{
		return (left.to_ullong() < right.to_ullong());
	}
};

template <size_t s_old_size, size_t s_new_size>
using rule_map =
    std::map<std::bitset<s_old_size>, std::bitset<s_new_size>, bitset_less<s_old_size>>;

template <size_t size, size_t s_old_size, size_t s_new_size>
std::bitset<size / s_old_size * s_new_size>
    sBlock(const std::bitset<size>& bitset, rule_map<s_old_size, s_new_size>& rule)
{
	if (size == 0 || s_old_size == 0 || s_new_size == 0)
	{
		throw std::invalid_argument("Bytes/sBlocks size cannot be 0");
	}
	if (size % s_old_size != 0)
	{
		throw std::invalid_argument("Invalid size of sBlock");
	}
	
    const int s_blocks_count = size / s_old_size;
    std::bitset<size / s_old_size * s_new_size> new_bytes;
    std::bitset<s_old_size> key_block;

    for (int block_number = 0; block_number < s_blocks_count; ++block_number)
    {
        for (int i = 0; i < s_old_size; ++i)
        {
            key_block[i] = bitset[block_number * s_old_size + i];
        }
        for (int i = 0; i < s_new_size; ++i)
        {
            new_bytes[block_number * s_new_size + i] = rule[key_block][i];
        }
    }
    return new_bytes;
}
