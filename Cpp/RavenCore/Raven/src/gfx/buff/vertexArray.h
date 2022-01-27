#pragma once

#include <GL\glew.h>
#include <vector>

#include "buffer.h"

namespace Raven {
	namespace GFX
	{

		class VertexArray
		{
		public:
			VertexArray();
			~VertexArray();

			void addBuffer(Buffer* buffer, GLuint index);
			void bind() const;
			void unbind() const;

		private:
			GLuint _arrayID;
			std::vector<Buffer*> _buffers;

		};

	}
}