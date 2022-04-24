#pragma once

#include <array>

namespace constants
{
	const std::array<size_t, 56> key_start_permutation = {
		49, 42, 35, 28, 21, 14, 7,  0,  50, 43, 36, 29, 22, 15,
		8,  1,  51, 44, 37, 30, 23, 16, 9,  2,  52, 45, 38, 31,
		55, 48, 41, 34, 27, 20, 13, 6,  54, 47, 40, 33, 26, 19,
		12, 5,  53, 46, 39, 32, 25, 18, 11, 4,  24, 17, 10, 3
	};

	const std::array<size_t, 16> shuffle_array = {
		1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1
	};

	const std::array<size_t, 48> key_end_permutation = {
		13, 16, 10, 23, 0,  4,  2,  27, 14, 5,  20, 9,  22, 18, 11, 3,
		25, 7,  15, 6,  26, 19, 12, 1,  40, 51, 30, 36, 46, 54, 29, 39,
		50, 44, 32, 47, 43, 48, 38, 55, 33, 52, 45, 41, 49, 35, 28, 31
	};

	const std::array<size_t, 64> key_reverse_permutation = {
		40,	8,	48,	16,	56,	24,	64,	32,	39,	7,	47,	15,	55,	23,	63,	31,
		38,	6,	46,	14,	54,	22,	62,	30,	37,	5,	45,	13,	53,	21,	61,	29,
		36,	4,	44,	12,	52,	20,	60,	28,	35,	3,	43,	11,	51,	19,	59,	27,
		34,	2,	42,	10,	50,	18,	58,	26,	33,	1,	41,	9,	49,	17,	57,	25
	};
}