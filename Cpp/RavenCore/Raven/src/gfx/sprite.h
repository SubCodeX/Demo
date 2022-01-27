#pragma once

#include "renderable2D.h"

namespace Raven
{
	namespace GFX
	{

		class Sprite : public Renderable2D
		{
		public:
			Sprite(float x, float y, float width, float height, const Raven::MATH::vec4& color);
			Sprite(float x, float y, float width, float height, Texture* texture);
			Sprite(float x, float y, float width, float height, Texture* texture, const Raven::MATH::vec4& color);

			//~Sprite();

		private:

		};

	}
}