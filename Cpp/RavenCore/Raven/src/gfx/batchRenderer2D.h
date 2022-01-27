#pragma once

#include <cstddef>
#include "renderer2D.h"
#include "renderable2D.h"
#include "buff\indexBuffer.h"

namespace Raven {
	namespace GFX
	{

#define RENDERER_MAX_SPRITES	262144
#define RENDERER_VERTEX_SIZE	sizeof(VertexData)
#define RENDERER_SPRITE_SIZE	RENDERER_VERTEX_SIZE * 4
#define RENDERER_BUFFER_SIZE	RENDERER_SPRITE_SIZE * RENDERER_MAX_SPRITES
#define RENDERER_INDICES_SIZE	RENDERER_MAX_SPRITES * 6

#define SHADER_VERTEX_INDEX		0
#define SHADER_TEXCOORD_INDEX	1
#define SHADER_TEXTURE_INDEX	2
#define SHADER_COLOR_INDEX		3

		class BatchRenderer2D : public Renderer2D
		{
		private:	
			GLuint _VAO;
			GLuint _VBO;
			VertexData* _buffer;
			IndexBuffer* _IBO;
			GLsizei _indexCount;

			std::vector<GLuint>  _textureSlots;
			
		public:
			BatchRenderer2D();
			~BatchRenderer2D();

			void begin() override;
			void submit(const Renderable2D* renderable) override;
			void end() override;
			void flush() override;

		private:
			void init();
		};

	}
}