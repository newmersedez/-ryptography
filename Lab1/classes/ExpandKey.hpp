#pragma once

#include "../interfaces/IExpandKey.hpp"
#include "../utils/constants.hpp"
#include "../utils/pBlock.hpp"

template <size_t key_size, size_t round_key_size, size_t round_key_count>
class KeyExpandClass
	: public IExpandKey<key_size, round_key_size, round_key_count>
{
private:
	typedef typename IExpandKey<key_size, round_key_size,
		round_key_count>::key_type key_type;
	typedef typename IExpandKey<key_size, round_key_size,
		round_key_count>::round_key_type round_key_type;
	typedef typename IExpandKey<key_size, round_key_size,
		round_key_count>::round_key_array_type round_key_array_type;

public:
	round_key_array_type generateRoundKeys(const key_type& key) override
	{
		std::bitset<key_size> divider(0xFFFFFFF);
		std::bitset<key_size> p_key = pBlock(key, constants::key_start_permutation);
		std::bitset<key_size / 2> c0_key = std::bitset<key_size / 2>(((p_key >> key_size / 2) & divider).to_ullong());
		std::bitset<key_size / 2> d0_key = std::bitset<key_size / 2>((p_key & divider).to_ullong());

		std::array<std::bitset<round_key_size>, round_key_count> round_keys_array;
		std::bitset<round_key_size> round_key;
		std::bitset<key_size / 2> ci_key;
		std::bitset<key_size / 2> di_key;
		size_t shuffle;

		for (size_t i = 0; i < round_key_count; ++i)
		{
			shuffle = constants::shuffle_array[i];
			ci_key = (c0_key << shuffle) | (c0_key >> ((key_size / 2) - shuffle));
			di_key = (d0_key << shuffle) | (d0_key >> ((key_size / 2)  - shuffle));
			round_key = std::bitset<round_key_size>(((ci_key << (key_size / 2)) | di_key).to_ullong());
			round_keys_array[i] = pBlock(round_key, constants::key_end_permutation);

			c0_key = ci_key;
			d0_key = di_key;
		}
		return round_keys_array;
	}
};
