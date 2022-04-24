#pragma once

#include <bitset>
#include <array>
#include "IExpandKey.hpp"

template <size_t decrypted_size, size_t encrypted_size,
	size_t key_size, size_t round_key_size, size_t round_key_count>
class ICrypto : IExpandKey<key_size, round_key_size, round_key_count>
{
public:
	typedef std::bitset<key_size> key;
	typedef std::bitset<round_key_size> round_key;
	typedef std::array<round_key, round_key_count> round_key_array;
	typedef std::bitset<decrypted_size> encrypted_bitset;
	typedef std::bitset<encrypted_size> decrypted_bitset;

    virtual ~ICrypto()
	{};

protected:
	virtual encrypted_bitset encrypt(const decrypted_bitset& bitset,
		const round_key_array& keys) = 0;
	virtual decrypted_bitset decrypt(const encrypted_bitset& bitset,
		const round_key_array& keys) = 0;
};
