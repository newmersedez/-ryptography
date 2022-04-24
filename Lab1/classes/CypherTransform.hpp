#pragma once

#include "../interfaces/ICypherTransform.hpp"

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
