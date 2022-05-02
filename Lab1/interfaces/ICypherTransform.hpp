#pragma once

#include <bitset>
#include "../utils/DES_traits.hpp"

class ICypherTransform
{
public:
    virtual ~ICypherTransform()
	{};
	
    virtual encrypted_block_type cypherTransform(const encrypted_block_type& block,
		const round_key_type& round_key) = 0;
};
