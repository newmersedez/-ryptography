#include <iostream>
#include "./classes/SymmetricEncrypter.hpp"

int main()
{
	const std::string inputFile = "test.txt";
	const std::string outputFile = "test.out.txt";
	SymmetricEncrypter<64, 48, 56, 16> crypto;

	crypto.encrypt(inputFile, outputFile);
	
	return 0;
}
