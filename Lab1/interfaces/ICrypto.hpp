#pragma once

#include <bitset>
#include <array>
#include "IExpandKey.hpp"
#include "../utils/DES_traits.hpp"

class ICrypto
{
public:
	virtual ~ICrypto()
	{};
	
	virtual encrypted_block_type encrypt(const decrypted_block_type& block) = 0;
	virtual decrypted_block_type decrypt(const encrypted_block_type& block) = 0;
};
