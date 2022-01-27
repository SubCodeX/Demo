#pragma once

#include "renderable2D.h"

namespace Raven
{
	namespace GFX
	{

		class StaticSprite : public Renderable2D
		{
		public:
			StaticSprite(float x, float y, float width, float height, const Raven::MATH::vec4& color, Shader& shader);
			~StaticSprite();

			inline const VertexArray* getVAO() const { return _vertexArray; }
			inline const IndexBuffer* getIBO() const { return _indexBuffer; }

			inline Shader& getShader() const { return _shader; }

		private:
			VertexArray* _vertexArray;
			IndexBuffer* _indexBuffer;
			Shader& _shader;


		};

	}
}