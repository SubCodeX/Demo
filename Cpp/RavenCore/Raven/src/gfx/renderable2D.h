#pragma once

#include <GL\glew.h>
#include "..\math\math.h"
#include "buff\buffers.h"

#include "shader.h"
#include "texture.h"
#include "renderer2D.h"

namespace Raven {
	namespace GFX
	{

		struct VertexData
		{
			Raven::MATH::vec3 vertex;
			Raven::MATH::vec2 texCoord;	
			float texID;
			unsigned int color;
		};

		class Renderable2D
		{
		protected:
			Raven::MATH::vec3 _position;
			Raven::MATH::vec2 _size;
			Raven::MATH::vec4 _color;
			std::vector<Raven::MATH::vec2> _texCoord;
			Texture* _texture;

			Renderable2D() { setTexCoordDefaults();	}

		public:
			Renderable2D(Raven::MATH::vec3 position, Raven::MATH::vec2 size, Raven::MATH::vec4 color)
				: _position(position), _size(size), _color(color)
			{ setTexCoordDefaults(); }

			virtual ~Renderable2D()
			{}
			
			virtual void submit(Renderer2D* renderer) const
			{
				renderer->submit(this);
			}

			inline const Raven::MATH::vec3 getPosition() const { return _position; }
			inline const Raven::MATH::vec2 getSize() const { return _size; }
			inline const Raven::MATH::vec4 getColor() const { return _color; }
			inline const std::vector<Raven::MATH::vec2>& getTexCoord() const { return _texCoord; }

			inline const GLuint getTextureID() const { return _texture == nullptr ? 0 : _texture->getID(); }

		private:
			void setTexCoordDefaults()
			{
				_texCoord.push_back(Raven::MATH::vec2(0.0f, 0.0f));
				_texCoord.push_back(Raven::MATH::vec2(1.0f, 0.0f));
				_texCoord.push_back(Raven::MATH::vec2(1.0f, 1.0f));
				_texCoord.push_back(Raven::MATH::vec2(0.0f, 1.0f));
			}
		
		};

	}
}