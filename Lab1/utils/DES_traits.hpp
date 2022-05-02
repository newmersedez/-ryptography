#pragma once

const size_t key_size = 56;
const size_t round_key_size = 48;
const size_t round_key_count = 16;
const size_t decrypted_size = 64;
const size_t encrypted_size = 32;

typedef std::bitset<key_size> key_type;
typedef std::bitset<round_key_size> round_key_type;
typedef std::array<round_key_type, round_key_count> round_key_array_type;

typedef std::bitset<encrypted_size> encrypted_block_type;
typedef std::bitset<decrypted_size> decrypted_block_type;
