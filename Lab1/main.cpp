#include <iostream>
#include "./classes/CypherContext.hpp"

int main()
{
	const std::string inputFile = "test.txt";
	const std::string outputFile = "test.out.txt";
	key_type key(123456);

	KeyExpandClass key_expand;
	CypherTransformClass cypher_transform;
	CypherContext crypto(key, EncryptionMode::ECB);

	crypto.setKeyExpand(&key_expand);
	crypto.setCypherTransform(&cypher_transform);
	crypto.encrypt(inputFile, outputFile);
	return 0;
}
