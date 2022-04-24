#pragma once

#include "../interfaces/ICrypto.hpp"

enum class EncryptionMode
{
	ECB,
	CBC,
	CFB,
	OFB,
	CTR,
	RD,
	RD_H
};

template <size_t decrypted_bitset_size, size_t encrypted_bitset_size,
	size_t key_size, size_t key_count>
class SymmetricEncrypter
	: public ICrypto<decrypted_bitset_size, encrypted_bitset_size, key_size, key_count>
{
private:
	typedef typename ICrypto<decrypted_bitset_size, encrypted_bitset_size,
		key_size, key_count>::key key;
	typedef typename ICrypto<decrypted_bitset_size, encrypted_bitset_size,
		key_size, key_count>::encrypted_bitset encrypted_bitset;
	typedef typename ICrypto<decrypted_bitset_size, encrypted_bitset_size,
		key_size, key_count>::decrypted_bitset decrypted_bitset;
	typedef typename ICrypto<decrypted_bitset_size, encrypted_bitset_size,
		key_size, key_count>::key_array key_array;

	key _key;
	EncryptionMode _mode;

	encrypted_bitset encrypt(const decrypted_bitset& bitset, const key& key) override
	{
	
	}
	
	decrypted_bitset decrypt(const encrypted_bitset& bitset, const key& key) override
	{
		
	}
	
	key_array expandKey(const key& key) override
	{
	
	}

public:
	SymmetricEncrypter() = default;
	explicit SymmetricEncrypter(const key& key, EncryptionMode mode) noexcept
		: _key(key), _mode(mode)
	{}

	void encrypt(const std::string& inputFile, const std::string& outputFile)
	{

	}

	void decrypt(const std::string& inputFile, const std::string& outputFile)
	{

	}

	~SymmetricEncrypter()
	{}
};
