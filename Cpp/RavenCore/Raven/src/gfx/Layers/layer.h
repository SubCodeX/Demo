#pragma once

#include "..\renderable2D.h"
#include "..\renderer2D.h"
#include "..\shader.h"

namespace Raven
{
	namespace GFX
	{

		class Layer
		{
		protected:
			Layer(Renderer2D* renderer, Shader* shader, Raven::MATH::mat4 projectionMatrix);
		public:
			virtual ~Layer();
			virtual void add(Renderable2D* renderable);

			virtual void render();

		protected:
			Renderer2D* _renderer;
			std::vector<Renderable2D*> _renderables;
			Shader* _shader;
			Raven::MATH::mat4 _projectionMatrix;

		};

	}
}