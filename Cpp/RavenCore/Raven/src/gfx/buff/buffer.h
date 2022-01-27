#pragma once

#include <GL\glew.h>

namespace Raven {
	namespace GFX
	{
		class Buffer
		{
		public:
			Buffer(GLfloat* data, GLsizei count, GLuint componentCount);
			~Buffer();

			void bind() const;
			void unbind() const;

			inline GLuint getComponentCount() const { return _componentCount; }

		private:
			GLuint _bufferID;
			GLuint _componentCount;
		};
	}
}