#pragma once

#include "../interfaces/ICypherTransform.hpp"
#include "../utils/pBlock.hpp"

template <size_t encrypted_size, size_t round_key_size>
class CypherTransformClass
	: public ICypherTransform<encrypted_size, round_key_size>
{
private:
	typedef typename ICypherTransform<encrypted_size,
		round_key_size>::encrypted_block_type encrypted_block_type;
	typedef typename ICypherTransform<encrypted_size,
		round_key_size>::round_key_type round_key_type;

public:
	CypherTransformClass()
	{}

    encrypted_block_type cypherTransform(const encrypted_block_type& block,
		const round_key_type& round_key) override
	{
		std::bitset<round_key_size> permutated_block
			= pBlock(std::bitset<round_key_size>(block.to_ullong()), constants::expanding_permutation);
		std::bitset<round_key_size> expanded_block
			= std::bitset<round_key_size>(permutated_block.to_ullong()) ^ round_key;
		std::bitset<encrypted_size> transformed_block;

		for (size_t i = 0; i < 8; ++i)
		{
			std::bitset<6> s_block = std::bitset<6>((expanded_block >> ((8 - i - 1) * 6)).to_ullong());
			std::bitset<6> edge = ((s_block >> 5) << 1) | s_block & std::bitset<6>(1);
			std::bitset<6> middle = s_block >> 1 & std::bitset<6>(15);
			std::bitset<4> new_s_block = constants::s_table[i][edge.to_ullong()][middle.to_ullong()];

			new_s_block = new_s_block << (i * 4);
			transformed_block = transformed_block | std::bitset<32>(new_s_block.to_ullong());
		}
		return transformed_block;
	}

	~CypherTransformClass()
	{}
};
