#pragma once

#include <fstream>
#include "../interfaces/ICrypto.hpp"
#include "../utils/constants.hpp"
#include "../utils/pBlock.hpp"
#include "../utils/sBlock.hpp"

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

template <size_t decrypted_size, size_t encrypted_size,
	size_t key_size, size_t round_key_size, size_t round_key_count>
class CypherContext : public ICrypto<decrypted_size, encrypted_size,
	key_size, round_key_size, round_key_count>
{
private:
	typedef typename ICrypto<decrypted_size, encrypted_size,
		key_size, round_key_size, round_key_count>::key_type key_type;
	typedef typename ICrypto<decrypted_size, encrypted_size,
		key_size, round_key_size, round_key_count>::round_key_type round_key_type;
	typedef typename ICrypto<decrypted_size, encrypted_size,
		key_size, round_key_size, round_key_count>::round_key_array_type round_key_array_type;
	typedef typename ICrypto<decrypted_size, encrypted_size,
		key_size, round_key_size, round_key_count>::encrypted_bitset_type encrypted_bitset_type;
	typedef typename ICrypto<decrypted_size, encrypted_size,
		key_size, round_key_size, round_key_count>::decrypted_bitset_type decrypted_bitset_type;

	key_type _key;
	EncryptionMode _mode;

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

	encrypted_bitset_type encrypt(const decrypted_bitset_type& bitset,
		const round_key_array_type& keys) override
	{
		switch(this->_mode)
		{
			case EncryptionMode::ECB:

		}
	}

	decrypted_bitset_type decrypt(const encrypted_bitset_type& bitset,
		const round_key_array_type& keys) override
	{
		
	}

	round_key_array_type generateRoundKeys(const key_type& key) override
	{
		std::bitset<key_size> divider(0xFFFFFFF);
		std::bitset<key_size> p_key = pBlock(key, constants::key_start_permutation);
		std::bitset<key_size / 2> c0_key = std::bitset<key_size / 2>(((p_key >> key_size / 2) & divider).to_ullong());
		std::bitset<key_size / 2> d0_key = std::bitset<key_size / 2>((p_key & divider).to_ullong());

		std::array<std::bitset<round_key_size>, round_key_count> round_keys_array;
		std::bitset<round_key_size> round_key;
		std::bitset<key_size / 2> ci_key;
		std::bitset<key_size / 2> di_key;
		size_t shuffle;

		for (size_t i = 0; i < round_key_count; ++i)
		{
			shuffle = constants::shuffle_array[i];
			ci_key = (c0_key << shuffle) | (c0_key >> ((key_size / 2) - shuffle));
			di_key = (d0_key << shuffle) | (d0_key >> ((key_size / 2)  - shuffle));
			round_key = std::bitset<round_key_size>(((ci_key << (key_size / 2)) | di_key).to_ullong());
			round_keys_array[i] = pBlock(round_key, constants::key_end_permutation);

			c0_key = ci_key;
			d0_key = di_key;
		}
		return round_keys_array;
	}

public:
	CypherContext() = delete;
	
	explicit CypherContext(const key_type& key, EncryptionMode mode, ...) noexcept
		: _key(key), _mode(mode)
	{}

	void encrypt(const std::string& input_file, const std::string& output_file)
	{
		std::ifstream in_stream = _openInputFileStream(input_file);
		std::ofstream out_stream = _openOutputFileStream(output_file);
		round_key_array_type round_keys_array = generateRoundKeys(_key);

		std::string str((std::istreambuf_iterator<char>(in_stream)), std::istreambuf_iterator<char>());
		std::bitset<56> bin_str[str.size()];
		std::bitset<56> bin_cypher[str.size];
		
		for (int i = 0; i < str.size(); ++i)
		{
			bin_str[i] = std::bitset<56>((int) str[i]);
			bin_cypher[i] = this->encrypt()
			// std::cout << bin_str[i] << std::endl; // print for checking
		}

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
