#include "tileLayer.h"

namespace Raven
{
	namespace GFX
	{
		TileLayer::TileLayer(Shader* shader)
			: Layer(new BatchRenderer2D(), shader, Raven::MATH::mat4::orthographic(-1.0f, 1.0f, -1.0f, 1.0f, -1.0f, 1.0f))
		{
		}

		TileLayer::~TileLayer()
		{			
		}
	}
}