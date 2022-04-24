#pragma once

#include <bitset>
#include <array>

template <size_t decrypted_bitset_size, size_t encrypted_bitset_size,
	size_t key_size, size_t key_count>
class ICrypto
{
public:
	typedef std::bitset<encrypted_bitset_size> encrypted_bitset;
	typedef std::bitset<decrypted_bitset_size> decrypted_bitset;
	typedef std::bitset<key_size> key; 
	typedef std::array<key, key_count> key_array;

    virtual ~ICrypto()
	{};

protected:
	virtual encrypted_bitset encrypt(const decrypted_bitset& bitset, const key& key) = 0;
	virtual decrypted_bitset decrypt(const encrypted_bitset& bitset, const key& key) = 0;
	virtual key_array expandKey(const key& key) = 0;
};
