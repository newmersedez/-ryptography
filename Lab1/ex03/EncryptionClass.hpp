#pragma once

#include "IEncryption.hpp"

enum class EncryptionMode
{
	ECB, 
	CBC, 
	CFB,
	OFB, 
	CTR, 
	RD, 
	RDH
};

template <size_t bitset_size, size_t key_size>
class EncryptioClass : public IEncryption<bitset_size, key_size>
{
private:
	key	_key;
	EncryptionMode _mode;

public:
	~EncryptioClass(const key& key, EncryptionMode mode);
	~EncryptioClass();
};
