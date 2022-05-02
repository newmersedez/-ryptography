#pragma once

#include <fstream>

#include "ExpandKey.hpp"
#include "CypherTransform.hpp"
#include "../utils/constants.hpp"
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

class CypherContext : public ICrypto
{

	key_type _key;
	EncryptionMode _mode;
	IExpandKey *_key_expand;
	ICypherTransform *_cypher_transform;

	std::ifstream _openInputFileStream(const std::string input_file)
	{
		std::ifstream in_stream(input_file);

		if (!in_stream.is_open())
			throw std::invalid_argument("Incorrect input filename");
		if (in_stream.peek() == std::ifstream::traits_type::eof())
			throw std::invalid_argument("Empty imput file");
		return in_stream;
	}

	std::ofstream _openOutputFileStream(const std::string output_file)
	{
		std::ofstream out_stream(output_file);

		if (!out_stream.is_open())
			throw std::invalid_argument("Incorrect output filename");
		return out_stream;
	}

	encrypted_block_type encrypt(const decrypted_block_type& block,
		const round_key_array_type& keys) override
	{
		encrypted_block_type ret;
		return ret;
	}

	decrypted_block_type decrypt(const encrypted_block_type& block,
		const round_key_array_type& keys) override
	{
		decrypted_block_type ret;
		return ret;
	}

public:
	CypherContext() = delete;
	
	explicit CypherContext(const key_type& key, EncryptionMode mode) noexcept
		: _key(key), _mode(mode), _key_expand(nullptr), _cypher_transform(nullptr)
	{}

	void setKeyExpand(const IExpandKey *key_expand)
	{
		this->_key_expand = const_cast<IExpandKey *>(key_expand);
	}

	void setCypherTransform(const ICypherTransform *cypher_transform)
	{
		this->_cypher_transform = const_cast<ICypherTransform *>(cypher_transform);
	}

	void encrypt(const std::string& input_file, const std::string& output_file)
	{
		if (this->_key_expand == nullptr)
			throw std::invalid_argument("Key expand not set");
		if (this->_cypher_transform == nullptr)
			throw std::invalid_argument("Cypher transform expand not set");

		std::ifstream in_stream = _openInputFileStream(input_file);
		std::ofstream out_stream = _openOutputFileStream(output_file);
		std::string str((std::istreambuf_iterator<char>(in_stream)), std::istreambuf_iterator<char>());
		round_key_array_type round_keys = this->_key_expand->generateRoundKeys(this->_key);
    	decrypted_block_type decrypted_block[str.size()];
		encrypted_block_type encrypted_block;

    	for (int i = 0; i < str.size(); ++i)
		{
    		decrypted_block[i] = decrypted_block_type((int) str[i]);
			encrypted_block = this->encrypt(decrypted_block[i], round_keys);
			out_stream << static_cast<char>(encrypted_block.to_ullong());
		}

		in_stream.close();
		out_stream.close();
	}

	void decrypt(const std::string& input_file, const std::string& output_file)
	{
		if (this->_key_expand == nullptr)
			throw std::invalid_argument("Key expand not set");
		if (this->_cypher_transform == nullptr)
			throw std::invalid_argument("Cypher transform expand not set");

		std::ifstream in_stream = _openInputFileStream(input_file);
		std::ofstream out_stream = _openOutputFileStream(output_file);
		std::string str((std::istreambuf_iterator<char>(in_stream)), std::istreambuf_iterator<char>());
		round_key_array_type round_keys = this->_key_expand->generateRoundKeys(this->_key);
		encrypted_block_type encrypted_block[str.size()];
    	decrypted_block_type decrypted_block;

    	for (int i = 0; i < str.size(); ++i)
		{
    		encrypted_block[i] = encrypted_block_type((int) str[i]);
			decrypted_block = this->decrypt(encrypted_block[i], round_keys);
			out_stream << static_cast<char>(decrypted_block.to_ullong());
		}

		in_stream.close();
		out_stream.close();
	}

	~CypherContext()
	{}
};
