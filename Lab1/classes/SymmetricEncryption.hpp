#pragma once

#include "ICrypto.hpp"

namespace DES
{
	enum class EncryptionMode
	{
		ECB,
		CBC,
		CFB,
		OFB,
		CTR,
		RD,
		RD_H
	};

	template <size_t decrypted_bitset_size, size_t encrypted_bitset_size,
		size_t key_size, size_t key_count>
	class SymmetricEncrypter
		: public ICrypto<decrypted_bitset_size, encrypted_bitset_size, key_size, key_count>
	{
	private:
		key _key;
		EncryptionMode _mode;

		encrypted_bitset encrypt(const decrypted_bitset& bitset, const key& key) override
		{

		}
		
		decrypted_bitset decrypt(const encrypted_bitset& bitset, const key& key) override
		{
			
		}
		
		key_array expandKey(const key& key) override
		{
		
		}

	public:
		SymmetricEncrypter() = delete;
		explicit SymmetricEncrypter(const key& key, EncryptionMode mode = EncryptionMode::ECB) noexcept
			: key(_key), mode(_mode)
		{}

		std::string	encrypt(const std::string& inputFile)
		{
		}

		std::string decrypt(const std::string& inputFile)
		{

		}

		~SymmetricEncrypter()
		{}
	};
}
