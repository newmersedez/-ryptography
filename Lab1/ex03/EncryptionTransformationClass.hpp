#pragma once

#include <bitset>

template <size_t bitset_size, size_t key_size>
class EncryptionTransformationClass
{
typedef std::bitset<bitset_size> bitset;
typedef std::bitset<key_size> key;

public:
	virtual ~EncryptionTransformationClass();
	virtual bitset encryptionTransformation(const bitset& bitset, const key& round_key) = 0;
};
