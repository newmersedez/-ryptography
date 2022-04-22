#pragma once

#include <bitset>
#include <map>
#include <algorithm>

template<size_t bytes_size>
struct bitset_less
{
	bool operator()(const std::bitset<bytes_size>& left,
		const std::bitset<bytes_size>& right) const
	{
		return (left.to_ullong() < right.to_ullong());
	}
};

template <size_t s_old_size, size_t s_new_size>
using rule_map =
	std::map<std::bitset<s_old_size>, std::bitset<s_new_size>, bitset_less<s_old_size>>;

template <size_t bytes_size, size_t s_old_size, size_t s_new_size>
std::bitset<bytes_size / s_old_size * s_new_size> 
	sBlock(const std::bitset<bytes_size>& bytes, rule_map<s_old_size, s_new_size>& rule)
{
	if (bytes_size == 0 || s_old_size == 0 || s_new_size == 0)
		throw std::invalid_argument("Bytes/sBlocks size cannot be 0");
	if (bytes_size % s_old_size != 0)
		throw std::invalid_argument("Invalid size of sBlock");

    const int s_blocks_count = bytes_size / s_old_size;
    std::bitset<bytes_size / s_old_size * s_new_size> new_bytes;
    std::bitset<s_old_size> key_block;

    for (int block_number = 0; block_number < s_blocks_count; ++block_number)
    {
        for (int s_block_index = 0; s_block_index < s_old_size; ++s_block_index)
        {
            key_block[s_block_index] = bytes[block_number * s_old_size + s_block_index];
        }
        for (int s_block_index = 0; s_block_index < s_new_size; ++s_block_index)
        {
            new_bytes[block_number * s_new_size + s_block_index] = rule[key_block][s_block_index];
        }
    }
    return new_bytes;
}
