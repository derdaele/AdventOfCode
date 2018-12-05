#pragma once

#define BEGIN_DAY(y, d) \
	namespace _##y { namespace _##d { \
		constexpr int day() { return d; }\
		constexpr int year() { return y; };

# define END_DAY }}