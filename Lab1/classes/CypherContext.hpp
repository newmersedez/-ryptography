#pragma once

#include "../interfaces/ICrypto.hpp"
#include "../interfaces/IExpandKey.hpp"
#include "../interfaces/ICypherTransform.hpp"

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


template <size_t key_size, size_t round_key_size, size_t round_key_count>
class KeyExpandClass
	: public IExpandKey<key_size, round_key_size, round_key_count>
{
private:
	typedef typename IExpandKey<key_size, round_key_size,
		round_key_count>::key_type key_type;
	typedef typename IExpandKey<key_size, round_key_size,
		round_key_count>::round_key_type round_key_type;
	typedef typename IExpandKey<key_size, round_key_size,
		round_key_count>::round_key_array_type round_key_array_type;

public:
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
};


template <size_t encrypted_size, size_t bitset_size, size_t key_size>
class CypherTransformClass
	: public ICypherTransform<encrypted_size, bitset_size, key_size>
{
private:
	typedef typename ICypherTransform<encrypted_size,
		bitset_size, key_size>::key_type key_type;
	typedef typename ICypherTransform<encrypted_size,
		bitset_size, key_size>::decrypted_bitset_type decrypted_bitset_type;
	typedef typename ICypherTransform<encrypted_size,
		bitset_size, key_size>::encrypted_bitset_type encrypted_bitset_type;

public:
	encrypted_bitset_type cypherTransform(const decrypted_bitset_type& bitset,
		const key_type& key) override
	{}
};


template <size_t decrypted_size, size_t encrypted_size,
	size_t key_size, size_t round_key_size, size_t round_key_count>
class CypherContext
	: public ICrypto<decrypted_size, encrypted_size, key_size, round_key_size, round_key_count>
{
private:
		typedef typename ICrypto<decrypted_size, encrypted_size, key_size, 
			round_key_size, round_key_count>::key_type key_type;
		typedef typename ICrypto<decrypted_size, encrypted_size, key_size,
			round_key_size, round_key_count>::round_key_type round_key_type;
		typedef typename ICrypto<decrypted_size, encrypted_size, key_size,
			round_key_size, round_key_count>::round_key_array_type round_key_array_type;
		typedef typename ICrypto<decrypted_size, encrypted_size, key_size,
			round_key_size, round_key_count>::encrypted_bitset_type encrypted_bitset_type;
		typedef typename ICrypto<decrypted_size, encrypted_size, key_size,
			round_key_size, round_key_count>::decrypted_bitset_type decrypted_bitset_type;

	key_type _key;
	EncryptionMode _mode;
	IExpandKey<key_size, round_key_size, round_key_count> *_key_expand;
	ICypherTransform<encrypted_size, decrypted_size, key_size> *_cypher_transform;

public:
	encrypted_bitset_type encrypt(const decrypted_bitset_type& bitset,
		const round_key_array_type& keys) override
	{}

	decrypted_bitset_type decrypt(const encrypted_bitset_type& bitset,
		const round_key_array_type& keys) override
	{}
};
