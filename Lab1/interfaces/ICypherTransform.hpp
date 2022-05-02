#pragma once

#include <bitset>

template <size_t encrypted_size, size_t round_key_size>
class ICypherTransform
{
protected:
	typedef std::bitset<encrypted_size> encrypted_block_type;
	typedef std::bitset<round_key_size> round_key_type;

public:
    virtual ~ICypherTransform()
	{};
	
    virtual encrypted_block_type cypherTransform(const encrypted_block_type& block,
		const round_key_type& round_key) = 0;
};
