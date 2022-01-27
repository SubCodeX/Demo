#include "mouse.h"

namespace Raven
{
	namespace IO
	{
		Mouse::Mouse()
		{
			for (int i = 0; i < MAX_MOUSE_BUTTONS; i += 1)
			{
				_buttons[i] = false;
			}
		}

		void Mouse::setButton(unsigned int button, bool pressed)
		{
			_buttons[button] = pressed;
		}

		bool Mouse::getButton(unsigned int button)
		{
			return _buttons[button];
		}

		void Mouse::setXY(double x, double y)
		{
			_x = x;
			_y = y;
		}

		double Mouse::getX()
		{
			return _x;
		}

		double Mouse::getY()
		{
			return _y;
		}
	}
}