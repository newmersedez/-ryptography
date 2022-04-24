#pragma once

#include <fstream>
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

	encrypted_bitset encrypt(const decrypted_bitset& bitset, const key_array& keys) override
	{
	
	}
	
	decrypted_bitset decrypt(const encrypted_bitset& bitset, const key_array& keys) override
	{
		
	}

	key_array generateRoundKeys(const key& key) override
	{

	}

	std::ifstream openInputFileStream(const std::string inputFile)
	{
		std::ifstream inStream(inputFile);

		if (!inStream.is_open())
			throw std::invalid_argument("Incorrect input filename");
		if (inStream.peek() == std::ifstream::traits_type::eof())
			throw std::invalid_argument("Empty imput file");
		return inStream;
	}

	std::ofstream openOutputFileStream(const std::string outputFile)
	{
		std::ofstream outStream(outputFile);

		if (!outStream.is_open())
			throw std::invalid_argument("Incorrect output filename");
		return outStream;
	}

public:
	SymmetricEncrypter() = delete;
	
	explicit SymmetricEncrypter(const key& key, EncryptionMode mode, ...) noexcept
		: _key(key), _mode(mode)
	{}

	void encrypt(const std::string& inputFile, const std::string& outputFile)
	{
		std::ifstream inStream = openInputFileStream(inputFile);
		std::ofstream outStream = openOutputFileStream(outputFile);
		
		inStream.close();
		outStream.close();
	}

	void decrypt(const std::string& inputFile, const std::string& outputFile)
	{
		std::ifstream inStream = openInputFileStream(inputFile);
		std::ofstream outStream = openOutputFileStream(outputFile);
		
		inStream.close();
		outStream.close();
	}

	~SymmetricEncrypter()
	{}
};
