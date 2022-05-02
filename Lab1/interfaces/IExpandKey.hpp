#pragma once

#include <bitset>
#include <array>
#include "../utils/DES_traits.hpp"

class IExpandKey
{
public:
    virtual ~IExpandKey()
	{};
	
    virtual round_key_array_type generateRoundKeys(const key_type& key) const = 0;
};
