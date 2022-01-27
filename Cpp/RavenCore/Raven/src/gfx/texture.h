#pragma once

#include <FreeImage.h>
#include <string>
#include <GL\glew.h>

#include "..\util\imageLoader.h"

namespace Raven
{
	namespace GFX
	{

		class Texture
		{
		public:
			Texture(const std::string& filename);
			~Texture();
			void bind() const;
			void unbind() const;

			inline const unsigned int getWidth() const { return _width; }
			inline const unsigned int getHeight() const { return _height; }
			inline const unsigned int getID() const { return _ID; }
		private:
			std::string _filename;
			GLuint _ID;
			GLsizei _width;
			GLsizei _height;

			GLuint load();
		};
	}
}