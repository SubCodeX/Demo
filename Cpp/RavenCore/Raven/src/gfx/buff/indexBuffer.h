#pragma once

#include <GL\glew.h>

namespace Raven {
	namespace GFX
	{
		class IndexBuffer
		{
		public:
			IndexBuffer(GLushort* data, GLsizei count);
			IndexBuffer(GLuint* data, GLsizei count);
			~IndexBuffer();

			void bind() const;
			void unbind() const;

			inline GLuint getCount() const { return _count; }

		private:
			GLuint _bufferID;
			GLuint _count;
		};
	}
}