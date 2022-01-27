#include "texture.h"

namespace Raven
{
	namespace GFX
	{

		Texture::Texture(const std::string& filename)
			: _filename(filename)
		{
			_ID = load();
		}

		Texture::~Texture()
		{

		}

		GLuint Texture::load()
		{
			BYTE* pixels = Raven::UTIL::loadImage(_filename.c_str(), &_width, &_height);
			
			GLuint textureID;
			glGenTextures(1, &textureID);
			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
			glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, _width, _height, 0, GL_BGR, GL_UNSIGNED_BYTE, pixels);
			glBindTexture(GL_TEXTURE_2D, 0);

			return textureID;
		}

		void Texture::bind() const
		{
			glBindTexture(GL_TEXTURE_2D, _ID);
		}

		void Texture::unbind() const
		{
			glBindTexture(GL_TEXTURE_2D, 0);
		}

	}
}