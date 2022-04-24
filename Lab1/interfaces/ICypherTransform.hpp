#pragma once

#include <bitset>

template <size_t encrypted_bitset_size, size_t bitset_size, size_t key_size>
class ICypherTransform
{
public:
	typedef std::bitset<bitset_size> bitset;
	typedef std::bitset<key_size> key;
	typedef std::bitset<encrypted_bitset_size> transform_bitset;

    virtual ~ICypherTransform()
	{};

protected:
    virtual transform_bitset cypherTransform(const bitset& bitset, const key& key) = 0;
};
