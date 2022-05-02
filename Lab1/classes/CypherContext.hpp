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

	encrypted_block_type encrypt(const decrypted_block_type& block) override
	{
		std::bitset<decrypted_size> divider(0xFFFFFFF);
		std::bitset<decrypted_size / 2> left = std::bitset<decrypted_size / 2>(((block >> decrypted_size / 2) & divider).to_ullong());
		std::bitset<decrypted_size / 2> right = std::bitset<decrypted_size / 2>((block & divider).to_ullong());
		round_key_array_type round_keys = this->_key_expand->generateRoundKeys(this->_key);
		encrypted_block_type result;

		left = this->_cypher_transform->cypherTransform(left, round_keys[0]);
		right = this->_cypher_transform->cypherTransform(right, round_keys[0]);
		result = std::bitset<encrypted_size>(((left << (encrypted_size / 2)) | right).to_ullong());
		return result;
	}

	decrypted_block_type decrypt(const encrypted_block_type& block) override
	{
		// std::bitset<encrypted_size> divider(0xFFFFFFF);
		// std::bitset<encrypted_size / 2> left = std::bitset<encrypted_size / 2>(((block >> encrypted_size / 2) & divider).to_ullong());
		// std::bitset<encrypted_size / 2> right = std::bitset<encrypted_size / 2>((block & divider).to_ullong());
		// round_key_array_type round_keys = this->_key_expand->generateRoundKeys(this->_key);
		// decrypted_block_type result;

		// left = this->_cypher_transform->cypherTransform(left, round_keys[0]);
		// right = this->_cypher_transform->cypherTransform(right, round_keys[0]);
		// result = std::bitset<encrypted_size>(((left << (encrypted_size / 2)) | right).to_ullong());
		// return result;
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

	encrypted_block_type encrypt(const std::string& input_file, const std::string& output_file)
	{
		if (this->_key_expand == nullptr)
			throw std::invalid_argument("Key expand class must not be empty");
		if (this->_cypher_transform == nullptr)
			throw std::invalid_argument("Cypher transform must not be empty");
		
		std::ifstream input_fstream(input_file);
		std::ofstream output_fstream(output_file);
		std::string str((std::istreambuf_iterator<char>(input_fstream)),
			std::istreambuf_iterator<char>());

    	decrypted_block_type decrypted_block[str.size()];
		encrypted_block_type encrypted_block;	
		
		for (size_t i = 0; i < str.size(); ++i)
		{
			decrypted_block[i] = decrypted_block_type((int) str[i]);
			encrypted_block = encrypt(decrypted_block[i]);
			output_fstream << static_cast<char>(encrypted_block.to_ullong());
		}
		
		input_fstream.close();
		output_fstream.close();
		return encrypted_block;
	}

	decrypted_block_type decrypt(const std::string& input_file, const std::string& output_file)
	{
		
	}

	~CypherContext()
	{}
};
