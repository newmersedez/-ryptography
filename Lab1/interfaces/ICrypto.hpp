#pragma once

#include <bitset>
#include <array>
#include "IExpandKey.hpp"

template <size_t encrypted_size, size_t decrypted_size,
	size_t key_size, size_t round_key_size, size_t round_key_count>
class ICrypto
{
protected:
	typedef std::bitset<key_size> key_type;
	typedef std::bitset<round_key_size> round_key_type;
	typedef std::array<round_key_type, round_key_count> round_key_array_type;
	typedef std::bitset<decrypted_size> encrypted_block_type;
	typedef std::bitset<encrypted_size> decrypted_block_type;

public:
	virtual ~ICrypto()
	{};
	
	virtual encrypted_block_type encrypt(const decrypted_block_type& block,
		const round_key_array_type& keys) = 0;

	virtual decrypted_block_type decrypt(const encrypted_block_type& block,
		const round_key_array_type& keys) = 0;
};
