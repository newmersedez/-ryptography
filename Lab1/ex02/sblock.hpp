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

template <size_t old_size, size_t new_size>
using rule_map =
	std::map<std::bitset<old_size>, std::bitset<new_size>, bitset_less<old_size>>;

template <size_t bytes_size, size_t old_size, size_t new_size>
std::bitset<bytes_size / old_size * new_size>
	sBlock(const std::bitset<bytes_size>& bytes,  const rule_map<old_size, new_size>& rule)
{
	if (bytes_size == 0 || old_size == 0 || new_size == 0)
		throw std::invalid_argument("Bytes/block size cannot be 0");
	if (bytes_size % old_size != 0)
		throw std::invalid_argument("Invalid size of sBlock");

	const size_t new_bytes_size = bytes_size / old_size * new_size ;
	std::bitset<new_bytes_size> new_bytes;
	size_t blocks_count = bytes_size / old_size;

	for (size_t i = 0; i < blocks_count; ++i)
	{
		std::bitset<old_size> old_block((bytes >> (i * old_size)).to_ullong());
		auto new_block_iter = rule.find(old_block);

		if (new_block_iter != rule.end())
		{
			std::bitset<new_bytes_size> new_block((*new_block_iter).second.to_ullong());
			new_bytes = new_block << (i * new_size) | new_bytes;
		}
		else
			throw std::logic_error("This block doesn`t exist");

	}
	return new_bytes;
}
