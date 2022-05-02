#pragma once

#include "../interfaces/IExpandKey.hpp"
#include "../utils/constants.hpp"
#include "../utils/pBlock.hpp"

class KeyExpandClass : public IExpandKey
{

public:
	KeyExpandClass()
	{}
	
	round_key_array_type generateRoundKeys(const key_type& key) const override
	{
		std::bitset<key_size> divider(0xFFFFFFF);
		std::bitset<key_size> p_key = pBlock(key, constants::key_start_permutation);
		std::bitset<key_size / 2> c0 = std::bitset<key_size / 2>(((p_key >> key_size / 2) & divider).to_ullong());
		std::bitset<key_size / 2> d0 = std::bitset<key_size / 2>((p_key & divider).to_ullong());

		std::array<std::bitset<round_key_size>, round_key_count> round_keys_array;
		std::bitset<round_key_size> round_key;
		std::bitset<key_size / 2> ci;
		std::bitset<key_size / 2> di;
		size_t shuffle;

		for (size_t round = 0; round < round_key_count; ++round)
		{
			shuffle = constants::shuffle_array[round];
			ci = (c0 << shuffle) | (c0 >> ((key_size / 2) - shuffle));
			di = (d0 << shuffle) | (d0 >> ((key_size / 2)  - shuffle));
			round_key = std::bitset<round_key_size>(((ci << (key_size / 2)) | di).to_ullong());
			round_keys_array[round] = pBlock(round_key, constants::key_end_permutation);

			c0 = ci;
			d0 = di;
		}
		return round_keys_array;
	}

	~KeyExpandClass()
	{}
};
