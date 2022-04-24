#pragma once

#include <bitset>

template <size_t encrypted_bitset_size, size_t decrypted_bitset_size, size_t key_size>
class ICrypto
{
typedef std::bitset<encrypted_bitset_size> encrypted_bitset;
typedef std::bitset<decrypted_bitset_size> decrypted_bitset;
typedef std::bitset<key_size> key;

public:
    virtual ~ICrypto();
	virtual encrypted_bitset encrypt(const decrypted_bitset& bitset, const key& key) = 0;
	virtual decrypted_bitset decrypt(const encrypted_bitset& bitset, const key& key) = 0;
};
