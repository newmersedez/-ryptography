#pragma once

#include "../interfaces/ICypherTransform.hpp"
#include "../utils/pBlock.hpp"

class CypherTransformClass : public ICypherTransform
{
public:
	CypherTransformClass()
	{}

    encrypted_block_type cypherTransform(const encrypted_block_type& block, const round_key_type& round_key) override
	{
		std::bitset<round_key_size> permutated_block
			= pBlock(std::bitset<round_key_size>(block.to_ullong()), constants::expanding_permutation);
		std::bitset<round_key_size> expanded_block
			= std::bitset<round_key_size>(permutated_block.to_ullong()) ^ round_key;
		encrypted_block_type transformed_block;

		for (size_t i = 0; i < 8; ++i)
		{
			std::bitset<6> s_block = std::bitset<6>((expanded_block >> ((8 - i - 1) * 6)).to_ullong());
			std::bitset<6> edge = ((s_block >> 5) << 1) | s_block & std::bitset<6>(1);
			std::bitset<6> middle = s_block >> 1 & std::bitset<6>(15);
			std::bitset<4> new_s_block = constants::s_table[i][edge.to_ullong()][middle.to_ullong()];

			new_s_block = new_s_block << (i * 4);
			transformed_block = transformed_block | std::bitset<32>(new_s_block.to_ullong());
		}
		transformed_block = pBlock(transformed_block, constants::feistel_permutation);
		return transformed_block;
	}

	~CypherTransformClass()
	{}
};
