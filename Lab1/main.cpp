#include <iostream>
#include "./classes/SymmetricEncrypter.hpp"

int main()
{
	const std::string inputFile = "test.txt";
	const std::string outputFile = "test.out.txt";
	std::bitset<56> key(123);

	SymmetricEncrypter<64, 64, 56, 48, 16> crypto(key, EncryptionMode::ECB);

	crypto.encrypt(inputFile, outputFile);
	return 0;
}
