#pragma once

#define MAX_KEYBOARD_KEYS 1024

namespace Raven
{
	namespace IO
	{
		class Keyboard
		{
		// Members
		public:
		private:
			bool _keys[MAX_KEYBOARD_KEYS];

		// Methods
		public:
			Keyboard();

			void setKey(unsigned int key, bool pressed);
			bool getKey(unsigned int key);
		private:
		};
	}
}