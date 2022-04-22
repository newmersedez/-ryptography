#pragma once

#include <bitset>

template <size_t bitset_size, size_t key_size>
class EncryptionDecryptionClass
{
typedef std::bitset<bitset_size> bitset;
typedef std::bitset<key_size> key;

public:
	~EncryptionClass();
	virtual bitset Encryption(const bitset& bitset, const key& key) = 0;
	virtual bitset Decryption(const bitset& bitset, const key& key) = 0;
};