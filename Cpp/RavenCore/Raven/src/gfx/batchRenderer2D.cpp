#include "batchRenderer2D.h"

namespace Raven {
	namespace GFX
	{
		BatchRenderer2D::BatchRenderer2D()
		{
			_indexCount = 0;
			init();
		}

		BatchRenderer2D::~BatchRenderer2D()
		{
			delete _IBO;
			glDeleteBuffers(1, &_VBO);
		}

		void BatchRenderer2D::init()
		{
			glGenVertexArrays(1, &_VAO);
			glGenBuffers(1, &_VBO);

			glBindVertexArray(_VAO);
			glBindBuffer(GL_ARRAY_BUFFER, _VBO);

			glBufferData(GL_ARRAY_BUFFER, RENDERER_BUFFER_SIZE, 0, GL_DYNAMIC_DRAW);
			
			glEnableVertexAttribArray(SHADER_VERTEX_INDEX);
			glEnableVertexAttribArray(SHADER_TEXCOORD_INDEX);
			glEnableVertexAttribArray(SHADER_TEXTURE_INDEX);
			glEnableVertexAttribArray(SHADER_COLOR_INDEX);
			
			glVertexAttribPointer(SHADER_VERTEX_INDEX, 3, GL_FLOAT, GL_FALSE, RENDERER_VERTEX_SIZE, (const GLvoid*)0);
			glVertexAttribPointer(SHADER_TEXCOORD_INDEX, 2, GL_FLOAT, GL_FALSE, RENDERER_VERTEX_SIZE, (const GLvoid*)(offsetof(VertexData, VertexData::texCoord)));
			glVertexAttribPointer(SHADER_TEXTURE_INDEX, 1, GL_FLOAT, GL_FALSE, RENDERER_VERTEX_SIZE, (const GLvoid*)(offsetof(VertexData, VertexData::texID)));
			glVertexAttribPointer(SHADER_COLOR_INDEX, 4, GL_UNSIGNED_BYTE, GL_TRUE, RENDERER_VERTEX_SIZE, (const GLvoid*)(offsetof(VertexData, VertexData::color)));
			
			glBindBuffer(GL_ARRAY_BUFFER, 0);

			GLuint* indices = new GLuint[RENDERER_INDICES_SIZE];

			int offset = 0;
			for (int i = 0; i < RENDERER_INDICES_SIZE; i += 6)
			{
				indices[i + 0] = offset + 0;
				indices[i + 1] = offset + 1;
				indices[i + 2] = offset + 2;

				indices[i + 3] = offset + 2;
				indices[i + 4] = offset + 3;
				indices[i + 5] = offset + 0;

				offset += 4;
			}

			_IBO = new IndexBuffer(indices, RENDERER_INDICES_SIZE);

			glBindVertexArray(0);
		}

		void BatchRenderer2D::begin()
		{
			glBindBuffer(GL_ARRAY_BUFFER, _VBO);
			_buffer = (VertexData*)glMapBuffer(GL_ARRAY_BUFFER, GL_WRITE_ONLY);
		}

		void BatchRenderer2D::submit(const Renderable2D* renderable)
		{
			const Raven::MATH::vec3& position = renderable->getPosition();
			const Raven::MATH::vec2& size = renderable->getSize();
			const Raven::MATH::vec4& color4f = renderable->getColor();
			const std::vector<Raven::MATH::vec2>& texCoord = renderable->getTexCoord();
			const GLuint texID = renderable->getTextureID();

			unsigned int color = 0;
			float texSlot = 0.0f;

			if (texID > 0)
			{
				bool found = false;

				for (int i = 0; i < _textureSlots.size(); i += 1)
				{
					if (_textureSlots[i] == texID)
					{
						texSlot = (float)(i + 1);
						found = true;
						break;
					}
				}

				if (!found)
				{
					if (_textureSlots.size() >= 32)
					{
						end();
						flush();
						begin();
					}
					_textureSlots.push_back(texID);
					texSlot = (float)(_textureSlots.size());
				}
			}
			//else
			//{
				int r = color4f.x * 255.0f;
				int g = color4f.y * 255.0f;
				int b = color4f.z * 255.0f;
				int a = color4f.w * 255.0f;

				color = a << 24 | b << 16 | g << 8 | r;
			//}

			_buffer->vertex = *_transformationCurrent * position; // Raven::MATH::vec3(position.x, position.y, position.z);
			_buffer->texCoord = texCoord[0];
			_buffer->texID = texSlot;
			_buffer->color = color;
			_buffer++;

			_buffer->vertex = *_transformationCurrent * Raven::MATH::vec3(position.x + size.x, position.y, position.z);
			_buffer->texCoord = texCoord[1];
			_buffer->texID = texSlot;
			_buffer->color = color;
			_buffer++;

			_buffer->vertex = *_transformationCurrent * Raven::MATH::vec3(position.x + size.x, position.y + size.y, position.z);
			_buffer->texCoord = texCoord[2];
			_buffer->texID = texSlot;
			_buffer->color = color;
			_buffer++;

			_buffer->vertex = *_transformationCurrent * Raven::MATH::vec3(position.x, position.y + size.y, position.z);
			_buffer->texCoord = texCoord[3];
			_buffer->texID = texSlot;
			_buffer->color = color;
			_buffer++;

			_indexCount += 6;
		}

		void BatchRenderer2D::end()
		{
			glUnmapBuffer(GL_ARRAY_BUFFER);
			glBindBuffer(GL_ARRAY_BUFFER, 0);
		}

		void BatchRenderer2D::flush()
		{
			for (int i = 0; i < _textureSlots.size(); i += 1)
			{
				glActiveTexture(GL_TEXTURE0 + i);
				glBindTexture(GL_TEXTURE_2D, _textureSlots[i]);
			}


			glBindVertexArray(_VAO);
			_IBO->bind();

			glDrawElements(GL_TRIANGLES, _indexCount, GL_UNSIGNED_INT, 0);

			_IBO->unbind();
			glBindVertexArray(0);

			_indexCount = 0;
		}

		

	}
}