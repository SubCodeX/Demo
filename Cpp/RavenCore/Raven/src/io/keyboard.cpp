#include "keyboard.h"

namespace Raven
{
	namespace IO
	{
		Keyboard::Keyboard()
		{
			for (int i = 0; i < MAX_KEYBOARD_KEYS; i += 1)
			{
				_keys[i] = false;
			}
		}

		void Keyboard::setKey(unsigned int key, bool pressed)
		{
			if (key >= MAX_KEYBOARD_KEYS)
			{
				//TODO : ADD TO LOG
				return;
			}

			_keys[key] = pressed;
		}

		bool Keyboard::getKey(unsigned int key)
		{
			if (key >= MAX_KEYBOARD_KEYS)
			{
				//TODO : ADD TO LOG
				return false;
			}

			return _keys[key];
		}
	}
}