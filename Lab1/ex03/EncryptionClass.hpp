#pragma once

#include <bitset>

template <size_t key_size>
class EncryptionDecryptionClass
{
typedef std::bitset<key_size> bitset;

public:

	~EncryptionClass();
	virtual bitset Encryption(const bitset& bitset, const bitset& key) = 0;
	virtual bitset Decryption(const bitset& bitset, const bitset& key) = 0;
};