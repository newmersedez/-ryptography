#include <iostream>
#include "./classes/CypherContext.hpp"

int main()
{
	const std::string inputFile = "test.txt";
	const std::string outputFile = "test.out.txt";
	const size_t decrypted_size = 64;
	const size_t encrypted_size = 32;
	const size_t key_size = 56;
	const size_t round_key_size = 48;
	const size_t round_key_count = 16;
	std::bitset<key_size> key(123456);

	KeyExpandClass<key_size, round_key_size, round_key_count> key_expand;
	CypherTransformClass<encrypted_size, round_key_size> cypher_transform;
	CypherContext<decrypted_size, encrypted_size, key_size, round_key_size,
		round_key_count> crypto(key, EncryptionMode::ECB);

	return 0;
}
