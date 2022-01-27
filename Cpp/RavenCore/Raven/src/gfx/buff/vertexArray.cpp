#include "vertexArray.h"

namespace Raven {
	namespace GFX
	{

		VertexArray::VertexArray()
		{
			glGenVertexArrays(1, &_arrayID);
		}

		VertexArray::~VertexArray()
		{
			for (int i = 0; i < _buffers.size(); i += 1)
			{
				delete _buffers[i];
			}

			glDeleteVertexArrays(1, &_arrayID);
		}

		void VertexArray::addBuffer(Buffer* buffer, GLuint index)
		{
			bind();
			buffer->bind();

			glEnableVertexAttribArray(index);
			glVertexAttribPointer(index, buffer->getComponentCount(), GL_FLOAT, GL_FALSE, 0, 0);

			buffer->unbind();
			unbind();
		}

		void VertexArray::bind() const
		{
			glBindVertexArray(_arrayID);
		}

		void VertexArray::unbind() const
		{
			glBindVertexArray(0);
		}

	}
}