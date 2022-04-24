#pragma once

#include <bitset>
#include <array>

template <size_t bitset_size, size_t key_size, size_t key_count>
class IExpandKey
{
typedef std::bitset<bitset_size> bitset;
typedef std::bitset<key_size> key;
typedef std::array<key, key_count> key_array;

public:
    virtual ~IExpandKey();
    virtual key_array expandKey(const bitset& bitset) = 0;
};
