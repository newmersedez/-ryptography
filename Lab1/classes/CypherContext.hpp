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

template <size_t encrypted_size, size_t decrypted_size,
	size_t key_size, size_t round_key_size, size_t round_key_count>
class CypherContext
	: public ICrypto<encrypted_size, decrypted_size, key_size, round_key_size, round_key_count>
{
private:
	typedef typename ICrypto<encrypted_size, decrypted_size, key_size,
		round_key_size, round_key_count>::key_type key_type;
	typedef typename ICrypto<encrypted_size, decrypted_size, key_size,
		round_key_size, round_key_count>::round_key_type round_key_type;
	typedef typename ICrypto<encrypted_size, decrypted_size, key_size,
		round_key_size, round_key_count>::round_key_array_type round_key_array_type;
	typedef typename ICrypto<encrypted_size, decrypted_size, key_size,
		round_key_size, round_key_count>::encrypted_block_type encrypted_block_type;
	typedef typename ICrypto<encrypted_size, decrypted_size, key_size,
		round_key_size, round_key_count>::decrypted_block_type decrypted_block_type;

	key_type _key;
	EncryptionMode _mode;
	IExpandKey<key_size, round_key_size, round_key_count> *_key_expand;
	ICypherTransform<encrypted_size, key_size> *_cypher_transform;

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

	void encrypt(const std::string& input_file, const std::string& output_file)
	{
		std::ifstream in_stream = _openInputFileStream(input_file);
		std::ofstream out_stream = _openOutputFileStream(output_file);
		

		in_stream.close();
		out_stream.close();
	}

	void decrypt(const std::string& input_file, const std::string& output_file)
	{
		std::ifstream in_stream = _openInputFileStream(input_file);
		std::ofstream out_stream = _openOutputFileStream(output_file);
		

		in_stream.close();
		out_stream.close();
	}

	~CypherContext()
	{}
};
