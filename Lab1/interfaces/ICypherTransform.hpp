#pragma once

#include <bitset>

template <size_t encrypted_bitset_size, size_t bitset_size, size_t key_size>
class ICypherTransform
{
public:
	typedef std::bitset<key_size> key_type;
	typedef std::bitset<bitset_size> bitset_type;
	typedef std::bitset<encrypted_bitset_size> transform_bitset_type;

    virtual ~ICypherTransform()
	{};

protected:
    virtual transform_bitset_type cypherTransform(const bitset_type& bitset, const key_type& key) = 0;
};
