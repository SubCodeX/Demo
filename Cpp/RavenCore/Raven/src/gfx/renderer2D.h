#pragma once

#include <GL\glew.h>
#include <vector>
#include "..\math\math.h"


namespace Raven {
	namespace GFX
	{
		class Renderable2D;

		class Renderer2D
		{
		protected:
			std::vector<Raven::MATH::mat4> _transformationStack;
			const Raven::MATH::mat4* _transformationCurrent;
			Renderer2D()			
			{
				_transformationStack.push_back(Raven::MATH::mat4::identity());
				_transformationCurrent = &_transformationStack.back();
			}
		public:
			void pushTransform(const Raven::MATH::mat4& transformationMatrix, bool override = false)
			{
				if (override)
				{
					_transformationStack.push_back(transformationMatrix);
					_transformationCurrent = &_transformationStack.back();
				}
				else
				{
					_transformationStack.push_back(_transformationStack.back() * transformationMatrix);
					_transformationCurrent = &_transformationStack.back();
				}
			}
			void popTransform()
			{
				if (_transformationStack.size() > 1)
				{
					_transformationStack.pop_back();
					_transformationCurrent = &_transformationStack.back();
				}

				// LOG IF TRYING TO POP IDENTITY MATRIX = _transformationStack[0];
			}			
			virtual void begin() {}
			virtual void submit(const Renderable2D* renderable) = 0;
			virtual void end() {}
			virtual void flush() = 0;
		};

	}
}