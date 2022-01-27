#pragma once

#define MAX_MOUSE_BUTTONS 32

namespace Raven
{
	namespace IO
	{
		class Mouse
		{
		// Members
		public:
		private:
			double _x, _y;
			bool _buttons[MAX_MOUSE_BUTTONS];

		// Methods
		public:
			Mouse();

			void setButton(unsigned int button, bool pressed);
			bool getButton(unsigned int button);

			void setXY(double x, double y);
			double getX();
			double getY();			
		private:
		};
	}
}