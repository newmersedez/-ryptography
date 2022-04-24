#include <iostream>
#include "./classes/CypherContext.hpp"

int main()
{
	const std::string inputFile = "test.txt";
	const std::string outputFile = "test.out.txt";
	std::bitset<56> key(123456);

	CypherContext<64, 32, 56, 48, 16> crypto;
	// CypherContext<64, 32, 56, 48, 16> crypto(key, EncryptionMode::ECB);

	// crypto.encrypt(inputFile, outputFile);
	return 0;
}
