#pragma once

#include <bitset>

template <size_t encrypted_size, size_t decrypted_size, size_t key_size>
class ICypherTransform
{
public:
	typedef std::bitset<key_size> key_type;
	typedef std::bitset<decrypted_size> decrypted_bitset_type;
	typedef std::bitset<encrypted_size> encrypted_bitset_type;

    virtual ~ICypherTransform()
	{};
	
    virtual encrypted_bitset_type cypherTransform(const decrypted_bitset_type& bitset,
		const key_type& key) = 0;
};
