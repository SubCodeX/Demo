#include "staticSprite.h"

namespace Raven
{
	namespace GFX
	{

		StaticSprite::StaticSprite(float x, float y, float width, float height, const Raven::MATH::vec4& color, Shader& shader)
			: Renderable2D(Raven::MATH::vec3(x, y, 0),Raven::MATH::vec2(width, height), color), _shader(shader)
		{
			_vertexArray = new VertexArray();
			GLfloat vertices[] =
			{
				0, 0, 0,
				width, 0, 0,
				width, height, 0,
				0, height, 0
			};

			GLfloat colors[] =
			{
				color.x, color.y, color.z, color.w,
				color.x, color.y, color.z, color.w,
				color.x, color.y, color.z, color.w,
				color.x, color.y, color.z, color.w
			};

			_vertexArray->addBuffer(new Buffer(vertices, 4 * 3, 3), 0);
			_vertexArray->addBuffer(new Buffer(colors, 4 * 4, 4), 1);

			GLushort indices[] = { 0, 1, 2, 2, 3, 0 };
			_indexBuffer = new IndexBuffer(indices, 6);
		}

		StaticSprite::~StaticSprite()
		{
			delete _vertexArray;
			delete _indexBuffer;
		}
	}
}