#include "sprite.h"

namespace Raven
{
	namespace GFX
	{

		Sprite::Sprite(float x, float y, float width, float height, const Raven::MATH::vec4& color)
			:Renderable2D(Raven::MATH::vec3(x, y, 0), Raven::MATH::vec2(width, height), color)
		{

		}

		Sprite::Sprite(float x, float y, float width, float height, Texture* texture)
			: Renderable2D(Raven::MATH::vec3(x, y, 0), Raven::MATH::vec2(width, height), Raven::MATH::vec4(1, 0, 1, 1))
		{
			_texture = texture;
		}

		Sprite::Sprite(float x, float y, float width, float height, Texture* texture, const Raven::MATH::vec4& color)
			: Renderable2D(Raven::MATH::vec3(x, y, 0), Raven::MATH::vec2(width, height), color)
		{
			_texture = texture;
		}

	}
}