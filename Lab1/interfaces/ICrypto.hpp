#pragma once

#include <bitset>
#include <array>
#include "IExpandKey.hpp"

template <size_t decrypted_size, size_t encrypted_size,
	size_t key_size, size_t round_key_size, size_t round_key_count>
class ICrypto
{
public:
	typedef std::bitset<key_size> key_type;
	typedef std::bitset<round_key_size> round_key_type;
	typedef std::array<round_key_type, round_key_count> round_key_array_type;
	typedef std::bitset<decrypted_size> encrypted_bitset_type;
	typedef std::bitset<encrypted_size> decrypted_bitset_type;

	virtual ~ICrypto()
	{};
	
	virtual encrypted_bitset_type encrypt(const decrypted_bitset_type& bitset,
		const round_key_array_type& keys) = 0;

	virtual decrypted_bitset_type decrypt(const encrypted_bitset_type& bitset,
		const round_key_array_type& keys) = 0;
};
